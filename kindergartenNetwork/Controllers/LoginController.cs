using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DTO.Account;
using Newtonsoft.Json;
using kindergartenNetwork.Models;

namespace kindergartenNetwork.Controllers
{
    public class LoginController : Controller
    {

        // GET: Login
        public ActionResult Index(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        public ActionResult LoginRedirect(string returnUrl)
        {
            ViewBag.returnUrl = returnUrl;
            return View();
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("LoginRedirect", "Login", Request.Url);
        }
        public JsonResult Login(LoginModel oUserAccount)
        {
            var cStatus = "error";
            var cMsg = "User Name or Password invalid";

            if (string.IsNullOrEmpty(oUserAccount.Email) || string.IsNullOrEmpty(oUserAccount.Password))
            {
                return Json(new { cStatus = "error", cMsg = "You cant Enter empty username or password !!" });
            }

            var getUserAccount = DAL.Account.UserAccounts.UserLogin(oUserAccount);
            if (getUserAccount.HasResult)
            {
                // ReSharper disable once AssignNullToNotNullAttribute
                var getUserAccountResult = getUserAccount.Results.FirstOrDefault();
                cStatus = "success";
                cMsg = "انتهت العملية بنجاح";
                if (getUserAccountResult.IsActive.Value && !getUserAccountResult.IsDeleted)
                {
                    CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                    serializeModel.Id = getUserAccountResult.Id;
                    serializeModel.Name = getUserAccountResult.Name;
                    serializeModel.Password = getUserAccountResult.Pass;
                    serializeModel.UserTypeId = getUserAccountResult.UserTypeId.Value;
                    serializeModel.Email = getUserAccountResult.Email;
                    serializeModel.Avatar = getUserAccountResult.Avatar;
                    serializeModel.Gender = getUserAccountResult.Gender;
                    serializeModel.Mobile = getUserAccountResult.Mobile;
                    serializeModel.ManagerGroupId = getUserAccountResult.ManagerGroupId;


                    string userData = JsonConvert.SerializeObject(serializeModel);
                    HttpCookie cookie = FormsAuthentication.GetAuthCookie(serializeModel.Name, false);
                    var ticket = FormsAuthentication.Decrypt(cookie.Value);
                    if (ticket != null)
                    {
                        var newticket = new FormsAuthenticationTicket(ticket.Version, ticket.Name, ticket.IssueDate, ticket.Expiration, true, userData, ticket.CookiePath);
                        cookie.Value = FormsAuthentication.Encrypt(newticket);
                        if (oUserAccount.Remember == 1)
                        {
                            cookie.Expires = newticket.Expiration.AddYears(2);
                        }
                    }
                    HttpContext.Response.Cookies.Set(cookie);
                    string url = Url.Action("index", "ControlPanel");

                    return Json(new { cStatus, isRedirect = true, redirectUrl = (string.IsNullOrEmpty(oUserAccount.ReturnUrl) ? url : oUserAccount.ReturnUrl) }, JsonRequestBehavior.AllowGet);

                }
                else if (getUserAccountResult.IsDeleted)
                {
                    cStatus = "error";
                    cMsg = "The account is deactivated, please contact Admin";
                }
                else
                {
                    cStatus = "error";
                    cMsg = "The account is deactivated, please contact Admin";
                }
            }
            return Json(new { cStatus, cMsg }, JsonRequestBehavior.AllowGet);
        }
    }
}