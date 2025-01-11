using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Common;

namespace DTO.Account
{
    public class TraceUserActivity :DbProcess
    {
        public TraceUserActivity()
        {
            OUser = new UserAccounts();
        }
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Action { get; set; }
        public int ElapsedTime { get; set; }
        public bool IsDeleted { get; set; }
        public string IpAddress { get; set; }
        public string Browser { get; set; }
        public string UserAgent { get; set; }
        public DateTime? OccurDate { get; set; }
        public DTO.Account.UserAccounts OUser { get; set; }
    }
}
