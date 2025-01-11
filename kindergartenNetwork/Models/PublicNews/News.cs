using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTO.News;

namespace kindergartenNetwork.Models.PublicNews
{
    public class News
    {
        public News()
        {
            LstNews = new List<DTO.News.News>();
            LstCategories = new List<DTO.News.Categories>();
            count = 1;
        }
        public List<DTO.News.News> LstNews { get; set; }
        public List<DTO.News.Categories> LstCategories { get; set; }
        public int count { get; set; }

    }
    public class SingelNews
    {
        public SingelNews()
        {
            ONews = new DTO.News.News();
            LstComments = new List<Comments>();
        }
        public DTO.News.News ONews { get; set; }
        public List<Comments> LstComments { get; set; }
    }
    public class StaticPage
    {
        public DTO.News.StaticPages OStaticPages { get; set; }
    }
    public class MainPage
    {
        public MainPage()
        {
            LstMedia = new List<DTO.News.Media>();
            LstNews = new List<DTO.News.News>();
        }
        public List<DTO.News.Media> LstMedia { get; set; }
        public List<DTO.News.News> LstNews { get; set; }
    }
    
    public class Media
    {
        public Media()
        {
            LstMedia = new List<DTO.News.Media>();

        }
        public List<DTO.News.Media> LstMedia { get; set; }
        public int Count { get; set; }
    }
    public class Document
    {
        public Document()
        {
            LstDocument = new List<DTO.News.EducationalResources>();
        }
        public List<DTO.News.EducationalResources> LstDocument { get; set; }
    }
    public class ContactUs
    {
        public DTO.News.StaticPages OContactUs { get; set; }
    }
    
}