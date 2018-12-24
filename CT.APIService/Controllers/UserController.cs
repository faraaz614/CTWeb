using CT.Common.Common;
using CT.Common.Entities;
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
                cTApiResponse.Data = data;
                cTApiResponse.IsSuccess = true;
                return Ok(cTApiResponse);
            });
        }

        [HttpPost]
        public IHttpActionResult DeleteDealerByID(UserEntity model)
        {
            return RunInSafe(() =>
            {
                var data = _userService.DeleteDealerByID(model);
                cTApiResponse.Data = data;
                cTApiResponse.IsSuccess = true;
                return Ok(cTApiResponse);
            });
        }

        [HttpPost]
        public IHttpActionResult GetDealerByID(UserEntity model)
        {
            return RunInSafe(() =>
            {
                var data = _userService.GetDealerByID(model);
                cTApiResponse.Data = data;
                cTApiResponse.IsSuccess = true;
                return Ok(cTApiResponse);
            });
        }

        [HttpPost]
        public IHttpActionResult GetDealers(UserEntity model)
        {
            return RunInSafe(() =>
            {
                var data = _userService.GetDealers(model);
                cTApiResponse.Data = data;
                cTApiResponse.IsSuccess = true;
                return Ok(cTApiResponse);
            });
        }

        [HttpPost]
        public IHttpActionResult GetBIDS(UserEntity userEntity)
        {
            return RunInSafe(() =>
            {
                var data = _userService.GetBIDS(userEntity);
                cTApiResponse.Data = data;
                cTApiResponse.IsSuccess = true;
                return Ok(cTApiResponse);
            });
        }

        [HttpPost]
        public IHttpActionResult ViewBID(VehicleBIDEntity vehicleEntity)
        {
            return RunInSafe(() =>
            {
                var data = _userService.ViewBID(vehicleEntity);
                cTApiResponse.Data = data;
                cTApiResponse.IsSuccess = true;
                return Ok(cTApiResponse);
            });
        }
    }
}
