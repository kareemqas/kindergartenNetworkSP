using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DTO.Common;

namespace DAL.News
{
    public class Media
    {
        #region Media
        public static ModelResult<List<DTO.News.Media>> MediaGet(DTO.News.Media oMedia)
        {
            var oResult = new ModelResult<List<DTO.News.Media>>();
            var conn = new SqlConnection(DbConnection.ConnectionString);
            try
            {
                using (conn)
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        #region SQLCOMMAND Builder
                        var command = @"SELECT TBL1.* ,TBL3.NameEn TypeNameEn, TBL3.Name TypeName  FROM Media TBL1 
                                    JOIN Constant TBL3 ON TBL3.Id = TBL1.MediaType
                                    WHERE TBL1.IsDeleted = 0 ";

                        if (oMedia.Id > 0)
                        {
                            command += " AND TBL1.Id = @Id";
                            cmd.Parameters.AddWithValue("@Id", oMedia.Id);
                        }
                        if (oMedia.MediaType > 0)
                        {
                            command += " AND TBL1.MediaType = @MediaType";
                            cmd.Parameters.AddWithValue("@MediaType", oMedia.MediaType);
                        }

                        if (!oMedia.IsList)
                        {
                            command += " order by @SortCol @SortType OFFSET(@Page - 1) * @RowsPerPage ROWS FETCH NEXT @RowsPerPage ROWS ONLY";
                            command = command.Replace("@SortCol", oMedia.SortCol);
                            command = command.Replace("@SortType", oMedia.SortType);
                            command = command.Replace("@Page", oMedia.Page.ToString());
                            command = command.Replace("@RowsPerPage", oMedia.RowPerPage.ToString());
                        }
                        #endregion
                        if (conn.State != ConnectionState.Open)
                            conn.Open();

                        cmd.CommandText = command;
                        SqlDataReader reader = cmd.ExecuteReader();
                        var lstMedia = new List<DTO.News.Media>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var obMedia = new DTO.News.Media();


                                obMedia.Id = Convert.ToInt32(reader["Id"]);
                                obMedia.MediaType = Convert.ToInt32(reader["MediaType"]);
                                if (reader["TypeName"] != DBNull.Value)
                                    obMedia.OMediaType.Name = Convert.ToString(reader["TypeName"]);
                                if(reader["Caption"] != DBNull.Value)
                                    obMedia.Caption = Convert.ToString(reader["Caption"]);
                                obMedia.FilePath = Convert.ToString(reader["FilePath"]);
                                obMedia.ExternalLink = Convert.ToString(reader["ExternalLink"]);
                                obMedia.IsInMainPage = Convert.ToBoolean(reader["IsInMainPage"]);

                                lstMedia.Add(obMedia);
                            }
                        }
                        int count = 0;
                        if (!oMedia.IsList)
                        {
                            using (SqlConnection connCount = new SqlConnection(DbConnection.ConnectionString))
                            {
                                using (var cmdCount = new SqlCommand())
                                {
                                    cmdCount.Connection = connCount;
                                    command = @"SELECT COUNT(1) FROM Media WHERE 1=1 AND IsDeleted = 0";
                                    if (oMedia.Id > 0)
                                    {
                                        command += " AND Id = @Id";
                                        cmdCount.Parameters.AddWithValue("@Id", oMedia.Id);
                                    }
                                    if (oMedia.MediaType > 0)
                                    {
                                        command += " AND MediaType = @MediaType";
                                        cmdCount.Parameters.AddWithValue("@MediaType", oMedia.MediaType);
                                    }
                                    cmdCount.CommandText = command;
                                    if (connCount.State != ConnectionState.Open)
                                        connCount.Open();
                                    count = Convert.ToInt32(cmdCount.ExecuteScalar());
                                    connCount.Close();
                                }
                            }
                        }
                        if (lstMedia.Count > 0)
                        {
                            oResult.HasResult = true;
                            oResult.Results = lstMedia;
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
        public static ModelResult<DTO.News.Media> MediaSave(DTO.News.Media oMedia)
        {
            var oResult = new ModelResult<DTO.News.Media>();
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
                        cmd.CommandText = "SP_MediaSave";
                        if (oMedia.Id >= 0)
                        {
                            cmd.Parameters.AddWithValue("@Id", oMedia.Id);
                        }

                        if (!string.IsNullOrEmpty(oMedia.Caption))
                        {
                            cmd.Parameters.AddWithValue("@Caption", oMedia.Caption);
                        }

                        cmd.Parameters.AddWithValue("@MediaType", oMedia.MediaType);
                        if (!string.IsNullOrEmpty(oMedia.FilePath))
                            cmd.Parameters.AddWithValue("@FilePath", oMedia.FilePath);
                        cmd.Parameters.AddWithValue("@IsInMainPage", oMedia.IsInMainPage);
                        if (!string.IsNullOrEmpty(oMedia.ExternalLink))
                            cmd.Parameters.AddWithValue("@ExternalLink", oMedia.ExternalLink);

                        conn.Open();
                        oMedia.Id = Convert.ToInt32(cmd.ExecuteScalar());
                        oResult.HasResult = true;
                        oResult.Results = oMedia;
                    }
                }
            }
            catch (Exception ex)
            {
                oResult.Message = ex.Message;
            }
            finally
            {
                conn.Close();
            }
            return oResult;
        }
        public static ModelResult<int> MediaDelete(int id)
        {
            int x;
            var oResult = new ModelResult<int>();
            using (var conn = new SqlConnection(DbConnection.ConnectionString))
            {

                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SP_MediaDelete";
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
        public static ModelResult<List<DTO.News.Media>> MediaInMainPageGet(DTO.News.Media oMedia)
        {
            var oResult = new ModelResult<List<DTO.News.Media>>();
            var conn = new SqlConnection(DbConnection.ConnectionString);
            try
            {
                using (conn)
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        #region SQLCOMMAND Builder
                        var command = @"SELECT TBL1.*, TBL3.NameEn TypeNameEn,TBL3.NameAr TypeNameAr 
                                    FROM Media TBL1 
                                    JOIN Constant TBL3 ON TBL3.Id = TBL1.MediaType
                                    WHERE TBL1.IsDeleted = 0 AND TBL1.IsInMainPage = 1";
                        if(oMedia.MediaType > 0)
                        {
                            command += " AND TBL1.MediaType = @MediaType";
                            cmd.Parameters.AddWithValue("@MediaType", oMedia.MediaType);
                        }
                        if (!oMedia.IsList)
                        {
                            command += " order by @SortCol @SortType OFFSET(@Page - 1) * @RowsPerPage ROWS FETCH NEXT @RowsPerPage ROWS ONLY";
                            command = command.Replace("@SortCol", oMedia.SortCol);
                            command = command.Replace("@SortType", oMedia.SortType);
                            command = command.Replace("@Page", oMedia.Page.ToString());
                            command = command.Replace("@RowsPerPage", oMedia.RowPerPage.ToString());
                        }
                        #endregion
                        if (conn.State != ConnectionState.Open)
                            conn.Open();

                        cmd.CommandText = command;
                        SqlDataReader reader = cmd.ExecuteReader();
                        var lstMedia = new List<DTO.News.Media>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var obMedia = new DTO.News.Media();
                                obMedia.MediaType = Convert.ToInt32(reader["MediaType"]);
                                if (reader["TypeName"] != DBNull.Value)
                                    obMedia.OMediaType.Name = Convert.ToString(reader["TypeName"]);
                                if (reader["Caption"] != DBNull.Value)
                                    obMedia.Caption = Convert.ToString(reader["Caption"]);
                                obMedia.FilePath = Convert.ToString(reader["FilePath"]);
                                obMedia.ExternalLink = Convert.ToString(reader["ExternalLink"]);
                                obMedia.IsInMainPage = Convert.ToBoolean(reader["IsInMainPage"]);

                                lstMedia.Add(obMedia);
                            }
                        }
                        int count = 0;
                        if (!oMedia.IsList)
                        {
                            using (SqlConnection connCount = new SqlConnection(DbConnection.ConnectionString))
                            {
                                using (var cmdCount = new SqlCommand())
                                {
                                    cmdCount.Connection = connCount;
                                    command = @"SELECT COUNT(1) FROM Media WHERE 1=1 AND IsDeleted = 0";
                                    if (oMedia.Id > 0)
                                    {
                                        command += " AND Id = @Id";
                                        cmdCount.Parameters.AddWithValue("@Id", oMedia.Id);
                                    }
                                    if (oMedia.MediaType > 0)
                                    {
                                        command += " AND MediaType = @MediaType";
                                        cmdCount.Parameters.AddWithValue("@MediaType", oMedia.MediaType);
                                    }
                                    cmdCount.CommandText = command;
                                    if (connCount.State != ConnectionState.Open)
                                        connCount.Open();
                                    count = Convert.ToInt32(cmdCount.ExecuteScalar());
                                    connCount.Close();
                                }
                            }
                        }
                        if (lstMedia.Count > 0)
                        {
                            oResult.HasResult = true;
                            oResult.Results = lstMedia;
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
