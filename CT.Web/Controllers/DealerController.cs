using CT.Common.Common;
using CT.Common.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CT.Web.Controllers
{
    public class DealerController : BaseController
    {
        public async Task<ActionResult> Index()
        {
            BaseUserEntity dealerslist = new BaseUserEntity();
            UserEntity userEntity = new UserEntity { RoleID = 1, ID = 1 };
            CTApiResponse cTApiResponse = await Post<UserEntity>("/User/GetDealers", userEntity);
            if (cTApiResponse.IsSuccess)
            {
                dealerslist = JsonConvert.DeserializeObject<BaseUserEntity>(Convert.ToString(cTApiResponse.Data));
            }
            return View(dealerslist);
        }

        public async Task<ActionResult> AddDealer(long dealerID = 0)
        {
            UserEntity model = new UserEntity();
            if (dealerID > 0)
            {
                model = new UserEntity { RoleID = 1, ID = dealerID };
                CTApiResponse cTApiResponse = await Post<UserEntity>("/User/GetDealerByID", model);
                if (cTApiResponse.IsSuccess)
                {
                    BaseUserEntity baseUserEntity = JsonConvert.DeserializeObject<BaseUserEntity>(Convert.ToString(cTApiResponse.Data));
                    model = baseUserEntity.UserEntity;
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> AddDealer(UserEntity model)
        {
            if (ModelState.ContainsKey("ID"))
                ModelState["ID"].Errors.Clear();

            if (ModelState.IsValid)
            {
                model.RoleID = 1;
                model.UserID = 1;
                CTApiResponse cTApiResponse = await Post<UserEntity>("/User/InsertUpdateDealer", model);
                if (cTApiResponse.IsSuccess)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(model);
        }

        public async Task<ActionResult> DeleteDealer(long dealerID = 0)
        {
            ViewData["UserDeleted"] = "";
            if (dealerID > 0)
            {
                UserEntity model = new UserEntity { RoleID = 1, ID = dealerID };
                CTApiResponse cTApiResponse = await Post<UserEntity>("/User/DeleteDealerByID", model);
                if (cTApiResponse.IsSuccess)
                {
                    BaseEntity baseEntity = JsonConvert.DeserializeObject<BaseEntity>(Convert.ToString(cTApiResponse.Data));
                    if (baseEntity.ResponseStatus.Status == 1)
                    {
                        ViewData["UserDeleted"] = "Deleted";
                    }
                }
            }
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> BID()
        {
            BaseVehicleBIDEntity baseVehicleEntity = new BaseVehicleBIDEntity();
            UserEntity userEntity = new UserEntity { RoleID = 1, ID = 1 };
            CTApiResponse data = await Post("/User/GetBIDS", userEntity);
            if (data.IsSuccess)
            {
                baseVehicleEntity = JsonConvert.DeserializeObject<BaseVehicleBIDEntity>(Convert.ToString(data.Data));
            }
            return View(baseVehicleEntity);
        }

        public async Task<ActionResult> ViewBID(long VehicleID)
        {
            BaseVehicleBIDEntity baseVehicleBIDEntity = new BaseVehicleBIDEntity();
            if (VehicleID > 0)
            {
                VehicleBIDEntity vehicleBIDEntity = new VehicleBIDEntity { RoleID = 1, ID = 1, VehicleID = VehicleID };
                CTApiResponse cTApiResponse = await Post("/User/ViewBID", vehicleBIDEntity);
                if (cTApiResponse.IsSuccess)
                {
                    baseVehicleBIDEntity = JsonConvert.DeserializeObject<BaseVehicleBIDEntity>(Convert.ToString(cTApiResponse.Data));
                }
            }
            return View(baseVehicleBIDEntity);
        }
    }
}