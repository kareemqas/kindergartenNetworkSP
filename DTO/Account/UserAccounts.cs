using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DTO.Common;

namespace DTO.Account
{
    public class UserAccounts : DbProcess
    {
        [Key]
        [Required]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Pass { get; set; }
        [EmailAddress]
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string Gender { get; set; }
        public string Avatar { get; set; }
        //[Required]
        public int? UserTypeId { get; set; }
        public bool? IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public string EmailPassword { get; set; }
        public int ManagerGroupId { get; set; }
        public  bool TraceUserActivity { get; set; }
        public UserType OUserType { get; set; }
        public List<UserType> LsUserType { get; set; }
    }

    public class UserProfile
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
        public string Mobile { get; set; }
        public string Gender { get; set; }
        public string Avatar { get; set; }
        public int tryNo { get; set; }
        
      
    }
}

