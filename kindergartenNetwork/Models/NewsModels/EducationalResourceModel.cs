using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTO.Account;
using DTO.News;
using kindergartenNetwork.Models;

namespace kindergartenNetwork.Models.NewsModels
{
    public class EducationalResourceModel
    {
        public EducationalResourceModel()
        {
            LstFileTypes = new List<Constant>();
            OEducationalResources = new EducationalResources();
            LstEducationalResources = new List<EducationalResources>();
        }
        public List<Constant> LstFileTypes { get; set; }
        public EducationalResources OEducationalResources { get; set; }
        public List<EducationalResources> LstEducationalResources { get; set; }
        public int Count { get; set; }
    }
}