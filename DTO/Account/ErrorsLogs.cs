using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.Account
{
   public class ErrorsLogs : Common.DbProcess
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
        public DateTime ErrorDate { get; set; }
        public bool IsAjax { get; set; }
        public int UserId { get; set; }
        public UserAccounts ObUserAccount { get; set; }

    }
}
