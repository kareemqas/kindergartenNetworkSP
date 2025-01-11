using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTO.Account;

namespace kindergartenNetwork.Models.MainModels
{
    public class UserModel
    {
        public List<UserType> LstUserTypes { get; set; }
        public List<Constant> LstPaymentType { get; set; }
    }

    public class InsertUpdateUserModel
    {
        public InsertUpdateUserModel()
        {
            LstUserTypes = new List<UserType>();
            LstPaymentType = new List<Constant>();
            OUserAccounts=new UserAccounts();
        }
        public UserAccounts OUserAccounts { get; set; }
        public List<UserType> LstUserTypes { get; set; }
        public List<Constant> LstPaymentType { get; set; }
    }

    public class UserTypeModel
    {
        public UserTypeModel()
        {
            OUserType = new UserType();
        }

        public UserType OUserType { get; set; }
    }

    public class ProfileModel
    {
        public ProfileModel()
        {
            OUserProfile=new UserProfile();
        }
        public UserProfile OUserProfile { get; set; }
    }
}