using DTO.Common;
using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using DTO.Account;

namespace DTO.News
{
    public class EducationalResources : DbProcess
    {
        public EducationalResources()
        {
            //OCategoryType = new Constant();
            OFileType = new Constant();
            OUserAccount = new UserAccounts();
        }
        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("Id")]
        public int Id { get; set; }

        [MaxLength(50)]
        [StringLength(50)]
        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("الملف")]
        public string FilePath { get; set; }

        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("Inserted By")]
        public int InsertedBy { get; set; }

        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("Inserted Date")]
        public DateTime InsertedDate { get; set; }

        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("نوع الملف")]
        public int FileType { get; set; }


        [MaxLength(50)]
        [StringLength(50)]
        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("عنوان الملف")]
        public string FileTitle { get; set; }
        [MaxLength(250)]
        [StringLength(250)]
        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("وصف الملف")]
        public string FileDescription { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        [DisplayName("صورة")]
        public string Image { get; set; }
        //[Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        //[DisplayName("تصنيف الملف")]
        //public int CategoryTypeId { get; set; }
        public UserAccounts OUserAccount { get; set; }
        public Constant OFileType { get; set; }
        //public Constant OCategoryType { get; set; }
    }


}
