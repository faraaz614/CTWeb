using CT.Common.Common;
using System;
using System.Web.Http;

namespace CT.APIService.Controllers
{
    public class BaseApiController : ApiController
    {
        public readonly CTApiResponse cTApiResponse;
        public BaseApiController()
        {
            cTApiResponse = new CTApiResponse();
        }

        [NonAction]
        public IHttpActionResult RunInSafe(Func<IHttpActionResult> fn)
        {
            try
            {
                return fn();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}