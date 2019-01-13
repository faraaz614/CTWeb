using CT.Common.Common;
using CT.Common.Entities;
using CT.Service.VehicleService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace CT.APIService.Controllers
{
    public class VehicleController : BaseApiController
    {
        VehicleService _VehicleService = null;
        public VehicleController()
        {
            _VehicleService = new VehicleService();
        }

        [HttpPost]
        public IHttpActionResult InsertUpdateVehicle(VehicleEntity model)
        {
            return RunInSafe(() =>
            {
                BaseEntity data = new BaseEntity();
                if (model.ID > 0)
                    data = _VehicleService.UpdateVehicle(model);
                else
                    data = _VehicleService.InsertVehicle(model);
                cTApiResponse.Data = data;
                cTApiResponse.IsSuccess = true;
                return Ok(cTApiResponse);
            });
        }

        public IHttpActionResult DeleteVehicleByID(int ID, int UserID, int RoleID)
        {
            return RunInSafe(() =>
            {
                VehicleEntity model = new VehicleEntity { ID = ID, UserID = UserID, RoleID = RoleID };
                var data = _VehicleService.DeleteVehicleByID(model);
                cTApiResponse.Data = data;
                cTApiResponse.IsSuccess = true;
                return Ok(cTApiResponse);
            });
        }

        public IHttpActionResult GetVehicleByID(int ID, int UserID, int RoleID)
        {
            return RunInSafe(() =>
            {
                VehicleEntity model = new VehicleEntity { ID = ID, UserID = UserID, RoleID = RoleID };
                BaseVehicleEntity data = _VehicleService.GetVehicleByID(model);
                if (data != null && data.VehicleEntity != null && data.VehicleEntity.VehicleBIDs != null)
                {
                    data.VehicleEntity.VehicleBIDs = data.VehicleEntity.VehicleBIDs.Where(x => x.DealerID == UserID).ToList();
                }
                cTApiResponse.Data = data;
                cTApiResponse.IsSuccess = true;
                return Ok(cTApiResponse);
            });
        }

        public IHttpActionResult GetVehicles(int UserID, int RoleID)
        {
            return RunInSafe(() =>
            {
                VehicleEntity model = new VehicleEntity { UserID = UserID, RoleID = RoleID };
                var data = _VehicleService.GetVehicles(model);
                cTApiResponse.Data = data;
                cTApiResponse.IsSuccess = true;
                return Ok(cTApiResponse);
            });
        }

        [HttpPost]
        public IHttpActionResult AddVehicleDetails(VehicleEntity model)
        {
            return RunInSafe(() =>
            {
                BaseEntity data = new BaseEntity();
                data = _VehicleService.AddVehicleDetails(model);
                cTApiResponse.Data = data;
                cTApiResponse.IsSuccess = true;
                return Ok(cTApiResponse);
            });
        }

        [HttpPost]
        public IHttpActionResult AddVehicleDocument(VehicleEntity model)
        {
            return RunInSafe(() =>
            {
                BaseEntity data = new BaseEntity();
                data = _VehicleService.AddVehicleDocument(model);
                cTApiResponse.Data = data;
                cTApiResponse.IsSuccess = true;
                return Ok(cTApiResponse);
            });
        }

        [HttpPost]
        public IHttpActionResult AddVehicleTechnical(VehicleEntity model)
        {
            return RunInSafe(() =>
            {
                BaseEntity data = new BaseEntity();
                data = _VehicleService.AddVehicleTechnical(model);
                cTApiResponse.Data = data;
                cTApiResponse.IsSuccess = true;
                return Ok(cTApiResponse);
            });
        }

        [HttpPost]
        public IHttpActionResult AddVehicleImages(VehicleEntity model)
        {
            return RunInSafe(() =>
            {
                BaseEntity data = new BaseEntity();
                data = _VehicleService.AddVehicleImages(model);
                cTApiResponse.Data = data;
                cTApiResponse.IsSuccess = true;
                return Ok(cTApiResponse);
            });
        }

        public IHttpActionResult DeleteVehicleImage(string ImageName, int VehicleID, int UserID, int RoleID)
        {
            return RunInSafe(() =>
            {
                VehicleImageEntity model = new VehicleImageEntity { ImageName = ImageName, VehicleID = VehicleID, UserID = UserID, RoleID = RoleID };
                BaseEntity data = new BaseEntity();
                data = _VehicleService.DeleteVehicleImage(model);
                cTApiResponse.Data = data;
                cTApiResponse.IsSuccess = true;
                return Ok(cTApiResponse);
            });
        }

        public IHttpActionResult CloseDeal(int ID, int UserID, int RoleID)
        {
            return RunInSafe(() =>
            {
                VehicleEntity model = new VehicleEntity { ID = ID, UserID = UserID, RoleID = RoleID };
                BaseEntity data = new BaseEntity();
                data = _VehicleService.CloseDeal(model);
                cTApiResponse.Data = data;
                cTApiResponse.IsSuccess = true;
                return Ok(cTApiResponse);
            });
        }
    }
}