using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Web;

namespace kindergartenNetwork.Models
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
            if (Roles.Any(r => role.Contains(r)))
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        public CustomPrincipal(string username)
        {
            Identity = new GenericIdentity(username);
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string EmailPassword { get; set; }
        public string Mobile { get; set; }
        public string Gender { get; set; }
        public string Avatar { get; set; }
        public int UserTypeId { get; set; }
        public int ManagerGroupId { get; set; }
        public int BranchId { get; set; }
        public string UserTypeName { get; set; }
        public string[] Roles { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool TraceUserActivity { get; set; }
        public int BenId { get; set; }
        public int BenTypeId { get; set; }
    }

    public class CustomPrincipalSerializeModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string EmailPassword { get; set; }
        public string Mobile { get; set; }
        public string Gender { get; set; }
        public string Avatar { get; set; }
        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; }
        public int ManagerGroupId { get; set; }
        public string[] Roles { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public bool TraceUserActivity { get; set; }
    }
}