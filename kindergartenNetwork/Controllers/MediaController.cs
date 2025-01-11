using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace kindergartenNetwork.Controllers
{
    public class MediaController : PublicBaseController
    {
        // GET: Media
        public ActionResult ImagesGallery()
        {
            var oModel = new Models.PublicNews.Media();
            var getMedia = DAL.News.Media.MediaGet(new DTO.News.Media { Page = Convert.ToInt32(1), RowPerPage = 9, MediaType = 11});
            if (getMedia.HasResult)
            {
                oModel.LstMedia = getMedia.Results;
                oModel.Count = getMedia.RowCount;
            }

            return View(oModel);
        }
        public ActionResult VideosGallery()
        {
            var oModel = new Models.PublicNews.Media();
            var getMedia = DAL.News.Media.MediaGet(new DTO.News.Media { Page = Convert.ToInt32(1), RowPerPage = 9, MediaType = 12 });
            if (getMedia.HasResult)
            {
                oModel.LstMedia = getMedia.Results;
                oModel.Count = getMedia.RowCount;
            }

            return View(oModel);
        }
        public JsonResult GetMoreMedia(int page, int type)
        {
            var cStatus = "error";
            var getMediaResult = new List<DTO.News.Media>();
            if (page > 0 && type > 0)
            {
                var getMedia = DAL.News.Media.MediaGet(new DTO.News.Media {Page = Convert.ToInt32(page), RowPerPage = 9, MediaType = type});

                if (getMedia.HasResult)
                {
                    getMediaResult = getMedia.Results;
                    var result = from q in getMediaResult
                                 select new
                        {
                            q.ExternalLink,
                            q.FilePath,
                            q.Caption,
                            q.Id
                        };
                    cStatus = "success";
                    return Json(new { cStatus, result}, JsonRequestBehavior.AllowGet);
                }
            }

            return Json(new { cStatus, getMediaResult}, JsonRequestBehavior.AllowGet);
        }
    }
}