using System;
using System.Collections.Generic;
using System.Linq;
using DTO.Account;
using kindergartenNetwork.Models;

namespace kindergartenNetwork.Helper
{
    public class CommonHelpExtension
    {
        //public static List<Pages> GetMenuPages(int userTypeId, bool inMenu)
        //{
        //    var oPage = new Pages();
        //    if (inMenu)
        //    {
        //        oPage.InMenu = true;
        //    }
        //    var getPages = DAL.Account.UserTypePages.GetUserTypePages(new UserTypePages { UserTypeId = userTypeId }, oPage);
        //    if (getPages.HasResult)
        //    {
        //        var getPagesResult = getPages.Results;
        //        foreach (var page in getPagesResult)
        //        {
        //            page.ChildPages = getPagesResult.Where(x => x.ParentId == page.Id).Where(x => x.InMenu != null && x.InMenu.Value).ToList();
        //        }
        //        return getPagesResult;
        //    }

        //    return new List<Pages>();
        //}

        public static List<Pages> GetMenuPages(int userTypeId, bool inMenu)
        {
            var oPage = new Pages();
            if (inMenu)
            {
                oPage.InMenu = true;
            }
            var getPages = DAL.Account.UserTypePages.GetUserTypePages(new UserTypePages { UserTypeId = userTypeId }, oPage);
            if (getPages.HasResult)
            {
                var getPagesResult = getPages.Results;
                foreach (var page in getPagesResult)
                {
                    page.ChildPages = getPagesResult.Where(x => x.ParentId == page.Id).Where(x => x.InMenu != null && x.InMenu.Value).ToList();
                }
                return getPagesResult;
            }
            else
                return new List<Pages>();
        }
        // for any date stored in UTC - need to display by timezone
        public static DateTime ConvertToTimeZone(DateTime date, string defaultTimeZone)
        {
            var dtZone = TimeZoneInfo.ConvertTime(
                DateTime.SpecifyKind(date, DateTimeKind.Utc),
                TimeZoneInfo.FindSystemTimeZoneById(defaultTimeZone));
            return dtZone;
        }

        // for any datetime.now or search by dates use this
        public static DateTime ConvertToUtc(DateTime date)
        {
            var dtZone = date;
            if (date.Hour == 0)
            {
                dtZone = dtZone.AddHours(DateTime.Now.Hour);
                dtZone = dtZone.AddMinutes(DateTime.Now.Minute);
                dtZone = dtZone.AddSeconds(DateTime.Now.Second);
            }
            dtZone = dtZone.ToUniversalTime();
            return dtZone;
        }


        // for any datetime.now or search by dates use this
        public static DateTime ConvertToUTC(DateTime date)
        {
            var dtZone = date;
            if (date.Hour == 0)
            {
                dtZone = dtZone.AddHours(DateTime.Now.Hour);
                dtZone = dtZone.AddMinutes(DateTime.Now.Minute);
                dtZone = dtZone.AddSeconds(DateTime.Now.Second);
            }
            dtZone = dtZone.ToUniversalTime();
            return dtZone;
        }
    }
}