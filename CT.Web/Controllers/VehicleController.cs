using CT.Common.Common;
using CT.Common.Entities;
using CT.Service.VehicleService;
using Newtonsoft.Json;
using System;
using System.Configuration;
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

        public ActionResult DeleteVehicleByID(long vechileID = 0)
        {
            if (vechileID > 0)
            {
                VehicleEntity model = new VehicleEntity { RoleID = User.RoleId, UserID = User.UserId, ID = vechileID };
                BaseEntity dataInfo = new BaseEntity();
                if (model.ID > 0)
                    dataInfo = new VehicleService().DeleteVehicleByID(model);
                if (dataInfo.ResponseStatus.Status == 1)
                {
                    TempData[CT.Web.Common.CommonUtility.Success.ToString()] = dataInfo.ResponseStatus.Message;
                }
                else
                    TempData[CT.Web.Common.CommonUtility.Error.ToString()] = dataInfo.ResponseStatus.Message;
            }
            return RedirectToAction("Index");
        }

        public ActionResult CloseDeal(long vehicleID = 0)
        {
            BaseEntity dataInfo = new BaseEntity();
            if (vehicleID > 0)
            {
                VehicleEntity model = new VehicleEntity { RoleID = User.RoleId, UserID = User.UserId, ID = vehicleID };
                if (model.ID > 0)
                    dataInfo = new VehicleService().CloseDeal(model);
                if (dataInfo.ResponseStatus.Status == 1)
                {
                    TempData[CT.Web.Common.CommonUtility.Success.ToString()] = dataInfo.ResponseStatus.Message;
                }
                else
                    TempData[CT.Web.Common.CommonUtility.Error.ToString()] = dataInfo.ResponseStatus.Message;
            }
            return Json(dataInfo.ResponseStatus.Status);
        }

        [HttpPost]
        public ActionResult AddVehicleImages(VehicleEntity model, HttpPostedFileBase[] files)
        {
            if (model.ID > 0)
            {
                foreach (HttpPostedFileBase file in files)
                {
                    if (file != null && file.ContentLength > 0 && ValidateImageExtension(Path.GetExtension(file.FileName)))
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
                    return RedirectToAction("AddVehicle", new { vechileID = model.ID });
                }
                else
                    TempData[CT.Web.Common.CommonUtility.Error.ToString()] = dataInfo.ResponseStatus.Message;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult DeleteVehicleImage(string ImageName, string vehicleID)
        {
            if (!string.IsNullOrEmpty(ImageName) && !string.IsNullOrEmpty(vehicleID))
            {
                VehicleImageEntity model = new VehicleImageEntity { RoleID = User.RoleId, UserID = User.UserId, VehicleID = Convert.ToInt64(vehicleID), ImageName = ImageName };
                BaseEntity dataInfo = new BaseEntity();
                if (model.VehicleID > 0)
                    dataInfo = new VehicleService().DeleteVehicleImage(model);
                if (dataInfo.ResponseStatus.Status == 1)
                {
                    TempData[CT.Web.Common.CommonUtility.Success.ToString()] = dataInfo.ResponseStatus.Message;
                    return Json(true);
                }
                else
                    TempData[CT.Web.Common.CommonUtility.Error.ToString()] = dataInfo.ResponseStatus.Message;
            }
            return Json(false);
        }

        [HttpPost]
        public ActionResult AddVehicleDetails(VehicleEntity model)
        {
            if (model.ID > 0)
            {
                model.RoleID = User.RoleId;
                model.UserID = User.UserId;
                BaseEntity dataInfo = new BaseEntity();
                if (model.ID > 0)
                    dataInfo = new VehicleService().AddVehicleDetails(model);
                if (dataInfo.ResponseStatus.Status == 1)
                {
                    TempData[CT.Web.Common.CommonUtility.Success.ToString()] = dataInfo.ResponseStatus.Message;
                    return RedirectToAction("AddVehicle", new { vechileID = model.ID });
                }
                else
                    TempData[CT.Web.Common.CommonUtility.Error.ToString()] = dataInfo.ResponseStatus.Message;
            }
            return RedirectToAction("AddVehicle", new { vechileID = model.ID });
        }

        [HttpPost]
        public ActionResult AddVehicleDocument(VehicleEntity model)
        {
            if (model.ID > 0)
            {
                model.RoleID = User.RoleId;
                model.UserID = User.UserId;
                BaseEntity dataInfo = new BaseEntity();
                if (model.ID > 0)
                    dataInfo = new VehicleService().AddVehicleDocument(model);
                if (dataInfo.ResponseStatus.Status == 1)
                {
                    TempData[CT.Web.Common.CommonUtility.Success.ToString()] = dataInfo.ResponseStatus.Message;
                    return RedirectToAction("AddVehicle", new { vechileID = model.ID });
                }
                else
                    TempData[CT.Web.Common.CommonUtility.Error.ToString()] = dataInfo.ResponseStatus.Message;
            }
            return RedirectToAction("AddVehicle", new { vechileID = model.ID });
        }

        [HttpPost]
        public ActionResult AddVehicleTechnical(VehicleEntity model)
        {
            if (model.ID > 0)
            {
                model.RoleID = User.RoleId;
                model.UserID = User.UserId;
                BaseEntity dataInfo = new BaseEntity();
                if (model.ID > 0)
                    dataInfo = new VehicleService().AddVehicleTechnical(model);
                if (dataInfo.ResponseStatus.Status == 1)
                {
                    TempData[CT.Web.Common.CommonUtility.Success.ToString()] = dataInfo.ResponseStatus.Message;
                    return RedirectToAction("AddVehicle", new { vechileID = model.ID });
                }
                else
                    TempData[CT.Web.Common.CommonUtility.Error.ToString()] = dataInfo.ResponseStatus.Message;
            }
            return RedirectToAction("AddVehicle", new { vechileID = model.ID });
        }
    }
}