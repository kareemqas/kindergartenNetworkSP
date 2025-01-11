using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTO.Account;
using DTO.News;

namespace kindergartenNetwork.Models.NewsModels
{
    public class VisitorModel
    {
    }

    public class InsertUpdateVisitorModel
    {
        public InsertUpdateVisitorModel()
        {
            OVisitor = new Visitors();
        }
        public Visitors OVisitor { get; set; }
    }


    public class ProfileModel
    {
        public ProfileModel()
        {
            OVisitorProfile = new VisitorProfile();
        }
        public VisitorProfile OVisitorProfile { get; set; }
    }
}