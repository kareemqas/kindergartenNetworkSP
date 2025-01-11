using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Account;
using DTO.Common;

namespace DAL.Account
{
    public class TraceUserActivity
    {
        public static ModelResult<List<DTO.Account.TraceUserActivity>> GetTraceUserActivity(DTO.Account.TraceUserActivity oTraceUserActivity , DateTime? dateMin , DateTime? dateMax)
        {
            using (SqlConnection conn = new SqlConnection(DbConnection.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;

                    var command = @"select TUA.* , UA.Name UserName 
                                        from TraceUserActivity TUA 
                                        left join UserAccounts UA on UA.Id = TUA.UserId 
                                        where TUA.IsDeleted = 0 ";

                    if (oTraceUserActivity.Id > 0)
                    {
                        command += "and TUA.Id =@Id ";
                        cmd.Parameters.AddWithValue("@Id", oTraceUserActivity.Id);
                    }
                    if (oTraceUserActivity.UserId > 0)
                    {
                        command += "and TUA.UserId =@UserId ";
                        cmd.Parameters.AddWithValue("@UserId", oTraceUserActivity.UserId);
                    }
                    if (dateMin.HasValue)
                    {
                        command += " And TUA.OccurDate >= @OccurDateMin ";
                        cmd.Parameters.AddWithValue("@OccurDateMin", dateMin);
                    }
                    if (dateMax.HasValue)
                    {
                        command += " And TUA.OccurDate <= @OccurDateMax ";
                        cmd.Parameters.AddWithValue("@OccurDateMax", dateMax);
                    }
                    if (!oTraceUserActivity.IsList) // isList=false get DataTable with paging ----- isList=True get list without paging
                    {
                        command += " order by @SortCol @SortType OFFSET (@Page -1 )* @RowsPerPage ROWS FETCH NEXT @RowsPerPage ROWS ONLY";
                        command = command.Replace("@SortCol", oTraceUserActivity.SortCol);
                        command = command.Replace("@SortType", oTraceUserActivity.SortType);
                        command = command.Replace("@Page", oTraceUserActivity.Page.ToString());
                        command = command.Replace("@RowsPerPage", oTraceUserActivity.RowPerPage.ToString());
                    }
                    cmd.CommandText = command;
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    var lstTraceUserActivity = new List<DTO.Account.TraceUserActivity>();

                    var oResult = new ModelResult<List<DTO.Account.TraceUserActivity>>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var opTraceUserActivity = new DTO.Account.TraceUserActivity();

                            opTraceUserActivity.Id = Convert.ToInt32(reader["Id"]);
                            opTraceUserActivity.UserId = Convert.ToInt32(reader["UserId"]);
                            opTraceUserActivity.ElapsedTime = Convert.ToInt32(reader["ElapsedTime"]);
                            opTraceUserActivity.Action = Convert.ToString(reader["Action"]);
                            opTraceUserActivity.Browser = Convert.ToString(reader["Browser"]);
                            opTraceUserActivity.IpAddress = Convert.ToString(reader["IpAddress"]);
                            opTraceUserActivity.OccurDate = Convert.ToDateTime(reader["OccurDate"]);
                            opTraceUserActivity.UserAgent = Convert.ToString(reader["UserAgent"]);

                            lstTraceUserActivity.Add(opTraceUserActivity);
                        }
                    }
                    int count = 0;
                    if (!oTraceUserActivity.IsList)
                    {
                        using (SqlConnection connCount = new SqlConnection(DbConnection.ConnectionString))
                        {
                            using (SqlCommand cmdCount = new SqlCommand())
                            {
                                cmdCount.Connection = connCount;
                                command = @"Select Count(1) from TraceUserActivity where 1 = 1 ";
                                if (oTraceUserActivity.Id > 0)
                                {
                                    command += "and Id =@Id ";
                                    cmdCount.Parameters.AddWithValue("@Id", oTraceUserActivity.Id);
                                }
                                if (oTraceUserActivity.UserId > 0)
                                {
                                    command += "and UserId =@UserId ";
                                    cmdCount.Parameters.AddWithValue("@UserId", oTraceUserActivity.UserId);
                                }
                                if (dateMin.HasValue)
                                {
                                    command += " And OccurDate >= @OccurDateMin ";
                                    cmd.Parameters.AddWithValue("@OccurDateMin", dateMin);
                                }
                                if (dateMax.HasValue)
                                {
                                    command += " And OccurDate <= @OccurDateMax ";
                                    cmd.Parameters.AddWithValue("@OccurDateMax", dateMax);
                                }
                                cmdCount.CommandText = command;
                                connCount.Open();
                                count = Convert.ToInt32(cmdCount.ExecuteScalar());
                            }
                        }
                    }
                    if (lstTraceUserActivity.Count > 0)
                    {
                        oResult.HasResult = true;
                        oResult.Results = lstTraceUserActivity;
                        oResult.RowCount = count;
                    }
                    return oResult;
                }
            }
        }
        public static ModelResult<DTO.Account.TraceUserActivity> AddTraceUserActivity(DTO.Account.TraceUserActivity oTraceUserActivity)
        {
            var oResult = new ModelResult<DTO.Account.TraceUserActivity>();
            try
            {
                using (var conn = new SqlConnection(DbConnection.ConnectionString))
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SP_TraceUserActivityAdd";
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (oTraceUserActivity.ElapsedTime > 0)
                            cmd.Parameters.AddWithValue("@ElapsedTime", oTraceUserActivity.ElapsedTime);
                        if (oTraceUserActivity.UserId > 0)
                            cmd.Parameters.AddWithValue("@UserId", oTraceUserActivity.UserId);

                        if (!string.IsNullOrEmpty(oTraceUserActivity.Action))
                            cmd.Parameters.AddWithValue("@Action", oTraceUserActivity.Action);
                        if (!string.IsNullOrEmpty(oTraceUserActivity.Browser))
                            cmd.Parameters.AddWithValue("@Browser", oTraceUserActivity.Browser);
                        if (!string.IsNullOrEmpty(oTraceUserActivity.IpAddress))
                            cmd.Parameters.AddWithValue("@IpAddress", oTraceUserActivity.IpAddress);
                        if (!string.IsNullOrEmpty(oTraceUserActivity.UserAgent))
                            cmd.Parameters.AddWithValue("@UserAgent", oTraceUserActivity.UserAgent);


                        conn.Open();
                        oTraceUserActivity.Id = Convert.ToInt32(cmd.ExecuteScalar());
                        oResult.HasResult = true;
                        oResult.Results = oTraceUserActivity;
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
    }
}
