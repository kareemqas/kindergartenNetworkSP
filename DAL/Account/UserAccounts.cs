using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO.Common;

namespace DAL.Account
{
    public class UserAccounts
    {
        //private static RijndaelCrypt rC = new RijndaelCrypt("ambiabhhm2883772");

        public static ModelResult<List<DTO.Account.UserAccounts>> UserLogin(DTO.Account.LoginModel oUserAccount)
        {
            using (SqlConnection conn = new SqlConnection(DbConnection.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;

                    //var command = @"select * from MD_Media where isDeleted = 0  ";
                    var command =
                        @"select a.*, t.Name As UserTypeName
                            from dbo.UserAccounts a 
                            join UserType t On a.UserTypeId = t.Id 
                            WHERE (a.Email=@Email OR a.Name=@Email OR a.Mobile=@Email) 
                                and Pass=@Pass COLLATE SQL_Latin1_General_CP1_CS_AS";
                    if (!string.IsNullOrEmpty(oUserAccount.Email))
                    {
                        cmd.Parameters.AddWithValue("@Email", oUserAccount.Email);
                    }
                    if (!string.IsNullOrEmpty(oUserAccount.Password))
                    {
                        cmd.Parameters.AddWithValue("@Pass", Common.Md5(oUserAccount.Password));
                    }
                    //if (!string.IsNullOrEmpty(oUserAccount.Password))
                    //{
                    //    cmd.Parameters.AddWithValue("@Pass", oUserAccount.Password);
                    //}
                    cmd.CommandText = command;
                    conn.Open();

                    var reader = cmd.ExecuteReader();
                    var lstUserAccount = new List<DTO.Account.UserAccounts>();

                    var oResult = new ModelResult<List<DTO.Account.UserAccounts>>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var opUserAccount = new DTO.Account.UserAccounts();
                            if (reader["Avatar"] != DBNull.Value)
                                opUserAccount.Avatar = Convert.ToString(reader["Avatar"]);
                            if (reader["Email"] != DBNull.Value)
                                opUserAccount.Email = Convert.ToString(reader["Email"]);
                            if (reader["Gender"] != DBNull.Value)
                                opUserAccount.Gender = Convert.ToString(reader["Gender"]);
                            opUserAccount.Id = Convert.ToInt32(reader["Id"]);
                            if (reader["IsActive"] != DBNull.Value)
                                opUserAccount.IsActive = Convert.ToBoolean(reader["IsActive"]);
                            if (reader["IsDeleted"] != DBNull.Value)
                                opUserAccount.IsDeleted = Convert.ToBoolean(reader["IsDeleted"]);
                            if (reader["Mobile"] != DBNull.Value)
                                opUserAccount.Mobile = Convert.ToString(reader["Mobile"]);
                            if (reader["UserTypeId"] != DBNull.Value)
                                opUserAccount.UserTypeId = Convert.ToInt32(reader["UserTypeId"]);
                            if (reader["Name"] != DBNull.Value)
                                opUserAccount.Name = Convert.ToString(reader["Name"]);
                            if (reader["Pass"] != DBNull.Value)
                                opUserAccount.Pass = Convert.ToString(reader["Pass"]);
                            if (reader["EmailPassword"] != DBNull.Value)
                                opUserAccount.EmailPassword = Convert.ToString(reader["EmailPassword"]);
                            opUserAccount.ManagerGroupId = Convert.ToInt32(reader["ManagerGroupId"]);
                            if (reader["TraceUserActivity"] != DBNull.Value)
                                opUserAccount.TraceUserActivity = Convert.ToBoolean(reader["TraceUserActivity"]);
                            //if (reader["BenId"] != DBNull.Value)
                            //    opUserAccount.BenId = Convert.ToInt32(reader["BenId"]);
                            //if (reader["BenTypeId"] != DBNull.Value)
                            //    opUserAccount.BenTypeId = Convert.ToInt32(reader["BenTypeId"]);

                            lstUserAccount.Add(opUserAccount);
                        }
                    }
                    if (lstUserAccount.Count > 0)
                    {
                        oResult.HasResult = true;
                        oResult.Results = lstUserAccount;
                        //oResult.RowCount = count;
                    }
                    return oResult;
                }
            }
        }

        public static ModelResult<DTO.Account.UserAccounts> AddEditAccount(DTO.Account.UserAccounts oUserAccount)
        {
            var oResult = new ModelResult<DTO.Account.UserAccounts>();
            try
            {
                using (var conn = new SqlConnection(DbConnection.ConnectionString))
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SP_AccountAddEdit";
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (oUserAccount.Id > 0)
                            cmd.Parameters.AddWithValue("@Id", oUserAccount.Id);
                        if (!string.IsNullOrEmpty(oUserAccount.Name))
                            cmd.Parameters.AddWithValue("@Name", oUserAccount.Name);
                        if (!string.IsNullOrEmpty(oUserAccount.Avatar))
                            cmd.Parameters.AddWithValue("@Avatar", oUserAccount.Avatar);
                        if (!string.IsNullOrEmpty(oUserAccount.Email))
                            cmd.Parameters.AddWithValue("@Email", oUserAccount.Email);
                        if (!string.IsNullOrEmpty(oUserAccount.Pass))
                            cmd.Parameters.AddWithValue("@EmailPassword", Common.Md5(oUserAccount.Pass));
                        if (!string.IsNullOrEmpty(oUserAccount.Gender))
                            cmd.Parameters.AddWithValue("@Gender", oUserAccount.Gender);
                        if (oUserAccount.ManagerGroupId > 0)
                            cmd.Parameters.AddWithValue("@ManagerGroupId", oUserAccount.ManagerGroupId);
                        if (!string.IsNullOrEmpty(oUserAccount.Mobile))
                            cmd.Parameters.AddWithValue("@Mobile", oUserAccount.Mobile);
                        if (!string.IsNullOrEmpty(oUserAccount.Pass))
                            cmd.Parameters.AddWithValue("@Pass", Common.Md5(oUserAccount.Pass));
                        if (oUserAccount.UserTypeId > 0)
                            cmd.Parameters.AddWithValue("@UserTypeId", oUserAccount.UserTypeId);
                        if (oUserAccount.TraceUserActivity)
                            cmd.Parameters.AddWithValue("@TraceUserActivity", oUserAccount.TraceUserActivity);
                        conn.Open();
                        oUserAccount.Id = Convert.ToInt32(cmd.ExecuteScalar());
                        oResult.HasResult = true;
                        oResult.Results = oUserAccount;
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
        public static ModelResult<List<DTO.Account.UserAccounts>> UserAccountGet(DTO.Account.UserAccounts oUserAccount)
        {
            var oResult = new ModelResult<List<DTO.Account.UserAccounts>>();
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
                        command = @"SELECT TBL1.* , TBL2.Name Parm1
                                    FROM UserAccounts TBL1
                                    LEFT JOIN UserType TBL2 ON TBL1.UserTypeId = TBL2.Id
                                    WHERE 1=1 AND TBL1.IsDeleted = 0 ";

                        if (oUserAccount.Id > 0)
                        {
                            command += " AND TBL1.Id = @Id";
                            cmd.Parameters.AddWithValue("@Id", oUserAccount.Id);
                        }
                        if (oUserAccount.UserTypeId >= 0)
                        {
                            command += " And TBL1.UserTypeId =@UserTypeId ";
                            cmd.Parameters.AddWithValue("@UserTypeId", oUserAccount.UserTypeId);
                        }
                        if (!string.IsNullOrEmpty(oUserAccount.Name))
                        {
                            command += " And TBL1.Name Like @Name ";
                            cmd.Parameters.AddWithValue("@Name", "%" + oUserAccount.Name + "%");
                        }
                        if (!oUserAccount.IsList)
                        {
                            command +=
                                " order by @SortCol @SortType OFFSET(@Page - 1) * @RowsPerPage ROWS FETCH NEXT @RowsPerPage ROWS ONLY";
                            command = command.Replace("@SortCol", oUserAccount.SortCol);
                            command = command.Replace("@SortType", oUserAccount.SortType);
                            command = command.Replace("@Page", oUserAccount.Page.ToString());
                            command = command.Replace("@RowsPerPage", oUserAccount.RowPerPage.ToString());
                        }

                        #endregion

                        if (conn.State != ConnectionState.Open)
                            conn.Open();

                        cmd.CommandText = command;
                        SqlDataReader reader = cmd.ExecuteReader();
                        var lstUsers = new List<DTO.Account.UserAccounts>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                #region User Account Parms

                                var obUsers = new DTO.Account.UserAccounts();
                                obUsers.Id = Convert.ToInt32(reader["Id"]);
                                if (reader["UserTypeId"] != DBNull.Value)
                                    obUsers.UserTypeId = Convert.ToInt32(reader["UserTypeId"]);
                                if (reader["Name"] != DBNull.Value)
                                    obUsers.Name = Convert.ToString(reader["Name"]);
                                if (reader["Email"] != DBNull.Value)
                                    obUsers.Email = Convert.ToString(reader["Email"]);
                                if (reader["Pass"] != DBNull.Value)
                                    obUsers.Pass = Convert.ToString(reader["Pass"]);
                                if (reader["EmailPassword"] != DBNull.Value)
                                    obUsers.EmailPassword = Convert.ToString(reader["EmailPassword"]);
                                if (reader["Mobile"] != DBNull.Value)
                                    obUsers.Mobile = Convert.ToString(reader["Mobile"]);
                                if (reader["Gender"] != DBNull.Value)
                                    obUsers.Gender = Convert.ToString(reader["Gender"]);
                                if (reader["ManagerGroupId"] != DBNull.Value)
                                    obUsers.ManagerGroupId = Convert.ToInt32(reader["ManagerGroupId"]);
                                if (reader["IsActive"] != DBNull.Value)
                                    obUsers.IsActive = Convert.ToBoolean(reader["IsActive"]);
                                if (reader["Avatar"] != DBNull.Value)
                                    obUsers.Avatar = Convert.ToString(reader["Avatar"]);
                                if (reader["TraceUserActivity"] != DBNull.Value)
                                    obUsers.TraceUserActivity = Convert.ToBoolean(reader["TraceUserActivity"]);

                                #region UserType Parms

                                var obUserType = new DTO.Account.UserType();
                                if (reader["Parm1"] != DBNull.Value)
                                    obUserType.Name = Convert.ToString(reader["Parm1"]);

                                obUsers.OUserType = obUserType;

                                #endregion

                                #endregion

                                lstUsers.Add(obUsers);
                            }
                        }
                        int count = 0;
                        if (!oUserAccount.IsList)
                        {
                            using (SqlConnection connCount = new SqlConnection(DbConnection.ConnectionString))
                            {
                                using (var cmdCount = new SqlCommand())
                                {
                                    cmdCount.Connection = connCount;
                                    command = @"SELECT COUNT(1) FROM UserAccounts WHERE 1=1 AND IsDeleted = 0";
                                    if (oUserAccount.Id > 0)
                                    {
                                        command += " AND Id = @Id";
                                        cmdCount.Parameters.AddWithValue("@Id", oUserAccount.Id);
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

        public static ModelResult<DTO.Account.UserAccounts> UserAccountInsert(DTO.Account.UserAccounts oUserAccount)
        {
            var oResult = new ModelResult<DTO.Account.UserAccounts>();
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
                        cmd.CommandText = "SP_UserAccountInsert";
                        if (oUserAccount.UserTypeId > 0)
                        {
                            cmd.Parameters.AddWithValue("@UserTypeId", oUserAccount.UserTypeId);
                        }
                        if (!string.IsNullOrEmpty(oUserAccount.Name))
                        {
                            cmd.Parameters.AddWithValue("@Name", oUserAccount.Name);
                        }
                        if (!string.IsNullOrEmpty(oUserAccount.Email))
                        {
                            cmd.Parameters.AddWithValue("@Email", oUserAccount.Email);
                        }
                        if (!string.IsNullOrEmpty(oUserAccount.Pass))
                        {
                            cmd.Parameters.AddWithValue("@Pass", Common.Md5(oUserAccount.Pass));
                        }
                        if (!string.IsNullOrEmpty(oUserAccount.Pass))
                        {
                            cmd.Parameters.AddWithValue("@EmailPassword", Common.Md5(oUserAccount.Pass));
                        }
                        if (!string.IsNullOrEmpty(oUserAccount.Mobile))
                        {
                            cmd.Parameters.AddWithValue("@Mobile", oUserAccount.Mobile);
                        }
                        cmd.Parameters.AddWithValue("@Avatar", oUserAccount.Avatar);
                        cmd.Parameters.AddWithValue("@IsActive", oUserAccount.IsActive);
                        cmd.Parameters.AddWithValue("@TraceUserActivity", oUserAccount.TraceUserActivity);
                        cmd.Parameters.AddWithValue("@ManagerGroupId", oUserAccount.ManagerGroupId);
                        cmd.Parameters.AddWithValue("@Gender", oUserAccount.Gender);

                        conn.Open();
                        oUserAccount.Id = Convert.ToInt32(cmd.ExecuteScalar());
                        oResult.HasResult = true;
                        oResult.Results = oUserAccount;
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

        public static ModelResult<DTO.Account.UserAccounts> UserAccountUpdate(DTO.Account.UserAccounts oUserAccount)
        {
            var oResult = new ModelResult<DTO.Account.UserAccounts>();
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
                        cmd.CommandText = "SP_AccountAddEdit";
                        if (oUserAccount.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@Id", oUserAccount.Id);
                        }
                        if (oUserAccount.UserTypeId > 0)
                        {
                            cmd.Parameters.AddWithValue("@UserTypeId", oUserAccount.UserTypeId);
                        }
                        if (!string.IsNullOrEmpty(oUserAccount.Name))
                        {
                            cmd.Parameters.AddWithValue("@Name", oUserAccount.Name);
                        }
                        if (!string.IsNullOrEmpty(oUserAccount.Email))
                        {
                            cmd.Parameters.AddWithValue("@Email", oUserAccount.Email);
                        }
                        if (!string.IsNullOrEmpty(oUserAccount.Pass))
                        {
                            cmd.Parameters.AddWithValue("@Pass",  Common.Md5(oUserAccount.Pass));
                        }
                        if (!string.IsNullOrEmpty(oUserAccount.Pass))
                        {
                            cmd.Parameters.AddWithValue("@EmailPassword", Common.Md5(oUserAccount.Pass));
                        }
                        if (!string.IsNullOrEmpty(oUserAccount.Mobile))
                        {
                            cmd.Parameters.AddWithValue("@Mobile", oUserAccount.Mobile);
                        }
                        cmd.Parameters.AddWithValue("@Avatar", oUserAccount.Avatar);
                        cmd.Parameters.AddWithValue("@IsActive", oUserAccount.IsActive);
                        cmd.Parameters.AddWithValue("@TraceUserActivity", oUserAccount.TraceUserActivity);
                        cmd.Parameters.AddWithValue("@ManagerGroupId", oUserAccount.ManagerGroupId);
                        cmd.Parameters.AddWithValue("@Gender", oUserAccount.Gender.Trim());

                        conn.Open();
                        oUserAccount.Id = Convert.ToInt32(cmd.ExecuteScalar());
                        oResult.HasResult = true;
                        oResult.Results = oUserAccount;
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

        public static ModelResult<DTO.Account.UserAccounts> DeleteUserAccount(DTO.Account.UserAccounts oUserAccount)
        {
            var oResult = new ModelResult<DTO.Account.UserAccounts>();
            using (var conn = new SqlConnection(DbConnection.ConnectionString))
            {

                using (var cmd = new System.Data.SqlClient.SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SP_UserAccountDelete";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", oUserAccount.Id);
                    conn.Open();

                    oUserAccount.Id = Convert.ToInt32(cmd.ExecuteNonQuery());
                    oResult.HasResult = true;
                    oResult.Results = oUserAccount;

                }

                return oResult;
            }
        }

        //****************************************************//
        public static ModelResult<List<DTO.Account.UserProfile>> UserProfileGet(DTO.Account.UserProfile oUserProfile)
        {
            var oResult = new ModelResult<List<DTO.Account.UserProfile>>();
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
                        command = @"SELECT * from UserAccounts WHERE 1=1 AND IsDeleted = 0 ";

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
                        var lstUsers = new List<DTO.Account.UserProfile>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                #region User Account Parms

                                var obUsers = new DTO.Account.UserProfile();
                                obUsers.Id = Convert.ToInt32(reader["Id"]);
                             
                                if (reader["Name"] != DBNull.Value)
                                    obUsers.Name = Convert.ToString(reader["Name"]);
                                if (reader["Email"] != DBNull.Value)
                                    obUsers.Email = Convert.ToString(reader["Email"]);
                                if (reader["Pass"] != DBNull.Value)
                                    obUsers.CurrentPassword = Convert.ToString(reader["Pass"]);
                                if (reader["Mobile"] != DBNull.Value)
                                    obUsers.Mobile = Convert.ToString(reader["Mobile"]);
                                if (reader["Gender"] != DBNull.Value)
                                    obUsers.Gender = Convert.ToString(reader["Gender"]);
                                if (reader["Avatar"] != DBNull.Value)
                                    obUsers.Avatar = Convert.ToString(reader["Avatar"]);
                               

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
        public static ModelResult<List<DTO.Account.UserAccounts>> GetUsersForGroup(DTO.Account.UserAccounts oUserAccount, int groupId)
        {
            using (SqlConnection conn = new SqlConnection(DbConnection.ConnectionString))
            {
                using (var cmd = new System.Data.SqlClient.SqlCommand())
                {
                    cmd.Connection = conn;
                    var command = @"select * from (select U.*,UT.Name as UserTypeName from UserAccounts U 
                            Join UserType UT on UT.Id=U.UserTypeId 
                            ) as myTable where 1=1 and IsDeleted=0  ";

                    if (groupId > 0)
                    {
                        command += "and Id not in (select ContactId from SMSGroupsMembers where Con_TypeId in(128) and GroupId=@GroupId ) ";
                        cmd.Parameters.AddWithValue("@GroupId", groupId);
                    }

                    if (oUserAccount.Id > 0)
                    {
                        command += "and Id =@Id ";
                        cmd.Parameters.AddWithValue("@Id", oUserAccount.Id);
                    }
                    if (!String.IsNullOrEmpty(oUserAccount.Name))
                    {
                        command += "and Name like @Name ";
                        cmd.Parameters.AddWithValue("@Name", "%" + oUserAccount.Name + "%");
                    }
                    if (oUserAccount.IsActive.HasValue)
                    {
                        command += "and IsActive = @IsActive ";
                        cmd.Parameters.AddWithValue("@IsActive", oUserAccount.IsActive.Value);
                    }
                    if (oUserAccount.UserTypeId > 0)
                    {
                        command += "and UserTypeId = @UserTypeId ";
                        cmd.Parameters.AddWithValue("@UserTypeId", oUserAccount.UserTypeId);
                    }
                    if (!oUserAccount.IsList)
                    {
                        command +=
                            "order by @SortCol @SortType OFFSET (@Page -1 )* @RowsPerPage ROWS FETCH NEXT @RowsPerPage ROWS ONLY";
                        cmd.CommandText = command;
                        cmd.CommandText = cmd.CommandText.Replace("@SortCol", oUserAccount.SortCol);
                        cmd.CommandText = cmd.CommandText.Replace("@SortType", oUserAccount.SortType);
                        cmd.CommandText = cmd.CommandText.Replace("@Page", oUserAccount.Page.ToString());
                        cmd.CommandText = cmd.CommandText.Replace("@RowsPerPage", oUserAccount.RowPerPage.ToString());
                    }
                    conn.Open();

                    var reader = cmd.ExecuteReader();

                    var lstResult = new List<DTO.Account.UserAccounts>();

                    var oResult = new ModelResult<List<DTO.Account.UserAccounts>>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var obUsers = new DTO.Account.UserAccounts();
                            obUsers.Id = Convert.ToInt32(reader["Id"]);
                            obUsers.Name = reader["Name"].ToString();
                            obUsers.Pass = reader["Pass"].ToString();
                            obUsers.Email = reader["Email"].ToString();
                            obUsers.EmailPassword = reader["EmailPassword"].ToString();
                            obUsers.Mobile = reader["Mobile"].ToString();
                            obUsers.Gender = reader["Gender"].ToString();
                            obUsers.Avatar = reader["Avatar"].ToString();
                            obUsers.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());
                            obUsers.UserTypeId = Convert.ToInt32(reader["UserTypeId"].ToString());

                            #region UserType Parms

                            var obUserType = new DTO.Account.UserType();
                            if (reader["UserTypeName"] != DBNull.Value)
                                obUserType.Name = Convert.ToString(reader["UserTypeName"]);

                            obUsers.OUserType = obUserType;

                            #endregion

                            lstResult.Add(obUsers);
                        }
                    }
                    int Count = 0;
                    using (SqlConnection connCount = new SqlConnection(DbConnection.ConnectionString))
                    {
                        //try
                        //{
                        using (System.Data.SqlClient.SqlCommand cmdCount = new System.Data.SqlClient.SqlCommand())
                        {

                            command =
                                @"select count(1) from (select U.*,UT.Name as UserTypeName from UserAccounts U 
                            Join UserType UT on UT.Id=U.UserTypeId 
                            ) as myTable where 1=1 and IsDeleted=0  ";
                            if (groupId > 0)
                            {
                                command += "and Id not in (select ContactId from SMSGroupsMembers where Con_TypeId in(128) and GroupId=@GroupId ) ";
                                cmdCount.Parameters.AddWithValue("@GroupId", groupId);
                            }
                            if (oUserAccount.Id > 0)
                            {
                                command += "and Id=@Id ";
                                cmdCount.Parameters.AddWithValue("@Id", oUserAccount.Id);
                            }
                            if (!String.IsNullOrEmpty(oUserAccount.Name))
                            {
                                command += "and Name like @Name ";
                                cmdCount.Parameters.AddWithValue("@Name", "%" + oUserAccount.Name + "%");
                            }
                            if (oUserAccount.IsActive.HasValue)
                            {
                                command += "and IsActive = @IsActive ";
                                cmdCount.Parameters.AddWithValue("@IsActive", oUserAccount.IsActive.Value);
                            }
                            if (oUserAccount.UserTypeId > 0)
                            {
                                command += "and UserTypeId = @UserTypeId ";
                                cmdCount.Parameters.AddWithValue("@UserTypeId", oUserAccount.UserTypeId);
                            }

                            cmdCount.CommandText = command;
                            cmdCount.Connection = connCount;
                            connCount.Open();
                            Count = Convert.ToInt32(cmdCount.ExecuteScalar());
                        }
                        //}
                        //catch
                        //{
                        //    connCount.Close();
                        //}
                    }
                    if (lstResult.Count > 0)
                        oResult.HasResult = true;

                    oResult.Results = lstResult;
                    oResult.RowCount = Count;

                    return oResult;
                }
            }
        }
    }
}
