using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Common;

namespace DTO.News
{
    public class NewsletterSubscribers: DbProcess
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public bool? IsActive { get; set; }
    }
}
