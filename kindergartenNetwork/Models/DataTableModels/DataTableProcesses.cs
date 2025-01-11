using System;

namespace kindergartenNetwork.Models.DataTableModels
{
    public class DataTableProcesses
    {
        public static string TabloSiraColumnGetir(JQueryDataTableParamModel param)
        {
            string lcSiraCol = "1";
            try
            {
                lcSiraCol = ObjectProcess.DataTableParamDegeriGetir(param, "mDataProp_" + param.iSortCol_0);
            }
            catch (Exception ex)
            {
                var message = ex.Message;
            }
            return lcSiraCol;
        }

        public static DataTableProcessModel DataTableEslestir(JQueryDataTableParamModel param, DataTableProcessModel Param)
        {
            Param.Search = SqlDuzelt(param.sSearch);
            Param.SortCol = SqlDuzelt(DataTableProcesses.TabloSiraColumnGetir(param));
            Param.Page = param.iDisplayStart / param.iDisplayLength + 1;
            Param.RowPerPage = param.iDisplayLength;
            Param.SortType = SqlDuzelt(param.sSortDir_0);
            return Param;
        }

        public static string SqlDuzelt(string parametre)
        {
            string sonuc = "";
            if (!string.IsNullOrEmpty(parametre))
            {
                sonuc = parametre.Replace("'", "''");
                sonuc = sonuc.Replace("Insert", "''");
                sonuc = sonuc.Replace("Update", "''");
                sonuc = sonuc.Replace("Delete", "''");
                sonuc = sonuc.Replace("Drop", "''");
                sonuc = sonuc.Replace("sp_", "''");
                sonuc = sonuc.Replace("xp_", "''"); 
                // xp_cmdshell ve xp_grantlogin sonuc = sonuc.Replace(";", "''"); sonuc = sonuc.Replace("--", "''"); sonuc = sonuc.Replace("\"", ""); sonuc = sonuc.Replace("&", ""); sonuc = sonuc.Replace(";", "");

            }
            return sonuc;
        }

        public static string SqlDuzeltOzel(string parametre)
        {
            string sonuc = "";
            if (!string.IsNullOrEmpty(parametre))
            {
                sonuc = parametre;
                sonuc = sonuc.Replace("Insert", "''");
                sonuc = sonuc.Replace("Update", "''");
                sonuc = sonuc.Replace("Delete", "''");
                sonuc = sonuc.Replace("Drop", "''");
                sonuc = sonuc.Replace("sp_", "''");
                sonuc = sonuc.Replace("xp_", "''"); 
                // xp_cmdshell ve xp_grantlogin sonuc = sonuc.Replace(";", "''"); sonuc = sonuc.Replace("--", "''"); sonuc = sonuc.Replace("\"", ""); sonuc = sonuc.Replace("&", ""); sonuc = sonuc.Replace(";", "");

            }
            return sonuc;
        }


    }
}