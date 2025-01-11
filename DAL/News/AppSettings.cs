using DTO.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL.News
{
    public class AppSettings
    {
        public static ModelResult<List<DTO.News.AppSettings>> AppSettingsGet(int key)
        {
            var oResult = new ModelResult<List<DTO.News.AppSettings>>();
            var conn = new SqlConnection(DbConnection.ConnectionString);
            try
            {
                using (conn)
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        #region SQLCOMMAND Builder
                        var command = @"SELECT TBL1.*,TBL2.Name KeyName,TBL3.Name ValueName FROM AppSettings TBL1 JOIN Constant TBL2 ON TBL1.ConKey = TBL2.Id JOIN Constant TBL3 ON TBL1.ConValue = TBL3.Id WHERE 1=1 ";
                        if (key > 0)
                        {
                            command += " AND TBL1.ConKey = @Key ";
                            cmd.Parameters.AddWithValue("@Key", key);
                        }
                        #endregion
                        if (conn.State != ConnectionState.Open)
                            conn.Open();

                        cmd.CommandText = command;
                        SqlDataReader reader = cmd.ExecuteReader();
                        var lstAppSettings = new List<DTO.News.AppSettings>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                var obAppSettings = new DTO.News.AppSettings();
                                obAppSettings.ConKey = Convert.ToInt32(reader["ConKey"]);
                                obAppSettings.ConValue = Convert.ToInt32(reader["ConValue"]);
                                var obKey = new DTO.Account.Constant();
                                obKey.Name = Convert.ToString(reader["KeyName"]);
                                obAppSettings.OKey = obKey;

                                var obValue = new DTO.Account.Constant();
                                obValue.Name = Convert.ToString(reader["ValueName"]);
                                obAppSettings.OValue = obValue;

                                lstAppSettings.Add(obAppSettings);
                            }
                        }
                        if (lstAppSettings.Count > 0)
                        {
                            oResult.HasResult = true;
                            oResult.Results = lstAppSettings;
                            oResult.RowCount = lstAppSettings.Count;
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
        public static ModelResult<DTO.News.AppSettings> AppSettingsSave(DTO.News.AppSettings oAppSetting)
        {
            var oResult = new ModelResult<DTO.News.AppSettings>();
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
                        cmd.CommandText = "SP_AppSettingsSave";
                        cmd.Parameters.AddWithValue("@Value", oAppSetting.ConValue);
                        cmd.Parameters.AddWithValue("@Key", oAppSetting.ConKey);
                        conn.Open();
                        oAppSetting.ConKey = Convert.ToInt32(cmd.ExecuteNonQuery());
                        oResult.HasResult = true;
                        oResult.Results = oAppSetting;
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
