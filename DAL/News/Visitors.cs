using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO.Common;

namespace DAL.News
{
    public class Visitors
    {
        //private static RijndaelCrypt rC = new RijndaelCrypt("ambiabhhm2883772");

        public static ModelResult<List<DTO.News.Visitors>> VisitorLogin(DTO.Account.LoginModel oVisitor)
        {
            using (SqlConnection conn = new SqlConnection(DbConnection.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;

                    //var command = @"select * from MD_Media where isDeleted = 0  ";
                    var command =
                        @"select *
                            from dbo.Visitors a 
                            WHERE (Email=@Email OR Name=@Email) 
                                and Pass=@Pass COLLATE SQL_Latin1_General_CP1_CS_AS AND IsApproved=1";
                    if (!string.IsNullOrEmpty(oVisitor.Email))
                    {
                        cmd.Parameters.AddWithValue("@Email", oVisitor.Email);
                    }
                    if (!string.IsNullOrEmpty(oVisitor.Password))
                    {
                        cmd.Parameters.AddWithValue("@Pass", Common.Md5(oVisitor.Password));
                    }
                    cmd.CommandText = command;
                    conn.Open();

                    var reader = cmd.ExecuteReader();
                    var lstVisitor = new List<DTO.News.Visitors>();

                    var oResult = new ModelResult<List<DTO.News.Visitors>>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var opVisitor = new DTO.News.Visitors();
                            if (reader["Avatar"] != DBNull.Value)
                                opVisitor.Avatar = Convert.ToString(reader["Avatar"]);
                            if (reader["Email"] != DBNull.Value)
                                opVisitor.Email = Convert.ToString(reader["Email"]);
                            opVisitor.Id = Convert.ToInt32(reader["Id"]);
                            if (reader["Name"] != DBNull.Value)
                                opVisitor.Name = Convert.ToString(reader["Name"]);
                            if (reader["Pass"] != DBNull.Value)
                                opVisitor.Pass = Convert.ToString(reader["Pass"]);
                            if (reader["IsApproved"] != DBNull.Value)
                                opVisitor.IsApproved = Convert.ToBoolean(reader["IsApproved"]);
                            lstVisitor.Add(opVisitor);
                        }
                    }
                    if (lstVisitor.Count > 0)
                    {
                        oResult.HasResult = true;
                        oResult.Results = lstVisitor;
                        //oResult.RowCount = count;
                    }
                    return oResult;
                }
            }
        }

        public static ModelResult<DTO.News.Visitors> AddEditVisitor(DTO.News.Visitors oVisitor)
        {
            var oResult = new ModelResult<DTO.News.Visitors>();
            try
            {
                using (var conn = new SqlConnection(DbConnection.ConnectionString))
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SP_VisitorAddEdit";
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (oVisitor.Id > 0)
                            cmd.Parameters.AddWithValue("@Id", oVisitor.Id);
                        if (!string.IsNullOrEmpty(oVisitor.Name))
                            cmd.Parameters.AddWithValue("@Name", oVisitor.Name);
                        if (!string.IsNullOrEmpty(oVisitor.Avatar))
                            cmd.Parameters.AddWithValue("@Avatar", oVisitor.Avatar);
                        if (!string.IsNullOrEmpty(oVisitor.Email))
                            cmd.Parameters.AddWithValue("@Email", oVisitor.Email);
                        if (!string.IsNullOrEmpty(oVisitor.Pass))
                            cmd.Parameters.AddWithValue("@Pass", Common.Md5(oVisitor.Pass));
                        if (!string.IsNullOrEmpty(oVisitor.ResetPassToken))
                            cmd.Parameters.AddWithValue("@ResetPassToken", oVisitor.ResetPassToken);
                        if (oVisitor.IsApproved.HasValue)
                            cmd.Parameters.AddWithValue("@IsApproved", oVisitor.IsApproved.Value);
                        conn.Open();
                        oVisitor.Id = Convert.ToInt32(cmd.ExecuteScalar());
                        oResult.HasResult = true;
                        oResult.Results = oVisitor;
                    } //Command 
                } // Connection
            }
            catch (Exception ex)
            {
                oResult.Message = ex.Message;
                oResult.HasResult = false;
            }
            return oResult;
        }

        //***************************************//
        public static ModelResult<List<DTO.News.Visitors>> VisitorsGet(DTO.News.Visitors oVisitor)
        {
            var oResult = new ModelResult<List<DTO.News.Visitors>>();
            var conn = new SqlConnection(DbConnection.ConnectionString);
            try
            {
                using (conn)
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;

                        #region SQLCOMMAND Builder

                        var command = "";
                        command = @"SELECT *
                                    FROM Visitors 
                                    WHERE 1=1  ";

                        if (oVisitor.Id > 0)
                        {
                            command += " AND Id = @Id";
                            cmd.Parameters.AddWithValue("@Id", oVisitor.Id);

                        }
                        if (!string.IsNullOrEmpty(oVisitor.Email))
                        {
                            command += " AND Email = @Email";
                            cmd.Parameters.AddWithValue("@Email", oVisitor.Email);

                        }
                        if (!string.IsNullOrEmpty(oVisitor.Name))
                        {
                            command += " And Name Like @Name ";
                            cmd.Parameters.AddWithValue("@Name", "%" + oVisitor.Name + "%");
                        }
                        if (!oVisitor.IsList)
                        {
                            command +=
                                " order by @SortCol @SortType OFFSET(@Page - 1) * @RowsPerPage ROWS FETCH NEXT @RowsPerPage ROWS ONLY";
                            command = command.Replace("@SortCol", oVisitor.SortCol);
                            command = command.Replace("@SortType", oVisitor.SortType);
                            command = command.Replace("@Page", oVisitor.Page.ToString());
                            command = command.Replace("@RowsPerPage", oVisitor.RowPerPage.ToString());
                        }

                        #endregion

                        if (conn.State != ConnectionState.Open)
                            conn.Open();

                        cmd.CommandText = command;
                        SqlDataReader reader = cmd.ExecuteReader();
                        var lstUsers = new List<DTO.News.Visitors>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                #region User Account Parms

                                var obUsers = new DTO.News.Visitors();
                                obUsers.Id = Convert.ToInt32(reader["Id"]);
                                if (reader["Name"] != DBNull.Value)
                                    obUsers.Name = Convert.ToString(reader["Name"]);
                                if (reader["Email"] != DBNull.Value)
                                    obUsers.Email = Convert.ToString(reader["Email"]);
                                if (reader["Pass"] != DBNull.Value)
                                    obUsers.Pass = Convert.ToString(reader["Pass"]);
                                if (reader["IsApproved"] != DBNull.Value)
                                    obUsers.IsApproved = Convert.ToBoolean(reader["IsApproved"]);
                                if (reader["Avatar"] != DBNull.Value)
                                    obUsers.Avatar = Convert.ToString(reader["Avatar"]);
                                if (reader["ResetPassToken"] != DBNull.Value)
                                    obUsers.ResetPassToken = Convert.ToString(reader["ResetPassToken"]);

                                

                                #endregion

                                lstUsers.Add(obUsers);
                            }
                        }
                        int count = 0;
                        if (!oVisitor.IsList)
                        {
                            using (SqlConnection connCount = new SqlConnection(DbConnection.ConnectionString))
                            {
                                using (var cmdCount = new SqlCommand())
                                {
                                    cmdCount.Connection = connCount;
                                    command = @"SELECT COUNT(1) FROM Visitors WHERE 1=1 AND IsDeleted = 0";
                                    if (oVisitor.Id > 0)
                                    {
                                        command += " AND Id = @Id";
                                        cmdCount.Parameters.AddWithValue("@Id", oVisitor.Id);
                                    }
                                    if (!string.IsNullOrEmpty(oVisitor.Name))
                                    {
                                        command += " And Name Like @Name ";
                                        cmdCount.Parameters.AddWithValue("@Name", "%" + oVisitor.Name + "%");
                                    }
                                    cmdCount.CommandText = command;
                                    if (connCount.State != ConnectionState.Open)
                                        connCount.Open();
                                    count = Convert.ToInt32(cmdCount.ExecuteScalar());
                                    connCount.Close();
                                }
                            }
                        }
                        if (lstUsers.Count > 0)
                        {
                            oResult.HasResult = true;
                            oResult.Results = lstUsers;
                            oResult.RowCount = count;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                oResult.Message = ex.Message;
                oResult.HasResult = false;
            }
            finally
            {
                conn.Close();
            }
            return oResult;
        }

        public static ModelResult<int> VisitorApprove(int id, int userId)
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
                        cmd.CommandText = "SP_VisitorApprove";
                        cmd.Parameters.AddWithValue("@Id", id);
                        cmd.Parameters.AddWithValue("@UserId", userId);
                        conn.Open();
                        x = Convert.ToInt32(cmd.ExecuteNonQuery());
                        if (x > 0)
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
        public static ModelResult<int> VisitorDelete(int id)
        {
            int x;
            var oResult = new ModelResult<int>();
            using (var conn = new SqlConnection(DbConnection.ConnectionString))
            {

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SP_VisitorDelete";
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

        //****************************************************//
        public static ModelResult<List<DTO.News.VisitorProfile>> VisitorProfileGet(DTO.News.VisitorProfile oUserProfile)
        {
            var oResult = new ModelResult<List<DTO.News.VisitorProfile>>();
            var conn = new SqlConnection(DbConnection.ConnectionString);
            try
            {
                using (conn)
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;

                        #region SQLCOMMAND Builder

                        var command = "";
                        command = @"SELECT * from Visitors WHERE 1=1 AND IsDeleted = 0 ";

                        if (oUserProfile.Id > 0)
                        {
                            command += " AND Id = @Id";
                            cmd.Parameters.AddWithValue("@Id", oUserProfile.Id);
                        }

                        if (!string.IsNullOrEmpty(oUserProfile.Name))
                        {
                            command += " And Name Like @Name ";
                            cmd.Parameters.AddWithValue("@Name", "%" + oUserProfile.Name + "%");
                        }

                        #endregion

                        if (conn.State != ConnectionState.Open)
                            conn.Open();

                        cmd.CommandText = command;
                        SqlDataReader reader = cmd.ExecuteReader();
                        var lstUsers = new List<DTO.News.VisitorProfile>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                #region User Account Parms

                                var obUsers = new DTO.News.VisitorProfile();
                                obUsers.Id = Convert.ToInt32(reader["Id"]);
                             
                                if (reader["Name"] != DBNull.Value)
                                    obUsers.Name = Convert.ToString(reader["Name"]);
                                if (reader["Email"] != DBNull.Value)
                                    obUsers.Email = Convert.ToString(reader["Email"]);
                                if (reader["Pass"] != DBNull.Value)
                                    obUsers.CurrentPassword = Convert.ToString(reader["Pass"]);
                                if (reader["Avatar"] != DBNull.Value)
                                    obUsers.Avatar = Convert.ToString(reader["Avatar"]);
                                if (reader["ResetPassToken"] != DBNull.Value)
                                    obUsers.ResetPassToken = Convert.ToString(reader["ResetPassToken"]);
                               

                                #endregion

                                lstUsers.Add(obUsers);
                            }
                        }
                        if (lstUsers.Count > 0)
                        {
                            oResult.HasResult = true;
                            oResult.Results = lstUsers;
                            
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                oResult.Message = ex.Message;
                oResult.HasResult = false;
            }
            finally
            {
                conn.Close();
            }
            return oResult;
        }
    }
}
