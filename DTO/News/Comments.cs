using DTO.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Account;

namespace DTO.News
{
   public class Comments : DbProcess
    {
        public Comments()
        {
            OApprovedBy = new UserAccounts();
            OArticle = new News();
        }
        public int Id { get; set; }

        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("رقم المقال")]
        public int ArticleId { get; set; }

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
        [DisplayName("التعليق")]
        public string Comment { get; set; }
        public bool? IsApproved { get; set; }
        public int ApprovedBy { get; set; }
        public DateTime Date { get; set; }

        public UserAccounts OApprovedBy { get; set; }
        public News OArticle { get; set; }
    }
}
