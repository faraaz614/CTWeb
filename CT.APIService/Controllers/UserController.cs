using CT.Common.Common;
using CT.Common.Entities;
using CT.Service.UserService;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
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
            BaseEntity data = new BaseEntity();

            //var httpRequest = HttpContext.Current.Request;

            //foreach (string files in httpRequest.Files)
            //{
            //    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);
            //    var file = httpRequest.Files[files];

            //    if (file != null && file.ContentLength > 0 && ValidateImageExtension(Path.GetExtension(file.FileName)))
            //    {
            //        try
            //        {
            //            model.ProfilePic = DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName);
            //            file.SaveAs(Path.Combine(HttpContext.Current.Server.MapPath("~/Images/Original/"), model.ProfilePic));
            //            resizeImage(HttpContext.Current.Server.MapPath("~/Images/450250/"), HttpContext.Current.Server.MapPath("~/Images/Original/"), model.ProfilePic, 450, 250, 450, 250);
            //        }
            //        catch (Exception)
            //        {

            //        }
            //    }
            //}

            if (model.ID > 0)
                data = _userService.UpdateDealer(model);
            else
                data = _userService.InsertDealer(model);

            return Ok(data);
        }

        public IHttpActionResult DeleteDealerByID(int DealerID, int RoleID)
        {
            return RunInSafe(() =>
            {
                UserEntity model = new UserEntity { ID = DealerID, RoleID = RoleID };
                var data = _userService.DeleteDealerByID(model);
                cTApiResponse.Data = data;
                cTApiResponse.IsSuccess = true;
                return Ok(cTApiResponse);
            });
        }

        public IHttpActionResult GetDealerByID(int DealerID, int UserID, int RoleID)
        {
            UserEntity model = new UserEntity { ID = DealerID, UserID = UserID, RoleID = RoleID };
            BaseUserEntity baseUserEntity = _userService.GetDealerByID(model);
            return Ok(baseUserEntity);
        }

        public IHttpActionResult GetDealers(int UserID, int RoleID)
        {
            UserEntity model = new UserEntity { ID = UserID, RoleID = RoleID };
            var data = _userService.GetDealers(model);
            return Ok(data);
        }

        public IHttpActionResult GetBIDS(int UserID, int RoleID)
        {
            return RunInSafe(() =>
            {
                UserEntity model = new UserEntity { UserID = UserID, RoleID = RoleID };
                var data = _userService.GetBIDS(model);
                cTApiResponse.Data = data;
                cTApiResponse.IsSuccess = true;
                return Ok(cTApiResponse);
            });
        }

        [HttpGet]
        public IHttpActionResult ViewBID(int VehicleID, int UserID, int RoleID)
        {
            return RunInSafe(() =>
            {
                VehicleBIDEntity vehicleEntity = new VehicleBIDEntity { VehicleID = VehicleID, UserID = UserID, RoleID = RoleID };
                var data = _userService.ViewBID(vehicleEntity);
                cTApiResponse.Data = data;
                cTApiResponse.IsSuccess = true;
                return Ok(cTApiResponse);
            });
        }

        public IHttpActionResult GetBIDSByUserID(int UserID, int RoleID)
        {
            UserEntity model = new UserEntity { UserID = UserID, RoleID = RoleID };
            BaseVehicleEntity baseVehicleEntity = _userService.GetBIDSByUserID(model);
            return Ok(baseVehicleEntity);
        }

        [HttpGet]
        public IHttpActionResult SaveBIDByUserID(int VehicleID, decimal BidAmount, int UserID, int RoleID)
        {
            VehicleBIDEntity model = new VehicleBIDEntity { VehicleID = VehicleID, BIDAmount = BidAmount, UserID = UserID, RoleID = RoleID };
            BaseVehicleBIDEntity baseVehicleBIDEntity = _userService.SaveBIDByUserID(model);
            return Ok(baseVehicleBIDEntity);
        }
    }
}
