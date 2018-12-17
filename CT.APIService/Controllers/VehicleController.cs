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
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [HttpPost]
        public IHttpActionResult DeleteVehicleByID(VehicleEntity model)
        {
            return RunInSafe(() =>
            {
                var data = _VehicleService.DeleteVehicleByID(model);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [HttpPost]
        public IHttpActionResult GetVehicleByID(VehicleEntity model)
        {
            return RunInSafe(() =>
            {
                var data = _VehicleService.GetVehicleByID(model);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }

        [HttpPost]
        public IHttpActionResult GetVehicles(VehicleEntity model)
        {
            return RunInSafe(() =>
            {
                var data = _VehicleService.GetVehicles(model);
                tebResponse.Data = data;
                tebResponse.IsSuccess = true;
                return Ok(tebResponse);
            });
        }
    }
}