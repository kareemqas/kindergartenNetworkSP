using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Common;

namespace DTO.Account
{
    public class ErrorsRepository : DbProcess
    {
        public int Id { get; set; }
        public string ErrorMessage { get; set; }
        public string Link { get; set; }
        public string IP { get; set; }
        public string Browser { get; set; }
        public string UserAgent { get; set; }
        public string RequestType { get; set; }
        public string PostedData { get; set; }
        public bool IsSolved { get; set; }
        public bool IsAjax { get; set; }
        public DateTime ErrorTime { get; set; }
    }
}
