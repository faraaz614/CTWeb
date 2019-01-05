using CT.Common.Common;
using CT.Common.Entities;
using CT.Service.VehicleService;
using ImageResizer;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CT.Web.Controllers
{
    public class VehicleController : BaseController
    {
        public ActionResult Index()
        {
            BaseVehicleEntity data = new VehicleService().GetVehicles(new VehicleEntity() { UserID = User.UserId, RoleID = User.RoleId });
            return View(data);
        }

        public ActionResult AddVehicle(long vechileID = 0)
        {
            VehicleEntity model = new VehicleEntity() { RoleID = User.RoleId, UserID = User.UserId, ID = vechileID };
            BaseVehicleEntity vehicleInfo = new VehicleService().GetVehicleByID(model);
            model = vehicleInfo.VehicleEntity;
            return View(model);
        }

        [HttpPost]
        public ActionResult AddVehicle(VehicleEntity model)
        {
            if (ModelState.ContainsKey("ID"))
                ModelState["ID"].Errors.Clear();

            if (ModelState.IsValid)
            {
                model.RoleID = User.RoleId;
                model.UserID = User.UserId;
                BaseEntity dataInfo = new BaseEntity();
                if (model.ID == 0)
                    dataInfo = new VehicleService().InsertVehicle(model);
                else
                    dataInfo = new VehicleService().UpdateVehicle(model);
                if (dataInfo.ResponseStatus.Status == 1)
                {
                    TempData[CT.Web.Common.CommonUtility.Success.ToString()] = dataInfo.ResponseStatus.Message;
                    return RedirectToAction("Index");
                }
                else
                    TempData[CT.Web.Common.CommonUtility.Error.ToString()] = dataInfo.ResponseStatus.Message;
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

        [HttpPost]
        public ActionResult AddVehicleImages(VehicleEntity model, HttpPostedFileBase[] files)
        {
            if (model.ID > 0)
            {
                foreach (HttpPostedFileBase file in files)
                {
                    if (file != null && file.ContentLength > 0)
                    {
                        try
                        {
                            string imagename = DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName);
                            file.SaveAs(Path.Combine(Server.MapPath("~/Images/Original/"), imagename));
                            string savedFileName = Path.Combine(Server.MapPath("~/Images/Original/"), imagename);
                            string destinationImagePath = Path.Combine(Server.MapPath("~/Images/450250/"), imagename);
                            resizeImage(Server.MapPath("~/Images/450250/"), Server.MapPath("~/Images/Original/"), imagename, 450, 250, 450, 250);
                            model.VehicleImage.Add(new VehicleImageEntity { ImageName = imagename, VehicleID = model.ID });
                        }
                        catch (Exception ex)
                        {
                            TempData[CT.Web.Common.CommonUtility.Error.ToString()] = ex.Message;
                            return RedirectToAction("AddVehicle", new
                            {
                                vechileID = model.ID
                            });
                        }
                    }
                }

                model.RoleID = User.RoleId;
                model.UserID = User.UserId;
                BaseEntity dataInfo = new VehicleService().AddVehicleImages(model);
                if (dataInfo.ResponseStatus.Status == 1)
                {
                    TempData[CT.Web.Common.CommonUtility.Success.ToString()] = dataInfo.ResponseStatus.Message;
                    return View("AddVehicle", new { vehicleId = model.ID });
                }
                else
                    TempData[CT.Web.Common.CommonUtility.Error.ToString()] = dataInfo.ResponseStatus.Message;
            }
            return View(model);
        }

        [HttpPost]
        public async Task<ActionResult> DeleteVehicleImage(string ImageName, string vehicleID)
        {
            if (!string.IsNullOrEmpty(ImageName) && !string.IsNullOrEmpty(vehicleID))
            {
                CTApiResponse cTApiResponse = await Post("/Vehicle/DeleteVehicleImage", new VehicleImageEntity { VehicleID = Convert.ToInt64(vehicleID), ImageName = ImageName });
                if (cTApiResponse.IsSuccess)
                {
                    return Json(true);
                }
            }
            return Json(false);
        }
    }
}