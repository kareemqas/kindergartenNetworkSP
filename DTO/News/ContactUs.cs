using DTO.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO.News
{
   public class ContactUs : DbProcess
    {
        public int Id { get; set; }

        [MaxLength(100)]
        [StringLength(100)]
        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("الموضوع")]
        public string Subject { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("الإسم")]
        public string Name { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [EmailAddress(ErrorMessage = "<u><b>{0}</b></u>: الصيغة غير صحيحة")]
        [DisplayName("البريد الإلكتروني")]
        public string Email { get; set; }
        [MaxLength(1000)]
        [StringLength(1000)]
        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("الرسالة")]
        public string Message { get; set; }
        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("الرد")]
        public string Reply { get; set; }
        public bool IsAnswered { get; set; }
        public bool? IsRead { get; set; }
        public DateTime InsertedDate { get; set; }

    }
}
