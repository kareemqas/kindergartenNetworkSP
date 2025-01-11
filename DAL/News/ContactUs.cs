using DTO.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL.News
{
    public class ContactUs: DbProcess
    {
        public static ModelResult<List<DTO.News.ContactUs>> ContactUsGet(DTO.News.ContactUs oContactUs)
        {
            var oResult = new ModelResult<List<DTO.News.ContactUs>>();
            var conn = new SqlConnection(DbConnection.ConnectionString);
            try
            {
                using (conn)
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        #region SQLCOMMAND Builder
                        var command = @"SELECT * FROM ContactUs WHERE 1 = 1  And IsDeleted = 0 ";
                        if (oContactUs.Id > 0)
                        {
                            command += " AND Id = @Id";
                            cmd.Parameters.AddWithValue("@Id", oContactUs.Id);
                        }
                        if (oContactUs.IsRead.HasValue)
                        {
                            command += " AND IsRead = @IsRead";
                            cmd.Parameters.AddWithValue("@IsRead", oContactUs.IsRead.Value);
                        }
                        if (!oContactUs.IsList)
                        {
                            command += " order by @SortCol @SortType OFFSET(@Page - 1) * @RowsPerPage ROWS FETCH NEXT @RowsPerPage ROWS ONLY";
                            command = command.Replace("@SortCol", oContactUs.SortCol);
                            command = command.Replace("@SortType", oContactUs.SortType);
                            command = command.Replace("@Page", oContactUs.Page.ToString());
                            command = command.Replace("@RowsPerPage", oContactUs.RowPerPage.ToString());
                        }
                        #endregion
                        if (conn.State != ConnectionState.Open)
                            conn.Open();

                        cmd.CommandText = command;
                        SqlDataReader reader = cmd.ExecuteReader();
                        var lstContactUs = new List<DTO.News.ContactUs>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var obContactUs = new DTO.News.ContactUs();
                                obContactUs.Id = Convert.ToInt32(reader["Id"]);
                                obContactUs.Email = Convert.ToString(reader["Email"]);
                                obContactUs.Name = Convert.ToString(reader["Name"]);
                                obContactUs.Subject = Convert.ToString(reader["Subject"]);
                                obContactUs.Message = Convert.ToString(reader["Message"]);
                                obContactUs.InsertedDate = Convert.ToDateTime(reader["InsertedDate"]);
                                obContactUs.IsAnswered = Convert.ToBoolean(reader["IsAnswered"]);
                                if(reader["Reply"] != DBNull.Value)
                                    obContactUs.Reply = Convert.ToString(reader["Reply"]);
                                obContactUs.IsRead = Convert.ToBoolean(reader["IsRead"]);
                                lstContactUs.Add(obContactUs);
                            }
                        }
                        int count = 0;
                        if (!oContactUs.IsList)
                        {
                            using (SqlConnection connCount = new SqlConnection(DbConnection.ConnectionString))
                            {
                                using (var cmdCount = new SqlCommand())
                                {
                                    cmdCount.Connection = connCount;
                                    command = @"SELECT COUNT(1) FROM ContactUs WHERE 1=1 ";
                                    if (oContactUs.Id > 0)
                                    {
                                        command += " AND Id = @Id";
                                        cmdCount.Parameters.AddWithValue("@Id", oContactUs.Id);
                                    }

                                    cmdCount.CommandText = command;
                                    if (connCount.State != ConnectionState.Open)
                                        connCount.Open();
                                    count = Convert.ToInt32(cmdCount.ExecuteScalar());
                                    connCount.Close();
                                }
                            }
                        }
                        if (lstContactUs.Count > 0)
                        {
                            oResult.HasResult = true;
                            oResult.Results = lstContactUs;
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
        public static ModelResult<DTO.News.ContactUs> ContactUsSave(DTO.News.ContactUs oContactUs)
        {
            var oResult = new ModelResult<DTO.News.ContactUs>();
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
                        cmd.CommandText = "SP_ContactUsSave";

                        cmd.Parameters.AddWithValue("@Subject", oContactUs.Subject);
                        cmd.Parameters.AddWithValue("@Email", oContactUs.Email);
                        cmd.Parameters.AddWithValue("@Name", oContactUs.Name);
                        cmd.Parameters.AddWithValue("@Message", oContactUs.Message);


                        conn.Open();
                        oContactUs.Id = Convert.ToInt32(cmd.ExecuteScalar());
                        oResult.HasResult = true;
                        oResult.Results = oContactUs;
                    }
                }
            }
            finally
            {
                conn.Close();
            }
            return oResult;
        }
        public static ModelResult<DTO.News.ContactUs> ContactUsReply(DTO.News.ContactUs oContactUs)
        {
            var oResult = new ModelResult<DTO.News.ContactUs>();
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
                        cmd.CommandText = "SP_ContactUsReply";
                        cmd.Parameters.AddWithValue("@Id", oContactUs.Id);
                        cmd.Parameters.AddWithValue("@Reply", oContactUs.Reply);
                        conn.Open();
                        oContactUs.Id = Convert.ToInt32(cmd.ExecuteScalar());
                        oResult.HasResult = true;
                        oResult.Results = oContactUs;
                    }
                }
            }
            finally
            {
                conn.Close();
            }
            return oResult;
        }
        public static ModelResult<int> ContactUsRead(int id)
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
                        cmd.CommandText = "SP_ContactUsRead";
                        cmd.Parameters.AddWithValue("@Id", id);
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
        public static ModelResult<int> ContactUsDelete(int id)
        {
            int x;
            var oResult = new ModelResult<int>();
            using (var conn = new SqlConnection(DbConnection.ConnectionString))
            {

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SP_ContactUsDelete";
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
