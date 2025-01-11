using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO.Common;

namespace DAL.Account
{
    public class ErrorsRepository
    {
        public static ModelResult<List<DTO.Account.ErrorsRepository>> ErrorsRepositoryGet(DTO.Account.ErrorsRepository oErrorsRepository)
        {
            var oResult = new ModelResult<List<DTO.Account.ErrorsRepository>>();
            var conn = new SqlConnection(DbConnection.ConnectionString);
            try
            {
                using (conn)
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        #region SQLCOMMAND Builder
                        var command = @"SELECT * FROM ErrorsRepository WHERE 1 = 1 ";
                        if (oErrorsRepository.Id > 0)
                        {
                            command += " AND Id = @Id";
                            cmd.Parameters.AddWithValue("@Id", oErrorsRepository.Id);
                        }
                        if (!oErrorsRepository.IsList)
                        {
                            command += " order by @SortCol @SortType OFFSET(@Page - 1) * @RowsPerPage ROWS FETCH NEXT @RowsPerPage ROWS ONLY";
                            command = command.Replace("@SortCol", oErrorsRepository.SortCol);
                            command = command.Replace("@SortType", oErrorsRepository.SortType);
                            command = command.Replace("@Page", oErrorsRepository.Page.ToString());
                            command = command.Replace("@RowsPerPage", oErrorsRepository.RowPerPage.ToString());
                        }
                        #endregion
                        if (conn.State != ConnectionState.Open)
                            conn.Open();

                        cmd.CommandText = command;
                        SqlDataReader reader = cmd.ExecuteReader();
                        var lstErrorsRepository = new List<DTO.Account.ErrorsRepository>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                #region Constant Parms
                                var obErrorsRepository = new DTO.Account.ErrorsRepository();
                                if (reader["ErrorMessage"] != DBNull.Value)
                                    obErrorsRepository.ErrorMessage = Convert.ToString(reader["ErrorMessage"]);
                                if (reader["Link"] != DBNull.Value)
                                    obErrorsRepository.Link = Convert.ToString(reader["Link"]);
                                if (reader["IP"] != DBNull.Value)
                                    obErrorsRepository.IP = Convert.ToString(reader["IP"]);
                                if (reader["Browser"] != DBNull.Value)
                                    obErrorsRepository.Browser = Convert.ToString(reader["Browser"]);
                                if (reader["UserAgent"] != DBNull.Value)
                                    obErrorsRepository.UserAgent = Convert.ToString(reader["UserAgent"]);
                                if (reader["RequestType"] != DBNull.Value)
                                    obErrorsRepository.RequestType = Convert.ToString(reader["RequestType"]);
                                if (reader["PostedData"] != DBNull.Value)
                                    obErrorsRepository.PostedData = Convert.ToString(reader["PostedData"]);
                                obErrorsRepository.IsSolved = Convert.ToBoolean(reader["IsSolved"]);
                                obErrorsRepository.IsAjax = Convert.ToBoolean(reader["IsAjax"]);
                                obErrorsRepository.ErrorTime = Convert.ToDateTime(reader["ErrorTime"]);
                                #endregion
                                lstErrorsRepository.Add(obErrorsRepository);
                            }
                        }
                        int count = 0;
                        if (!oErrorsRepository.IsList)
                        {
                            using (SqlConnection connCount = new SqlConnection(DbConnection.ConnectionString))
                            {
                                using (var cmdCount = new SqlCommand())
                                {
                                    cmdCount.Connection = connCount;
                                    command = @"SELECT COUNT(1) FROM ErrorsRepository WHERE 1=1 ";
                                    if (oErrorsRepository.Id > 0)
                                    {
                                        command += " AND Id = @Id";
                                        cmd.Parameters.AddWithValue("@Id", oErrorsRepository.Id);
                                    }
                                    cmdCount.CommandText = command;
                                    if (connCount.State != ConnectionState.Open)
                                        connCount.Open();
                                    count = Convert.ToInt32(cmdCount.ExecuteScalar());
                                    connCount.Close();
                                }
                            }
                        }
                        if (lstErrorsRepository.Count > 0)
                        {
                            oResult.HasResult = true;
                            oResult.Results = lstErrorsRepository;
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
        public static ModelResult<DTO.Account.ErrorsRepository> ErrorsRepositoryInsert(DTO.Account.ErrorsRepository oErrorsRepository)
        {
            var oResult = new ModelResult<DTO.Account.ErrorsRepository>();
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
                        cmd.CommandText = "SP_ErrorsRepositoryInsert";

                        if (!string.IsNullOrEmpty(oErrorsRepository.Browser))
                            cmd.Parameters.AddWithValue("@Browser", oErrorsRepository.Browser);
                        if (!string.IsNullOrEmpty(oErrorsRepository.ErrorMessage))
                            cmd.Parameters.AddWithValue("@ErrorMessage", oErrorsRepository.ErrorMessage);
                        if (!string.IsNullOrEmpty(oErrorsRepository.IP))
                            cmd.Parameters.AddWithValue("@IP", oErrorsRepository.IP);
                        cmd.Parameters.AddWithValue("@IsAjax", oErrorsRepository.IsAjax);
                        if (!string.IsNullOrEmpty(oErrorsRepository.Link))
                            cmd.Parameters.AddWithValue("@Link", oErrorsRepository.Link);
                        if (!string.IsNullOrEmpty(oErrorsRepository.PostedData))
                            cmd.Parameters.AddWithValue("@PostedData", oErrorsRepository.PostedData);
                        if (!string.IsNullOrEmpty(oErrorsRepository.RequestType))
                            cmd.Parameters.AddWithValue("@RequestType", oErrorsRepository.RequestType);
                        if (!string.IsNullOrEmpty(oErrorsRepository.UserAgent))
                            cmd.Parameters.AddWithValue("@UserAgent", oErrorsRepository.UserAgent);

                        conn.Open();
                        oErrorsRepository.Id = Convert.ToInt32(cmd.ExecuteScalar());
                        oResult.HasResult = true;
                        oResult.Results = oErrorsRepository;
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
