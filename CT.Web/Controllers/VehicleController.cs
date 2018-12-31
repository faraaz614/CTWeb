using CT.Common.Common;
using CT.Common.Entities;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CT.Web.Controllers
{
    public class VehicleController : BaseController
    {
        public async Task<ActionResult> Index()
        {
            BaseVehicleEntity Vehicleslist = new BaseVehicleEntity();
            VehicleEntity vehicleEntity = new VehicleEntity { RoleID = 1, UserID = 1 };
            CTApiResponse cTApiResponse = await Post("/Vehicle/GetVehicles", vehicleEntity);
            if (cTApiResponse.IsSuccess)
            {
                Vehicleslist = JsonConvert.DeserializeObject<BaseVehicleEntity>(Convert.ToString(cTApiResponse.Data));
            }
            return View(Vehicleslist);
        }

        public async Task<ActionResult> AddVehicle(long vechileID = 0)
        {
            VehicleEntity model = new VehicleEntity();
            model = new VehicleEntity { RoleID = 1, UserID = 1, ID = vechileID };
            CTApiResponse cTApiResponse = await Post("/Vehicle/GetVehicleByID", model);
            if (cTApiResponse.IsSuccess)
            {
                BaseVehicleEntity baseVehicleEntity = JsonConvert.DeserializeObject<BaseVehicleEntity>(Convert.ToString(cTApiResponse.Data));
                model = baseVehicleEntity.VehicleEntity;
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> AddVehicle(VehicleEntity model)
        {
            if (ModelState.ContainsKey("ID"))
                ModelState["ID"].Errors.Clear();

            if (ModelState.IsValid)
            {
                model.RoleID = User.RoleId;
                model.UserID = User.UserId;
                CTApiResponse cTApiResponse = await Post("/Vehicle/InsertUpdateVehicle", model);
                if (cTApiResponse.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        public async Task<ActionResult> DeleteVehicleByID(long vechileID = 0)
        {
            ViewData["VehicleDeleted"] = "";
            if (vechileID > 0)
            {
                VehicleEntity model = new VehicleEntity { RoleID = 1, UserID = 1, ID = vechileID };
                CTApiResponse cTApiResponse = await Post("/Vehicle/DeleteVehicleByID", model);
                if (cTApiResponse.IsSuccess)
                {
                    BaseEntity baseEntity = JsonConvert.DeserializeObject<BaseEntity>(Convert.ToString(cTApiResponse.Data));
                    if (baseEntity.ResponseStatus.Status == 1)
                    {
                        ViewData["VehicleDeleted"] = "Deleted";
                    }
                }
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<ActionResult> AddVehicleDetails(VehicleEntity model)
        {
            if (model.ID > 0)
            {
                CTApiResponse cTApiResponse = await Post("/Vehicle/AddVehicleDetails", model);
                if (cTApiResponse.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> AddVehicleDocument(VehicleEntity model)
        {
            if (model.ID > 0)
            {
                CTApiResponse cTApiResponse = await Post("/Vehicle/AddVehicleDocument", model);
                if (cTApiResponse.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> AddVehicleTechnical(VehicleEntity model)
        {
            if (model.ID > 0)
            {
                CTApiResponse cTApiResponse = await Post("/Vehicle/AddVehicleTechnical", model);
                if (cTApiResponse.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }
    }
}