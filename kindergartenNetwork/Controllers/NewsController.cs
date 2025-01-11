using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO.News;

namespace kindergartenNetwork.Controllers
{
    public class NewsController : PublicBaseController
    {
        // GET: News
        public ActionResult Index(int? page)
        {
            var oModel = new Models.PublicNews.News();
            if (!page.HasValue)
            {
                page = 1;
            }
            var getNews = DAL.News.News.NewsGet(new News { CategoryId = Convert.ToInt32(1), Page = Convert.ToInt32(page), RowPerPage = 6, SortCol = "PublishDate", SortType = "desc" }, 0);
            if (getNews.HasResult)
            {
                oModel.LstNews = getNews.Results;
                oModel.count = getNews.RowCount;
            }
            else
                return RedirectToAction("index", "Home");
            return View(oModel);
        }
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                var oModel = new Models.PublicNews.SingelNews();
                var getSNews = DAL.News.News.NewsGet(new News {IsList = true, Id = id.Value}, 0);
                if (getSNews.HasResult)
                {
                    oModel.ONews = getSNews.Results.FirstOrDefault();
                    ViewBag.MetaDescription = oModel.ONews.Title;
                    ViewBag.MetaKeywords = string.IsNullOrEmpty(oModel.ONews.Keywords)
                        ? ""
                        : oModel.ONews.Keywords.TrimEnd(',');

                    return View(oModel);
                }
            }

            return RedirectToAction("index", "News");
        }
    }
}