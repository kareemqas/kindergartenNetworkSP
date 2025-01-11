using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using System.Web.Security;
using DAL;
using DTO.Account;
using DTO.News;
using kindergartenNetwork.Helper;
using kindergartenNetwork.Models.DataTableModels;
using kindergartenNetwork.Models.NewsModels;
using Newtonsoft.Json;

namespace kindergartenNetwork.Controllers
{
    public class ControlPanelController : BaseController
    {
        // GET: ControlPanel
        public ActionResult Index()
        {
            return View();
        }


        #region UploadSaveAlbumImg
        public JsonResult UploadSaveAlbumImg()
        {
            var fileName = "";
            var file = Request.Files[0];
            var caption = Request.Form["caption"];
            if (file != null && file.ContentLength > 0)
            {
                fileName = Path.GetFileName(file.FileName);
                if (fileName != null)
                {
                    string ext = fileName.Split('.')[fileName.Split('.').Length - 1];
                    string n = Guid.NewGuid().ToString();
                    fileName = n + "." + ext;
                    var path = Path.Combine(Server.MapPath("/Content/UploadedFile/Albums/Original/"), fileName);
                    file.SaveAs(path);
                    var thumbPath = Path.Combine(Server.MapPath("/Content/UploadedFile/Albums/Thumbnail/"), fileName);
                    var largePath = Path.Combine(Server.MapPath("/Content/UploadedFile/Albums/Large/"), fileName);
                    GeneralHelper.ResizeImage(path, thumbPath, 250, ext, true);
                    GeneralHelper.ResizeImage(path, largePath, 950, ext, false);
                }
            }


            DAL.News.Media.MediaSave(new Media { Caption = caption, FilePath = fileName, MediaType = 11 });
            return Json(new { result = "success", Filename = fileName }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult UploadNewsImg()
        {
            var fileName = "";
            var file = Request.Files[0];
            if (file != null && file.ContentLength > 0)
            {
                fileName = Path.GetFileName(file.FileName);
                if (fileName != null)
                {
                    string ext = fileName.Split('.')[fileName.Split('.').Length - 1];
                    string n = Guid.NewGuid().ToString();
                    fileName = n + "." + ext;
                    var path = Path.Combine(Server.MapPath("/Content/UploadedFile/News/Original/"), fileName);
                    file.SaveAs(path);
                    var thumbPath = Path.Combine(Server.MapPath("/Content/UploadedFile/News/Thumbnail/"), fileName);
                    var largePath = Path.Combine(Server.MapPath("/Content/UploadedFile/News/Large/"), fileName);
                    GeneralHelper.ResizeImage(path, thumbPath, 250, ext, true);
                    GeneralHelper.ResizeImage(path, largePath, 950, ext, false);
                }
            }
            return Json(new { result = "success", Filename = fileName, }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UploadAlbumImg()
        {
            var fileName = "";
            var file = Request.Files[0];
            if (file != null && file.ContentLength > 0)
            {
                fileName = Path.GetFileName(file.FileName);
                if (fileName != null)
                {
                    string ext = fileName.Split('.')[fileName.Split('.').Length - 1];
                    string n = Guid.NewGuid().ToString();
                    fileName = n + "." + ext;
                    var path = Path.Combine(Server.MapPath("/Content/UploadedFile/Albums/Original/"), fileName);
                    file.SaveAs(path);
                    var thumbPath = Path.Combine(Server.MapPath("/Content/UploadedFile/Albums/Thumbnail/"), fileName);
                    var largePath = Path.Combine(Server.MapPath("/Content/UploadedFile/Albums/Large/"), fileName);
                    GeneralHelper.ResizeImage(path, thumbPath, 250, ext, true);
                    GeneralHelper.ResizeImage(path, largePath, 950, ext, false);
                }
            }
            return Json(new { result = "success", Filename = fileName, }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpLoadFile()
        {
            var fileName = "";
            var file = Request.Files[0];
            if (file != null && file.ContentLength > 0)
            {
                fileName = Path.GetFileName(file.FileName);
                if (fileName != null)
                {
                    string ext = fileName.Split('.')[fileName.Split('.').Length - 1];
                    string n = Guid.NewGuid().ToString();
                    fileName = n + "." + ext;
                    var path = Path.Combine(Server.MapPath("/Content/UploadedFile/Attachments/"), fileName);
                    file.SaveAs(path);
                }

            }
            return Json(new { result = "success", Filename = fileName, }, JsonRequestBehavior.AllowGet);
        }
        public static string GetMimeTypeByWindowsRegistry(string fileNameOrExtension)
        {
            string mimeType = "application/unknown";
            string ext = fileNameOrExtension.Contains(".") ? Path.GetExtension(fileNameOrExtension).ToLower() : "." + fileNameOrExtension;
            Microsoft.Win32.RegistryKey regKey = Microsoft.Win32.Registry.ClassesRoot.OpenSubKey(ext);
            if (regKey != null && regKey.GetValue("Content Type") != null) mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }
        #endregion

        #region StaticPages
        public ActionResult HomePage()
        {
            var oModel = new StaticPageModel();
            var getStaticPage = DAL.News.StaticPages.StaticPagesGet(new StaticPages{Id = 1});
            if (getStaticPage.HasResult)
                oModel.OStaticPage = getStaticPage.Results.First();
            return View(oModel);
        }
        public ActionResult AboutPage()
        {
            var oModel = new StaticPageModel();
            var getStaticPage = DAL.News.StaticPages.StaticPagesGet(new StaticPages { Id = 2 });
            if (getStaticPage.HasResult)
                oModel.OStaticPage = getStaticPage.Results.First();
            return View(oModel);
        }
        public ActionResult ContactPage()
        {
            var oModel = new StaticPageModel();
            var getStaticPage = DAL.News.StaticPages.StaticPagesGet(new StaticPages { Id = 3 });
            if (getStaticPage.HasResult)
                oModel.OStaticPage = getStaticPage.Results.First();
            return View(oModel);
        }
        public JsonResult GetStaticPagesDataTable(JQueryDataTableParamModel param)
        {
            var oStaticPage = new StaticPages();
            if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
                oStaticPage.Id = Convert.ToInt32(Request.QueryString["Id"]);

            DataTableProcessModel m = new DataTableProcessModel();
            DataTableProcessModel dtProcess = DataTableProcesses.DataTableEslestir(param, m);
            oStaticPage.SortCol = dtProcess.SortCol;
            oStaticPage.SortType = dtProcess.SortType;
            oStaticPage.Page = dtProcess.Page;
            oStaticPage.RowPerPage = dtProcess.RowPerPage;

            var getStaticPage = DAL.News.StaticPages.StaticPagesGet(oStaticPage);

            
            if (getStaticPage.HasResult)
            {
                var getStaticPageResult = getStaticPage.Results;

                int rowCount = getStaticPage.RowCount;
                int lnRowCount = rowCount;

                var result = from q in getStaticPageResult
                             select new
                             {
                                 q.Id,
                                 q.Image,
                                 q.PageName
                             };
                return Json(new
                {
                    param.sEcho,
                    iTotalRecords = rowCount,
                    iTotalDisplayRecords = lnRowCount,
                    aaData = result
                },
                    JsonRequestBehavior.AllowGet);
            }
            else
            {

                int rowCount = getStaticPage.RowCount;
                int lnRowCount = rowCount;

                var result = from q in getStaticPage.Results
                             select new
                             {
                                 q.Id,
                                 q.Image,
                                 q.PageName
                             };

                return Json(new
                {
                    param.sEcho,
                    iTotalRecords = rowCount,
                    iTotalDisplayRecords = lnRowCount,
                    aaData = result
                },
                    JsonRequestBehavior.AllowGet);
            }
        }
        //public PartialViewResult SaveStaticPageModal(string id)
        //{
        //    var staticPageId = Convert.ToInt32(id);
        //    var oModel = new Models.News.StaticPageModel();
        //    var getStaticPage = DAL.News.StaticPages.StaticPagesGet(new DTO.News.StaticPages { IsList = true, Id = staticPageId });
        //    if (getStaticPage.HasResult)
        //        oModel.OStaticPages = (getStaticPage.Results as List<DTO.News.StaticPages>).FirstOrDefault();

        //    return PartialView("StaticPageParts/_StaticPageSaveModal", oModel);
        //}
        [ValidateInput(false)]
        public JsonResult SaveStaticPage(StaticPages oStaticPages)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;
            if (ModelState.IsValid)
            {
                oStaticPages.UpdatedBy = User.Id;
                var oStaticPagesSave = DAL.News.StaticPages.StaticPageSave(oStaticPages);
                if (oStaticPagesSave.HasResult)
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


        public ActionResult OurGoals()
        {
            var oModel = new StaticDataModel();
            var getStaticData = DAL.News.StaticData.GetStaticData(new StaticData { Type = 1 });
            if (getStaticData.HasResult)
                oModel.LstStaticData = getStaticData.Results;
            return View(oModel);
        }
        public ActionResult Statistics()
        {
            var oModel = new StaticDataModel();
            var getStaticData = DAL.News.StaticData.GetStaticData(new StaticData { Type = 2 });
            if (getStaticData.HasResult)
                oModel.LstStaticData = getStaticData.Results;
            return View(oModel);
        }
        public ActionResult OurMethodology()
        {
            var oModel = new StaticDataModel();
            var getStaticData = DAL.News.StaticData.GetStaticData(new StaticData { Type = 3 });
            if (getStaticData.HasResult)
                oModel.LstStaticData = getStaticData.Results;
            return View(oModel);
        }

        public JsonResult UpdateStaticData(string str)
        {
            string cStatus;
            string cMsg;

            var oStaticData = JsonConvert.DeserializeObject<List<StaticData>>(str);
            foreach (var s in oStaticData)
            {
                var oResult = DAL.News.StaticData.UpdateStaticData(s);
                if (!oResult.HasResult)
                {
                    cStatus = "error";
                    cMsg = Resources.NotifyMsg.ErrorMsg;
                    return Json(new { cStatus, cMsg }, JsonRequestBehavior.AllowGet);
                }

            }

            cStatus = "success";
            cMsg = Resources.NotifyMsg.SaveSuccessMsg;

            return Json(new { cStatus, cMsg }, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region TeamMembers
        public ActionResult TeamMembers()
        {
            return View();
        }
        public JsonResult GetTeamMembersDataTable(JQueryDataTableParamModel param)
        {
            var oMember = new TeamMembers();
            if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
                oMember.Id = Convert.ToInt32(Request.QueryString["Id"]);

            DataTableProcessModel m = new DataTableProcessModel();
            DataTableProcessModel dtProcess = DataTableProcesses.DataTableEslestir(param, m);
            oMember.SortCol = dtProcess.SortCol;
            oMember.SortType = dtProcess.SortType;
            oMember.Page = dtProcess.Page;
            oMember.RowPerPage = dtProcess.RowPerPage;
            var getTeamMembersList = new List<TeamMembers>();
            var getTeamMembers = DAL.News.TeamMembers.TeamMembersGet(oMember);


            if (getTeamMembers.HasResult)
            {
                getTeamMembersList = getTeamMembers.Results;

                int rowCount = getTeamMembers.RowCount;
                int lnRowCount = rowCount;

                var result = from q in getTeamMembersList
                             select new
                             {
                                 q.Id,
                                 q.Name,
                                 q.Avatar,
                                 q.JobTitle
                             };

                return Json(new
                {
                    param.sEcho,
                    iTotalRecords = rowCount,
                    iTotalDisplayRecords = lnRowCount,
                    aaData = result
                },
                    JsonRequestBehavior.AllowGet);
            }
            else
            {

                int rowCount = getTeamMembers.RowCount;
                int lnRowCount = rowCount;

                var result = from q in getTeamMembersList
                             select new
                             {
                                 q.Id,
                                 q.Name,
                                 q.Avatar,
                                 q.JobTitle
                             };

                return Json(new
                {
                    param.sEcho,
                    iTotalRecords = rowCount,
                    iTotalDisplayRecords = lnRowCount,
                    aaData = result
                },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public PartialViewResult SaveTeamMemberModal(string id)
        {
            var oModel = new TeamMembersModel();
            var memberId = Convert.ToInt32(id);
            if (memberId > 0)
            {
                var getTeamMember = DAL.News.TeamMembers.TeamMembersGet(new TeamMembers { Id = memberId }); 
                if (getTeamMember.HasResult)
                    oModel.OTeamMember = getTeamMember.Results.FirstOrDefault();
            }
            else
            {
                oModel.OTeamMember = new TeamMembers();
            }
            return PartialView("TeamMemberParts/_TeamMemberSaveModal", oModel);
        }
        public JsonResult SaveTeamMember(TeamMembers oMember)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;
            if (ModelState.IsValid)
            {
                var oCategorySave = DAL.News.TeamMembers.TeamMemberSave(oMember);
                if (oCategorySave.HasResult)
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
        public JsonResult DeleteTeamMember(int id)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;
            var oResult = DAL.News.TeamMembers.TeamMemberDelete(id);
            if (oResult.HasResult)
            {
                cStatus = "success";
                cMsg = Resources.NotifyMsg.DeleteSuccessMsg;
            }
            return Json(new { cStatus, cMsg, }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Categories
        public ActionResult Categories()
        {
            return View();
        }
        public JsonResult GetCategoriesDataTable(JQueryDataTableParamModel param)
        {
            var oCategory = new Categories();
            if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
                oCategory.Id = Convert.ToInt32(Request.QueryString["Id"]);

            DataTableProcessModel m = new DataTableProcessModel();
            DataTableProcessModel dtProcess = DataTableProcesses.DataTableEslestir(param, m);
            oCategory.SortCol = dtProcess.SortCol;
            oCategory.SortType = dtProcess.SortType;
            oCategory.Page = dtProcess.Page;
            oCategory.RowPerPage = dtProcess.RowPerPage;
            oCategory.IsList = true;

            var getCategory = DAL.News.News.CategoryGet(oCategory);

           
            if (getCategory.HasResult)
            {
                var getCategoryResult = getCategory.Results.Where(x=>x.Id > 1).ToList();

                int rowCount = getCategory.RowCount;
                int lnRowCount = rowCount;

                var result = from q in getCategoryResult
                             select new
                             {
                                 q.Id,
                                 q.NameAr,
                                 q.NameEn
                             };

                return Json(new
                {
                    param.sEcho,
                    iTotalRecords = rowCount,
                    iTotalDisplayRecords = lnRowCount,
                    aaData = result
                },
                    JsonRequestBehavior.AllowGet);
            }
            else
            {

                int rowCount = getCategory.RowCount;
                int lnRowCount = rowCount;

                var result = from q in getCategory.Results
                             select new
                             {
                                 q.Id,
                                 q.NameAr,
                                 q.NameEn
                             };

                return Json(new
                {
                    param.sEcho,
                    iTotalRecords = rowCount,
                    iTotalDisplayRecords = lnRowCount,
                    aaData = result
                },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public PartialViewResult SaveCategoryModal(string id)
        {
            var oModel = new CategoryModel();
            var categoryId = Convert.ToInt32(id);
            if (categoryId > 0)
            {
                var getCategory = DAL.News.News.CategoryGet(new Categories { IsList = true, Id = categoryId });
                if (getCategory.HasResult)
                    oModel.OCategory = getCategory.Results.FirstOrDefault();
            }
            else
            {
                oModel.OCategory = new Categories();
            }
            return PartialView("CategoryParts/_CategorySaveModal", oModel);
        }
        public JsonResult SaveCategory(Categories oCategory)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;
            if (ModelState.IsValid)
            {
                var oCategorySave = DAL.News.News.CategorySave(oCategory);
                if (oCategorySave.HasResult)
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
        public JsonResult DeleteCategory(int id)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;
            var oResult = DAL.News.News.CategoryDelete(id);
            if (oResult.HasResult)
            {
                cStatus = "success";
                cMsg = Resources.NotifyMsg.DeleteSuccessMsg;
            }
            return Json(new { cStatus, cMsg, }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region News
        public ActionResult News()
        {
            var oModel = new NewsModel();

            var getUsers = DAL.Account.UserAccounts.UserAccountGet(new UserAccounts { IsList = true });
            if (getUsers.HasResult)
                oModel.LstUsers = getUsers.Results.Where(x => x.Id != 1).ToList();
            var getNewCategory = DAL.News.News.CategoryGet(new Categories { Id = 1, IsList = true });
            if (getNewCategory.HasResult)
                oModel.OCategory = getNewCategory.Results.FirstOrDefault();
            return View(oModel);
        }
        public JsonResult GetNewsDataTable(JQueryDataTableParamModel param)
        {
            var oNews = new News();
            if (!string.IsNullOrEmpty(Request.QueryString["NewsSearch"]))
                oNews.Title = Request.QueryString["NewsSearch"];
            if (!string.IsNullOrEmpty(Request.QueryString["Category"]))
                oNews.CategoryId = Convert.ToInt32(Request.QueryString["Category"]);
            if (!string.IsNullOrEmpty(Request.QueryString["InsertedBy"]))
                oNews.InsertedBy = Convert.ToInt32(Request.QueryString["InsertedBy"]);
            if (!string.IsNullOrEmpty(Request.QueryString["isArticle"]))
                oNews.IsArticle = Convert.ToBoolean(Request.QueryString["isArticle"]);

            CultureInfo newCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            newCulture.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            newCulture.DateTimeFormat.DateSeparator = "-";
            Thread.CurrentThread.CurrentCulture = newCulture;
            if (!string.IsNullOrEmpty(Request.QueryString["FromDate"]))
            {
                string date = Request.QueryString["FromDate"];
                oNews.FromDate = CommonHelpExtension.ConvertToUTC(Convert.ToDateTime(date, newCulture));
            }
            if (!string.IsNullOrEmpty(Request.QueryString["ToDate"]))
            {
                string date = Request.QueryString["ToDate"];
                oNews.ToDate = CommonHelpExtension.ConvertToUTC(Convert.ToDateTime(date, newCulture));
            }
            DataTableProcessModel m = new DataTableProcessModel();
            DataTableProcessModel dtProcess = DataTableProcesses.DataTableEslestir(param, m);
            oNews.SortCol = dtProcess.SortCol;
            oNews.SortType = dtProcess.SortType;
            oNews.Page = dtProcess.Page;
            oNews.RowPerPage = dtProcess.RowPerPage;

            var getNews = DAL.News.News.NewsGet(oNews, 0);

            

            if (getNews.RowCount > 0)
            {
                var getNewsResult = getNews.Results;

                int rowCount = getNews.RowCount;
                int lnRowCount = rowCount;

                var result = from q in getNewsResult
                             select new
                             {
                                 q.Id,
                                 q.IsActive,
                                 q.CategoryId,
                                 q.Image,
                                 q.Status,
                                 q.Title,
                                 q.ViewsCount,
                                 q.InsertedBy,
                                 PublishDate = q.PublishDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                 InsertedByName = q.OInsertedBy.Name,
                                 q.OCategory.NameAr,
                                 q.OCategory.NameEn,

                             };

                return Json(new
                {
                    param.sEcho,
                    iTotalRecords = rowCount,
                    iTotalDisplayRecords = lnRowCount,
                    aaData = result
                },
                    JsonRequestBehavior.AllowGet);
            }
            else
            {
                var lst = new List<News>();
                int rowCount = getNews.RowCount;
                int lnRowCount = rowCount;

                var result = from q in lst
                             select new
                             {
                                 q.Id,
                                 q.IsActive,
                                 q.CategoryId,
                                 q.Image,
                                 q.Status,
                                 q.Title,
                                 q.ViewsCount,
                                 q.InsertedBy,
                                 PublishDate = q.PublishDate.ToString("yyyy-MM-dd HH:mm:ss"),
                                 q.OInsertedBy.Name,
                                 q.OCategory.NameAr,
                                 q.OCategory.NameEn,
                             };

                return Json(new
                {
                    param.sEcho,
                    iTotalRecords = rowCount,
                    iTotalDisplayRecords = lnRowCount,
                    aaData = result
                },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public PartialViewResult SaveNewsModal(string id, string categoryId)
        {
            var newsId = Convert.ToInt32(id);
            var catId = 0;
            if (!string.IsNullOrEmpty(categoryId))
            {
                catId = Convert.ToInt32(categoryId);
            }
            ViewBag.CategoryId = catId;
            var oModel = new NewsModel();
            if (newsId > 0)
            {
                var getNews = DAL.News.News.NewsGet(new News { Id = newsId }, 0);
                if (getNews.HasResult)
                    oModel.ONews = getNews.Results.FirstOrDefault();
            }
            else
            {
                oModel.ONews = new News();
            }
            var getNewCategories = DAL.News.News.CategoryGet(new Categories { IsList = true });
            if (getNewCategories.HasResult)
                oModel.LstCategory = getNewCategories.Results.Where(x=>x.Id > 1).ToList();

            return PartialView("NewsParts/_NewsSaveModal", oModel);
        }
        [ValidateInput(false)]
        public JsonResult SaveNews(News oNews)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;
            if (ModelState.IsValid)
            {
                oNews.InsertedBy = User.Id;
                var oNewsSave = DAL.News.News.NewsSave(oNews);
                if (oNewsSave.HasResult)
                {
                    var newsId = oNewsSave.Results.Id;
                    cStatus = "success";
                    cMsg = Resources.NotifyMsg.SaveSuccessMsg;
                    return Json(new { cStatus, cMsg, id = newsId }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { cStatus = "notValid", cMsg = GeneralHelper.GetErrorMessage(ModelState, Resources.NotifyMsg.ErrorInField) }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { cStatus, cMsg }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteNews(int id)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;
            var oResult = DAL.News.News.NewsDelete(id);
            if (oResult.HasResult)
            {
                cStatus = "success";
                cMsg = Resources.NotifyMsg.DeleteSuccessMsg;
            }
            return Json(new { cStatus, cMsg, }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SearchAutoCompleteNews(string id, int? categoryId)
        {
            var getNewsResult = new List<News>();
            var oNews = new News();
            oNews.IsList = true;
            oNews.Title = id;
            if (categoryId.HasValue)
            {
                oNews.CategoryId = categoryId.Value;
                if (categoryId > 1)
                    oNews.IsArticle = true;
            }

            var getNews = DAL.News.News.NewsGet(oNews, 0);

            if (getNews.HasResult)
            {
                getNewsResult = getNews.Results;
                var result = from q in getNewsResult
                    select new
                    {
                        Title = q.Title.Substring(0, 30),
                        q.Id
                    };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(getNewsResult, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Articles()
        {
            var oModel = new NewsModel();

            var getUsers = DAL.Account.UserAccounts.UserAccountGet(new UserAccounts { IsList = true });
            if (getUsers.HasResult)
                oModel.LstUsers = getUsers.Results.Where(x => x.Id != 1).ToList();
            var getNewCategory = DAL.News.News.CategoryGet(new Categories { IsList = true });
            if (getNewCategory.HasResult)
                oModel.LstCategory = getNewCategory.Results.Where(x=>x.Id > 1).ToList();
            return View(oModel);
        }
        #endregion

        #region App Settings
        public ActionResult AppSettings()
        {
            return View();
        }
        public JsonResult GetAppSettingsDataTable(JQueryDataTableParamModel param)
        {
            //DataTableProcessModel m = new DataTableProcessModel();
            //DataTableProcessModel dtProcess = DataTableProcesses.DataTableEslestir(param, m);
            var getAppSettings = DAL.News.AppSettings.AppSettingsGet(0);
            var getAppSettingsResult = new List<AppSettings>();
            if (getAppSettings.HasResult)
            {
                getAppSettingsResult = getAppSettings.Results;

                int rowCount = getAppSettings.RowCount;
                int lnRowCount = rowCount;

                var result = from q in getAppSettingsResult
                             select new
                             {
                                 q.ConKey,
                                 q.ConValue,
                                 KeyName = q.OKey.Name,
                                 ValueName = q.OValue.Name
                             };
                return Json(new
                {
                    param.sEcho,
                    iTotalRecords = rowCount,
                    iTotalDisplayRecords = lnRowCount,
                    aaData = result
                },
                    JsonRequestBehavior.AllowGet);
            }
            else
            {

                int rowCount = getAppSettings.RowCount;
                int lnRowCount = rowCount;

                var result = from q in getAppSettingsResult
                             select new
                             {
                                 q.ConKey,
                                 q.ConValue,
                                 KeyName = q.OKey.Name,
                                 ValueName = q.OValue.Name
                             };

                return Json(new
                {
                    param.sEcho,
                    iTotalRecords = rowCount,
                    iTotalDisplayRecords = lnRowCount,
                    aaData = result
                },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public PartialViewResult SaveAppSettingsModal(string id)
        {
            var oModel = new AppSettingsModel();
            var key = Convert.ToInt32(id);
            var getAppSettings = DAL.News.AppSettings.AppSettingsGet(key);
            if (getAppSettings.HasResult)
                oModel.OAppSetting = getAppSettings.Results.FirstOrDefault();
            oModel.LstValues = GeneralHelper.GetConstants(key);
            return PartialView("AppSettingsParts/_AppSettingsSaveModal", oModel);
        }
        public JsonResult SaveAppSettings(AppSettings oAppSettings)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;
            if (ModelState.IsValid)
            {
                var oAppSettingsSave = DAL.News.AppSettings.AppSettingsSave(oAppSettings);
                if (oAppSettingsSave.HasResult)
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
        #endregion

        #region Social
        public ActionResult Social()
        {
            var oModel = new Models.NewsModels.SocialModel();
            var getSocial = DAL.News.SocialNW.GetSocialNW(new SocialNW());
            if (getSocial.HasResult)
                oModel.LstSocial = getSocial.Results;
            return View(oModel);
        }
        public JsonResult UpdateSocial(string str)
        {
            string cStatus;
            string cMsg;

            var oSocialNw = JsonConvert.DeserializeObject<List<SocialNW>>(str);
            foreach (var s in oSocialNw)
            {
                var oResult = DAL.News.SocialNW.UpdateSocialNW(s);
                if (!oResult.HasResult)
                {
                    cStatus = "error";
                    cMsg = Resources.NotifyMsg.ErrorMsg;
                    return Json(new { cStatus, cMsg }, JsonRequestBehavior.AllowGet);
                }

            }

            cStatus = "success";
            cMsg = Resources.NotifyMsg.SaveSuccessMsg;

            return Json(new { cStatus, cMsg }, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region ContactUs
        public ActionResult ContactUs()
        {
            return View();
        }
        public JsonResult GetContactUsDataTable(JQueryDataTableParamModel param)
        {
            var oContactUs = new DTO.News.ContactUs();

            DataTableProcessModel m = new DataTableProcessModel();
            DataTableProcessModel dtProcess = DataTableProcesses.DataTableEslestir(param, m);
            oContactUs.SortCol = dtProcess.SortCol;
            oContactUs.SortType = dtProcess.SortType;
            oContactUs.Page = dtProcess.Page;
            oContactUs.RowPerPage = dtProcess.RowPerPage;

            var getcontactUs = DAL.News.ContactUs.ContactUsGet(oContactUs);

            var getContactUsResult = new List<DTO.News.ContactUs>();
            if (getcontactUs.HasResult)
            {
                getContactUsResult = getcontactUs.Results;

                int rowCount = getcontactUs.RowCount;
                int lnRowCount = rowCount;

                var result = from q in getContactUsResult
                             select new
                             {
                                 q.Id,
                                 q.Subject,
                                 q.Email,
                                 q.Name,
                                 q.Message,
                                 InsertedDate = q.InsertedDate.ToString("yyyy-MM-dd HH:mm"),
                                 q.Reply,
                                 q.IsAnswered,
                                 q.IsRead


                             };
                return Json(new
                {
                    param.sEcho,
                    iTotalRecords = rowCount,
                    iTotalDisplayRecords = lnRowCount,
                    aaData = result
                },
                    JsonRequestBehavior.AllowGet);
            }
            else
            {

                int rowCount = getcontactUs.RowCount;
                int lnRowCount = rowCount;

                var result = from q in getContactUsResult
                             select new
                             {
                                 q.Id,
                                 q.Subject,
                                 q.Email,
                                 q.Name,
                                 q.Message,
                                 InsertedDate = q.InsertedDate.ToString("yyyy-MM-dd HH:mm"),
                                 q.Reply,
                                 q.IsAnswered,
                                 q.IsRead
                             };

                return Json(new
                {
                    param.sEcho,
                    iTotalRecords = rowCount,
                    iTotalDisplayRecords = lnRowCount,
                    aaData = result
                },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public PartialViewResult ViewContactUs(string id)
        {
            var oModel = new Models.NewsModels.ContactUsModel();
            int cId = Convert.ToInt32(id);
            if (cId > 0)
            {
                var getContactUs = DAL.News.ContactUs.ContactUsGet(new DTO.News.ContactUs { Id = cId });
                if (getContactUs.HasResult)
                {
                    oModel.OContactUs = getContactUs.Results.FirstOrDefault();
                    DAL.News.ContactUs.ContactUsRead(cId);
                }
            }
            return PartialView("LayoutParts/_ViewContactUS", oModel);
        }
        [ValidateOnlyIncomingValuesAttribute]
        [HttpPost]
        public JsonResult ReplyContactUs([Bind(Include = "Reply,Id,Email")] DTO.News.ContactUs oContactUs)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;
            if (ModelState.IsValid)
            {
                oContactUs.Email = string.Empty;
                var oContactUsSave = DAL.News.ContactUs.ContactUsReply(oContactUs);
                if (oContactUsSave.HasResult)
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
        public JsonResult DeleteContactUs(int id)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;
            var oResult = DAL.News.ContactUs.ContactUsDelete(id);
            if (oResult.HasResult)
            {
                cStatus = "success";
                cMsg = Resources.NotifyMsg.DeleteSuccessMsg;
            }
            return Json(new { cStatus, cMsg, }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Media
        public ActionResult MediaAlbums()
        {
            var oModel = new MediaModel();
            oModel.ListMediaType = GeneralHelper.GetConstants(10);
            return View();
        }
        public JsonResult GetMediasDataTable(JQueryDataTableParamModel param)
        {
            var oMedia = new Media();
            if (!string.IsNullOrEmpty(Request.QueryString["TypeId"]))
                oMedia.MediaType = Convert.ToInt32(Request.QueryString["TypeId"]);

            DataTableProcessModel m = new DataTableProcessModel();
            DataTableProcessModel dtProcess = DataTableProcesses.DataTableEslestir(param, m);
            oMedia.SortCol = dtProcess.SortCol;
            oMedia.SortType = dtProcess.SortType;
            oMedia.Page = dtProcess.Page;
            oMedia.RowPerPage = dtProcess.RowPerPage;

            var getMedia = DAL.News.Media.MediaGet(oMedia);

            var getMediaResult = new List<Media>();
            if (getMedia.HasResult)
            {
                getMediaResult = getMedia.Results;

                int rowCount = getMedia.RowCount;
                int lnRowCount = rowCount;

                var result = from q in getMediaResult
                             select new
                             {
                                 q.Id,
                                 q.FilePath,
                                 q.ExternalLink,
                                 q.Caption,
                                 q.MediaType,
                                 MediaTypeName = q.OMediaType.Name,
                                 FT = GetMimeTypeByWindowsRegistry(q.FilePath)


                             };
                return Json(new
                {
                    param.sEcho,
                    iTotalRecords = rowCount,
                    iTotalDisplayRecords = lnRowCount,
                    aaData = result
                },
                    JsonRequestBehavior.AllowGet);
            }
            else
            {

                int rowCount = getMedia.RowCount;
                int lnRowCount = rowCount;

                var result = from q in getMediaResult
                             select new
                             {
                                 q.Id,
                                 q.FilePath,
                                 q.ExternalLink,
                                 CaptionAr = q.Caption,
                                 MediaTypeAr = q.OMediaType.Name,
                                 FT = GetMimeTypeByWindowsRegistry(q.FilePath)
                             };

                return Json(new
                {
                    param.sEcho,
                    iTotalRecords = rowCount,
                    iTotalDisplayRecords = lnRowCount,
                    aaData = result
                },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public PartialViewResult SaveMediaModal(string id)
        {
            var mediaId = Convert.ToInt32(id);
            var oModel = new MediaModel();
            oModel.ListMediaType = GeneralHelper.GetConstants(10);
            if (mediaId > 0)
            {
                var getMedia = DAL.News.Media.MediaGet(new Media { IsList = true, Id = mediaId });
                if (getMedia.HasResult)
                    oModel.OMedia = getMedia.Results.FirstOrDefault();
            }
            else
            {
                oModel.OMedia = new Media();
            }

            oModel.ListMediaType = GeneralHelper.GetConstants(10);
            return PartialView("MediaParts/_MediaSaveModal", oModel);
        }
        public JsonResult SaveMedia(Media oMedia)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;
            if (ModelState.IsValid)
            {
                //if (oMedia.MediaType == 13)
                //{
                //    oMedia.ExternalLink = GeneralHelper.ExtractSoundCloudTracId(oMedia.ExternalLink);
                //}
                var oMediaSAve = DAL.News.Media.MediaSave(oMedia);
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
        public JsonResult DeleteMedia(int id)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;
            var oResult = DAL.News.Media.MediaDelete(id);
            if (oResult.HasResult)
            {
                cStatus = "success";
                cMsg = Resources.NotifyMsg.DeleteSuccessMsg;
            }
            return Json(new { cStatus, cMsg, }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region EducationalResources 

        public ActionResult EducationalResources()
        {
            var oModel = new EducationalResourceModel();
            oModel.LstFileTypes = GeneralHelper.GetConstants(1);
            return View(oModel);
        }
        public JsonResult GetEducationalResourcesDataTable(JQueryDataTableParamModel param)
        {
            var oAttachment = new EducationalResources();
            if (!string.IsNullOrEmpty(Request.QueryString["Id"]))
                oAttachment.Id = Convert.ToInt32(Request.QueryString["Id"]);
            if (!string.IsNullOrEmpty(Request.QueryString["Type"]))
                oAttachment.FileType = Convert.ToInt32(Request.QueryString["Type"]);

            DataTableProcessModel m = new DataTableProcessModel();
            DataTableProcessModel dtProcess = DataTableProcesses.DataTableEslestir(param, m);
            oAttachment.SortCol = dtProcess.SortCol;
            oAttachment.SortType = dtProcess.SortType;
            oAttachment.Page = dtProcess.Page;
            oAttachment.RowPerPage = dtProcess.RowPerPage;

            var getAttachment = DAL.News.EducationalResources.AttachmentGet(oAttachment);

            var getAttachmentResult = new List<EducationalResources>();
            if (getAttachment.HasResult)
            {
                getAttachmentResult = getAttachment.Results;

                int rowCount = getAttachment.RowCount;
                int lnRowCount = rowCount;

                var result = from q in getAttachmentResult
                             select new
                             {
                                 q.Id,
                                 q.FileDescription,
                                 q.FilePath,
                                 q.FileTitle,
                                 q.FileType,
                                 UserName = q.OUserAccount.Name,
                                 InsertedDate = q.InsertedDate.ToString("yyyy-MM-dd HH:mm"),
                                 TypeName = q.OFileType.Name,
                                 q.OFileType.Icon
                             };
                return Json(new
                {
                    param.sEcho,
                    iTotalRecords = rowCount,
                    iTotalDisplayRecords = lnRowCount,
                    aaData = result
                },
                    JsonRequestBehavior.AllowGet);
            }
            else
            {

                int rowCount = getAttachment.RowCount;
                int lnRowCount = rowCount;

                var result = from q in getAttachmentResult
                             select new
                             {
                                 q.Id,
                                 q.FileDescription,
                                 q.FilePath,
                                 q.FileTitle,
                                 q.FileType,
                                 UserName = q.OUserAccount.Name,
                                 InsertedDate = q.InsertedDate.ToString("yyyy-MM-dd HH:mm"),
                                 TypeName = q.OFileType.Name,
                                 q.OFileType.Icon
                             };

                return Json(new
                {
                    param.sEcho,
                    iTotalRecords = rowCount,
                    iTotalDisplayRecords = lnRowCount,
                    aaData = result
                },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public PartialViewResult SaveEducationalResourceModal(string id)
        {
            var attachmentId = Convert.ToInt32(id);
            var oModel = new EducationalResourceModel();
            if (attachmentId > 0)
            {
                var getAttachment = DAL.News.EducationalResources.AttachmentGet(new EducationalResources { IsList = true, Id = attachmentId });
                if (getAttachment.HasResult)
                    oModel.OEducationalResources = getAttachment.Results.FirstOrDefault();
            }
            else
            {
                oModel.OEducationalResources = new EducationalResources();
            }

            oModel.LstFileTypes = GeneralHelper.GetConstants(1);
            return PartialView("EducationalResourceParts/_EducationalResourceSaveModal", oModel);
        }
        public JsonResult SaveEducationalResource(EducationalResources oAttachment)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;
            if (ModelState.IsValid)
            {
                oAttachment.InsertedBy = User.Id;
                var oAttachmentSave = DAL.News.EducationalResources.AttachmentSave(oAttachment);
                if (oAttachmentSave.HasResult)
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
        public JsonResult DeleteEducationalResource(int id)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;
            var oResult = DAL.News.EducationalResources.AttachmentDelete(id);
            if (oResult.HasResult)
            {
                cStatus = "success";
                cMsg = Resources.NotifyMsg.DeleteSuccessMsg;
            }
            return Json(new { cStatus, cMsg, }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult SearchAutoCompleteEducationalResources(string id)
        {
            var getAttachmentResult = new List<EducationalResources>();
            var getAttachment = DAL.News.EducationalResources.AttachmentGet(new EducationalResources { IsList = true, FileTitle = id.Trim() });

            if (getAttachment.HasResult)
            {
                getAttachmentResult = getAttachment.Results;
                var result = from q in getAttachmentResult
                             select new
                             {
                                 q.FileTitle,
                                 q.Id
                             };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return Json(getAttachmentResult, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Comments
        public ActionResult Comments()
        {
            return View();
        }
        public JsonResult GetCommentsDataTable(JQueryDataTableParamModel param)
        {
            var oComment = new Comments();
            if (!string.IsNullOrEmpty(Request.QueryString["Name"]))
                oComment.Name = Request.QueryString["Name"];
            if (!string.IsNullOrEmpty(Request.QueryString["Email"]))
                oComment.Email = Request.QueryString["Email"];

            DataTableProcessModel m = new DataTableProcessModel();
            DataTableProcessModel dtProcess = DataTableProcesses.DataTableEslestir(param, m);
            oComment.SortCol = dtProcess.SortCol;
            oComment.SortType = dtProcess.SortType;
            oComment.Page = dtProcess.Page;
            oComment.RowPerPage = dtProcess.RowPerPage;

            var getComments = DAL.News.Comments.CommentsGet(oComment);

            var getCommentsResult = new List<Comments>();
            if (getComments.HasResult)
            {
                getCommentsResult = getComments.Results;

                int rowCount = getComments.RowCount;
                int lnRowCount = rowCount;

                var result = from q in getCommentsResult
                             select new
                             {
                                 q.Id,
                                 q.ArticleId,
                                 q.Email,
                                 q.Name,
                                 q.Comment,
                                 Date = q.Date.ToString("yyyy-MM-dd HH:mm"),
                                 q.IsApproved,
                                 q.OArticle.Title


                             };
                return Json(new
                {
                    param.sEcho,
                    iTotalRecords = rowCount,
                    iTotalDisplayRecords = lnRowCount,
                    aaData = result
                },
                    JsonRequestBehavior.AllowGet);
            }
            else
            {

                int rowCount = getComments.RowCount;
                int lnRowCount = rowCount;

                var result = from q in getCommentsResult
                             select new
                             {
                                 q.Id,
                                 q.ArticleId,
                                 q.Email,
                                 q.Name,
                                 q.Comment,
                                 Date = q.Date.ToString("yyyy-MM-dd HH:mm"),
                                 q.IsApproved,
                                 q.OArticle.Title
                             };

                return Json(new
                {
                    param.sEcho,
                    iTotalRecords = rowCount,
                    iTotalDisplayRecords = lnRowCount,
                    aaData = result
                },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public PartialViewResult ViewComment(string id)
        {
            var oModel = new CommentModel();
            int cId = Convert.ToInt32(id);
            if (cId > 0)
            {
                var getComments = DAL.News.Comments.CommentsGet(new Comments { Id = cId });
                if (getComments.HasResult)
                {
                    oModel.OComment = getComments.Results.First();
                }
            }
            return PartialView("LayoutParts/_ViewComment", oModel);
        }
        public JsonResult ApproveComment(int id)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;
            var oResult = DAL.News.Comments.CommentApprove(id, User.Id);
            if (oResult.HasResult)
            {
                cStatus = "success";
                cMsg = Resources.NotifyMsg.SuccessMsg;
            }
            return Json(new { cStatus, cMsg, }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteComment(int id)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;
            var oResult = DAL.News.Comments.CommentDelete(id);
            if (oResult.HasResult)
            {
                cStatus = "success";
                cMsg = Resources.NotifyMsg.DeleteSuccessMsg;
            }
            return Json(new { cStatus, cMsg, }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region PagesHdr
        public ActionResult PagesHdr()
        {
            var oModel = new PagesHdrModel();
            var getPagesHdr = DAL.News.PagesHdr.GetPagesHdr(new PagesHdr());
            if (getPagesHdr.HasResult)
                oModel.LstPagesHdrs = getPagesHdr.Results;
            return View(oModel);
        }
        public JsonResult UpdatePagesHdr(string str)
        {
            string cStatus;
            string cMsg;

            var oPagesHdr = JsonConvert.DeserializeObject<List<PagesHdr>>(str);
            foreach (var s in oPagesHdr)
            {
                var oResult = DAL.News.PagesHdr.UpdatePageHdr(s);
                if (!oResult.HasResult)
                {
                    cStatus = "error";
                    cMsg = Resources.NotifyMsg.ErrorMsg;
                    return Json(new { cStatus, cMsg }, JsonRequestBehavior.AllowGet);
                }

            }

            cStatus = "success";
            cMsg = Resources.NotifyMsg.SaveSuccessMsg;

            return Json(new { cStatus, cMsg }, JsonRequestBehavior.AllowGet);

        }
        #endregion

        #region Visitors
        public ActionResult Visitors()
        {
            var oModel = new VisitorModel();

            return View(oModel);
        }
        public PartialViewResult SaveVisitorModal(string id)
        {
            var oModel = new InsertUpdateVisitorModel();
            var userId = Convert.ToInt32(id);
            if (userId > 0)
            {
                oModel.OVisitor.Id = userId;
                oModel.OVisitor.IsList = true;
                var getVisitor = DAL.News.Visitors.VisitorsGet(oModel.OVisitor);
                if (getVisitor.HasResult)
                    oModel.OVisitor = getVisitor.Results.FirstOrDefault();
            }
            return PartialView("VisitorParts/_SaveVisitorModal", oModel);
        }
        public JsonResult GetVisitorsDataTable(JQueryDataTableParamModel param)
        {
            var oVisitor = new Visitors();

            if (!string.IsNullOrEmpty(Request.QueryString["Name"]))
                oVisitor.Name = Request.QueryString["Name"];

            DataTableProcessModel m = new DataTableProcessModel();
            DataTableProcessModel dtProcess = DataTableProcesses.DataTableEslestir(param, m);
            oVisitor.SortCol = dtProcess.SortCol;
            oVisitor.SortType = dtProcess.SortType;
            oVisitor.Page = dtProcess.Page;
            oVisitor.RowPerPage = dtProcess.RowPerPage;
            var getVisitors = DAL.News.Visitors.VisitorsGet(oVisitor);

            var getVisitorsResult = new List<Visitors>();
            if (getVisitors.HasResult)
            {
                getVisitorsResult = getVisitors.Results;
                int rowCount = getVisitors.RowCount;
                int lnRowCount = rowCount;

                var result = from q in getVisitorsResult
                             select new
                             {
                                 q.Id,
                                 q.Name,
                                 q.IsApproved,
                                 q.Email,
                                 q.Avatar
                             };

                return Json(new
                {
                    param.sEcho,
                    iTotalRecords = rowCount,
                    iTotalDisplayRecords = lnRowCount,
                    aaData = result
                },
                    JsonRequestBehavior.AllowGet);
            }
            else
            {

                int rowCount = getVisitors.RowCount;
                int lnRowCount = rowCount;

                var result = from q in getVisitorsResult
                             select new
                             {
                                 q.Id,
                                 q.Name,
                                 q.IsApproved,
                                 q.Email,
                                 q.Avatar
                             };

                return Json(new
                {
                    param.sEcho,
                    iTotalRecords = rowCount,
                    iTotalDisplayRecords = lnRowCount,
                    aaData = result
                },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult SaveVisitor([Bind(Exclude = "Id")] Visitors oVisitor)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;
            if (ModelState.IsValid)
            {
                var id = oVisitor.Id;
                var oVisitorInsert = DAL.News.Visitors.AddEditVisitor(oVisitor);
                if (oVisitorInsert.HasResult)
                {
                    cStatus = "success";
                    cMsg = id > 0 ? Resources.NotifyMsg.UpdateSuccessMsg : Resources.NotifyMsg.InsertSuccessMsg;
                    return Json(new { cStatus, cMsg }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { cStatus = "notValid", cMsg = Resources.NotifyMsg.NotValidMsg }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { cStatus, cMsg }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UploadVisitorImg()
        {
            var fileName = "";
            var file = Request.Files[0];
            //var LogInUser = new UserAccounts();
            //var OUserType = new UserAccounts();

            if (file != null && file.ContentLength > 0)
            {
                //imageName = Path.GetFileNameWithoutExtension(file.FileName);
                fileName = Path.GetFileName(file.FileName);
                if (fileName != null)
                {
                    string ext = fileName.Split('.')[fileName.Split('.').Length - 1];
                    string n = Guid.NewGuid().ToString();
                    fileName = n + "." + ext;
                    var path = Path.Combine(Server.MapPath("/Content/UploadedFile/Account/Avatar/Original/"), fileName);
                    file.SaveAs(path);


                    var thumbPath = Path.Combine(Server.MapPath("/Content/UploadedFile/Account/Avatar/Thumbnail/"), fileName);
                    var largePath = Path.Combine(Server.MapPath("/Content/UploadedFile/Account/Avatar/Large/"), fileName);
                    GeneralHelper.ResizeImage(path, thumbPath, 250, ext, true);
                    GeneralHelper.ResizeImage(path, largePath, 950, ext, false);
                }
            }
            return Json(new { result = "success", Filename = fileName, }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ApproveVisitor(int id)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;

            var oResult = DAL.News.Visitors.VisitorApprove(id, User.Id);
            if (oResult.HasResult)
            {
                cStatus = "success";
                cMsg = Resources.NotifyMsg.DeleteSuccessMsg;
            }
            return Json(new { cStatus, cMsg, }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult DeleteVisitor(int id)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;

            var oResult = DAL.News.Visitors.VisitorDelete(id);
            if (oResult.HasResult)
            {
                cStatus = "success";
                cMsg = Resources.NotifyMsg.DeleteSuccessMsg;
            }
            return Json(new { cStatus, cMsg, }, JsonRequestBehavior.AllowGet);

        }

        //[Bind(Include = "Id,Name,Email,Mobile,Gender,Avatar")]
        //public PartialViewResult VisitorProfileModal()
        //{

        //    var oModel = new ProfileModel();

        //    if (User.Id > 0)
        //    {
        //        oModel.OVisitorProfile.Id = User.Id;
        //        var getUser = DAL.News.Visitors.VisitorProfileGet(oModel.OVisitorProfile);
        //        if (getUser.HasResult)
        //            oModel.OVisitorProfile = getUser.Results.FirstOrDefault();
        //    }

        //    return PartialView("VisitorParts/_VisitorProfileModel", oModel);
        //}
        public JsonResult UpdateVisitorProfile([Bind(Exclude = "Pass")] Visitors oVisitor)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;
            if (ModelState.IsValid)
            {
                var oUserUpdate = DAL.News.Visitors.AddEditVisitor(oVisitor);
                if (oUserUpdate.HasResult)
                {
                    cStatus = "success";
                    cMsg = Resources.NotifyMsg.UpdateSuccessMsg;
                    return Json(new { cStatus, cMsg }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { cStatus = "notValid", cMsg = Resources.NotifyMsg.NotValidMsg }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { cStatus, cMsg }, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult ChangePasswordModal()
        {
            return PartialView("VisitorParts/_ChangePasswordModel");
        }
        //public JsonResult UpdatePassword([Bind(Include = "CurrentPassword,NewPassword,ConfirmPassword")] VisitorProfile oVisitorProfile)
        //{
        //    string cStatus;
        //    string cMsg;
        //    String oldPass = oVisitorProfile.CurrentPassword;
        //    String newPass = oVisitorProfile.NewPassword;
        //    //String confirmPass = oUserProfile.ConfirmPassword;
        //    int tryNo = oVisitorProfile.tryNo;
        //    if (tryNo < 4)
        //    {
        //        string encrpOldPass = Common.Md5(oldPass);
        //        if (User.Password == encrpOldPass)
        //        {

        //            var oProfile = new UserAccounts();
        //            oProfile.Id = User.Id;
        //            oProfile.Pass = newPass;
        //            var oUserPassWord = DAL.Account.UserAccounts.AddEditAccount(oProfile);
        //            if (oUserPassWord.HasResult)
        //            {
        //                cStatus = "success";
        //                cMsg = Resources.NotifyMsg.UpdateSuccessMsg;
        //                return Json(new { cStatus, cMsg }, JsonRequestBehavior.AllowGet);
        //            }

        //            return Json(new { cStatus = "notValid", cMsg = Resources.NotifyMsg.NotValidMsg }, JsonRequestBehavior.AllowGet);


        //        }

        //        return Json(new { cStatus = "notValid", cMsg = Resources.NotifyMsg.ErrorIncorrect }, JsonRequestBehavior.AllowGet);

        //    }

        //    FormsAuthentication.SignOut();
        //    return Json(new
        //    {
        //        cStatus = "error",
        //        cMsg = Resources.NotifyMsg.ErrorPass,
        //        isRedirect = true,
        //        redirectUrl = Url.Action("LoginRedirect", "Login")
        //    }, JsonRequestBehavior.AllowGet);

        //}
        public JsonResult VisitorsSearchAutoComplete(string id)
        {
            var getVisitorsResult = new List<Visitors>();
            var getVisitors = DAL.News.Visitors.VisitorsGet(new Visitors { Name = id, IsList = true });

            if (getVisitors.HasResult)
            {
                getVisitorsResult = getVisitors.Results;
                var result = from q in getVisitorsResult
                             select new
                             {
                                 q.Name,
                                 q.Id
                             };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(getVisitorsResult, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region NewsletterSubscribers
        public ActionResult NewsletterSubscribers()
        {
            return View();
        }
        public JsonResult GetNewsletterSubscribersDataTable(JQueryDataTableParamModel param)
        {
            var oSubscriber = new NewsletterSubscribers();

            DataTableProcessModel m = new DataTableProcessModel();
            DataTableProcessModel dtProcess = DataTableProcesses.DataTableEslestir(param, m);
            oSubscriber.SortCol = dtProcess.SortCol;
            oSubscriber.SortType = dtProcess.SortType;
            oSubscriber.Page = dtProcess.Page;
            oSubscriber.RowPerPage = dtProcess.RowPerPage;

            var getNewsletterSubscribers = DAL.News.NewsletterSubscribers.NewsletterSubscribersGet(oSubscriber);

            var getNewsletterSubscribersResult = new List<DTO.News.NewsletterSubscribers>();
            if (getNewsletterSubscribers.HasResult)
            {
                getNewsletterSubscribersResult = getNewsletterSubscribers.Results;

                int rowCount = getNewsletterSubscribers.RowCount;
                int lnRowCount = rowCount;

                var result = from q in getNewsletterSubscribersResult
                             select new
                             {
                                 q.Id,
                                 q.Email,
                                 q.IsActive


                             };
                return Json(new
                {
                    param.sEcho,
                    iTotalRecords = rowCount,
                    iTotalDisplayRecords = lnRowCount,
                    aaData = result
                },
                    JsonRequestBehavior.AllowGet);
            }
            else
            {

                int rowCount = getNewsletterSubscribers.RowCount;
                int lnRowCount = rowCount;

                var result = from q in getNewsletterSubscribersResult
                             select new
                             {
                                 q.Id,
                                 q.Email,
                                 q.IsActive
                             };

                return Json(new
                {
                    param.sEcho,
                    iTotalRecords = rowCount,
                    iTotalDisplayRecords = lnRowCount,
                    aaData = result
                },
                    JsonRequestBehavior.AllowGet);
            }
        }
        public JsonResult NewsletterSubscriberChangeStatus(string id, bool status)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;
            int cId = Convert.ToInt32(id);
            if (cId > 0)
            {
                var changeStatus = DAL.News.NewsletterSubscribers.NewsletterSubscriberChangeStatus(cId, status);
                if (changeStatus.HasResult)
                { 
                    cStatus = "success";
                    cMsg = Resources.NotifyMsg.DeleteSuccessMsg;
                }
            }
            return Json(new { cStatus, cMsg, }, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}