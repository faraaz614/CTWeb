using CT.Common.Common;
using CT.Web.Common;
using CT.Web.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
                // validate user by username and password  //get data from db
                var user = new UserEntity() { UserID = 1, RoleID = 1, FirstName = "Srikanth", LastName = "Srikanth", UserName = "srikanth@cartimez.in" }; //UserLoginService.Instance.ValidateUser(loginModel.Username, loginModel.Password);
                if (user != null)
                {
                    CustomPrincipalSerializeModel serializeModel = new CustomPrincipalSerializeModel();
                    serializeModel.UserId = user.UserID;
                    serializeModel.UserName = user.UserName;
                    serializeModel.RoleId = user.RoleID;
                    //serializeModel.ProfilePicture = user.ProfilePicture;
                    //serializeModel.ProfileName = user.ProfileName;
                    string userData = JsonConvert.SerializeObject(serializeModel);
                    FormsAuthenticationTicket authTicket = new FormsAuthenticationTicket(
                                                           1,
                                                           user.UserName,
                                                           DateTime.Now,
                                                           DateTime.Now.AddHours(2),
                                                           loginModel.RememberMe,
                                                           userData);
                    string encTicket = FormsAuthentication.Encrypt(authTicket);
                    var faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);
                    faCookie.Expires = authTicket.Expiration;
                    Response.Cookies.Add(faCookie);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    loginModel.ErrorMessage = "Access Denied! Wrong Credential";
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