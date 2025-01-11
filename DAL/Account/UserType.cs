using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO.Common;

namespace DAL.Account
{
    public class UserType
    {
        public static ModelResult<List<DTO.Account.UserType>> GetUserType(DTO.Account.UserType userType)
        {
            var oResult = new ModelResult<List<DTO.Account.UserType>>();
            var lsUserType = new List<DTO.Account.UserType>();
            using (var conn = new SqlConnection(DbConnection.ConnectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    var command = @"select * from UserType where 1=1 and IsDeleted=0 ";
                    if (userType.Id > 0)
                    {
                        command += "and Id = @Id ";
                        cmd.Parameters.AddWithValue("@Id", userType.Id);
                    }
                    if (!string.IsNullOrEmpty(userType.Name))
                    {
                        command += "and Name like @Name ";
                        cmd.Parameters.AddWithValue("@Name", "%" + userType.Name + "%");
                    }


                    if (!userType.IsList) // isList=false get DataTable with paging ----- isList=True get list without paging
                    {
                        command +=
                            " order by @SortCol @SortType OFFSET (@Page -1 )* @RowsPerPage ROWS FETCH NEXT @RowsPerPage ROWS ONLY";
                        command = command.Replace("@SortCol", userType.SortCol);
                        command = command.Replace("@SortType", userType.SortType);
                        command = command.Replace("@Page", userType.Page.ToString());
                        command = command.Replace("@RowsPerPage", userType.RowPerPage.ToString());
                    }
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = command;
                    conn.Open();

                    SqlDataReader reader = cmd.ExecuteReader();

                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var oUserType = new DTO.Account.UserType();
                            oUserType.Id = Convert.ToInt32(reader["Id"]);
                            oUserType.Name = reader["Name"].ToString();
                            oUserType.IsDeleted = Convert.ToBoolean(reader["IsDeleted"]);

                            lsUserType.Add(oUserType);
                        }
                    }

                    int count;

                    using (var connCount = new SqlConnection(DbConnection.ConnectionString))
                    {
                        using (var cmdCount = new SqlCommand())
                        {

                            command = @"select count(*) from UserType  where 1=1 and IsDeleted=0 ";

                            if (userType.Id > 0)
                            {
                                command += "and Id = @Id ";
                                cmdCount.Parameters.AddWithValue("@Id", userType.Id);
                            }

                            if (!string.IsNullOrEmpty(userType.Name))
                            {
                                command += "and Name like @Name ";
                                cmdCount.Parameters.AddWithValue("@Name", userType.Name);
                            }
                            cmdCount.CommandType = CommandType.Text;
                            cmdCount.CommandText = command;

                            cmdCount.Connection = connCount;
                            connCount.Open();
                            count = Convert.ToInt32(cmdCount.ExecuteScalar());
                        }
                    }

                    if (lsUserType.Count > 0)
                    {
                        oResult.HasResult = true;
                        oResult.Results = lsUserType;
                        oResult.RowCount = count;
                    }
                    return oResult;
                }
            }

        }
        public static ModelResult<DTO.Account.UserType> AddEditUserType(DTO.Account.UserType userType)
        {
            var oResult = new ModelResult<DTO.Account.UserType>();

            using (var conn = new SqlConnection(DbConnection.ConnectionString))
            {
                using (var cmd = new SqlCommand())
                {

                    cmd.Connection = conn;
                    cmd.CommandText = "SP_UserTypeAddEdit";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Name", userType.Name);
                    if (userType.Id > 0)
                        cmd.Parameters.AddWithValue("@Id", userType.Id);
                    conn.Open();

                    userType.Id = Convert.ToInt32(cmd.ExecuteScalar());
                    oResult.HasResult = true;
                    oResult.Results = userType;

                }
                return oResult;
            }
        }
        public static ModelResult<DTO.Account.UserType> Delete(DTO.Account.UserType userType)
        {
            var oResult = new ModelResult<DTO.Account.UserType>();
            using (var conn = new SqlConnection(DbConnection.ConnectionString))
            {

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SP_UserTypeDelete";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", userType.Id);
                    conn.Open();

                    userType.Id = Convert.ToInt32(cmd.ExecuteNonQuery());
                    oResult.HasResult = true;
                    oResult.Results = userType;

                }

                return oResult;
            }
        }
    }
}
