using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO.Common;

namespace DAL.Account
{
    public class ErrorsLogs
    {
        public static ModelResult<List<DTO.Account.ErrorsLogs>> ErrorLogsGet(DTO.Account.ErrorsLogs oErrorsLogs)
        {

            var oResult = new ModelResult<List<DTO.Account.ErrorsLogs>>();
            var conn = new SqlConnection(DbConnection.ConnectionString);
            try
            {
                using (conn)
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        #region SQLCOMMAND Builder
                        var command = @"SELECT * FROM ErrorsLogs WHERE 1=1  ";
                        if (oErrorsLogs.Id > 0)
                        {
                            command += "and Id =@Id ";
                            cmd.Parameters.AddWithValue("@Id", oErrorsLogs.Id);
                        }
                        if (!oErrorsLogs.IsList) // isList=false get DataTable with paging ----- isList=True get list without paging
                        {
                            command += " order by @SortCol @SortType OFFSET (@Page -1 )* @RowsPerPage ROWS FETCH NEXT @RowsPerPage ROWS ONLY";
                            command = command.Replace("@SortCol", oErrorsLogs.SortCol);
                            command = command.Replace("@SortType", oErrorsLogs.SortType);
                            command = command.Replace("@Page", oErrorsLogs.Page.ToString());
                            command = command.Replace("@RowsPerPage", oErrorsLogs.RowPerPage.ToString());
                        }
                        #endregion
                        if (conn.State != ConnectionState.Open)
                            conn.Open();

                        cmd.CommandText = command;
                        SqlDataReader reader = cmd.ExecuteReader();
                        var lstErrorsLogs = new List<DTO.Account.ErrorsLogs>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {

                                var obErrorLog = new DTO.Account.ErrorsLogs();
                                obErrorLog.Id = Convert.ToInt32(reader["Id"]);
                                obErrorLog.ErrorMessage = Convert.ToString(reader["ErrorMessage"]);
                                obErrorLog.Link = Convert.ToString(reader["Link"]);
                                obErrorLog.IP = Convert.ToString(reader["IP"]);
                                obErrorLog.Browser = Convert.ToString(reader["Browser"]);
                                obErrorLog.UserAgent = Convert.ToString(reader["UserAgent"]);
                                obErrorLog.RequestType = Convert.ToString(reader["RequestType"]);
                                obErrorLog.PostedData = Convert.ToString(reader["PostedData"]);
                                obErrorLog.IsSolved = Convert.ToBoolean(reader["IsSolved"]);
                                obErrorLog.ErrorDate = Convert.ToDateTime(reader["ErrorDate"]);
                                obErrorLog.IsAjax = Convert.ToBoolean(reader["IsAjax"]);

                                lstErrorsLogs.Add(obErrorLog);
                            }
                        }
                        int count = 0;
                        if (!oErrorsLogs.IsList)
                        {
                            using (SqlConnection connCount = new SqlConnection(DbConnection.ConnectionString))
                            {
                                using (var cmdCount = new SqlCommand())
                                {
                                    cmdCount.Connection = connCount;
                                    command = @"SELECT COUNT(1) FROM ErrorsLogs WHERE 1=1 ";
                                    if (oErrorsLogs.Id > 0)
                                    {
                                        command += " AND Id = @Id";
                                        cmd.Parameters.AddWithValue("@Id", oErrorsLogs.Id);
                                    }
                                    cmdCount.CommandText = command;
                                    if (connCount.State != ConnectionState.Open)
                                        connCount.Open();
                                    count = Convert.ToInt32(cmdCount.ExecuteScalar());
                                    connCount.Close();
                                }
                            }
                        }
                        if (lstErrorsLogs.Count > 0)
                        {
                            oResult.HasResult = true;
                            oResult.Results = lstErrorsLogs;
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
        public static ModelResult<DTO.Account.ErrorsLogs> ErrorLogsInsert(DTO.Account.ErrorsLogs oErrorsLogs)
        {
            var oResult = new ModelResult<DTO.Account.ErrorsLogs>();
            var conn = new SqlConnection(DbConnection.ConnectionString);
            try
            {
                using (conn)
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SP_ErrorsLogsInsert";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@ErrorMessage", oErrorsLogs.ErrorMessage);
                        cmd.Parameters.AddWithValue("@Link", oErrorsLogs.Link);
                        cmd.Parameters.AddWithValue("@IP", oErrorsLogs.IP);
                        cmd.Parameters.AddWithValue("@Browser", oErrorsLogs.Browser);
                        cmd.Parameters.AddWithValue("@UserAgent", oErrorsLogs.UserAgent);
                        cmd.Parameters.AddWithValue("@RequestType", oErrorsLogs.RequestType);
                        if (!string.IsNullOrEmpty(oErrorsLogs.PostedData))
                            cmd.Parameters.AddWithValue("@PostedData", oErrorsLogs.PostedData);
                        cmd.Parameters.AddWithValue("@IsSolved", oErrorsLogs.IsSolved);
                        cmd.Parameters.AddWithValue("@IsAjax", oErrorsLogs.IsAjax);

                        conn.Open();
                        oErrorsLogs.Id = Convert.ToInt32(cmd.ExecuteScalar());
                        oResult.HasResult = true;
                        oResult.Results = oErrorsLogs;
                    }
                }
            }
            finally
            {
                conn.Close();
            }
            return oResult;
        }
        public static ModelResult<int> ErrorLogDelete(int id)
        {
            int x;
            var oResult = new ModelResult<int>();
            using (var conn = new SqlConnection(DbConnection.ConnectionString))
            {

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SP_ErorrLogsDelete";
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
