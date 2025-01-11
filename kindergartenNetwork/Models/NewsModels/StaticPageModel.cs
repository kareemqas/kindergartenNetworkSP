using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTO.News;

namespace kindergartenNetwork.Models.NewsModels
{
    public class StaticPageModel
    {
        public StaticPageModel()
        {
            OStaticPage = new StaticPages();
            LstNews = new List<News>();
            LstSocials = new List<SocialNW>();
            LstTeamMembers = new List<TeamMembers>();
            LstOurGoals = new List<StaticData>();
            LstStatistics = new List<StaticData>();
            OurMethodology = new List<StaticData>();
        }
        public StaticPages OStaticPage { get; set; }
        public List<News> LstNews { get; set; }
        public List<SocialNW> LstSocials { get; set; }
        public List<TeamMembers> LstTeamMembers { get; set; }
        public List<StaticData> LstOurGoals { get; set; }
        public List<StaticData> LstStatistics { get; set; }
        public List<StaticData> OurMethodology { get; set; }
    }
    public class StaticDataModel
    {
        public StaticDataModel()
        {
            LstStaticData = new List<StaticData>();
        }
        public List<StaticData> LstStaticData { get; set; }
    }
}