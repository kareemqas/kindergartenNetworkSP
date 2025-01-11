using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO.Common;
using System.Linq;

namespace DAL.Account
{
    public class UserTypePages
    {
        public static ModelResult<List<DTO.Account.UserTypePages>> IsValidAdminTypePage(DTO.Account.UserTypePages oUserTypePages)
        {
            using (SqlConnection conn = new SqlConnection(DbConnection.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    var command = @"Select PageID, UserTypeID From UserTypePages Where UserTypeId = @UserTypeId And PageId = @PageId";
                    if (oUserTypePages.UserTypeId != 0)
                    {
                        cmd.Parameters.AddWithValue("@UserTypeId", oUserTypePages.UserTypeId);
                    }
                    if (oUserTypePages.PageId  != 0)
                    {
                        cmd.Parameters.AddWithValue("@PageId", oUserTypePages.PageId);
                    }                   
                    cmd.CommandText = command;
                    conn.Open();

                    var reader = cmd.ExecuteReader();
                    var lstUserTypePages = new List<DTO.Account.UserTypePages>();

                    var oResult = new ModelResult<List<DTO.Account.UserTypePages>>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var opUserTypePages = new DTO.Account.UserTypePages();
                            {
                                opUserTypePages.PageId = Convert.ToInt32(reader["PageId"]);
                                opUserTypePages.UserTypeId = Convert.ToInt32(reader["UserTypeId"]);
                            }
                            lstUserTypePages.Add(opUserTypePages);
                        }
                    }
                    if (lstUserTypePages.Count > 0)
                    {
                        oResult.HasResult = true;
                        oResult.Results = lstUserTypePages;
                    }
                    return oResult;
                }
            }
        }
        public static ModelResult<List<DTO.Account.Pages>> GetUserTypePages(DTO.Account.UserTypePages oUserTypePages , DTO.Account.Pages oPages)
        {
            using (SqlConnection conn = new SqlConnection(DbConnection.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    String command = "SP_GetAccountTypePages";
                    cmd.Connection = conn;
                    cmd.CommandText = command;
                    cmd.CommandType = CommandType.StoredProcedure;
                    conn.Open();

                    if (oUserTypePages.UserTypeId != 0)
                        cmd.Parameters.AddWithValue("@UserTypeId", oUserTypePages.UserTypeId);

                    if (oPages.InMenu.HasValue)
                        cmd.Parameters.AddWithValue("@InMenu", oPages.InMenu.Value);

                    if (oPages.ParentId.HasValue)
                        cmd.Parameters.AddWithValue("@ParentId", oPages.ParentId.Value);


                    if (oPages.TypeId > 0)
                        cmd.Parameters.AddWithValue("@TypeId", oPages.TypeId);

                    var reader = cmd.ExecuteReader();
                    var lstPage = new List<DTO.Account.Pages>();

                    var oResult = new ModelResult<List<DTO.Account.Pages>>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var opPage = new DTO.Account.Pages();
                            opPage.Id = Convert.ToInt32(reader["Id"]);
                            opPage.Name = reader["Name"].ToString();
                            opPage.Icon = reader["Icon"].ToString();
                            opPage.Link = reader["Link"].ToString();
                            opPage.OrderId = Convert.ToInt32(reader["OrderId"].ToString());
                            opPage.InMenu = Convert.ToBoolean(reader["InMenu"].ToString());
                            opPage.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());
                            if (reader["ParentId"] != DBNull.Value)
                                opPage.ParentId = Convert.ToInt32(reader["ParentId"]);
                            opPage.TypeId = Convert.ToInt32(reader["TypeId"]);

                            lstPage.Add(opPage);
                        }
                    }
                    if (lstPage.Count > 0)
                    {
                        oResult.HasResult = true;
                        oResult.Results = lstPage;
                    }
                    return oResult;
                }
            }
        }

        #region UserShortcuts
        public static ModelResult<List<DTO.Account.Pages>> GetSelectedUserShortcuts(DTO.Account.UserAccounts user)
        {
            using (SqlConnection conn = new SqlConnection(DbConnection.ConnectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandText= @"SELECT * FROM Pages p
                                        JOIN UserShortcuts ush
                                        ON (p.Id = ush.PageId AND ush.UserId = @userId)
                                        WHERE p.IsActive = 1 and p.IsDeleted = 0
                                        order by ush.OrderNo ";
                    comm.Parameters.AddWithValue("@userId", user.Id);
                    comm.CommandType = CommandType.Text;

                    if (conn.State != ConnectionState.Open)
                        conn.Open();

                    var oResult = new ModelResult<List<DTO.Account.Pages>>();
                    var lstPage = new List<DTO.Account.Pages>();
                    var reader = comm.ExecuteReader();
                    if(reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var opPage = new DTO.Account.Pages();
                            opPage.Id = Convert.ToInt32(reader["Id"]);
                            opPage.Name = reader["Name"].ToString();
                            opPage.Link = reader["Link"].ToString();
                            opPage.Icon = Convert.ToString(reader["Icon"]);
                            lstPage.Add(opPage);
                        }
                        if (lstPage.Any())
                        {
                            oResult.HasResult = true;
                            oResult.Results = lstPage;
                        }
                        else
                        {
                            oResult.HasResult = false;
                            oResult.Results = new List<DTO.Account.Pages>();
                        }
                    }
                    return oResult;
                }
            }
        }
        public static ModelResult<int> AddUserShortcuts(string pagesIds, int userId)
        {
            var oResult = new ModelResult<int>();
            using (SqlConnection conn = new SqlConnection(DbConnection.ConnectionString))
            {
                using (SqlCommand comm = new SqlCommand())
                {
                    comm.Connection = conn;
                    comm.CommandType = CommandType.StoredProcedure;
                    comm.CommandText = "SP_UserShortcutsAdd";

                    if (userId > 0)
                        comm.Parameters.AddWithValue("@UserId",userId);
                    //if (!string.IsNullOrEmpty(pagesIds))
                        comm.Parameters.AddWithValue("@PagesIds",pagesIds);

                    conn.Open();
                    comm.ExecuteNonQuery();
                    oResult.HasResult = true;
                    conn.Close();
                }
                return oResult;
            }
        }
        #endregion UserShortcuts

        public static ModelResult<int> AddUserTypePages(int userTypeId, string pages)
        {
            var oResult = new ModelResult<int>();
            using (var conn = new SqlConnection(DbConnection.ConnectionString))
            {

                try
                {
                    using (var cmd = new SqlCommand())
                    {

                        cmd.Connection = conn;
                        cmd.CommandText = "SP_UserTypePagesAdd";
                        cmd.CommandType = CommandType.StoredProcedure;
                        if (userTypeId > 0)
                            cmd.Parameters.AddWithValue("@UserTypeId", userTypeId);
                        if (!string.IsNullOrEmpty(pages))
                            cmd.Parameters.AddWithValue("@Pages", pages);
                        conn.Open();

                        cmd.ExecuteScalar();
                        oResult.HasResult = true;
                        conn.Close();
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

        public static ModelResult<DTO.Account.UserTypePages> DeleteUserTypePages(DTO.Account.UserTypePages userTypePages)
        {
            var oResult = new ModelResult<DTO.Account.UserTypePages>();
            using (var conn = new SqlConnection(DbConnection.ConnectionString))
            {

                using (var cmd = new System.Data.SqlClient.SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SP_UserTypePagesDelete";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@UserTypeId", userTypePages.UserTypeId);
                    conn.Open();

                    userTypePages.UserTypeId = Convert.ToInt32(cmd.ExecuteNonQuery());
                    oResult.HasResult = true;
                    oResult.Results = userTypePages;

                }

                return oResult;
            }
        }
    }
}
