using DTO.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Account;
using DTO.News;
using System.ComponentModel;

namespace DTO.News
{
    public class News : DbProcess
    {
        public News()
        {
            OInsertedBy = new UserAccounts();
            OUpdatedBy = new UserAccounts();
            OCategory = new Categories();
            LstNewskeyWords = new List<NewskeyWords>();
        }

        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("Id")]
        public int Id { get; set; }

        [MaxLength(200)]
        [StringLength(200)]
        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("العنوان")]
        public string Title { get; set; }

        [MaxLength(500)]
        [StringLength(500)]
        [DisplayName("الملخص")]
        public string Summary { get; set; }

        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("الوصف")]
        public string Details { get; set; }
        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("تاريخ الإدخال")]
        public DateTime PublishDate { get; set; }
        public DateTime InsertedDate { get; set; }

        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("Inserted By")]
        public int InsertedBy { get; set; }

        [DisplayName("Updated By")]
        public int? UpdatedBy { get; set; }

        [DisplayName("Updated Date")]
        public DateTime? UpdatedDate { get; set; }

        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("عدد المشاهدات")]
        public int ViewsCount { get; set; }

        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("الحالة")]
        public int Status { get; set; }

        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("اللغة")]
        public int LangId { get; set; }

        [MaxLength(255)]
        [StringLength(255)]
        [DisplayName("الصورة")]
        public string Image { get; set; }

        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("التصنيف")]
        public int CategoryId { get; set; }
        [DisplayName("الأوسمة")]
        public string Keywords { get; set; }
        public bool IsActive { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = @"{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? FromDate { get; set; }
        [DataType(DataType.Date), DisplayFormat(DataFormatString = @"{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? ToDate { get; set; }

        public bool IsArticle { get; set; }
        public UserAccounts OInsertedBy { get; set; }

        public UserAccounts OUpdatedBy { get; set; }

        public Categories OCategory { get; set; }

        public List<NewskeyWords> LstNewskeyWords { get; set; }

    }

    public class Categories :DbProcess
    {
        public Categories()
        {
            News = new List<News>();
        }
        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("Id")]
        public int Id { get; set; }

        [MaxLength(255)]
        [StringLength(255)]
        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("الاسم")]
        public string NameAr { get; set; }

        [MaxLength(255)]
        [StringLength(255)]
        //[Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("الاسم انجليزي")]
        public string NameEn { get; set; }
        public List<News> News { get; set; }
    }



    public class NewskeyWords : DbProcess
    {
        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("Id")]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("Key Word")]
        public string KeyWord { get; set; }

        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("News Id")]
        public int NewsId { get; set; }

        public News News { get; set; }

    }



}
