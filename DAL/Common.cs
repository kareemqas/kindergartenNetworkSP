using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class Common
    {
        public static string Md5(string sPassword)
        {
            System.Security.Cryptography.MD5CryptoServiceProvider x =
                new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(sPassword);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToLower());
            }
            return s.ToString();
        }

        /*
        public static void BackupDatabase(string fileFullPath)
        {
            try
            {
                using (var conn = new SqlConnection(DbConnection.ConnectionString))
                {
                    using (var cmd = new SqlCommand())
                    {

                        cmd.Connection = conn;
                        cmd.CommandText = "SP_BackupDatabase";
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@FileFullPath", fileFullPath);
                        conn.Open();

                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }
        public static void RestoreDatabase(string fileFullPath)
        {
            try
            {
                using (var conn = new SqlConnection(DbConnection.ConnectionString))
                {
                    using (var cmd = new SqlCommand())
                    {

                        cmd.Connection = conn;
                        var command = @"ALTER DATABASE Osol SET SINGLE USER WITH ROLLBACK IMMEDIATE; 
                                        RESTORE DATABASE Osol FROM DISK='" + fileFullPath + "' WITH REPLACE;";
                        cmd.CommandText = command;
                        conn.Open();

                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }
        */

        public static void BackupDatabase(string fileFullPath)
        {
            //try
            //{
            using (var conn = new SqlConnection(DbConnection.ConnectionString))
            {
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = conn;
                    cmd.CommandText = "BACKUP DATABASE " + conn.Database + " TO DISK='" + fileFullPath + "'";
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            //}
            //catch (Exception ex)
            //{
            //    var message = ex.Message;
            //}
        }
        public static void RestoreDatabase(string fileFullPath)
        {
            try
            {
                using (var conn = new SqlConnection(DbConnection.ConnectionString))
                {
                    using (var cmd = new SqlCommand())
                    {

                        cmd.Connection = conn;
                        var command = @"ALTER DATABASE " + conn.Database + @" SET SINGLE USER WITH ROLLBACK IMMEDIATE; 
                                        RESTORE DATABASE " + conn.Database + " FROM DISK='" + fileFullPath +
                                      "' WITH REPLACE;";
                        cmd.CommandText = command;
                        conn.Open();

                        cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
        }
    }
}
