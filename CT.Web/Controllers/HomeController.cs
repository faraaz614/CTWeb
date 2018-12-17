using CT.Common.Common;
using CT.Web.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CT.Web.Controllers
{
    public class HomeController : BaseController
    {
        public async Task<ActionResult> Index()
        {
            bool status = true;//db data
            string message = "";//db data
            if (status)
            {
                TempData[CommonUtility.Success.ToString()] = message;
            }
            else
            {
                TempData[CommonUtility.Error.ToString()] = message;
            }
            return View();
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

        public ActionResult Login()
        {
            return View();
        }
    }
}