using DTO.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL.News
{
    public class Comments : DbProcess
    {
        public static ModelResult<List<DTO.News.Comments>> CommentsGet(DTO.News.Comments oComment)
        {
            var oResult = new ModelResult<List<DTO.News.Comments>>();
            var conn = new SqlConnection(DbConnection.ConnectionString);
            try
            {
                using (conn)
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        #region SQLCOMMAND Builder
                        var command = @"SELECT *, Article.Title
                                        FROM Comments Comment 
                                        Join News Article ON Comment.ArticleId = Article.Id 
                                        WHERE 1 = 1  ";
                        if (oComment.Id > 0)
                        {
                            command += " AND Comment.Id = @Id";
                            cmd.Parameters.AddWithValue("@Id", oComment.Id);
                        }
                        if (oComment.ArticleId > 0)
                        {
                            command += " AND Comment.ArticleId = @ArticleId";
                            cmd.Parameters.AddWithValue("@ArticleId", oComment.ArticleId);
                        }
                        if (oComment.IsApproved.HasValue)
                        {
                            command += " AND Comment.IsApproved = @IsApproved";
                            cmd.Parameters.AddWithValue("@IsApproved", oComment.IsApproved);
                        }
                        if (!string.IsNullOrEmpty(oComment.Name))
                        {
                            command += " And Comment.Name Like @Name ";
                            cmd.Parameters.AddWithValue("@Name", "%" + oComment.Name + "%");
                        }
                        if (!string.IsNullOrEmpty(oComment.Email))
                        {
                            command += " And Comment.Email Like @Email ";
                            cmd.Parameters.AddWithValue("@Email", "%" + oComment.Email + "%");
                        }
                        if (!oComment.IsList)
                        {
                            command += " order by @SortCol @SortType OFFSET(@Page - 1) * @RowsPerPage ROWS FETCH NEXT @RowsPerPage ROWS ONLY";
                            command = command.Replace("@SortCol", oComment.SortCol);
                            command = command.Replace("@SortType", oComment.SortType);
                            command = command.Replace("@Page", oComment.Page.ToString());
                            command = command.Replace("@RowsPerPage", oComment.RowPerPage.ToString());
                        }
                        #endregion
                        if (conn.State != ConnectionState.Open)
                            conn.Open();

                        cmd.CommandText = command;
                        SqlDataReader reader = cmd.ExecuteReader();
                        var lstComments = new List<DTO.News.Comments>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var obComment = new DTO.News.Comments();
                                obComment.Id = Convert.ToInt32(reader["Id"]);
                                obComment.Email = Convert.ToString(reader["Email"]);
                                obComment.Name = Convert.ToString(reader["Name"]);
                                obComment.Comment = Convert.ToString(reader["Comment"]);
                                obComment.ArticleId = Convert.ToInt32(reader["ArticleId"]);
                                obComment.Date = Convert.ToDateTime(reader["InsertedDate"]);
                                obComment.IsApproved = Convert.ToBoolean(reader["IsApproved"]);
                                if (reader["Title"] != DBNull.Value)
                                    obComment.OArticle.Title = Convert.ToString(reader["Title"]);
                                lstComments.Add(obComment);
                            }
                        }
                        int count = 0;
                        if (!oComment.IsList)
                        {
                            using (SqlConnection connCount = new SqlConnection(DbConnection.ConnectionString))
                            {
                                using (var cmdCount = new SqlCommand())
                                {
                                    cmdCount.Connection = connCount;
                                    command = @"SELECT COUNT(1) FROM Comments WHERE 1=1 ";
                                    if (oComment.Id > 0)
                                    {
                                        command += " AND Id = @Id";
                                        cmdCount.Parameters.AddWithValue("@Id", oComment.Id);
                                    }
                                    if (oComment.ArticleId > 0)
                                    {
                                        command += " AND ArticleId = @ArticleId";
                                        cmdCount.Parameters.AddWithValue("@ArticleId", oComment.ArticleId);
                                    }
                                    if (oComment.IsApproved.HasValue)
                                    {
                                        command += " AND IsApproved = @IsApproved";
                                        cmdCount.Parameters.AddWithValue("@IsApproved", oComment.IsApproved);
                                    }
                                    if (!string.IsNullOrEmpty(oComment.Name))
                                    {
                                        command += " And Name Like @Name ";
                                        cmdCount.Parameters.AddWithValue("@Name", "%" + oComment.Name + "%");
                                    }
                                    if (!string.IsNullOrEmpty(oComment.Email))
                                    {
                                        command += " And Email Like @Email ";
                                        cmdCount.Parameters.AddWithValue("@Email", "%" + oComment.Email + "%");
                                    }

                                    cmdCount.CommandText = command;
                                    if (connCount.State != ConnectionState.Open)
                                        connCount.Open();
                                    count = Convert.ToInt32(cmdCount.ExecuteScalar());
                                    connCount.Close();
                                }
                            }
                        }
                        if (lstComments.Count > 0)
                        {
                            oResult.HasResult = true;
                            oResult.Results = lstComments;
                            oResult.RowCount = count;
                        }
                    }
                }
            }
            finally
            {
                conn.Close();
            }
            return oResult;
        }
        public static ModelResult<DTO.News.Comments> CommentSave(DTO.News.Comments oComment)
        {
            var oResult = new ModelResult<DTO.News.Comments>();
            var conn = new SqlConnection(DbConnection.ConnectionString);
            try
            {
                using (conn)
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        cmd.CommandText = "SP_CommentSave";

                        cmd.Parameters.AddWithValue("@ArticleId", oComment.ArticleId);
                        cmd.Parameters.AddWithValue("@Email", oComment.Email);
                        cmd.Parameters.AddWithValue("@Name", oComment.Name);
                        cmd.Parameters.AddWithValue("@Comment", oComment.Comment);


                        conn.Open();
                        oComment.Id = Convert.ToInt32(cmd.ExecuteScalar());
                        oResult.HasResult = true;
                        oResult.Results = oComment;
                    }
                }
            }
            finally
            {
                conn.Close();
            }
            return oResult;
        }
        public static ModelResult<int> CommentApprove(int id, int userId)
        {
            int x;
            var oResult = new ModelResult<int>();
            var conn = new SqlConnection(DbConnection.ConnectionString);
            try
            {
                using (conn)
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Clear();
                        cmd.CommandText = "SP_CommentApprove";
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        conn.Open();
                        x = Convert.ToInt32(cmd.ExecuteNonQuery());
                        if(x > 0)
                            oResult.HasResult = true;

                    }
                }
            }
            finally
            {
                conn.Close();
            }
            return oResult;
        }
        public static ModelResult<int> CommentDelete(int id)
        {
            int x;
            var oResult = new ModelResult<int>();
            using (var conn = new SqlConnection(DbConnection.ConnectionString))
            {

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SP_CommentDelete";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    conn.Open();

                    x = Convert.ToInt32(cmd.ExecuteNonQuery());
                    if (x > 0)
                        oResult.HasResult = true;
                    oResult.Results = x;

                }
                return oResult;
            }
        }
    }
}
