using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTO.Account;

namespace kindergartenNetwork.Models.MainModels
{
    public class PageModel
    {
        public List<Pages> LstParent { get; set; } 
    }

    public class InsertUpdatePageModel
    {
        public InsertUpdatePageModel()
        {
            LstPageCategory = new List<PagesCategory>();
            LstParent = new List<Pages>();
        }
        public Pages OPage { get; set; }
        public List<PagesCategory> LstPageCategory { get; set; }
        public List<Pages> LstParent { get; set; }  
    }
}