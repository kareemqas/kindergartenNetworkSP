using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DTO.News;

namespace kindergartenNetwork.Models.NewsModels
{
    public class SocialModel
    {
        public SocialModel()
        {
            LstSocial = new List<SocialNW>();
        }
        public List<SocialNW> LstSocial { get; set; }
    }
    public class PagesHdrModel
    {
        public PagesHdrModel()
        {
            LstPagesHdrs = new List<PagesHdr>();
        }
        public List<PagesHdr> LstPagesHdrs { get; set; }
    }
}