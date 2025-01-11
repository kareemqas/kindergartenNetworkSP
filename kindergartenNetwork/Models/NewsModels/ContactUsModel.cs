using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTO.News;

namespace kindergartenNetwork.Models.NewsModels
{
    public class ContactUsModel
    {
        public ContactUsModel()
        {
            OContactUs = new ContactUs();
        }
        public DTO.News.ContactUs OContactUs { get; set; }
    }
}