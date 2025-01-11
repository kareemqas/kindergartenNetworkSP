using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace kindergartenNetwork.Helper
{
    public class LocalizedControllerActivator : IControllerActivator
    {
        private string _DefaultLanguage = "ar";

        public IController Create(RequestContext requestContext, Type controllerType)
        {
            string lang = (requestContext.RouteData.Values["lang"] ?? _DefaultLanguage) as string;

            if (lang != _DefaultLanguage)
            {
                try
                {
                    CultureInfo newCulture = new CultureInfo(lang == "ar" ? "ar-eg":"en-us");
                    newCulture.DateTimeFormat.ShortDatePattern = "dd-MM-yyyy";
                    newCulture.DateTimeFormat.DateSeparator = "-";
                    Thread.CurrentThread.CurrentCulture = Thread.CurrentThread.CurrentUICulture = newCulture;
                }
                catch (Exception)
                {
                    throw new NotSupportedException($"ERROR: Invalid language code '{lang}'.");
                }
            }

            return DependencyResolver.Current.GetService(controllerType) as IController;
        }
    }
}