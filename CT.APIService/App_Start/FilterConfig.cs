using CT.APIService.Models;
using System.Web;
using System.Web.Mvc;

namespace CT.APIService
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new CustomException());
            filters.Add(new HandleErrorAttribute());
        }
    }
}
