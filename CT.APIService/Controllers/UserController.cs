using CT.Common.Common;
using CT.Service.UserService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CT.APIService.Controllers
{
    public class UserController : BaseApiController
    {
        UserService _userService = null;
        public UserController()
        {
            _userService = new UserService();
        }

        [HttpPost]
        public IHttpActionResult InsertUpdateDealer(UserEntity model)
        {
            return RunInSafe(() =>
            {
                BaseEntity data = new BaseEntity();
                if (model.ID > 0)
                    data = _userService.UpdateDealer(model);
                else
                    data = _userService.InsertDealer(model);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [HttpPost]
        public IHttpActionResult DeleteDealerByID(UserEntity model)
        {
            return RunInSafe(() =>
            {
                var data = _userService.DeleteDealerByID(model);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [HttpPost]
        public IHttpActionResult GetDealerByID(UserEntity model)
        {
            return RunInSafe(() =>
            {
                var data = _userService.GetDealerByID(model);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [HttpPost]
        public IHttpActionResult GetDealers(UserEntity model)
        {
            return RunInSafe(() =>
            {
                var data = _userService.GetDealers(model);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }
    }
}
