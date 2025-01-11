using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using kindergartenNetwork.Models;

namespace kindergartenNetwork.Controllers
{
    [RoutePrefix("DefaultLocalized")]
    public class PublicBaseController : Controller
    {
        public int LangId { get; set; }
        protected virtual new CustomPrincipal User => HttpContext.User as CustomPrincipal;
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            filterContext.Controller.ViewData["User"] = User;
            var fileContents = System.IO.File.ReadAllText(Server.MapPath(@"~/App_Data/Language.json"));
            var languages = JsonConvert.DeserializeObject<List<Models.Common.LanguageModel>>(fileContents);
            var langob = languages.Find(q => q.TowLetterCode.ToLower() == Convert.ToString(filterContext.RouteData.Values["lang"] ?? "ar").ToLower());
            ViewBag.LangId = langob.Id;
            ViewBag.LangCode = langob.Code;
            ViewBag.LangDir = langob.Dir;
            ViewBag.LangTowLetterCode = langob.TowLetterCode;
            ViewBag.LangName = langob.Name;

            ViewBag.MetaDescription = Resources.PublicNews.ProjectName;
            ViewBag.MetaKeywords = "";

            var getAppSetting = DAL.News.AppSettings.AppSettingsGet(0);
            var appSettingResult = new List<DTO.News.AppSettings>();
            if (getAppSetting.HasResult)
                appSettingResult = getAppSetting.Results;

            ViewBag.IsPublicSiteOpen = appSettingResult.FirstOrDefault(q => q.ConKey == 18).ConValue;
            ViewBag.AllowComments = appSettingResult.FirstOrDefault(q => q.ConKey == 21).ConValue;
            if (!HttpContext.User.Identity.IsAuthenticated)
            {
                if (Convert.ToInt32(ViewBag.IsPublicSiteOpen) == 19)
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "System", action = "ComingSoon" }));
                }
            }
        }
    }
}