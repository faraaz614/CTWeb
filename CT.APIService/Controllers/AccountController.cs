using CT.Common.Common;
using CT.Service.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CT.APIService.Controllers
{
    public class AccountController : BaseApiController
    {
        LoginService _loginService = null;
        public AccountController()
        {
            _loginService = new LoginService();
        }
        
        [HttpGet]
        public IHttpActionResult Login(string UserName, string Password)
        {
            return RunInSafe(()=> {
                UserEntity user = new UserEntity { UserName = UserName, Password = Password };
                var data = _loginService.Login(user);
                cTApiResponse.Data = data;
                cTApiResponse.IsSuccess = true;
                return Ok(cTApiResponse);
            });
        }
    }
}
