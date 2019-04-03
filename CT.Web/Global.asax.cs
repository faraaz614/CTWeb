using CT.Service.UserService;
using CT.Web.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Timers;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Security;

namespace CT.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        static Timer timer = null;

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            if (timer == null)
            {
                timer = new Timer();
                timer.Interval = 300000; // Every 5 min
                timer.Elapsed += new ElapsedEventHandler(CloseDealByTimer);
                timer.Start();
            }
        }

        private void CloseDealByTimer(object sender, ElapsedEventArgs e)
        {
            new UserService().CloseBID(0);
        }

        protected void Application_PostAuthenticateRequest(Object sender, EventArgs e)
        {
            HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null) return;
            if (authCookie != null && !String.IsNullOrEmpty(authCookie.Value))
            {
                try
                {
                    FormsAuthenticationTicket authTicket = FormsAuthentication.Decrypt(authCookie.Value);
                    CustomPrincipalSerializeModel serializeModel = JsonConvert.DeserializeObject<CustomPrincipalSerializeModel>(authTicket.UserData);
                    CustomPrincipal newUser = new CustomPrincipal(authTicket.Name);
                    newUser.UserId = serializeModel.UserId;
                    newUser.UserName = serializeModel.UserName;
                    newUser.ProfileName = serializeModel.ProfileName;
                    newUser.RoleId = serializeModel.RoleId;
                    newUser.ProfilePicture = serializeModel.ProfilePicture;
                    HttpContext.Current.User = newUser;
                }
                catch (CryptographicException)
                {
                    FormsAuthentication.SignOut();
                }
                
            }

        }
    }
}
