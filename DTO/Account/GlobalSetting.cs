using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Common;

namespace DTO.Account
{
    public class GlobalSetting : DbProcess
    {
        public int ConId { get; set; }
        [Required]
        public string value { get; set; }
        public bool IsDeleted { get; set; }
        public string Name { get; set; }
        public Constant OConstant { get; set; }
        public List<Constant> LsConstant { get; set; }
    }
}
