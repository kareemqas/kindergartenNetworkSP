using DTO.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL.News
{
    public class TeamMembers : DbProcess
    {
        public static ModelResult<List<DTO.News.TeamMembers>> TeamMembersGet(DTO.News.TeamMembers oMember)
        {
            var oResult = new ModelResult<List<DTO.News.TeamMembers>>();
            var conn = new SqlConnection(DbConnection.ConnectionString);
            try
            {
                using (conn)
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        #region SQLCOMMAND Builder
                        var command = @"SELECT * FROM TeamMembers WHERE 1 = 1  And IsDeleted = 0 ";
                        if (oMember.Id > 0)
                        {
                            command += " AND Id = @Id";
                            cmd.Parameters.AddWithValue("@Id", oMember.Id);
                        }
                        if (oMember.IsWithUs)
                        {
                            command += " AND IsWithUs = @IsWithUs";
                            cmd.Parameters.AddWithValue("@IsWithUs", oMember.IsWithUs);
                        }
                        if (!oMember.IsList)
                        {
                            command += " order by @SortCol @SortType OFFSET(@Page - 1) * @RowsPerPage ROWS FETCH NEXT @RowsPerPage ROWS ONLY";
                            command = command.Replace("@SortCol", oMember.SortCol);
                            command = command.Replace("@SortType", oMember.SortType);
                            command = command.Replace("@Page", oMember.Page.ToString());
                            command = command.Replace("@RowsPerPage", oMember.RowPerPage.ToString());
                        }
                        #endregion
                        if (conn.State != ConnectionState.Open)
                            conn.Open();

                        cmd.CommandText = command;
                        SqlDataReader reader = cmd.ExecuteReader();
                        var lstTeamMembers = new List<DTO.News.TeamMembers>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var obMember = new DTO.News.TeamMembers();
                                obMember.Id = Convert.ToInt32(reader["Id"]);
                                obMember.Avatar = Convert.ToString(reader["Avatar"]);
                                obMember.Name = Convert.ToString(reader["Name"]);
                                obMember.JobTitle = Convert.ToString(reader["JobTitle"]);
                                obMember.LinkedInUrl = Convert.ToString(reader["LinkedInUrl"]);
                                obMember.FaceBookUrl = Convert.ToString(reader["FaceBookUrl"]);
                                obMember.InstgramUrl = Convert.ToString(reader["InstgramUrl"]);
                                obMember.IsWithUs = Convert.ToBoolean(reader["IsWithUs"]);
                                lstTeamMembers.Add(obMember);
                            }
                        }
                        int count = 0;
                        if (!oMember.IsList)
                        {
                            using (SqlConnection connCount = new SqlConnection(DbConnection.ConnectionString))
                            {
                                using (var cmdCount = new SqlCommand())
                                {
                                    cmdCount.Connection = connCount;
                                    command = @"SELECT COUNT(1) FROM TeamMembers WHERE 1=1 ";
                                    if (oMember.Id > 0)
                                    {
                                        command += " AND Id = @Id";
                                        cmdCount.Parameters.AddWithValue("@Id", oMember.Id);
                                    }
                                    if (oMember.IsWithUs)
                                    {
                                        command += " AND IsWithUs = @IsWithUs";
                                        cmdCount.Parameters.AddWithValue("@IsWithUs", oMember.IsWithUs);
                                    }

                                    cmdCount.CommandText = command;
                                    if (connCount.State != ConnectionState.Open)
                                        connCount.Open();
                                    count = Convert.ToInt32(cmdCount.ExecuteScalar());
                                    connCount.Close();
                                }
                            }
                        }
                        if (lstTeamMembers.Count > 0)
                        {
                            oResult.HasResult = true;
                            oResult.Results = lstTeamMembers;
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
        public static ModelResult<DTO.News.TeamMembers> TeamMemberSave(DTO.News.TeamMembers oMember)
        {
            var oResult = new ModelResult<DTO.News.TeamMembers>();
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
                        cmd.CommandText = "SP_TeamMemberSave";

                        cmd.Parameters.AddWithValue("@Id", oMember.Id);
                        cmd.Parameters.AddWithValue("@Name", oMember.Name);
                        cmd.Parameters.AddWithValue("@JobTitle", oMember.JobTitle);
                        cmd.Parameters.AddWithValue("@Avatar", oMember.Avatar);
                        cmd.Parameters.AddWithValue("@LinkedInUrl", oMember.LinkedInUrl);
                        cmd.Parameters.AddWithValue("@FaceBookUrl", oMember.FaceBookUrl);
                        cmd.Parameters.AddWithValue("@InstgramUrl", oMember.InstgramUrl);
                        cmd.Parameters.AddWithValue("@IsWithUs", oMember.IsWithUs);


                        conn.Open();
                        oMember.Id = Convert.ToInt32(cmd.ExecuteScalar());
                        oResult.HasResult = true;
                        oResult.Results = oMember;
                    }
                }
            }
            finally
            {
                conn.Close();
            }
            return oResult;
        }
        public static ModelResult<int> TeamMemberDelete(int id)
        {
            int x;
            var oResult = new ModelResult<int>();
            using (var conn = new SqlConnection(DbConnection.ConnectionString))
            {

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SP_TeamMemberDelete";
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
