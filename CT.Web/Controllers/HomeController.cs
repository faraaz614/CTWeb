using CT.Common.Common;
using CT.Common.Entities;
using CT.Service.UserService;
using CT.Web.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CT.Web.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            DashEntity model = new DashEntity();
            model = new UserService().DashBoard(new UserEntity { UserID = User.UserId, RoleID = User.RoleId });
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }
    }
}