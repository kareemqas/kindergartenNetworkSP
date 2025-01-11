using DTO.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL.News
{
    public class ImportantLinks
    {
        public static ModelResult<List<DTO.News.ImportantLinks>> ImportantLinksGet(DTO.News.ImportantLinks oImportantLinks)
        {
            var oResult = new ModelResult<List<DTO.News.ImportantLinks>>();
            var conn = new SqlConnection(DbConnection.ConnectionString);
            try
            {
                using (conn)
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        #region SQLCOMMAND Builder
                        var command = @"SELECT * FROM ImportantLinks WHERE IsDeleted = 0 ";

                        if (oImportantLinks.Id > 0)
                        {
                            command += " AND Id = @Id";
                            cmd.Parameters.AddWithValue("@Id", oImportantLinks.Id);
                        }
                        if (oImportantLinks.IsActive)
                        {
                            command += " AND IsActive = @IsActive";
                            cmd.Parameters.AddWithValue("@IsActive", oImportantLinks.IsActive);
                        }
                        if (!oImportantLinks.IsList)
                        {
                            command += " order by @SortCol @SortType OFFSET(@Page - 1) * @RowsPerPage ROWS FETCH NEXT @RowsPerPage ROWS ONLY";
                            command = command.Replace("@SortCol", oImportantLinks.SortCol);
                            command = command.Replace("@SortType", oImportantLinks.SortType);
                            command = command.Replace("@Page", oImportantLinks.Page.ToString());
                            command = command.Replace("@RowsPerPage", oImportantLinks.RowPerPage.ToString());
                        }
                        #endregion
                        if (conn.State != ConnectionState.Open)
                            conn.Open();

                        cmd.CommandText = command;
                        SqlDataReader reader = cmd.ExecuteReader();
                        var lstImportantLinks = new List<DTO.News.ImportantLinks>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var obImportantLinks = new DTO.News.ImportantLinks();

                                obImportantLinks.Id = Convert.ToInt32(reader["Id"]);
                                obImportantLinks.Image = Convert.ToString(reader["Image"]);
                                obImportantLinks.IsActive = Convert.ToBoolean(reader["IsActive"]);
                                obImportantLinks.Link = Convert.ToString(reader["Link"]);
                                obImportantLinks.Name = Convert.ToString(reader["Name"]);

                                lstImportantLinks.Add(obImportantLinks);
                            }
                        }
                        int count = 0;
                        if (!oImportantLinks.IsList)
                        {
                            using (SqlConnection connCount = new SqlConnection(DbConnection.ConnectionString))
                            {
                                using (var cmdCount = new SqlCommand())
                                {
                                    cmdCount.Connection = connCount;
                                    command = @"SELECT COUNT(1) FROM ImportantLinks WHERE 1=1 AND IsDeleted = 0";
                                    if (oImportantLinks.Id > 0)
                                    {
                                        command += " AND Id = @Id";
                                        cmdCount.Parameters.AddWithValue("@Id", oImportantLinks.Id);
                                    }
                                    cmdCount.CommandText = command;
                                    if (connCount.State != ConnectionState.Open)
                                        connCount.Open();
                                    count = Convert.ToInt32(cmdCount.ExecuteScalar());
                                    connCount.Close();
                                }
                            }
                        }
                        if (lstImportantLinks.Count > 0)
                        {
                            oResult.HasResult = true;
                            oResult.Results = lstImportantLinks;
                            oResult.RowCount = count;
                        }
                    }
                }
            }
            //catch (Exception ex)
            //{
            //    oResult.Message = ex.Message;
            //    oResult.HasResult = false;
            //}
            finally
            {
                conn.Close();
            }
            return oResult;
        }
        public static ModelResult<DTO.News.ImportantLinks> ImportantLinkSave(DTO.News.ImportantLinks oImportantLinks)
        {
            var oResult = new ModelResult<DTO.News.ImportantLinks>();
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
                        cmd.CommandText = "SP_ImportantLinkSave";
                        cmd.Parameters.AddWithValue("@Id", oImportantLinks.Id);
                        cmd.Parameters.AddWithValue("@Image", oImportantLinks.Image);
                        cmd.Parameters.AddWithValue("@IsActive", oImportantLinks.IsActive);
                        cmd.Parameters.AddWithValue("@Link", oImportantLinks.Link);
                        cmd.Parameters.AddWithValue("@Name", oImportantLinks.Name);
                        conn.Open();
                        oImportantLinks.Id = Convert.ToInt32(cmd.ExecuteScalar());
                        oResult.HasResult = true;
                        oResult.Results = oImportantLinks;
                    }
                }
            }
            finally
            {
                conn.Close();
            }
            return oResult;
        }
        public static ModelResult<int> ImportantLinkDelete(int id)
        {
            int x;
            var oResult = new ModelResult<int>();
            using (var conn = new SqlConnection(DbConnection.ConnectionString))
            {

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SP_ImportantLinkDelete";
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
