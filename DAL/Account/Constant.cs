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
    public class Constant
    {
        public static ModelResult<List<DTO.Account.Constant>> ConstantGet(DTO.Account.Constant oConstant)
        {
            var oResult = new ModelResult<List<DTO.Account.Constant>>();
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
                        command = @"Select TBL1.* , 
                                    TBL2.Name Parm1, TBL2.Comment Parm2
                                    FROM Constant TBL1
                                    LEFT JOIN Constant TBL2 ON (TBL1.ParentId = TBL2.Id)
                                    WHERE 1=1 AND TBL1.IsDeleted = 0";
                        
                        if (oConstant.Id > 0)
                        {
                            command += " AND TBL1.Id = @Id";
                            cmd.Parameters.AddWithValue("@Id", oConstant.Id);
                        }
                        if (oConstant.ParentId >= 0)
                        {
                            command += " And TBL1.ParentId =@ParentId ";
                            cmd.Parameters.AddWithValue("@ParentId", oConstant.ParentId);
                        }
                        if (!string.IsNullOrEmpty(oConstant.Name))
                        {
                            command += " And TBL1.Name Like @Name ";
                            cmd.Parameters.AddWithValue("@Name", "%" + oConstant.Name + "%");
                        }
                        if (!oConstant.IsList)
                        {
                            command += " order by @SortCol @SortType OFFSET(@Page - 1) * @RowsPerPage ROWS FETCH NEXT @RowsPerPage ROWS ONLY";
                            command = command.Replace("@SortCol", oConstant.SortCol);
                            command = command.Replace("@SortType", oConstant.SortType);
                            command = command.Replace("@Page", oConstant.Page.ToString());
                            command = command.Replace("@RowsPerPage", oConstant.RowPerPage.ToString());
                        }
                        #endregion
                        if (conn.State != ConnectionState.Open)
                            conn.Open();

                        cmd.CommandText = command;
                        SqlDataReader reader = cmd.ExecuteReader();
                        var lstConstant = new List<DTO.Account.Constant>();
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                #region Constant Parms
                                var obConstant = new DTO.Account.Constant();
                                
                                if(reader["Name"] != DBNull.Value)
                                    obConstant.Name = Convert.ToString(reader["Name"]);
                                if (reader["Comment"] != DBNull.Value)
                                    obConstant.Comment = Convert.ToString(reader["Comment"]);
                                #region Parent Constant Content Parms
                                var obParent = new DTO.Account.Constant();
                                if (reader["Parm1"] != DBNull.Value)
                                    obParent.Name = Convert.ToString(reader["Parm1"]);
                                obConstant.OParent = obParent;
                                #endregion
                                obConstant.Id = Convert.ToInt32(reader["Id"]);
                                if (reader["ParentId"] != DBNull.Value)
                                    obConstant.ParentId = Convert.ToInt32(reader["ParentId"]);
                                if (reader["Icon"] != DBNull.Value)
                                    obConstant.Icon = Convert.ToString(reader["Icon"]);
                                if (reader["IsDeleted"] != DBNull.Value)
                                    obConstant.IsDeleted = Convert.ToBoolean(reader["IsDeleted"]);
                                #endregion
                                lstConstant.Add(obConstant);
                            }
                        }
                        int count = 0;
                        if (!oConstant.IsList)
                        {
                            using (SqlConnection connCount = new SqlConnection(DbConnection.ConnectionString))
                            {
                                using (var cmdCount = new SqlCommand())
                                {
                                    cmdCount.Connection = connCount;
                                    command = @"SELECT COUNT(1) FROM Constant WHERE 1=1 AND IsDeleted = 0";
                                    if (oConstant.Id > 0)
                                    {
                                        command += " AND Id = @Id";
                                        cmdCount.Parameters.AddWithValue("@Id", oConstant.Id);
                                    }
                                    if (oConstant.ParentId >= 0)
                                    {
                                        command += " And ParentId =@ParentId ";
                                        cmdCount.Parameters.AddWithValue("@ParentId", oConstant.ParentId);
                                    }
                                    cmdCount.CommandText = command;
                                    if (connCount.State != ConnectionState.Open)
                                        connCount.Open();
                                    count = Convert.ToInt32(cmdCount.ExecuteScalar());
                                    connCount.Close();
                                }
                            }
                        }
                        if (lstConstant.Count > 0)
                        {
                            oResult.HasResult = true;
                            oResult.Results = lstConstant;
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
        public static ModelResult<DTO.Account.Constant> ConstantInsert(DTO.Account.Constant oConstant)
        {
            var oResult = new ModelResult<DTO.Account.Constant>();
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
                        cmd.CommandText = "SP_ConstantInsert";
                        if (oConstant.ParentId >= 0)
                        {
                            cmd.Parameters.AddWithValue("@ParentId", oConstant.ParentId);
                        }
                        if (!string.IsNullOrEmpty(oConstant.Icon))
                        {
                            cmd.Parameters.AddWithValue("@Icon", oConstant.Icon);
                        }
                        if (!string.IsNullOrEmpty(oConstant.Name))
                        {
                            cmd.Parameters.AddWithValue("@Name", oConstant.Name);
                        }
                        if (!string.IsNullOrEmpty(oConstant.Comment))
                        {
                            cmd.Parameters.AddWithValue("@Comment", oConstant.Comment);
                        }
                        conn.Open();
                        oConstant.Id = Convert.ToInt32(cmd.ExecuteScalar());
                        oResult.HasResult = true;
                        oResult.Results = oConstant;
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
        public static ModelResult<DTO.Account.Constant> ConstantUpdate(DTO.Account.Constant oConstant)
        {
            var oResult = new ModelResult<DTO.Account.Constant>();
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
                        cmd.CommandText = "SP_ConstantUpdate";
                        if (oConstant.Id > 0)
                        {
                            cmd.Parameters.AddWithValue("@Id", oConstant.Id);
                        }
                        if (oConstant.ParentId >= 0)
                        {
                            cmd.Parameters.AddWithValue("@ParentId", oConstant.ParentId);
                        }
                        if (!string.IsNullOrEmpty(oConstant.Icon))
                        {
                            cmd.Parameters.AddWithValue("@Icon", oConstant.Icon);
                        }
                        if (!string.IsNullOrEmpty(oConstant.Name))
                        {
                            cmd.Parameters.AddWithValue("@Name", oConstant.Name);
                        }
                        if (!string.IsNullOrEmpty(oConstant.Comment))
                        {
                            cmd.Parameters.AddWithValue("@Comment", oConstant.Comment);
                        }

                        conn.Open();
                        oConstant.Id = Convert.ToInt32(cmd.ExecuteScalar());
                        oResult.HasResult = true;
                        oResult.Results = oConstant;
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
        public static ModelResult<DTO.Account.Constant> DeleteConstant(DTO.Account.Constant oConstant)
        {
            var oResult = new ModelResult<DTO.Account.Constant>();
            using (var conn = new SqlConnection(DbConnection.ConnectionString))
            {

                using (var cmd = new System.Data.SqlClient.SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "SP_ConstantDelete";
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@Id", oConstant.Id);
                    conn.Open();

                    oConstant.Id = Convert.ToInt32(cmd.ExecuteNonQuery());
                    oResult.HasResult = true;
                    oResult.Results = oConstant;

                }

                return oResult;
            }
        }
    }
}
