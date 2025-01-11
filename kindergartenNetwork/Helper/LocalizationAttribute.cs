using System;
using System.Globalization;
using System.Threading;
using System.Web;
using System.Web.Mvc;

namespace kindergartenNetwork.Helper
{
    class LocalizationAttribute : ActionFilterAttribute
    {
        private string _DefaultLanguage = "en-us";

        public LocalizationAttribute(string defaultLanguage)
        {
            _DefaultLanguage = defaultLanguage;
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string lang = (string)filterContext.RouteData.Values["lang"] ?? _DefaultLanguage;
            HttpCookie cookie = new HttpCookie("Language") { Value = lang };
            cookie.Expires = DateTime.Now.AddYears(2);
            filterContext.HttpContext.ApplicationInstance.Response.Cookies.Add(cookie);
            Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
            if (lang != _DefaultLanguage)
            {
                try
                {
                    Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = new CultureInfo(lang);
                }
                catch (Exception)
                {
                    throw new NotSupportedException($"ERROR: Invalid language code '{lang}'.");
                }
            }

        }
    }
}
