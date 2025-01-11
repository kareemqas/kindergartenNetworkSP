using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace DAL
{
    public class DbConnection
    {
        public static string ConnectionString = System.IO.File.ReadAllText(HttpContext.Current.Server.MapPath("~/bin/db.txt"));
    }
}
