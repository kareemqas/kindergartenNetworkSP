using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO.News;
using kindergartenNetwork.Helper;

namespace kindergartenNetwork.Controllers
{
    public class ArticlesController : PublicBaseController
    {
        // GET: Articles
        public ActionResult Index(int? page, int? category, string search)
        {
            var oModel = new Models.PublicNews.News();
            var getCategories = DAL.News.News.CategoryGet(new Categories { IsList = true });
            if (getCategories.HasResult)
            {
                oModel.LstCategories = getCategories.Results.Where(x => x.Id > 1).ToList();
            }
            if (!page.HasValue)
            {
                page = 1;
            }
            var oNews = new News();
            oNews.IsArticle = true;
            oNews.Page = Convert.ToInt32(page);
            oNews.RowPerPage = 4;
            oNews.SortCol = "PublishDate";
            oNews.SortType = "desc";
            if (category.HasValue)
            {
                var catId = Convert.ToInt32(category);
                if (catId > 1)
                    oNews.CategoryId = catId;
            }
            if (!string.IsNullOrEmpty(search))
            {
                oNews.Title = search;
            }
            var getNews = DAL.News.News.NewsGet(oNews, 0);
            if (getNews.HasResult)
            {
                oModel.LstNews = getNews.Results;
                oModel.count = getNews.RowCount;
                return View(oModel);
            }

            return RedirectToAction("index", "Home");
        }
        public ActionResult Details(int? id)
        {
            if (id.HasValue)
            {
                var oModel = new Models.PublicNews.SingelNews();
                var getSNews = DAL.News.News.NewsGet(new News { IsList = true, Id = id.Value }, 0);
                if (getSNews.HasResult)
                {
                    oModel.ONews = getSNews.Results.FirstOrDefault();
                    ViewBag.MetaDescription = oModel.ONews.Title;
                    ViewBag.MetaKeywords = string.IsNullOrEmpty(oModel.ONews.Keywords)
                        ? ""
                        : oModel.ONews.Keywords.TrimEnd(',');
                    var getComments = DAL.News.Comments.CommentsGet(new Comments{ArticleId = id.Value, IsApproved = true, IsList = true});
                    if (getComments.HasResult)
                        oModel.LstComments = getComments.Results;
                    return View(oModel);
                }
            }

            return RedirectToAction("index", "Articles");
        }

        public JsonResult SaveComment(Comments oComment)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;
            if (ModelState.IsValid)
            {
                var oMediaSAve = DAL.News.Comments.CommentSave(oComment);
                if (oMediaSAve.HasResult)
                {
                    cStatus = "success";
                    cMsg = Resources.NotifyMsg.SaveSuccessMsg;
                    return Json(new { cStatus, cMsg }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { cStatus = "notValid", cMsg = GeneralHelper.GetErrorMessage(ModelState, Resources.NotifyMsg.ErrorInField) }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { cStatus, cMsg }, JsonRequestBehavior.AllowGet);
        }
    }
}