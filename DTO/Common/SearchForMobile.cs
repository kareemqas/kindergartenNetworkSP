using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DTO.Common
{
    public class SearchForMobile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Con_Type { get; set; }
        public string Mobile { get; set; }
        public string Icon { get; set; }
        public List<object> MobileList { get; set; }
    }
}
