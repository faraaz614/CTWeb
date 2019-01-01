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

        [HttpPost]
        public IHttpActionResult DeleteVehicleByID(VehicleEntity model)
        {
            return RunInSafe(() =>
            {
                var data = _VehicleService.DeleteVehicleByID(model);
                cTApiResponse.Data = data;
                cTApiResponse.IsSuccess = true;
                return Ok(cTApiResponse);
            });
        }

        [HttpPost]
        public IHttpActionResult GetVehicleByID(VehicleEntity model)
        {
            return RunInSafe(() =>
            {
                var data = _VehicleService.GetVehicleByID(model);
                cTApiResponse.Data = data;
                cTApiResponse.IsSuccess = true;
                return Ok(cTApiResponse);
            });
        }

        [HttpPost]
        public IHttpActionResult GetVehicles(VehicleEntity model)
        {
            return RunInSafe(() =>
            {
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

        [HttpPost]
        public IHttpActionResult DeleteVehicleImage(VehicleImageEntity model)
        {
            return RunInSafe(() =>
            {
                BaseEntity data = new BaseEntity();
                data = _VehicleService.DeleteVehicleImage(model);
                cTApiResponse.Data = data;
                cTApiResponse.IsSuccess = true;
                return Ok(cTApiResponse);
            });
        }
    }
}