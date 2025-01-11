using DTO.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.News
{
    public class SocialNW : DbProcess
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Link { get; set; }
        public string Icon { get; set; }
    }
    public class PagesHdr : DbProcess
    {

        public int Id { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
    }
}
