using CT.Common.Common;
using CT.Service.Login;
using CT.Service.UserService;
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
            UserEntity user = new UserEntity { UserName = UserName, Password = Password };
            var data = _loginService.Login(user);
            BaseUserEntity baseUserEntity = new UserService().GetDealerByID(new UserEntity { ID = data.UserID, UserID = data.UserID, RoleID = data.RoleID });
            return Ok(baseUserEntity);
        }
    }
}
