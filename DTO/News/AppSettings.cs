using DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.News
{
    public class AppSettings
    {
        public int ConKey { get; set; }
        public int ConValue { get; set; }
        public Account.Constant OKey { get; set; }
        public Account.Constant OValue { get; set; }
    }
}
