using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO.Common;

namespace DAL.News
{
    public class News
    {
        #region News
        public static ModelResult<List<DTO.News.News>> NewsGet(DTO.News.News oNews, int langId)
        {
            var oResult = new ModelResult<List<DTO.News.News>>();
            var conn = new SqlConnection(DbConnection.ConnectionString);
            try
            {
                using (conn)
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        #region SQLCOMMAND Builder
                        var command = @"SELECT TBL1.*,TBL2.NameAr,TBL2.NameEn,TBL3.Name InsertedByName FROM News TBL1
                                    JOIN Categories TBL2 ON TBL1.CategoryId = TBL2.Id
                                    JOIN UserAccounts TBL3 ON TBL1.InsertedBy = TBL3.Id
                                    WHERE 1=1 AND TBL1.IsDeleted = 0";
                        if(langId > 0)
                        {
                            command += " AND TBL1.langId = @langId";
                            cmd.Parameters.AddWithValue("@langId", langId);
                        }

                        if (oNews.Id > 0)
                        {
                            command += " AND TBL1.Id = @Id";
                            cmd.Parameters.AddWithValue("@Id", oNews.Id);
                        }
                        if (oNews.IsArticle)
                        {
                            command += " AND TBL1.CategoryId > 1";
                        }
                        if (oNews.CategoryId > 0)
                        {
                            command += " AND TBL1.CategoryId = @CategoryId";
                            cmd.Parameters.AddWithValue("@CategoryId", oNews.CategoryId);
                        }
                        if (oNews.FromDate.HasValue)
                        {
                            command += " AND Cast (TBL1.InsertedDate as date) >= @FromDate";
                            cmd.Parameters.AddWithValue("@FromDate", oNews.FromDate.Value.ToString("yyyy-MM-dd"));
                        }
                        if (oNews.ToDate.HasValue)
                        {
                            command += " AND Cast (TBL1.InsertedDate as date) <= @ToDate";
                            cmd.Parameters.AddWithValue("@ToDate", oNews.ToDate.Value.ToString("yyyy-MM-dd"));
                        }
                        if (oNews.InsertedBy > 0)
                        {
                            command += " AND TBL1.InsertedBy = @InsertedBy";
                            cmd.Parameters.AddWithValue("@InsertedBy", oNews.InsertedBy);
                        }
                        if (!string.IsNullOrEmpty(oNews.Title))
                        {
                            command += " And (TBL1.Title Like @Title OR TBL1.Summary Like @Title OR TBL1.Details Like @Title ) ";
                            cmd.Parameters.AddWithValue("@Title", "%" + oNews.Title + "%");
                        }
                        if (!oNews.IsList)
                        {
                            command += " order by @SortCol @SortType OFFSET(@Page - 1) * @RowsPerPage ROWS FETCH NEXT @RowsPerPage ROWS ONLY";
                            command = command.Replace("@SortCol", oNews.SortCol);
                            command = command.Replace("@SortType", oNews.SortType);
                            command = command.Replace("@Page", oNews.Page.ToString());
                            command = command.Replace("@RowsPerPage", oNews.RowPerPage.ToString());
                        }
                        #endregion
                        if (conn.State != ConnectionState.Open)
                            conn.Open();

                        cmd.CommandText = command;
                        SqlDataReader reader = cmd.ExecuteReader();
                        var lstNews = new List<DTO.News.News>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                #region Constant Parms
                                var obNews = new DTO.News.News();
                                #region News Params
                                /*****************/
                                var oCategory = new DTO.News.Categories();
                                oCategory.Id = Convert.ToInt32(reader["Id"]);
                                oCategory.NameAr = Convert.ToString(reader["NameAr"]);
                                if(reader["NameEn"] != DBNull.Value)
                                    oCategory.NameEn = Convert.ToString(reader["NameEn"]);
                                obNews.OCategory = oCategory;
                                /******************/
                                var oInsertedBy = new DTO.Account.UserAccounts();
                                oInsertedBy.Name = Convert.ToString(reader["InsertedByName"]);
                                obNews.OInsertedBy = oInsertedBy;
                                /*************/
                                obNews.Id = Convert.ToInt32(reader["Id"]);
                                obNews.Title = Convert.ToString(reader["Title"]);
                                obNews.Summary = Convert.ToString(reader["Summary"]);
                                obNews.Details = Convert.ToString(reader["Details"]);
                                obNews.PublishDate = Convert.ToDateTime(reader["PublishDate"]);
                                obNews.InsertedDate = Convert.ToDateTime(reader["InsertedDate"]);
                                obNews.InsertedBy = Convert.ToInt32(reader["InsertedBy"]);
                                if(reader["UpdatedBy"] != DBNull.Value)
                                    obNews.UpdatedBy = Convert.ToInt32(reader["UpdatedBy"]);
                                if (reader["UpdatedDate"] != DBNull.Value)
                                    obNews.UpdatedDate = Convert.ToDateTime(reader["UpdatedDate"]);
                                obNews.ViewsCount = Convert.ToInt32(reader["ViewsCount"]);
                                obNews.Status = Convert.ToInt32(reader["Status"]);
                                if (reader["Keywords"] != DBNull.Value)
                                    obNews.Keywords = Convert.ToString(reader["Keywords"]);
                                obNews.Image = Convert.ToString(reader["Image"]);
                                obNews.IsActive = Convert.ToBoolean(reader["IsActive"]);
                                obNews.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                                #endregion
                                #endregion
                                lstNews.Add(obNews);
                            }
                        }
                        int count = 0;
                        if (!oNews.IsList)
                        {
                            using (SqlConnection connCount = new SqlConnection(DbConnection.ConnectionString))
                            {
                                using (var cmdCount = new SqlCommand())
                                {
                                    cmdCount.Connection = connCount;
                                    command = @"SELECT COUNT(1) FROM News WHERE 1=1 AND IsDeleted = 0";
                                    if (langId > 0)
                                    {
                                        command += " AND langId = @langId";
                                        cmdCount.Parameters.AddWithValue("@langId", langId);
                                    }
                                    if (oNews.Id > 0)
                                    {
                                        command += " AND Id = @Id";
                                        cmdCount.Parameters.AddWithValue("@Id", oNews.Id);
                                    }
                                    if (oNews.IsArticle)
                                    {
                                        command += " AND CategoryId > 1";
                                    }
                                    if (!string.IsNullOrEmpty(oNews.Title))
                                    {
                                        command += " And (Title Like @Title OR Summary Like @Title Or Details  Like @Title )";
                                        cmdCount.Parameters.AddWithValue("@Title", "%" + oNews.Title + "%");
                                    }
                                    if (oNews.CategoryId > 0)
                                    {
                                        command += " AND CategoryId = @CategoryId";
                                        cmdCount.Parameters.AddWithValue("@CategoryId", oNews.CategoryId);
                                    }
                                    if (oNews.FromDate.HasValue)
                                    {
                                        command += " AND Cast (InsertedDate as date) >= @FromDate";
                                        cmdCount.Parameters.AddWithValue("@FromDate", oNews.FromDate.Value.ToString("yyyy-MM-dd"));
                                    }
                                    if (oNews.ToDate.HasValue)
                                    {
                                        command += " AND Cast (InsertedDate as date) <= @ToDate";
                                        cmdCount.Parameters.AddWithValue("@ToDate", oNews.ToDate.Value.ToString("yyyy-MM-dd"));
                                    }
                                    if (oNews.InsertedBy > 0)
                                    {
                                        command += " AND InsertedBy = @InsertedBy";
                                        cmdCount.Parameters.AddWithValue("@InsertedBy", oNews.InsertedBy);
                                    }
                                    cmdCount.CommandText = command;
                                    if (connCount.State != ConnectionState.Open)
                                        connCount.Open();
                                    count = Convert.ToInt32(cmdCount.ExecuteScalar());
                                    connCount.Close();
                                }
                            }
                        }
                        if (lstNews.Count > 0)
                        {
                            oResult.HasResult = true;
                            oResult.Results = lstNews;
                            oResult.RowCount = count;
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
        public static ModelResult<DTO.News.News> NewsSave(DTO.News.News oNews)
        {
            var oResult = new ModelResult<DTO.News.News>();
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
                        cmd.CommandText = "SP_NewsSave";

                        cmd.Parameters.AddWithValue("@Id", oNews.Id);
                        cmd.Parameters.AddWithValue("@Title", oNews.Title);
                        if (!string.IsNullOrEmpty(oNews.Summary))
                            cmd.Parameters.AddWithValue("@Summary", oNews.Summary);
                        cmd.Parameters.AddWithValue("@Details", oNews.Details);

                        if (oNews.UpdatedBy > 0)
                            cmd.Parameters.AddWithValue("@UpdatedBy", oNews.UpdatedBy);
                        cmd.Parameters.AddWithValue("@InsertedBy", oNews.InsertedBy);
                        if(!string.IsNullOrEmpty(oNews.Image))
                            cmd.Parameters.AddWithValue("@Image", oNews.Image);
                        if (!string.IsNullOrEmpty(oNews.Keywords))
                            cmd.Parameters.AddWithValue("@Keywords", oNews.Keywords);
                        cmd.Parameters.AddWithValue("@CategoryId", oNews.CategoryId);
                        cmd.Parameters.AddWithValue("@PublishDate", oNews.PublishDate);
                        cmd.Parameters.AddWithValue("@IsActive", oNews.IsActive);

                        conn.Open();
                        oNews.Id = Convert.ToInt32(cmd.ExecuteScalar());
                        oResult.HasResult = true;
                        oResult.Results = oNews;
                    }
                }
            }
            finally
            {
                conn.Close();
            }
            return oResult;
        }
        public static ModelResult<int> NewsDelete(int id)
        {
            int x;
            var oResult = new ModelResult<int>();
            using (var conn = new SqlConnection(DbConnection.ConnectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SP_NewsDelete";
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
        public static ModelResult<List<DTO.News.News>> NewsInMainPageGet(DTO.News.News oNews, int langId)
        {
            var oResult = new ModelResult<List<DTO.News.News>>();
            var conn = new SqlConnection(DbConnection.ConnectionString);
            try
            {
                using (conn)
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        #region SQLCOMMAND Builder
                        var command = @"SELECT TBL1.*,TBL2.NameAr,TBL2.NameEn,TBL3.Name InsertedByName FROM News TBL1
                                    JOIN Categories TBL2 ON TBL1.CategoryId = TBL2.Id
                                    JOIN UserAccounts TBL3 ON TBL1.InsertedBy = TBL3.Id
                                    WHERE 1=1 AND TBL1.IsDeleted = 0 AND TBL1.IsActive = 1 AND TBL1.IsInHome = 1 ";
                        if (langId > 0)
                        {
                            command += " AND TBL1.langId = @langId";
                            cmd.Parameters.AddWithValue("@langId", langId);
                        }
                        if (oNews.CategoryId > 0)
                        {
                            command += " AND TBL1.CategoryId = @CategoryId";
                            cmd.Parameters.AddWithValue("@CategoryId", oNews.CategoryId);
                        }
                        if (!oNews.IsList)
                        {
                            command += " order by @SortCol @SortType OFFSET(@Page - 1) * @RowsPerPage ROWS FETCH NEXT @RowsPerPage ROWS ONLY";
                            command = command.Replace("@SortCol", oNews.SortCol);
                            command = command.Replace("@SortType", oNews.SortType);
                            command = command.Replace("@Page", oNews.Page.ToString());
                            command = command.Replace("@RowsPerPage", oNews.RowPerPage.ToString());
                        }
                        #endregion
                        if (conn.State != ConnectionState.Open)
                            conn.Open();

                        cmd.CommandText = command;
                        SqlDataReader reader = cmd.ExecuteReader();
                        var lstNews = new List<DTO.News.News>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                #region Constant Parms
                                var obNews = new DTO.News.News();
                                #region News Params
                                /*****************/
                                var oCategory = new DTO.News.Categories();
                                oCategory.Id = Convert.ToInt32(reader["Id"]);
                                oCategory.NameAr = Convert.ToString(reader["NameAr"]);
                                if (reader["NameEn"] != DBNull.Value)
                                    oCategory.NameEn = Convert.ToString(reader["NameEn"]);
                                obNews.OCategory = oCategory;
                                /******************/
                                var oInsertedBy = new DTO.Account.UserAccounts();
                                oInsertedBy.Name = Convert.ToString(reader["InsertedByName"]);
                                obNews.OInsertedBy = oInsertedBy;
                                /*************/
                                obNews.Id = Convert.ToInt32(reader["Id"]);
                                obNews.Title = Convert.ToString(reader["Title"]);
                                obNews.Summary = Convert.ToString(reader["Summary"]);
                                obNews.Details = Convert.ToString(reader["Details"]);
                                obNews.PublishDate = Convert.ToDateTime(reader["PublishDate"]);
                                obNews.InsertedDate = Convert.ToDateTime(reader["InsertedDate"]);
                                obNews.InsertedBy = Convert.ToInt32(reader["InsertedBy"]);
                                if (reader["UpdatedBy"] != DBNull.Value)
                                    obNews.UpdatedBy = Convert.ToInt32(reader["UpdatedBy"]);
                                if (reader["UpdatedDate"] != DBNull.Value)
                                    obNews.UpdatedDate = Convert.ToDateTime(reader["UpdatedDate"]);
                                obNews.ViewsCount = Convert.ToInt32(reader["ViewsCount"]);
                                obNews.Status = Convert.ToInt32(reader["Status"]);
                                if (reader["Keywords"] != DBNull.Value)
                                    obNews.Keywords = Convert.ToString(reader["Keywords"]);
                                obNews.LangId = Convert.ToInt32(reader["LangId"]);
                                obNews.Image = Convert.ToString(reader["Image"]);
                                obNews.IsActive = Convert.ToBoolean(reader["IsActive"]);
                                obNews.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                                #endregion
                                #endregion
                                lstNews.Add(obNews);
                            }
                        }
                        int count = 0;
                        if (!oNews.IsList)
                        {
                            using (SqlConnection connCount = new SqlConnection(DbConnection.ConnectionString))
                            {
                                using (var cmdCount = new SqlCommand())
                                {
                                    cmdCount.Connection = connCount;
                                    command = @"SELECT COUNT(1) FROM News WHERE 1=1 AND IsDeleted = 0";
                                    if (oNews.Id > 0)
                                    {
                                        command += " AND Id = @Id";
                                        cmdCount.Parameters.AddWithValue("@Id", oNews.Id);
                                    }
                                    if (!string.IsNullOrEmpty(oNews.Title))
                                    {
                                        command += " And Title Like @Title ";
                                        cmdCount.Parameters.AddWithValue("@Title", "%" + oNews.Title + "%");
                                    }
                                    cmdCount.CommandText = command;
                                    if (connCount.State != ConnectionState.Open)
                                        connCount.Open();
                                    count = Convert.ToInt32(cmdCount.ExecuteScalar());
                                    connCount.Close();
                                }
                            }
                        }
                        if (lstNews.Count > 0)
                        {
                            oResult.HasResult = true;
                            oResult.Results = lstNews;
                            oResult.RowCount = count;
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



        public static ModelResult<List<DTO.News.News>> NewsHomePageListGet(int langId)
        {
            var oResult = new ModelResult<List<DTO.News.News>>();
            var conn = new SqlConnection(DbConnection.ConnectionString);
            try
            {
                using (conn)
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        #region SQLCOMMAND Builder
                        var command = @"select * from (
                        SELECT Top 6 N.*,C.NameAr,C.NameEn,UA.Name InsertedByName,splits FROM News N
                        JOIN Categories C ON N.CategoryId = C.Id
                        JOIN UserAccounts UA ON N.InsertedBy = UA.Id
                        cross apply dbo.SplitString(N.HomePosition,',')
                        WHERE 1=1 AND N.IsDeleted = 0 AND N.IsActive = 1 AND N.IsInHome = 1 AND splits=2 AND LangId= @LangId -- slider
                        order by PublishDate desc, Id desc
                        union ALL
                        SELECT TOP 8 N.*,C.NameAr,C.NameEn,UA.Name InsertedByName,splits FROM News N
                        JOIN Categories C ON N.CategoryId = C.Id
                        JOIN UserAccounts UA ON N.InsertedBy = UA.Id
                        cross apply dbo.SplitString(N.HomePosition,',')
                        WHERE 1=1 AND N.IsDeleted = 0 AND N.IsActive = 1 AND N.IsInHome = 1 AND splits=3 AND N.CategoryId=1  AND LangId= @LangId
                        order by PublishDate desc, Id desc
                        union ALL
                        SELECT TOP 8 N.*,C.NameAr,C.NameEn,UA.Name InsertedByName,splits FROM News N
                        JOIN Categories C ON N.CategoryId = C.Id
                        JOIN UserAccounts UA ON N.InsertedBy = UA.Id
                        cross apply dbo.SplitString(N.HomePosition,',')
                        WHERE 1=1 AND N.IsDeleted = 0 AND N.IsActive = 1 AND N.IsInHome = 1 AND splits=3 AND N.CategoryId=2 AND LangId= @LangId
                        order by PublishDate desc, Id desc
                        union ALL
                        SELECT TOP 8 N.*,C.NameAr,C.NameEn,UA.Name InsertedByName,splits FROM News N
                        JOIN Categories C ON N.CategoryId = C.Id
                        JOIN UserAccounts UA ON N.InsertedBy = UA.Id
                        cross apply dbo.SplitString(N.HomePosition,',')
                        WHERE 1=1 AND N.IsDeleted = 0 AND N.IsActive = 1 AND N.IsInHome = 1 AND splits=3 AND N.CategoryId=3 AND LangId= @LangId
                        order by PublishDate desc, Id desc
                        union ALL
                        SELECT TOP 8 N.*,C.NameAr,C.NameEn,UA.Name InsertedByName,splits FROM News N
                        JOIN Categories C ON N.CategoryId = C.Id
                        JOIN UserAccounts UA ON N.InsertedBy = UA.Id
                        cross apply dbo.SplitString(N.HomePosition,',')
                        WHERE 1=1 AND N.IsDeleted = 0 AND N.IsActive = 1 AND N.IsInHome = 1 AND splits=3 AND N.CategoryId=4 AND LangId= @LangId
                        order by PublishDate desc, Id desc
                        union ALL
                        SELECT TOP 8 N.*,C.NameAr,C.NameEn,UA.Name InsertedByName,splits FROM News N
                        JOIN Categories C ON N.CategoryId = C.Id
                        JOIN UserAccounts UA ON N.InsertedBy = UA.Id
                        cross apply dbo.SplitString(N.HomePosition,',')
                        WHERE 1=1 AND N.IsDeleted = 0 AND N.IsActive = 1 AND N.IsInHome = 1 AND splits=3 AND N.CategoryId=5 AND LangId= @LangId
                        order by PublishDate desc, Id desc
                        ) HomeNews";
                        #endregion

                        cmd.Parameters.AddWithValue("@LangId", langId);
                        if (conn.State != ConnectionState.Open)
                            conn.Open();

                        cmd.CommandText = command;
                        SqlDataReader reader = cmd.ExecuteReader();
                        var lstNews = new List<DTO.News.News>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                #region Constant Parms
                                var obNews = new DTO.News.News();
                                #region News Params
                                /*****************/
                                var oCategory = new DTO.News.Categories();
                                oCategory.Id = Convert.ToInt32(reader["Id"]);
                                oCategory.NameAr = Convert.ToString(reader["NameAr"]);
                                if (reader["NameEn"] != DBNull.Value)
                                    oCategory.NameEn = Convert.ToString(reader["NameEn"]);
                                obNews.OCategory = oCategory;
                                /******************/
                                var oInsertedBy = new DTO.Account.UserAccounts();
                                oInsertedBy.Name = Convert.ToString(reader["InsertedByName"]);
                                obNews.OInsertedBy = oInsertedBy;
                                /*************/
                                obNews.Id = Convert.ToInt32(reader["Id"]);
                                obNews.Title = Convert.ToString(reader["Title"]);
                                obNews.Summary = Convert.ToString(reader["Summary"]);
                                obNews.Details = Convert.ToString(reader["Details"]);
                                obNews.PublishDate = Convert.ToDateTime(reader["PublishDate"]);
                                obNews.InsertedDate = Convert.ToDateTime(reader["InsertedDate"]);
                                obNews.InsertedBy = Convert.ToInt32(reader["InsertedBy"]);
                                if (reader["UpdatedBy"] != DBNull.Value)
                                    obNews.UpdatedBy = Convert.ToInt32(reader["UpdatedBy"]);
                                if (reader["UpdatedDate"] != DBNull.Value)
                                    obNews.UpdatedDate = Convert.ToDateTime(reader["UpdatedDate"]);
                                obNews.ViewsCount = Convert.ToInt32(reader["ViewsCount"]);
                                obNews.Status = Convert.ToInt32(reader["Status"]);
                                if (reader["Keywords"] != DBNull.Value)
                                    obNews.Keywords = Convert.ToString(reader["Keywords"]);
                                obNews.LangId = Convert.ToInt32(reader["LangId"]);
                                obNews.Image = Convert.ToString(reader["Image"]);
                                obNews.IsActive = Convert.ToBoolean(reader["IsActive"]);
                                obNews.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                                #endregion
                                #endregion
                                lstNews.Add(obNews);
                            }
                        }
                        if (lstNews.Count > 0)
                        {
                            oResult.HasResult = true;
                            oResult.Results = lstNews;
                            oResult.RowCount = lstNews.Count;
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

        #endregion
        #region News Category
        public static ModelResult<List<DTO.News.Categories>> CategoryGet(DTO.News.Categories oCategory)
        {
            var oResult = new ModelResult<List<DTO.News.Categories>>();
            var conn = new SqlConnection(DbConnection.ConnectionString);
            try
            {
                using (conn)
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        #region SQLCOMMAND Builder
                        var command = @"SELECT * FROM Categories WHERE 1=1 ";

                        if (oCategory.Id > 0)
                        {
                            command += " AND Id = @Id";
                            cmd.Parameters.AddWithValue("@Id", oCategory.Id);
                        }
                        if (!oCategory.IsList)
                        {
                            command += " order by @SortCol @SortType OFFSET(@Page - 1) * @RowsPerPage ROWS FETCH NEXT @RowsPerPage ROWS ONLY";
                            command = command.Replace("@SortCol", oCategory.SortCol);
                            command = command.Replace("@SortType", oCategory.SortType);
                            command = command.Replace("@Page", oCategory.Page.ToString());
                            command = command.Replace("@RowsPerPage", oCategory.RowPerPage.ToString());
                        }
                        #endregion
                        if (conn.State != ConnectionState.Open)
                            conn.Open();

                        cmd.CommandText = command;
                        SqlDataReader reader = cmd.ExecuteReader();
                        var lstCategory = new List<DTO.News.Categories>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                #region Category Params
                                var obCategory = new DTO.News.Categories();
                                obCategory.Id = Convert.ToInt32(reader["Id"]);
                                obCategory.NameAr = Convert.ToString(reader["NameAr"]);
                                if(reader["NameEn"] != DBNull.Value)
                                    obCategory.NameEn = Convert.ToString(reader["NameEn"]);
                                #endregion
                                lstCategory.Add(obCategory);
                            }
                        }
                        int count = 0;
                        if (!oCategory.IsList)
                        {
                            using (SqlConnection connCount = new SqlConnection(DbConnection.ConnectionString))
                            {
                                using (var cmdCount = new SqlCommand())
                                {
                                    cmdCount.Connection = connCount;
                                    command = @"SELECT COUNT(1) FROM Categories WHERE 1=1 ";
                                    if (oCategory.Id > 0)
                                    {
                                        command += " AND Id = @Id";
                                        cmdCount.Parameters.AddWithValue("@Id", oCategory.Id);
                                    }

                                    cmdCount.CommandText = command;
                                    if (connCount.State != ConnectionState.Open)
                                        connCount.Open();
                                    count = Convert.ToInt32(cmdCount.ExecuteScalar());
                                    connCount.Close();
                                }
                            }
                        }
                        if (lstCategory.Count > 0)
                        {
                            oResult.HasResult = true;
                            oResult.Results = lstCategory;
                            oResult.RowCount = count;
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
        public static ModelResult<DTO.News.Categories> CategorySave(DTO.News.Categories oCategory)
        {
            var oResult = new ModelResult<DTO.News.Categories>();
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
                        cmd.CommandText = "SP_CategorySave";

                        cmd.Parameters.AddWithValue("@Id", oCategory.Id);                        
                        cmd.Parameters.AddWithValue("@NameAr", oCategory.NameAr);
                        if (!string.IsNullOrEmpty(oCategory.NameEn))
                        {
                            cmd.Parameters.AddWithValue("@NameEn", oCategory.NameEn);
                        }

                        conn.Open();
                        oCategory.Id = Convert.ToInt32(cmd.ExecuteScalar());
                        oResult.HasResult = true;
                        oResult.Results = oCategory;
                    }
                }
            }
            finally
            {
                conn.Close();
            }
            return oResult;
        }
        public static ModelResult<int> CategoryDelete(int id)
        {
            int x;
            var oResult = new ModelResult<int>();
            using (var conn = new SqlConnection(DbConnection.ConnectionString))
            {

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SP_CategoryDelete";
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
        #endregion
        #region
        public static ModelResult<List<DTO.News.News>> NewsByTag(DTO.News.NewskeyWords oKeyword , int langId)
        {
            var oResult = new ModelResult<List<DTO.News.News>>();
            var conn = new SqlConnection(DbConnection.ConnectionString);
            try
            {
                using (conn)
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        #region SQLCOMMAND Builder
                        var command = @"SELECT TBL1.KeyWord,TBL2.*,TBL3.NameAr,TBL3.NameEn,TBL4.Name InsertedByName FROM NewskeyWords TBL1 
                                    LEFT JOIN News TBL2 ON TBL1.NewsId = TBL2.Id 
                                    JOIN Categories TBL3 ON TBL2.CategoryId = TBL3.Id
                                    JOIN UserAccounts TBL4 ON TBL2.InsertedBy = TBL4.Id
                                    WHERE 1=1 AND TBL2.IsDeleted = 0 ";
                        if (langId > 0)
                        {
                            command += " AND TBL2.LangId = @LangId";
                            cmd.Parameters.AddWithValue("@LangId", langId);
                        }
                        if (!string.IsNullOrEmpty(oKeyword.KeyWord))
                        {
                            command += " And TBL1.KeyWord Like @KeyWord ";
                            cmd.Parameters.AddWithValue("@KeyWord", "%" + oKeyword.KeyWord + "%");
                        }
                        if (!oKeyword.IsList)
                        {
                            command += " order by @SortCol @SortType OFFSET(@Page - 1) * @RowsPerPage ROWS FETCH NEXT @RowsPerPage ROWS ONLY";
                            command = command.Replace("@SortCol", oKeyword.SortCol);
                            command = command.Replace("@SortType", oKeyword.SortType);
                            command = command.Replace("@Page", oKeyword.Page.ToString());
                            command = command.Replace("@RowsPerPage", oKeyword.RowPerPage.ToString());
                        }
                        #endregion
                        if (conn.State != ConnectionState.Open)
                            conn.Open();

                        cmd.CommandText = command;
                        SqlDataReader reader = cmd.ExecuteReader();
                        var lstNews = new List<DTO.News.News>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                #region Constant Parms
                                var obNews = new DTO.News.News();
                                #region News Params
                                /*****************/
                                var oCategory = new DTO.News.Categories();
                                oCategory.Id = Convert.ToInt32(reader["Id"]);
                                oCategory.NameAr = Convert.ToString(reader["NameAr"]);
                                if (reader["NameEn"] != DBNull.Value)
                                    oCategory.NameEn = Convert.ToString(reader["NameEn"]);
                                obNews.OCategory = oCategory;
                                /******************/
                                var oInsertedBy = new DTO.Account.UserAccounts();
                                oInsertedBy.Name = Convert.ToString(reader["InsertedByName"]);
                                obNews.OInsertedBy = oInsertedBy;
                                /*************/
                                obNews.Id = Convert.ToInt32(reader["Id"]);
                                obNews.Title = Convert.ToString(reader["Title"]);
                                obNews.Summary = Convert.ToString(reader["Summary"]);
                                obNews.Details = Convert.ToString(reader["Details"]);
                                obNews.PublishDate = Convert.ToDateTime(reader["PublishDate"]);
                                obNews.InsertedDate = Convert.ToDateTime(reader["InsertedDate"]);
                                obNews.InsertedBy = Convert.ToInt32(reader["InsertedBy"]);
                                if (reader["UpdatedBy"] != DBNull.Value)
                                    obNews.UpdatedBy = Convert.ToInt32(reader["UpdatedBy"]);
                                if (reader["UpdatedDate"] != DBNull.Value)
                                    obNews.UpdatedDate = Convert.ToDateTime(reader["UpdatedDate"]);
                                obNews.ViewsCount = Convert.ToInt32(reader["ViewsCount"]);
                                obNews.Status = Convert.ToInt32(reader["Status"]);
                                if (reader["Keywords"] != DBNull.Value)
                                    obNews.Keywords = Convert.ToString(reader["Keywords"]);
                                obNews.LangId = Convert.ToInt32(reader["LangId"]);
                                obNews.Image = Convert.ToString(reader["Image"]);
                                obNews.IsActive = Convert.ToBoolean(reader["IsActive"]);
                                obNews.CategoryId = Convert.ToInt32(reader["CategoryId"]);
                                #endregion
                                #endregion
                                lstNews.Add(obNews);
                            }
                        }
                        int count = 0;
                        if (!oKeyword.IsList)
                        {
                            using (SqlConnection connCount = new SqlConnection(DbConnection.ConnectionString))
                            {
                                using (var cmdCount = new SqlCommand())
                                {
                                    cmdCount.Connection = connCount;
                                    command = @"SELECT COUNT(1) FROM NewskeyWords TBL1  LEFT JOIN News TBL2 ON TBL1.NewsId = TBL2.Id  WHERE TBL2.IsDeleted = 0";
                                    if (!string.IsNullOrEmpty(oKeyword.KeyWord))
                                    {
                                        command += " And TBL1.KeyWord Like @KeyWord ";
                                        cmdCount.Parameters.AddWithValue("@KeyWord", "%" + oKeyword.KeyWord + "%");
                                    }
                                    cmdCount.CommandText = command;
                                    if (connCount.State != ConnectionState.Open)
                                        connCount.Open();
                                    count = Convert.ToInt32(cmdCount.ExecuteScalar());
                                    connCount.Close();
                                }
                            }
                        }
                        if (lstNews.Count > 0)
                        {
                            oResult.HasResult = true;
                            oResult.Results = lstNews;
                            oResult.RowCount = count;
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
        #endregion
    }

}
