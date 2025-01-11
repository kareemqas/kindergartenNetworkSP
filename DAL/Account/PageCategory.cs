using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Common;

namespace DAL.Account
{
    public class PageCategory
    {
        public static ModelResult<List<DTO.Account.PagesCategory>> PagesCategoryGet(DTO.Account.PagesCategory oPagesCategory)
        {
            var oResult = new ModelResult<List<DTO.Account.PagesCategory>>();
            var conn = new SqlConnection(DbConnection.ConnectionString);
            try
            {
                using (conn)
                {
                    using (var cmd = new SqlCommand())
                    {
                        cmd.Connection = conn;
                        #region SQLCOMMAND
                        var command = "";
                        command = @"SELECT TBL1.* 
 
										FROM dbo.PagesCategory TBL1 
										WHERE 1=1";
                        #endregion
                        if (oPagesCategory.Id > 0)
                        {
                            command += " AND TBL1.Id = @Id";
                            cmd.Parameters.AddWithValue("@Id", oPagesCategory.Id);
                        }
                        cmd.CommandText = command;
                        if (conn.State != ConnectionState.Open)
                            conn.Open();
                        SqlDataReader reader = cmd.ExecuteReader();
                        var lstPagesCategory = new List<DTO.Account.PagesCategory>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                #region PagesCategory Parms
                                var obPagesCategory = new DTO.Account.PagesCategory();
                                obPagesCategory.Id = Convert.ToInt32(reader["Id"]);
                                if (reader["Name"] != DBNull.Value)
                                    obPagesCategory.Name = Convert.ToString(reader["Name"]);
                                if (reader["IsDeleted"] != DBNull.Value)
                                    obPagesCategory.IsDeleted = Convert.ToBoolean(reader["IsDeleted"]);
                                #endregion
                                lstPagesCategory.Add(obPagesCategory);
                            }
                        }
                        if (lstPagesCategory.Count > 0)
                        {
                            oResult.HasResult = true;
                            oResult.Results = lstPagesCategory;
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
    }
}
