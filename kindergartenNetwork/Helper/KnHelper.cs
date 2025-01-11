using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace kindergartenNetwork.Helper
{
    public class KnHelper
    {
        //ref : https://www.c-sharpcorner.com/code/533/get-youtube-video-thumbnail-image-in-asp-net.aspx
        public static string getYouTubeThumbnail(string YoutubeUrl)
        {
            string youTubeThumb = string.Empty;
            if (YoutubeUrl == "")
                return "";

            if (YoutubeUrl.IndexOf("=") > 0)
            {
                youTubeThumb = YoutubeUrl.Split('=')[1];
            }
            else if (YoutubeUrl.IndexOf("/v/") > 0)
            {
                string strVideoCode = YoutubeUrl.Substring(YoutubeUrl.IndexOf("/v/") + 3);
                int ind = strVideoCode.IndexOf("?");
                youTubeThumb = strVideoCode.Substring(0, ind == -1 ? strVideoCode.Length : ind);
            }
            else if (YoutubeUrl.IndexOf('/') < 6)
            {
                youTubeThumb = YoutubeUrl.Split('/')[3];
            }
            else if (YoutubeUrl.IndexOf('/') > 6)
            {
                youTubeThumb = YoutubeUrl.Split('/')[1];
            }

            return "http://img.youtube.com/vi/" + youTubeThumb + "/0.jpg";
        }
        public static string GetConstantNames(string str, int parent)
        {
            StringBuilder constantsNames = new StringBuilder();
            var lstConstant = GeneralHelper.GetConstants(parent);
            if (!string.IsNullOrEmpty(str))
            {
                var splits = str.Split(',');
                foreach (var split in splits)
                {
                    var x = lstConstant.Where(q => q.Id == Convert.ToInt32(split));
                    constantsNames.Append("<span class=\"label label-sm label-success\"> " + x.FirstOrDefault().Name + " </span> &nbsp");
                }
            }
            return constantsNames.ToString();
        }

    }
}