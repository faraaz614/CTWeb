using CT.Service.UserService;
using System.Web.Mvc;

namespace CT.Web.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            UserService userService = new UserService();
            //Service.Entities.BaseEntity info = userService.SaveDealer(new Service.Entities.UserEntity { FirstName = "Shaik", LastName = "Mohammad", UserName = "rafeeq786@gmail.com", Password = "123456", ProfilePic = "rafeeq.jpg" });
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