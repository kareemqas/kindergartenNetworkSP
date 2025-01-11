using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DTO.Common;

namespace DTO.News
{
    public class Visitors : DbProcess
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Pass { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string ResetPassToken { get; set; }
        public bool? IsApproved { get; set; }
    }

    public class VisitorProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        [Required]
        public string CurrentPassword { get; set; }
        [Required]
        public string NewPassword { get; set; }
        [Required]
        public string ConfirmPassword { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string ResetPassToken { get; set; }
        public int tryNo { get; set; }
        
      
    }
}

