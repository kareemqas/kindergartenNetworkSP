using DTO.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL.News
{
    public class NewsletterSubscribers : DbProcess
    {
        public static ModelResult<List<DTO.News.NewsletterSubscribers>> NewsletterSubscribersGet(DTO.News.NewsletterSubscribers oSubscriber)
        {
            var oResult = new ModelResult<List<DTO.News.NewsletterSubscribers>>();
            var conn = new SqlConnection(DbConnection.ConnectionString);
            try
            {
                using (conn)
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        #region SQLCOMMAND Builder
                        var command = @"SELECT * FROM NewsletterSubscribers WHERE 1 = 1  ";
                        if (oSubscriber.Id > 0)
                        {
                            command += " AND Id = @Id";
                            cmd.Parameters.AddWithValue("@Id", oSubscriber.Id);
                        }
                        if (oSubscriber.IsActive.HasValue)
                        {
                            command += " AND IsActive = @IsActive";
                            cmd.Parameters.AddWithValue("@IsActive", oSubscriber.IsActive.Value);
                        }
                        if (!oSubscriber.IsList)
                        {
                            command += " order by @SortCol @SortType OFFSET(@Page - 1) * @RowsPerPage ROWS FETCH NEXT @RowsPerPage ROWS ONLY";
                            command = command.Replace("@SortCol", oSubscriber.SortCol);
                            command = command.Replace("@SortType", oSubscriber.SortType);
                            command = command.Replace("@Page", oSubscriber.Page.ToString());
                            command = command.Replace("@RowsPerPage", oSubscriber.RowPerPage.ToString());
                        }
                        #endregion
                        if (conn.State != ConnectionState.Open)
                            conn.Open();

                        cmd.CommandText = command;
                        SqlDataReader reader = cmd.ExecuteReader();
                        var lstNewsletterSubscribers = new List<DTO.News.NewsletterSubscribers>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var obContactUs = new DTO.News.NewsletterSubscribers();
                                obContactUs.Id = Convert.ToInt32(reader["Id"]);
                                obContactUs.Email = Convert.ToString(reader["Email"]);
                                obContactUs.IsActive = Convert.ToBoolean(reader["IsActive"]);
                                lstNewsletterSubscribers.Add(obContactUs);
                            }
                        }
                        int count = 0;
                        if (!oSubscriber.IsList)
                        {
                            using (SqlConnection connCount = new SqlConnection(DbConnection.ConnectionString))
                            {
                                using (var cmdCount = new SqlCommand())
                                {
                                    cmdCount.Connection = connCount;
                                    command = @"SELECT COUNT(1) FROM NewsletterSubscribers WHERE 1=1 ";
                                    if (oSubscriber.Id > 0)
                                    {
                                        command += " AND Id = @Id";
                                        cmdCount.Parameters.AddWithValue("@Id", oSubscriber.Id);
                                    }

                                    cmdCount.CommandText = command;
                                    if (connCount.State != ConnectionState.Open)
                                        connCount.Open();
                                    count = Convert.ToInt32(cmdCount.ExecuteScalar());
                                    connCount.Close();
                                }
                            }
                        }
                        if (lstNewsletterSubscribers.Count > 0)
                        {
                            oResult.HasResult = true;
                            oResult.Results = lstNewsletterSubscribers;
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
        public static ModelResult<DTO.News.NewsletterSubscribers> NewsletterSubscriberSave(DTO.News.NewsletterSubscribers oSubscriber)
        {
            var oResult = new ModelResult<DTO.News.NewsletterSubscribers>();
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
                        cmd.CommandText = "SP_NewsletterSubscriberSave";

                        cmd.Parameters.AddWithValue("@Email", oSubscriber.Email);

                        conn.Open();
                        oSubscriber.Id = Convert.ToInt32(cmd.ExecuteScalar());
                        oResult.HasResult = true;
                        oResult.Results = oSubscriber;
                    }
                }
            }
            finally
            {
                conn.Close();
            }
            return oResult;
        }
        public static ModelResult<int> NewsletterSubscriberChangeStatus(int id, bool isActive)
        {
            int x;
            var oResult = new ModelResult<int>();
            using (var conn = new SqlConnection(DbConnection.ConnectionString))
            {

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SP_NewsletterSubscriberChangeStatus";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", id);
                    cmd.Parameters.AddWithValue("@IsActive", isActive);
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
