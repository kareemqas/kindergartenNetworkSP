using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTO.Account;

namespace kindergartenNetwork.Models.MainModels
{
    public class UserPermissionModel
    {
        public UserPermissionModel()
        {
            LstUserTypes = new List<UserType>();
            LstPages = new List<Pages>();
        }
        public List<UserType> LstUserTypes { get; set; }
        public List<Pages> LstPages { get; set; }
    }
}