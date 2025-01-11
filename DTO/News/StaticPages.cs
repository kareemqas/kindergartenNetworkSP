using DTO.Common;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DTO.News
{ 
    public class StaticPages :DbProcess
    {

        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("Id")]
        public int Id { get; set; }

        [MaxLength(150)]
        [StringLength(150)]
        //[Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("الاسم")]
        public string PageName { get; set; }

        [MaxLength(500)]
        [StringLength(500)]
        //[Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("العنوان")]
        public string Title { get; set; }

        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("الوصف")]
        public string Description { get; set; }

        [MaxLength(255)]
        [StringLength(255)]
        [DisplayName("الصورة")]
        public string Image { get; set; }

        [MaxLength(255)]
        [StringLength(255)]
        [DisplayName("الصورة 2")]
        public string Image2 { get; set; }

        [MaxLength(255)]
        [StringLength(255)]
        [DisplayName("الصورة 3")]
        public string Image3 { get; set; }

        [MaxLength(50)]
        [StringLength(50)]
        [DisplayName("جوال")]
        public string Mobile { get; set; }

        [MaxLength(50)]
        [StringLength(50)]
        [DisplayName("هاتف")]
        public string Phone { get; set; }

        [MaxLength(50)]
        [StringLength(50)]
        [DisplayName("ايميل 1")]
        public string Email1 { get; set; }

        [MaxLength(50)]
        [StringLength(50)]
        [DisplayName("ايميل 2")]
        public string Email2 { get; set; }

        [DisplayName("Updated By")]
        public int? UpdatedBy { get; set; }

        [DisplayName("Updated Date")]
        public DateTime? UpdatedDate { get; set; }
        public bool IsImageView { get; set; }
        public Account.UserAccounts OUpdatedBy { get; set; }
    }

    public class StaticData : DbProcess
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Data { get; set; }
        public bool Status { get; set; }
        public string Icon { get; set; }
        public int Type { get; set; }
    }
}
