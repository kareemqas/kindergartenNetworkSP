using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO.Common;

namespace DTO.News
{
    public class TeamMembers : DbProcess
    {
        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("Id")]
        public int Id { get; set; }

        [MaxLength(150)]
        [StringLength(150)]
        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("الاسم")]
        public string Name { get; set; }

        [MaxLength(150)]
        [StringLength(150)]
        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("المسمى الوظيفي")]
        public string JobTitle { get; set; }

        [MaxLength(500)]
        [StringLength(500)]
        [DisplayName("رابط لينكد ان")]
        public string LinkedInUrl { get; set; }

        [MaxLength(500)]
        [StringLength(500)]
        [DisplayName("رابط الفيس بوك")]
        public string FaceBookUrl { get; set; }

        [MaxLength(500)]
        [StringLength(500)]
        [DisplayName("رابط انستغرام")]
        public string InstgramUrl { get; set; }

        [MaxLength(500)]
        [StringLength(500)]
        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("الصورة")]
        public string Avatar { get; set; }

        public bool IsWithUs { get; set; }
        public bool IsDeleted { get; set; }
    }
}
