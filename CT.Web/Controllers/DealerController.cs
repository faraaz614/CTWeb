using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CT.Web.Controllers
{
    public class DealerController : Controller
    {
        // GET: Dealer
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult BID()
        {
            return View();
        }

        public ActionResult AddDeal()
        {
            return View();
        }
    }
}