using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTO.Account;
using DTO.News;

namespace kindergartenNetwork.Models.NewsModels
{
    public class AppSettingsModel
    {
        public AppSettingsModel()
        {
            LstValues = new List<Constant>();
            OAppSetting = new AppSettings();
        }
        public AppSettings OAppSetting { get; set; }
        public List<Constant> LstValues { get; set; }
    }
}