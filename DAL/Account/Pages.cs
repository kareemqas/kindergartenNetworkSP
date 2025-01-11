using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO.Common;
using System.Linq;

namespace DAL.Account
{
    public class Pages
    {
        public static ModelResult<List<DTO.Account.Pages>> GetPages(DTO.Account.Pages oPage)
        {
            using (SqlConnection conn = new SqlConnection(DbConnection.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    var command = @"Select *
                                    From Pages P
                                    
                                    WHERE P.IsDeleted=0 ";
                    if (oPage.Id != 0)
                    {
                        command += "and P.Id =isnull(@Id,P.Id) ";
                        cmd.Parameters.AddWithValue("@Id", oPage.Id);
                    }
                    if (!String.IsNullOrEmpty(oPage.Name))
                    {
                        command += "and P.Name like isnull(@Name,P.Name) ";
                        cmd.Parameters.AddWithValue("@Name", "%" + oPage.Name + "%");
                    }
                    if (oPage.IsActive.HasValue)
                    {
                        command += "and P.IsActive = isnull(@IsActive,P.IsActive) ";
                        cmd.Parameters.AddWithValue("@IsActive", oPage.IsActive.Value);
                    }
                    if (oPage.ParentId.HasValue)
                    {
                        command += "and P.ParentId = isnull(@ParentId,P.ParentId) ";
                        cmd.Parameters.AddWithValue("@ParentID", oPage.ParentId.Value);
                    }
                    if (!string.IsNullOrEmpty(oPage.Link))
                    {
                        command += "and P.Link = isnull(@Link,P.Link) ";
                        cmd.Parameters.AddWithValue("@Link", oPage.Link);
                    }
                    command += "Order By 1";
                    cmd.CommandText = command;
                    conn.Open();

                    var reader = cmd.ExecuteReader();
                    var lstPages = new List<DTO.Account.Pages>();

                    var oResult = new ModelResult<List<DTO.Account.Pages>>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var opPages = new DTO.Account.Pages();
                            {
                                opPages.Id = Convert.ToInt32(reader["Id"]);
                                opPages.Name = reader["Name"].ToString();
                                if (reader["Link"] != DBNull.Value)
                                    opPages.Link = reader["Link"].ToString();
                                opPages.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());
                                opPages.InMenu = Convert.ToBoolean(reader["InMenu"].ToString());
                                opPages.ForAdmin = Convert.ToBoolean(reader["ForAdmin"].ToString());
                                if (reader["ParentId"] != DBNull.Value)
                                    opPages.ParentId = Convert.ToInt32(reader["ParentId"].ToString());
                                opPages.NeedLogin = Convert.ToBoolean(reader["NeedLogin"].ToString());
                                opPages.Icon = reader["Icon"].ToString();
                                opPages.TypeId = Convert.ToInt32(reader["TypeId"]);
                                //opPages.ModuleId = Convert.ToInt32(reader["ModuleId"]);
                            }
                            lstPages.Add(opPages);
                        }
                    }
                    if (lstPages.Count > 0)
                    {
                        oResult.HasResult = true;
                        oResult.Results = lstPages;
                        //oResult.RowCount = count;
                    }
                    return oResult;
                }
            }
        }
        public static ModelResult<List<DTO.Account.Pages>> GetBtnInPage(DTO.Account.Pages oPage, int userTypeId)
        {
            using (SqlConnection conn = new SqlConnection(DbConnection.ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    var command = @"Select A1.* 
                                        From Pages A1
                                        join Pages A2 on A1.ParentId=A2.Id
                                        join UserTypePages UTP on A1.Id = PageId and UTP.UserTypeId = @UserTypeId
                                        where A1.IsActive = 1 and A1.TypeID=3 ";
                    if (!string.IsNullOrEmpty(oPage.Link))
                    {
                        command += "and A2.Link like isnull(@Link,A2.Link) ";
                        cmd.Parameters.AddWithValue("@Link", "%" + oPage.Link + "%");
                    }
                    if (userTypeId > 0)
                    {
                        cmd.Parameters.AddWithValue("@UserTypeId", userTypeId);
                    }
                    command += "Order By 1";
                    cmd.CommandText = command;
                    conn.Open();

                    var reader = cmd.ExecuteReader();
                    var lstPages = new List<DTO.Account.Pages>();

                    var oResult = new ModelResult<List<DTO.Account.Pages>>();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            var opPages = new DTO.Account.Pages();
                            {
                                opPages.Id = Convert.ToInt32(reader["Id"]);
                                opPages.Name = reader["Name"].ToString();
                                opPages.Link = reader["Link"].ToString();
                                opPages.IsActive = Convert.ToBoolean(reader["IsActive"].ToString());
                                opPages.InMenu = Convert.ToBoolean(reader["InMenu"].ToString());
                                opPages.ForAdmin = Convert.ToBoolean(reader["ForAdmin"].ToString());
                                opPages.ParentId = Convert.ToInt32(reader["ParentId"].ToString());
                                opPages.NeedLogin = Convert.ToBoolean(reader["NeedLogin"].ToString());
                                opPages.Icon = reader["Icon"].ToString();
                                opPages.TypeId = Convert.ToInt32(reader["TypeId"]);
                            }
                            lstPages.Add(opPages);
                        }
                    }
                    if (lstPages.Count > 0)
                    {
                        oResult.HasResult = true;
                        oResult.Results = lstPages;
                        //oResult.RowCount = count;
                    }
                    return oResult;
                }
            }
        }
        /*************************/
        public static ModelResult<List<DTO.Account.Pages>> PagesGet(DTO.Account.Pages oPages , bool IsTool)
        {
            var oResult = new ModelResult<List<DTO.Account.Pages>>();
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
                                    FROM Pages TBL1
                                    LEFT JOIN Pages TBL2 ON TBL1.ParentId = TBL2.Id
                                    
                                    WHERE 1=1 AND TBL1.IsDeleted = 0 ";

                        if (oPages.Id > 0)
                        {
                            command += " AND TBL1.Id = @Id";
                            cmd.Parameters.AddWithValue("@Id", oPages.Id);
                        }
                        if (oPages.ParentId >= 0)
                        {
                            command += " And TBL1.ParentId =@ParentId ";
                            cmd.Parameters.AddWithValue("@ParentId", oPages.ParentId);
                        }
                        if (!string.IsNullOrEmpty(oPages.Name))
                        {
                            command += " And TBL1.Name Like @Name ";
                            cmd.Parameters.AddWithValue("@Name", "%" + oPages.Name + "%");
                        }
                        if (IsTool)
                        {
                            command += " And TBL1.TypeId <> 3 ";
                        }
                        if (!oPages.IsList)
                        {
                            command += " order by @SortCol @SortType OFFSET(@Page - 1) * @RowsPerPage ROWS FETCH NEXT @RowsPerPage ROWS ONLY";
                            command = command.Replace("@SortCol", oPages.SortCol);
                            command = command.Replace("@SortType", oPages.SortType);
                            command = command.Replace("@Page", oPages.Page.ToString());
                            command = command.Replace("@RowsPerPage", oPages.RowPerPage.ToString());
                        }
                        #endregion
                        if (conn.State != ConnectionState.Open)
                            conn.Open();

                        cmd.CommandText = command;
                        SqlDataReader reader = cmd.ExecuteReader();
                        var lstPages = new List<DTO.Account.Pages>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                #region Pages Parms
                                var obPages = new DTO.Account.Pages();
                                obPages.Id = Convert.ToInt32(reader["Id"]);
                                if (reader["ParentId"] != DBNull.Value)
                                    obPages.ParentId = Convert.ToInt32(reader["ParentId"]);
                                if (reader["Name"] != DBNull.Value)
                                    obPages.Name = Convert.ToString(reader["Name"]);
                                if (reader["Link"] != DBNull.Value)
                                    obPages.Link = Convert.ToString(reader["Link"]);
                                if (reader["OrderId"] != DBNull.Value)
                                    obPages.OrderId = Convert.ToInt32(reader["OrderId"]);
                                if (reader["InMenu"] != DBNull.Value)
                                    obPages.InMenu = Convert.ToBoolean(reader["InMenu"]);
                                if (reader["ForAdmin"] != DBNull.Value)
                                    obPages.ForAdmin = Convert.ToBoolean(reader["ForAdmin"]);
                                if (reader["NeedLogin"] != DBNull.Value)
                                    obPages.NeedLogin = Convert.ToBoolean(reader["NeedLogin"]);
                                if (reader["IsActive"] != DBNull.Value)
                                    obPages.IsActive = Convert.ToBoolean(reader["IsActive"]);
                                if (reader["Icon"] != DBNull.Value)
                                    obPages.Icon = Convert.ToString(reader["Icon"]);
                                if (reader["TypeId"] != DBNull.Value)
                                    obPages.TypeId = Convert.ToInt32(reader["TypeId"]);

                                #region Parent Parms
                                var obParentPage = new DTO.Account.Pages();
                                if (reader["Parm1"] != DBNull.Value)
                                    obParentPage.Name = Convert.ToString(reader["Parm1"]);

                                obPages.OParentPage = obParentPage;
                                #endregion

                                #endregion
                                lstPages.Add(obPages);
                            }
                        }
                        int count = 0;
                        if (!oPages.IsList)
                        {
                            using (SqlConnection connCount = new SqlConnection(DbConnection.ConnectionString))
                            {
                                using (var cmdCount = new SqlCommand())
                                {
                                    cmdCount.Connection = connCount;
                                    command = @"SELECT COUNT(1) FROM Pages WHERE 1=1 AND IsDeleted = 0";
                                    if (oPages.Id > 0)
                                    {
                                        command += " AND Id = @Id";
                                        cmdCount.Parameters.AddWithValue("@Id", oPages.Id);
                                    }
                                    if (oPages.ParentId >= 0)
                                    {
                                        command += " And ParentId =@ParentId ";
                                        cmdCount.Parameters.AddWithValue("@ParentId", oPages.ParentId);
                                    }
                                    cmdCount.CommandText = command;
                                    if (connCount.State != ConnectionState.Open)
                                        connCount.Open();
                                    count = Convert.ToInt32(cmdCount.ExecuteScalar());
                                    connCount.Close();
                                }
                            }
                        }
                        if (lstPages.Count > 0)
                        {
                            oResult.HasResult = true;
                            oResult.Results = lstPages;
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
        public static ModelResult<DTO.Account.Pages> PagesInsert(DTO.Account.Pages oPages)
        {
            var oResult = new ModelResult<DTO.Account.Pages>();
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
                        cmd.CommandText = "SP_PageInsert";
                        if (oPages.TypeId > 0)
                        {
                            cmd.Parameters.AddWithValue("@TypeId", oPages.TypeId);
                        }
                        if (oPages.InMenu.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@InMenu", oPages.InMenu);
                        }
                        cmd.Parameters.AddWithValue("@NeedLogin", oPages.NeedLogin);
                        cmd.Parameters.AddWithValue("@IsActive", oPages.IsActive);
                        cmd.Parameters.AddWithValue("@ForAdmin", oPages.ForAdmin);

                        if (oPages.ParentId >= 0)
                        {
                            cmd.Parameters.AddWithValue("@ParentId", oPages.ParentId);
                        }
                        if (!string.IsNullOrEmpty(oPages.Icon))
                        {
                            cmd.Parameters.AddWithValue("@Icon", oPages.Icon);
                        }
                        if (!string.IsNullOrEmpty(oPages.Name))
                        {
                            cmd.Parameters.AddWithValue("@Name", oPages.Name);
                        }
                        if (!string.IsNullOrEmpty(oPages.Link))
                        {
                            cmd.Parameters.AddWithValue("@Link", oPages.Link);
                        }
                        conn.Open();
                        oPages.Id = Convert.ToInt32(cmd.ExecuteScalar());
                        oResult.HasResult = true;
                        oResult.Results = oPages;
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
        public static ModelResult<DTO.Account.Pages> PagesUpdate(DTO.Account.Pages oPages)
        {
            var oResult = new ModelResult<DTO.Account.Pages>();
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
                        cmd.CommandText = "SP_PageUpdate";
                        if (oPages.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@Id", oPages.Id);
                        }
                        if (oPages.TypeId > 0)
                        {
                            cmd.Parameters.AddWithValue("@TypeId", oPages.TypeId);
                        }
                        if (oPages.InMenu.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@InMenu", oPages.InMenu);
                        }
                        cmd.Parameters.AddWithValue("@NeedLogin", oPages.NeedLogin);
                        cmd.Parameters.AddWithValue("@IsActive", oPages.IsActive);
                        cmd.Parameters.AddWithValue("@ForAdmin", oPages.ForAdmin);

                        if (oPages.ParentId >= 0)
                        {
                            cmd.Parameters.AddWithValue("@ParentId", oPages.ParentId);
                        }
                        if (!string.IsNullOrEmpty(oPages.Icon))
                        {
                            cmd.Parameters.AddWithValue("@Icon", oPages.Icon);
                        }
                        if (!string.IsNullOrEmpty(oPages.Name))
                        {
                            cmd.Parameters.AddWithValue("@Name", oPages.Name);
                        }
                        if (!string.IsNullOrEmpty(oPages.Link))
                        {
                            cmd.Parameters.AddWithValue("@Link", oPages.Link);
                        }


                        conn.Open();
                        oPages.Id = Convert.ToInt32(cmd.ExecuteScalar());
                        oResult.HasResult = true;
                        oResult.Results = oPages;
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
        public static ModelResult<DTO.Account.Pages> DeletePage(DTO.Account.Pages oPage)
        {
            var oResult = new ModelResult<DTO.Account.Pages>();
            using (var conn = new SqlConnection(DbConnection.ConnectionString))
            {

                using (var cmd = new System.Data.SqlClient.SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SP_PageDelete";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", oPage.Id);
                    conn.Open();

                    oPage.Id = Convert.ToInt32(cmd.ExecuteNonQuery());
                    oResult.HasResult = true;
                    oResult.Results = oPage;

                }

                return oResult;
            }
        }
    }
}
