using CT.Common.Common;
using CT.Service.Login;
using CT.Web.Common;
using CT.Web.Models;
using Newtonsoft.Json;
using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace CT.Web.Controllers
{
    public class AccountController : BaseController
    {

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login(string returnUrl = "")
        {
            LoginModel loginModel = new LoginModel();
            return View(loginModel);
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                UserEntity user = new LoginService().Login(new UserEntity { UserName = loginModel.Username, Password = loginModel.Password });
                if (user != null && user.ResponseStatus.Status == 1)
                {
                    CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                    serializeModel.UserId = user.UserID;
                    serializeModel.UserName = user.UserName;
                    serializeModel.RoleId = user.RoleID;
                    string userData = JsonConvert.SerializeObject(serializeModel);
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                                                           1,
                                                           user.UserName,
                                                           DateTime.Now,
                                                           DateTime.Now.AddHours(2),
                                                           loginModel.RememberMe,
                                                           userData);
                    string encTicket = FormsAuthentication.Encrypt(authTicket);
                    HttpCookie faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    faCookie.Expires = authTicket.Expiration;
                    Response.Cookies.Add(faCookie);
                    return RedirectToAction("Index", "Home");

                }
                else
                {
                    loginModel.ErrorMessage = user.ResponseStatus.Message;
                }
            }
            return View(loginModel);
        }

        [AllowAnonymous]
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}