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
    public class Media : DbProcess
    {
        public Media()
        {
            OMediaType = new Constant();
        }
        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("Id")]
        public int Id { get; set; }

        [MaxLength(255)]
        [StringLength(255)]

        [DisplayName("الملف")]
        public string FilePath { get; set; }

        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("نوع الوسائط")]
        public int MediaType { get; set; }

        [MaxLength(255)]
        [StringLength(255)]
        [DisplayName("الوصف")]
        public string Caption { get; set; }

        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("بالرئيسية")]
        public bool IsInMainPage { get; set; }
        [DisplayName("الرابط")]
        public string ExternalLink { get; set; }
        public MediaExternalLink MediaExternalLink { get; set; }

        public Account.Constant OMediaType { get; set; }
    }
    public class MediaExternalLink
    {
        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("Media Id")]
        public int MediaId { get; set; }

        [MaxLength(300)]
        [StringLength(300)]
        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("الرابط")]
        public string Link { get; set; }

        public Media oMedia { get; set; }
    }

}
