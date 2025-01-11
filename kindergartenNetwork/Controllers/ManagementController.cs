 using System;
using System.Collections.Generic;
 using System.Data.Entity;
 using System.Data.SqlClient;
 using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
 using System.Web.UI.WebControls;
 using DAL;
 using DTO.Account;
 using EntityFrameworkPaginate;
 using kindergartenNetwork.Helper;
using kindergartenNetwork.Models.DataTableModels;
using Newtonsoft.Json;
using kindergartenNetwork.Models;
 using kindergartenNetwork.Controllers;
 using Microsoft.Ajax.Utilities;


 namespace kindergartenNetwork.Controllers
{
    public class ManagementController : BaseController
    {
        // GET: Management
        //public ActionResult Index()
        //{
        //    return View();
        //}

        #region Constant
        public ActionResult Constant()
        {
            var oModel = new Models.MainModels.ConstantModel();
            var getParentConstant = DAL.Account.Constant.ConstantGet(new Constant { ParentId = 0, IsList = true });
            if (getParentConstant.HasResult)
            {
                oModel.LstConstants = getParentConstant.Results;
            }
            return View(oModel);
        }
        public JsonResult GetConstantDataTable(JQueryDataTableParamModel param)
        {
            var oConstant = new Constant();


            if (!string.IsNullOrEmpty(Request.QueryString["ConstantId"]))
                oConstant.Id = Convert.ToInt32(Request.QueryString["ConstantId"]);
            if (!string.IsNullOrEmpty(Request.QueryString["ParentId"]))
                oConstant.ParentId = Convert.ToInt32(Request.QueryString["ParentId"]);

            DataTableProcessModel m = new DataTableProcessModel();
            DataTableProcessModel dtProcess = DataTableProcesses.DataTableEslestir(param, m);
            oConstant.SortCol = dtProcess.SortCol;
            oConstant.SortType = dtProcess.SortType;
            oConstant.Page = dtProcess.Page;
            oConstant.RowPerPage = dtProcess.RowPerPage;
            var getConstant = DAL.Account.Constant.ConstantGet(oConstant);

            var getConstantResult = new List<Constant>();
            if (getConstant.HasResult)
            {
                getConstantResult = getConstant.Results;

                int rowCount = getConstant.RowCount;
                int lnRowCount = rowCount;

                var result = from q in getConstantResult
                             select new
                             {
                                 q.Id,
                                 q.Name,
                                 ParentName = q.OParent.Name,
                                 q.ParentId,
                                 q.Comment,
                                 q.Icon
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

                int rowCount = getConstant.RowCount;
                int lnRowCount = rowCount;

                var result = from q in getConstantResult
                             select new
                             {
                                 q.Id,
                                 q.Name,
                                 ParentNameAr = q.OParent.Name,
                                 q.ParentId,
                                 q.Comment,
                                 q.Icon
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
        public PartialViewResult InsertConstantModal()
        {
            var oModel = new Models.MainModels.ConstantModel();
            var getParentConstant = DAL.Account.Constant.ConstantGet(new Constant { ParentId = 0, IsList = true });
            if (getParentConstant.HasResult)
            {
                oModel.LstConstants = getParentConstant.Results;
            }
            return PartialView("ConstantParts/_ConstantInsertModal", oModel);
        }
        public PartialViewResult UpdateConstantModal(string id)
        {
            var constantId = Convert.ToInt32(id);
            var oModel = new Models.MainModels.UpdateConstantModel();
            var getParentConstant = DAL.Account.Constant.ConstantGet(new Constant { ParentId = 0, IsList = true });
            if (getParentConstant.HasResult)
            {
                oModel.LstConstants = getParentConstant.Results;
            }
            if (constantId > 0)
            {
                var getConstant =
                    DAL.Account.Constant.ConstantGet(new Constant { Id = constantId, IsList = true });
                if (getConstant.HasResult)
                    oModel.OConstant = getConstant.Results.FirstOrDefault();
            }
            return PartialView("ConstantParts/_ConstantUpdateModal", oModel);
        }
        public JsonResult InsertConstant([Bind(Exclude = "Id")] Constant oConstant)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;
            if (ModelState.IsValid)
            {
                var oUserTypeInsert = DAL.Account.Constant.ConstantInsert(oConstant);
                if (oUserTypeInsert.HasResult)
                {
                    cStatus = "success";
                    cMsg = Resources.NotifyMsg.InsertSuccessMsg;
                    return Json(new { cStatus, cMsg }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { cStatus = "notValid", cMsg = GeneralHelper.GetErrorMessage(ModelState, Resources.NotifyMsg.ErrorInField) }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { cStatus, cMsg }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateConstant(Constant oConstant)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;
            if (ModelState.IsValid)
            {
                var oUserTypeInsert = DAL.Account.Constant.ConstantUpdate(oConstant);
                if (oUserTypeInsert.HasResult)
                {
                    cStatus = "success";
                    cMsg = Resources.NotifyMsg.UpdateSuccessMsg;
                    return Json(new { cStatus, cMsg }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { cStatus = "notValid", cMsg = GeneralHelper.GetErrorMessage(ModelState, Resources.NotifyMsg.ErrorInField) }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { cStatus, cMsg }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult DeleteConstant(int id)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;
            var oConstant = new Constant();
            oConstant.Id = Convert.ToInt32(id);

            var oResult = DAL.Account.Constant.DeleteConstant(oConstant);
            if (oResult.HasResult)
            {
                cStatus = "success";
                cMsg = Resources.NotifyMsg.DeleteSuccessMsg;
            }
            return Json(new { cStatus, cMsg }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult ConstantSearchAutoComplete(string id)
        {
            var getConstantResult = new List<Constant>();
            var getConstant = DAL.Account.Constant.ConstantGet(new Constant { IsList = true, Name = id.Trim() });

            if (getConstant.HasResult)
            {
                getConstantResult = getConstant.Results;
                var result = from q in getConstantResult
                             select new
                             {
                                 q.Name,
                                 q.Id
                             };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(getConstantResult, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Users
        public ActionResult Users()
        {
            var oModel = new Models.MainModels.UserModel();
            var isNotSuper = User.UserTypeId != 1;

            var getLstType = DAL.Account.UserType.GetUserType(new UserType());
            if (getLstType.HasResult)
                oModel.LstUserTypes = getLstType.Results;
            return View(oModel);
        }
        public PartialViewResult InsertUsersModal()
        {
            var oModel = new Models.MainModels.UserModel();
            var isNotSuper = User.UserTypeId != 1;
            var getLstType = DAL.Account.UserType.GetUserType(new UserType());
            if (getLstType.HasResult)
                oModel.LstUserTypes = getLstType.Results;

            return PartialView("UserParts/_UserInsertModal", oModel);
        }
        public PartialViewResult UpdateUsersModal(string id)
        {
            var usertId = Convert.ToInt32(id);
            var oModel = new Models.MainModels.InsertUpdateUserModel();
            var isNotSuper = User.UserTypeId != 1;
            var getLstType = DAL.Account.UserType.GetUserType(new UserType());
            if (getLstType.HasResult)
                oModel.LstUserTypes = getLstType.Results;

            if (usertId > 0)
            {
                oModel.OUserAccounts.Id = usertId;
                oModel.OUserAccounts.IsList = true;
                var getUser = DAL.Account.UserAccounts.UserAccountGet(oModel.OUserAccounts);
                if (getUser.HasResult)
                    oModel.OUserAccounts = getUser.Results.FirstOrDefault();
            }

            return PartialView("UserParts/_UserUpdateModal", oModel);
        }
        public JsonResult GetUserDataTable(JQueryDataTableParamModel param)
        {
            var oUser = new UserAccounts();

            if (!string.IsNullOrEmpty(Request.QueryString["Name"]))
                oUser.Name = Request.QueryString["Name"];
            if (!string.IsNullOrEmpty(Request.QueryString["UserTypeId"]))
                oUser.UserTypeId = Convert.ToInt32(Request.QueryString["UserTypeId"]);

            DataTableProcessModel m = new DataTableProcessModel();
            DataTableProcessModel dtProcess = DataTableProcesses.DataTableEslestir(param, m);
            oUser.SortCol = dtProcess.SortCol;
            oUser.SortType = dtProcess.SortType;
            oUser.Page = dtProcess.Page;
            oUser.RowPerPage = dtProcess.RowPerPage;
            var getUsers = DAL.Account.UserAccounts.UserAccountGet(oUser);

            var getUserResult = new List<UserAccounts>();
            if (getUsers.HasResult)
            {
                getUserResult = getUsers.Results;
                if (User.Id != 1)
                {
                    var toRemove = getUserResult.Single(q => q.Id == 1);
                    getUserResult.Remove(toRemove);
                }
                int rowCount = getUsers.RowCount;
                int lnRowCount = rowCount;

                var result = from q in getUserResult
                             select new
                             {
                                 q.Id,
                                 q.Name,
                                 q.IsActive,
                                 UserTypeName = q.OUserType.Name,
                                 q.Email,
                                 q.Avatar,
                                 q.Mobile,
                                 q.Gender

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

                int rowCount = getUsers.RowCount;
                int lnRowCount = rowCount;

                var result = from q in getUserResult
                             select new
                             {
                                 q.Id,
                                 q.Name,
                                 q.IsActive,
                                 UserTypeName = q.OUserType.Name,
                                 q.Email,
                                 q.Avatar,
                                 q.Mobile,
                                 q.Gender
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
        public JsonResult InsertUser([Bind(Exclude = "Id")] UserAccounts oUserAccounts)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;
            if (ModelState.IsValid)
            {
                var oUserInsert = DAL.Account.UserAccounts.UserAccountInsert(oUserAccounts);
                if (oUserInsert.HasResult)
                {
                    cStatus = "success";
                    cMsg = Resources.NotifyMsg.InsertSuccessMsg;
                    return Json(new { cStatus, cMsg }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { cStatus = "notValid", cMsg = Resources.NotifyMsg.NotValidMsg }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { cStatus, cMsg }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdateUser(UserAccounts oUserAccounts)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;
            if (ModelState.IsValid)
            {
                var oUserUpdate = DAL.Account.UserAccounts.UserAccountUpdate(oUserAccounts);
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
        public JsonResult UploadUserImg()
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
        public JsonResult DeleteUserAccount(int id)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;
            var oUser = new UserAccounts();
            oUser.Id = Convert.ToInt32(id);

            var oResult = DAL.Account.UserAccounts.DeleteUserAccount(oUser);
            if (oResult.HasResult)
            {
                cStatus = "success";
                cMsg = Resources.NotifyMsg.DeleteSuccessMsg;
            }
            return Json(new { cStatus, cMsg, }, JsonRequestBehavior.AllowGet);

        }

        //[Bind(Include = "Id,Name,Email,Mobile,Gender,Avatar")]
        public PartialViewResult MyProfileModal()
        {

            var oModel = new Models.MainModels.ProfileModel();

            if (User.Id > 0)
            {
                oModel.OUserProfile.Id = User.Id;
                var getUser = DAL.Account.UserAccounts.UserProfileGet(oModel.OUserProfile);
                if (getUser.HasResult)
                    oModel.OUserProfile = getUser.Results.FirstOrDefault();
            }

            return PartialView("UserParts/_UserProfileModel", oModel);
        }
        public JsonResult UpdateMyProfile([Bind(Exclude = "UserTypeId,IsActive,IsDeleted,EmailPassword,ManagerGroupId,TraceUserActivity,Pass")] UserAccounts oUserAccounts)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;
            if (ModelState.IsValid)
            {
                var oUserUpdate = DAL.Account.UserAccounts.AddEditAccount(oUserAccounts);
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
            return PartialView("UserParts/_ChangePasswordModel");
        }
        public JsonResult UpdatePassword([Bind(Include = "CurrentPassword,NewPassword,ConfirmPassword")] UserProfile oUserProfile)
        {
            string cStatus;
            string cMsg;
            String oldPass = oUserProfile.CurrentPassword;
            String newPass = oUserProfile.NewPassword;
            //String confirmPass = oUserProfile.ConfirmPassword;
            int tryNo = oUserProfile.tryNo;
            if (tryNo < 4)
            {
                string encrpOldPass = Common.Md5(oldPass);
                if (User.Password == encrpOldPass)
                {

                    var oProfile = new UserAccounts();
                    oProfile.Id = User.Id;
                    oProfile.Pass = newPass;
                    var oUserPassWord = DAL.Account.UserAccounts.AddEditAccount(oProfile);
                    if (oUserPassWord.HasResult)
                    {
                        cStatus = "success";
                        cMsg = Resources.NotifyMsg.UpdateSuccessMsg;
                        return Json(new { cStatus, cMsg }, JsonRequestBehavior.AllowGet);
                    }

                    return Json(new { cStatus = "notValid", cMsg = Resources.NotifyMsg.NotValidMsg }, JsonRequestBehavior.AllowGet);


                }

                return Json(new { cStatus = "notValid", cMsg = Resources.NotifyMsg.ErrorIncorrect }, JsonRequestBehavior.AllowGet);

            }

            FormsAuthentication.SignOut();
            return Json(new
            {
                cStatus = "error",
                cMsg = Resources.NotifyMsg.ErrorPass,
                isRedirect = true,
                redirectUrl = Url.Action("LoginRedirect", "Login")
            }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult UsersSearchAutoComplete(string id)
        {
            var getUsersResult = new List<UserAccounts>();
            var getUsers = DAL.Account.UserAccounts.UserAccountGet(new UserAccounts { Name = id, IsList = true });

            if (getUsers.HasResult)
            {
                getUsersResult = getUsers.Results;
                var result = from q in getUsersResult
                             select new
                             {
                                 q.Name,
                                 q.Id
                             };
                return Json(result, JsonRequestBehavior.AllowGet);
            }
            return Json(getUsersResult, JsonRequestBehavior.AllowGet);
        }

        #endregion
        #region UserTypes
        public ActionResult UserTypes()
        {
            return View();
        }
        public PartialViewResult UserTypePartial(int? id)
        {

            var oModel = new Models.MainModels.UserTypeModel();
            if (id != 0)
            {
                oModel.OUserType.Id = Convert.ToInt32(id);

                if (oModel.OUserType.Id > 0)
                {
                    oModel.OUserType.Id = oModel.OUserType.Id;
                    oModel.OUserType.IsList = true;

                    var isNotSuper = User.UserTypeId != 1;
                    var oResult = DAL.Account.UserType.GetUserType(oModel.OUserType);
                    if (oResult.HasResult)
                    {
                        oModel.OUserType = oResult.Results.FirstOrDefault();
                    }
                    else
                        Response.Redirect("/Error/Error");
                }
            }
            else
                oModel.OUserType = new UserType();
            return PartialView("UserParts/_UserAddEditType", oModel);
        }
        public JsonResult AddEditUserType(UserType oUserType)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;

            if (!string.IsNullOrEmpty(oUserType.Name))
            {
                var oResult = DAL.Account.UserType.AddEditUserType(oUserType);
                if (oResult.HasResult)
                {
                    cStatus = "success";
                    cMsg = Resources.NotifyMsg.InsertSuccessMsg;
                }
            }
            return Json(new { cStatus, cMsg }, JsonRequestBehavior.AllowGet);
        }

        public JsonResult DeleteUserType(int id)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;
            var oUserType = new UserType();
            oUserType.Id = Convert.ToInt32(id);

            var oResult = DAL.Account.UserType.Delete(oUserType);
            if (oResult.HasResult)
            {
                cStatus = "success";
                cMsg = Resources.NotifyMsg.DeleteSuccessMsg;
            }
            return Json(new { cStatus, cMsg, }, JsonRequestBehavior.AllowGet);

        }

        public JsonResult UserTypeTableAjax(JQueryDataTableParamModel param)
        {
            var oUserType = new UserType();
            if (!string.IsNullOrEmpty(Request.QueryString["Name"]))
            {
                oUserType.Name = Request.QueryString["Name"];
            }

            DataTableProcessModel m = new DataTableProcessModel();
            DataTableProcessModel dtProcess = DataTableProcesses.DataTableEslestir(param, m);
            oUserType.SortCol = dtProcess.SortCol;
            oUserType.SortType = dtProcess.SortType;
            oUserType.Page = dtProcess.Page;
            oUserType.RowPerPage = dtProcess.RowPerPage;
            var isNotSuper = User.UserTypeId != 1;
            var oResultModel = DAL.Account.UserType.GetUserType(oUserType);

            var lstUserType = new List<UserType>();
            if (oResultModel.HasResult)
            {
                lstUserType = oResultModel.Results;

                int rowCount = oResultModel.RowCount;
                int lnRowCount = rowCount;

                var result = from u in lstUserType
                             select new
                             {
                                 u.Id,
                                 u.Name,

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
                int rowCount = oResultModel.RowCount;
                int lnGoster = rowCount;

                var result = from u in lstUserType
                             select new
                             {

                                 u.Id,
                                 u.Name,
                             };

                return Json(new
                {
                    param.sEcho,
                    iTotalRecords = rowCount,
                    iTotalDisplayRecords = lnGoster,
                    aaData = result
                },
                    JsonRequestBehavior.AllowGet);
            }


        }

        #endregion
        #region UserPermission
        public ActionResult UserPermission()
        {
            var oModel = new Models.MainModels.UserPermissionModel();

            var oUserType = new UserType();
            oUserType.IsList = true;
            var isNotSuper = User.UserTypeId != 1;
            var oResult = DAL.Account.UserType.GetUserType(oUserType);
            if (oResult.HasResult)
            {
                oModel.LstUserTypes = oResult.Results;
            }

            var oUserTypePages = new UserTypePages();
            oUserTypePages.UserTypeId = User.UserTypeId;
            //var oResulPages = DAL.Account.UserTypePages.IsValidAdminTypePage(oUserTypePages);

            var page = new Pages();

            var oResulPages = DAL.Account.UserTypePages.GetUserTypePages(oUserTypePages, page);

            if (oResulPages.HasResult)
            {
                oModel.LstPages = oResulPages.Results;
            }
            return View(oModel);
        }
        public JsonResult GetUserTypePermission(string id)
        {
            var cResult = "Error";
            var cMessage = "something wrong!";
            var lstPages = new List<Pages>();

            if (!string.IsNullOrEmpty(id))
            {
                int oUserTypeId = Convert.ToInt32(id);
                var oUserTypePages = new UserTypePages();
                oUserTypePages.UserTypeId = oUserTypeId;
                var oPages = new Pages();

                var oResult = DAL.Account.UserTypePages.GetUserTypePages(oUserTypePages, oPages);
                if (oResult.HasResult)
                {
                    lstPages = oResult.Results;
                    cResult = "OK";
                    cMessage = "Successfuly";
                }

            }

            return Json(new
            {
                cResult,
                cMessage,
                lstPages
            },
            JsonRequestBehavior.AllowGet);
        }

        public JsonResult SaveUserPermission(int userTypeId, string pages)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;
            var oPageInsert = DAL.Account.UserTypePages.AddUserTypePages(userTypeId, pages);
            if (oPageInsert.HasResult)
            {
                cStatus = "success";
                cMsg = Resources.NotifyMsg.SuccessMsg;
                return Json(new { cStatus, cMsg }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { cStatus, cMsg }, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region Pages
        public ActionResult Pages()
        {
            var oModel = new Models.MainModels.PageModel();

            var getLstType = DAL.Account.Pages.PagesGet(new Pages { IsList = true }, true);
            if (getLstType.HasResult)
                oModel.LstParent = getLstType.Results;
            return View(oModel);
        }
        public JsonResult GetPagesDataTable(JQueryDataTableParamModel param)
        {
            var oPages = new Pages();

            if (!string.IsNullOrEmpty(Request.QueryString["PageId"]))
                oPages.Id = Convert.ToInt32(Request.QueryString["PageId"]);
            if (!string.IsNullOrEmpty(Request.QueryString["ParentId"]))
                oPages.ParentId = Convert.ToInt32(Request.QueryString["ParentId"]);

            DataTableProcessModel m = new DataTableProcessModel();
            DataTableProcessModel dtProcess = DataTableProcesses.DataTableEslestir(param, m);
            oPages.SortCol = dtProcess.SortCol;
            oPages.SortType = dtProcess.SortType;
            oPages.Page = dtProcess.Page;
            oPages.RowPerPage = dtProcess.RowPerPage;
            var getPages = DAL.Account.Pages.PagesGet(oPages, false);

            var getPagesResult = new List<Pages>();
            if (getPages.HasResult)
            {
                getPagesResult = getPages.Results;

                int rowCount = getPages.RowCount;
                int lnRowCount = rowCount;

                var result = from q in getPagesResult
                             select new
                             {
                                 q.Id,
                                 q.Name,
                                 q.IsActive,
                                 ParentName = q.OParentPage.Name,
                                 q.Link

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

                int rowCount = getPages.RowCount;
                int lnRowCount = rowCount;

                var result = from q in getPagesResult
                             select new
                             {
                                 q.Id,
                                 q.Name,
                                 q.IsActive,
                                 ParentName = q.OParentPage.Name,
                                 q.Link
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
        public PartialViewResult InsertPagesModal()
        {
            var oModel = new Models.MainModels.InsertUpdatePageModel();

            var getLstType = DAL.Account.Pages.PagesGet(new Pages { IsList = true }, true);
            if (getLstType.HasResult)
                oModel.LstParent = getLstType.Results;

            var getPageCategory = DAL.Account.PageCategory.PagesCategoryGet(new PagesCategory());
            if (getPageCategory.HasResult)
                oModel.LstPageCategory = getPageCategory.Results;


            return PartialView("PageParts/_PagesInsertModal", oModel);
        }
        public PartialViewResult UpdatePagesModal(string id)
        {
            var pageId = Convert.ToInt32(id);
            var oModel = new Models.MainModels.InsertUpdatePageModel();

            var getLstType = DAL.Account.Pages.PagesGet(new Pages { IsList = true }, true);
            if (getLstType.HasResult)
                oModel.LstParent = getLstType.Results;

            var getPageCategory = DAL.Account.PageCategory.PagesCategoryGet(new PagesCategory());
            if (getPageCategory.HasResult)
                oModel.LstPageCategory = getPageCategory.Results;

            if (pageId > 0)
            {
                var getPage = DAL.Account.Pages.PagesGet(new Pages { Id = pageId }, false);
                if (getPage.HasResult)
                    oModel.OPage = getPage.Results.FirstOrDefault();
            }

            return PartialView("PageParts/_PagesUpdateModal", oModel);
        }
        public JsonResult InsertPages([Bind(Exclude = "Id")] Pages oPage)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;
            if (ModelState.IsValid)
            {
                var oPageInsert = DAL.Account.Pages.PagesInsert(oPage);
                if (oPageInsert.HasResult)
                {
                    cStatus = "success";
                    cMsg = Resources.NotifyMsg.InsertSuccessMsg;
                    return Json(new { cStatus, cMsg }, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(new { cStatus = "notValid", cMsg = Resources.NotifyMsg.NotValidMsg }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { cStatus, cMsg }, JsonRequestBehavior.AllowGet);
        }
        public JsonResult UpdatePages(Pages oPage)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;
            if (ModelState.IsValid)
            {
                var oPageUpdate = DAL.Account.Pages.PagesUpdate(oPage);
                if (oPageUpdate.HasResult)
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
        public JsonResult DeletePage(int id)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;
            var oPage = new Pages();
            oPage.Id = Convert.ToInt32(id);

            var oResult = DAL.Account.Pages.DeletePage(oPage);
            if (oResult.HasResult)
            {
                cStatus = "success";
                cMsg = Resources.NotifyMsg.DeleteSuccessMsg;
            }
            return Json(new { cStatus, cMsg }, JsonRequestBehavior.AllowGet);

        }
        public JsonResult PagesSearchAutoComplete(string id)
        {
            var getPagesResult = new List<Pages>();
            var getPAges = DAL.Account.Pages.PagesGet(new Pages { Name = id, IsList = true }, false);

            if (getPAges.HasResult)
            {
                getPagesResult = getPAges.Results;
                var result = from q in getPagesResult
                             select new
                             {
                                 q.Name,
                                 q.Id
                             };
                return Json(result, JsonRequestBehavior.AllowGet);
            }

            return Json(getPagesResult, JsonRequestBehavior.AllowGet);
        }
        #endregion 

        #region Errors Repository 
        public ActionResult ErrorsLogs()
        {
            return View();
        }
        public JsonResult GetErrorsLogsDataTable(JQueryDataTableParamModel param)
        {
            var oErrorLog = new ErrorsLogs();

            DataTableProcessModel m = new DataTableProcessModel();
            DataTableProcessModel dtProcess = DataTableProcesses.DataTableEslestir(param, m);
            oErrorLog.SortCol = dtProcess.SortCol;
            oErrorLog.SortType = dtProcess.SortType;
            oErrorLog.Page = dtProcess.Page;
            oErrorLog.RowPerPage = dtProcess.RowPerPage;
            oErrorLog.IsList = false;

            var getErrorsLogs = DAL.Account.ErrorsLogs.ErrorLogsGet(oErrorLog);

            var getErrorsLogsResult = new List<ErrorsLogs>();
            if (getErrorsLogs.HasResult)
            {
                getErrorsLogsResult = getErrorsLogs.Results;

                int rowCount = getErrorsLogs.RowCount;
                int lnRowCount = rowCount;

                var result = from q in getErrorsLogsResult
                             select new
                             {
                                 q.Id,
                                 q.UserId,
                                 q.IP,
                                 q.UserAgent,
                                 q.Browser,
                                 q.ErrorMessage,
                                 q.Link,
                                 ErrorDate = q.ErrorDate.ToString("yyyy-MM-dd hh:mm tt"),
                                 q.IsSolved,
                                 q.IsAjax,
                                 q.PostedData
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

                int rowCount = getErrorsLogs.RowCount;
                int lnRowCount = rowCount;

                var result = from q in getErrorsLogsResult
                             select new
                             {
                                 q.Id,
                                 q.UserId,
                                 q.IP,
                                 q.UserAgent,
                                 q.Browser,
                                 q.ErrorMessage,
                                 q.Link,
                                 ActionDate = q.ErrorDate.ToString("yyyy-MM-dd hh:mm tt"),
                                 q.IsSolved,
                                 q.IsAjax,
                                 q.PostedData

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
        public JsonResult DeleteErrorLog(int id)
        {
            var cStatus = "error";
            var cMsg = Resources.NotifyMsg.ErrorMsg;
            var oResult = DAL.Account.ErrorsLogs.ErrorLogDelete(Convert.ToInt32(id));
            if (oResult.HasResult)
            {
                cStatus = "success";
                cMsg = Resources.NotifyMsg.DeleteSuccessMsg;
            }
            return Json(new { cStatus, cMsg, }, JsonRequestBehavior.AllowGet);

        }
        #endregion       

    }
}
