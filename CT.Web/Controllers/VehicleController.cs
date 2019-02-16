using CT.Common.Common;
using CT.Common.Entities;
using CT.Service.VehicleService;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CT.Web.Controllers
{
    public class VehicleController : BaseController
    {
        public ActionResult Index(string SearchText, int PageNo = 1, int PageSize = 10)
        {
            VehicleEntity vehicleEntity = new VehicleEntity
            {
                Action = "Index",
                Controller = "Vehicle",
                UserID = User.UserId,
                RoleID = User.RoleId,
                PageNo = PageNo,
                PageSize = PageSize,
                SearchText = SearchText
            };
            BaseVehicleEntity list = new VehicleService().GetVehicles(vehicleEntity);
            return View(list);
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

                if (model.VehicleImage.Count > 0)
                {
                    model.RoleID = User.RoleId;
                    model.UserID = User.UserId;
                    BaseEntity dataInfo = new VehicleService().AddVehicleImages(model);
                    if (dataInfo.ResponseStatus.Status == 1)
                        TempData[CT.Web.Common.CommonUtility.Success.ToString()] = dataInfo.ResponseStatus.Message;
                    else
                        TempData[CT.Web.Common.CommonUtility.Error.ToString()] = dataInfo.ResponseStatus.Message;
                    return RedirectToAction("AddVehicle", new { vechileID = model.ID });
                }
                else
                {
                    TempData[CT.Web.Common.CommonUtility.Error.ToString()] = "Please upload Image(s).";
                    return RedirectToAction("AddVehicle", new { vechileID = model.ID });
                }
            }
            return RedirectToAction("Index");
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

        public ActionResult SendNotification(long vehicleID, string body)
        {
            string fcm_url = ConfigurationManager.AppSettings["fcm_url"];
            string Authorization = ConfigurationManager.AppSettings["Authorization"];
            string Sender = ConfigurationManager.AppSettings["Sender"];
            string title = ConfigurationManager.AppSettings["NotificationTitle"];

            WebRequest tRequest = WebRequest.Create(fcm_url);
            tRequest.Method = "post";
            //serverKey - Key from Firebase cloud messaging server  
            tRequest.Headers.Add(string.Format("Authorization: key={0}", Authorization));
            //Sender Id - From firebase project setting  
            tRequest.Headers.Add(string.Format("Sender: id={0}", Sender));
            tRequest.ContentType = "application/json";
            var payload = new
            {
                to = "/topics/cartimez", // or generated token
                priority = "high",
                content_available = true,
                notification = new
                {
                    body = body,
                    title = title,
                    badge = 1
                },
            };
            string postbody = JsonConvert.SerializeObject(payload).ToString();
            Byte[] byteArray = Encoding.UTF8.GetBytes(postbody);
            tRequest.ContentLength = byteArray.Length;
            using (Stream dataStream = tRequest.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse tResponse = tRequest.GetResponse())
                {
                    using (Stream dataStreamResponse = tResponse.GetResponseStream())
                    {
                        if (dataStreamResponse != null)
                        {
                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {
                                String sResponseFromServer = tReader.ReadToEnd();
                                if (sResponseFromServer.Contains("message_id"))
                                {
                                    NotificationEntity notificationEntity = new NotificationEntity
                                    {
                                        UserID = User.UserId,
                                        RoleID = User.RoleId,
                                        VehicleID = vehicleID,
                                        Body = body,
                                        Title = title
                                    };
                                    BaseEntity baseEntity = new VehicleService().AddNotification(notificationEntity);
                                    TempData[CT.Web.Common.CommonUtility.Success.ToString()] = "Notifications sent.";
                                }
                                else
                                {
                                    TempData[CT.Web.Common.CommonUtility.Error.ToString()] = "Error Occurred.";
                                }
                            }
                        }
                    }
                }
            }
            return RedirectToAction("Index");
        }
    }
}