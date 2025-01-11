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
    public class ImportantLinks : DbProcess
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("الصورة")]
        public string Image { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("الاسم")]
        public string Name { get; set; }
        [MaxLength(50)]
        [StringLength(50)]
        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("الرابط")]
        public string Link { get; set; }

        [Required(ErrorMessage = "<u><b>{0}</b></u>: هذا الحقل إلزامي")]
        [DisplayName("فعال")]
        public bool IsActive { get; set; }

    }
}
