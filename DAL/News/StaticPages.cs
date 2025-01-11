using DTO.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL.News
{
    public class StaticPages
    {
        public static ModelResult<List<DTO.News.StaticPages>> StaticPagesGet(DTO.News.StaticPages oStaticPage)
        {
            var oResult = new ModelResult<List<DTO.News.StaticPages>>();
            var conn = new SqlConnection(DbConnection.ConnectionString);
            try
            {
                using (conn)
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        #region SQLCOMMAND Builder
                        var command = @"SELECT TBL1.*,TBL2.Name UpdatedByUser FROM StaticPages TBL1 LEFT JOIN UserAccounts TBL2 ON TBL1.UpdatedBy = TBL2.Id WHERE 1=1 ";

                        if (oStaticPage.Id > 0)
                        {
                            command += " AND TBL1.Id = @Id";
                            cmd.Parameters.AddWithValue("@Id", oStaticPage.Id);
                        }
                        #endregion
                        if (conn.State != ConnectionState.Open)
                            conn.Open();

                        cmd.CommandText = command;
                        SqlDataReader reader = cmd.ExecuteReader();
                        var lstStaticPages = new List<DTO.News.StaticPages>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var obStaticPages = new DTO.News.StaticPages();
                                var obUserAccount = new DTO.Account.UserAccounts();
                                if (reader["UpdatedByUser"] != DBNull.Value)
                                    obUserAccount.Name = Convert.ToString(reader["UpdatedByUser"]);
                                obStaticPages.OUpdatedBy = obUserAccount;
                                obStaticPages.Id = Convert.ToInt32(reader["Id"]);
                                if (reader["Image"] != DBNull.Value)
                                    obStaticPages.Image = Convert.ToString(reader["Image"]);
                                if (reader["PageName"] != DBNull.Value)
                                    obStaticPages.PageName = Convert.ToString(reader["PageName"]);
                                if (reader["Title"] != DBNull.Value)
                                    obStaticPages.Title = Convert.ToString(reader["Title"]);
                                obStaticPages.Description = Convert.ToString(reader["Description"]);
                                if (reader["Image2"] != DBNull.Value)
                                    obStaticPages.Image2 = Convert.ToString(reader["Image2"]);
                                if (reader["Image3"] != DBNull.Value)
                                    obStaticPages.Image3 = Convert.ToString(reader["Image3"]);
                                if (reader["Mobile"] != DBNull.Value)
                                    obStaticPages.Mobile = Convert.ToString(reader["Mobile"]);
                                if (reader["Phone"] != DBNull.Value)
                                    obStaticPages.Phone = Convert.ToString(reader["Phone"]);
                                if (reader["Email1"] != DBNull.Value)
                                    obStaticPages.Email1 = Convert.ToString(reader["Email1"]);
                                if (reader["Email2"] != DBNull.Value)
                                    obStaticPages.Email2 = Convert.ToString(reader["Email2"]);
                                if (reader["UpdatedBy"] != DBNull.Value)
                                    obStaticPages.UpdatedBy = Convert.ToInt32(reader["UpdatedBy"]);
                                if (reader["UpdatedDate"] != DBNull.Value)
                                    obStaticPages.UpdatedDate = Convert.ToDateTime(reader["UpdatedDate"]);
                                if (reader["IsImageView"] != DBNull.Value)
                                    obStaticPages.IsImageView = Convert.ToBoolean(reader["IsImageView"]);
                                lstStaticPages.Add(obStaticPages);
                            }
                        }
                        if (lstStaticPages.Count > 0)
                        {
                            oResult.HasResult = true;
                            oResult.Results = lstStaticPages;
                            oResult.RowCount = lstStaticPages.Count;
                        }
                    }
                }
            }
            //catch (Exception ex)
            //{
            //    oResult.Message = ex.Message;
            //    oResult.HasResult = false;
            //}
            finally
            {
                conn.Close();
            }
            return oResult;
        }
        public static ModelResult<DTO.News.StaticPages> StaticPageSave(DTO.News.StaticPages oStaticPage)
        {
            var oResult = new ModelResult<DTO.News.StaticPages>();
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
                        cmd.CommandText = "SP_StaticPageSave";
                        if (oStaticPage.Id >= 0)
                        {
                            cmd.Parameters.AddWithValue("@Id", oStaticPage.Id);
                        }
                        cmd.Parameters.AddWithValue("@Title", oStaticPage.Title);
                        cmd.Parameters.AddWithValue("@UpdatedBy", oStaticPage.UpdatedBy);
                        cmd.Parameters.AddWithValue("@Description", oStaticPage.Description);
                        //if (!string.IsNullOrEmpty(oStaticPage.PageNameEn))
                        //{
                        //    cmd.Parameters.AddWithValue("@PageNameEn", oStaticPage.PageNameEn);
                        //}
                        if (!string.IsNullOrEmpty(oStaticPage.Image3))
                        {
                            cmd.Parameters.AddWithValue("@Image3", oStaticPage.Image3);
                        }
                        if (!string.IsNullOrEmpty(oStaticPage.Image2))
                        {
                            cmd.Parameters.AddWithValue("@Image2", oStaticPage.Image2);
                        }
                        if (!string.IsNullOrEmpty(oStaticPage.Image))
                        {
                            cmd.Parameters.AddWithValue("@Image", oStaticPage.Image);
                        }
                        if (!string.IsNullOrEmpty(oStaticPage.Mobile))
                        {
                            cmd.Parameters.AddWithValue("@Mobile", oStaticPage.Mobile);
                        }
                        if (!string.IsNullOrEmpty(oStaticPage.Phone))
                        {
                            cmd.Parameters.AddWithValue("@Phone", oStaticPage.Phone);
                        }
                        if (!string.IsNullOrEmpty(oStaticPage.Email1))
                        {
                            cmd.Parameters.AddWithValue("@Email1", oStaticPage.Email1);
                        }
                        if (!string.IsNullOrEmpty(oStaticPage.Email2))
                        {
                            cmd.Parameters.AddWithValue("@Email2", oStaticPage.Email2);
                        }
                        if (oStaticPage.UpdatedDate.HasValue)
                        {
                            cmd.Parameters.AddWithValue("@UpdatedDate", oStaticPage.UpdatedDate.Value);
                        }
                        if (oStaticPage.IsImageView)
                        {
                            cmd.Parameters.AddWithValue("@IsImageView", oStaticPage.IsImageView);
                        }


                        conn.Open();
                        oStaticPage.Id = Convert.ToInt32(cmd.ExecuteScalar());
                        oResult.HasResult = true;
                        oResult.Results = oStaticPage;
                    }
                }
            }
            finally
            {
                conn.Close();
            }
            return oResult;
        }
    }

    public class StaticData
    {
        public static ModelResult<int> UpdateStaticData(DTO.News.StaticData oStaticData)
        {
            var oResult = new ModelResult<int>();
            using (var conn = new SqlConnection(DbConnection.ConnectionString))
            {
                try
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = "SP_UpdateStaticData";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@Id", oStaticData.Id);
                        cmd.Parameters.AddWithValue("@Title", oStaticData.Title);
                        cmd.Parameters.AddWithValue("@Data", oStaticData.Data);
                        cmd.Parameters.AddWithValue("@Status", oStaticData.Status);
                        cmd.Parameters.AddWithValue("@Icon", oStaticData.Icon);
                        conn.Open();
                        oResult.Results = Convert.ToInt32(cmd.ExecuteScalar());
                        oResult.HasResult = true;
                    }
                }
                catch
                {
                    conn.Close();
                    throw;
                }
                return oResult;
            }
        }

        public static ModelResult<List<DTO.News.StaticData>> GetStaticData(DTO.News.StaticData oStaticData)
        {
            var oResult = new ModelResult<List<DTO.News.StaticData>>();
            var conn = new SqlConnection(DbConnection.ConnectionString);
            try
            {
                using (conn)
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        cmd.CommandText = @"select * from StaticData where 1=1 ";
                        if (oStaticData.Type > 0)
                        {
                            cmd.CommandText += "and Type=@Type ";
                            cmd.Parameters.AddWithValue("@Type", oStaticData.Type);
                        }
                        cmd.CommandText += " order by Id asc ";
                        cmd.CommandType = CommandType.Text;
                        conn.Open();

                        SqlDataReader reader = cmd.ExecuteReader();
                        var lstResult = new List<DTO.News.StaticData>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var objStaticData = new DTO.News.StaticData();
                                objStaticData.Id = Convert.ToInt32(reader["Id"]);
                                objStaticData.Title = reader["Title"].ToString();
                                objStaticData.Data = reader["Data"].ToString();
                                objStaticData.Status = Convert.ToBoolean(reader["Status"].ToString());
                                objStaticData.Icon = reader["Icon"].ToString();
                                objStaticData.Type = Convert.ToInt32(reader["Type"].ToString());
                                lstResult.Add(objStaticData);
                            }
                        }
                        if (lstResult.Count > 0)
                        {
                            oResult.HasResult = true;
                            oResult.Results = lstResult;
                            oResult.RowCount = lstResult.Count;
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
    }
}
