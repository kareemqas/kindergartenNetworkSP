using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;
using kindergartenNetwork.Models;
using Newtonsoft.Json;

namespace kindergartenNetwork
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }

        protected void Application_PostAuthenticateRequest(object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie != null)
            {
                FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                CustomPrincipalSerializeModel serializeModel = JsonConvert.DeserializeObject<CustomPrincipalSerializeModel>(authTicket.UserData);
                CustomPrincipal obUser = new CustomPrincipal(authTicket.Name)
                {
                    Id = serializeModel.Id,
                    Name = serializeModel.Name,
                    Email = serializeModel.Email,
                    Mobile = serializeModel.Mobile,
                    Gender = serializeModel.Gender,
                    Avatar = serializeModel.Avatar,
                    UserTypeId = serializeModel.UserTypeId,
                    UserTypeName = serializeModel.UserTypeName,
                    Roles = serializeModel.Roles,
                    ManagerGroupId = serializeModel.ManagerGroupId,
                    TraceUserActivity = serializeModel.TraceUserActivity

                };
                HttpContext.Current.User = obUser;
            }
        }
        protected void Application_Error(object sender, EventArgs e)
        {
            Exception exception = Server.GetLastError();
            var oErrorLog = new DTO.Account.ErrorsLogs();
            oErrorLog.ErrorMessage = exception.Message;
            oErrorLog.Browser = Request.RequestContext.HttpContext.Request.Browser.Browser;
            oErrorLog.UserAgent = Request.RequestContext.HttpContext.Request.UserAgent;
            oErrorLog.IP = Request.RequestContext.HttpContext.Request.UserHostAddress;
            oErrorLog.RequestType = Request.RequestContext.HttpContext.Request.RequestType;
            oErrorLog.IsAjax = Request.RequestContext.HttpContext.Request.IsAjaxRequest();
            oErrorLog.Link = Request.RequestContext.HttpContext.Request.Url.ToString();
            if (!string.IsNullOrEmpty(oErrorLog.PostedData))
                oErrorLog.PostedData = Server.UrlDecode(Request.RequestContext.HttpContext.Request.Form.ToString());
            if (oErrorLog.RequestType.ToLower() == "get")
                oErrorLog.PostedData = Server.UrlDecode(Request.RequestContext.HttpContext.Request.QueryString.ToString());
            DAL.Account.ErrorsLogs.ErrorLogsInsert(oErrorLog);
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            CultureInfo newCulture = (CultureInfo)Thread.CurrentThread.CurrentCulture.Clone();
            newCulture.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
            newCulture.DateTimeFormat.DateSeparator = "-";
            Thread.CurrentThread.CurrentCulture = newCulture;

            HttpCookie cookie = HttpContext.Current.Request.Cookies["Language"];
            if (cookie?.Value != null)
            {
                Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo(cookie.Value);
                Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
                Thread.CurrentThread.CurrentCulture.DateTimeFormat.DateSeparator = "-";

            }
            else
            {
                Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo("ar-eg");
                Thread.CurrentThread.CurrentCulture.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
                Thread.CurrentThread.CurrentCulture.DateTimeFormat.DateSeparator = "-";
            }
        }
    }
}
