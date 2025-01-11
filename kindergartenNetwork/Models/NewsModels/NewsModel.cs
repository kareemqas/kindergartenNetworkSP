using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTO.Account;
using DTO.News;

namespace kindergartenNetwork.Models.NewsModels
{
    public class NewsModel
    {
        public NewsModel()
        {
            LstCategory = new List<Categories>();
            LstUsers = new List<UserAccounts>();
            ONews = new News();
            OCategory =new Categories();
            LstComments= new List<Comments>();
        }
        public List<Categories> LstCategory { get; set; }
        public List<UserAccounts> LstUsers { get; set; }
        public News ONews { get; set; }
        public Categories OCategory { get; set; }
        public List<Comments> LstComments { get; set; }
    }
    public class CategoryModel
    {
        public CategoryModel()
        {
            OCategory = new Categories();
        }
        public Categories OCategory { get; set; }
    }
    public class CommentModel
    {
        public CommentModel()
        {
            OComment = new Comments();
        }
        public Comments OComment { get; set; }
    }
}