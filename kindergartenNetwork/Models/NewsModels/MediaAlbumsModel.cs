using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTO.Account;
using DTO.News;

namespace kindergartenNetwork.Models.NewsModels
{
    public class MediaModel
    {
        public MediaModel()
        {
            ListMediaType = new List<Constant>();
            OMedia = new Media();
        }
        public Media OMedia { get; set; }

        public List<Constant> ListMediaType { get; set; }
    }
}