using System.Collections.Generic;
using DTO.Common;
using System.ComponentModel.DataAnnotations;

namespace DTO.Account
{
    public class Pages : DbProcess
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        [StringLength(300)]
        public string Link { get; set; }
        public int? ParentId { get; set; }
        public int OrderId { get; set; }
        public bool? InMenu { get; set; }
        public bool ForAdmin { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        [Required]
        public int TypeId { get; set; }
        public string Icon { get; set; }
        public bool NeedLogin { get; set; }
        public Pages OParentPage { get; set; }
        public List<Pages> ChildPages { get; set; }
    }
}
