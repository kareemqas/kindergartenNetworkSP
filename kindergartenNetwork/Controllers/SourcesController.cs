using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DTO.News;

namespace kindergartenNetwork.Controllers
{
    public class SourcesController : PublicBaseController
    {
        // GET: Sources
        public ActionResult EducationalResources(int? page, int? category, string search)
        {
            var oModel = new Models.NewsModels.EducationalResourceModel();
            oModel.LstFileTypes = Helper.GeneralHelper.GetConstants(1);
            if (!page.HasValue)
            {
                page = 1;
            }
            var oResource = new EducationalResources();
            oResource.Page = Convert.ToInt32(page);
            oResource.RowPerPage = 9;
            oResource.SortCol = "InsertedDate";
            oResource.SortType = "desc";
            if (category.HasValue)
            {
                var catId = Convert.ToInt32(category);
                if (catId > 0)
                    oResource.FileType = catId;
            }
            if (!string.IsNullOrEmpty(search))
            {
                oResource.FileTitle = search;
            }
            var getEducationalResources = DAL.News.EducationalResources.AttachmentGet(oResource);
            if (getEducationalResources.HasResult)
            {
                oModel.LstEducationalResources = getEducationalResources.Results;
                oModel.Count = getEducationalResources.RowCount;
                return View(oModel);
            }

            return RedirectToAction("index", "Home");
        }

        public ActionResult Instructors(int? page)
        {
            var oModel = new Models.NewsModels.TeamMembersModel();
            if (!page.HasValue)
            {
                page = 1;
            }
            var getTeamMembers = DAL.News.TeamMembers.TeamMembersGet(new TeamMembers {Page = Convert.ToInt32(page), RowPerPage = 8, SortCol = "Id", SortType = "desc" });
            if (getTeamMembers.HasResult)
            {
                oModel.LstTeamMembers = getTeamMembers.Results;
                oModel.Count = getTeamMembers.RowCount;
            }
            else
                return RedirectToAction("index", "Home");
            return View(oModel);
        }
    }
}