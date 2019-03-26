using CT.Common.Common;
using CT.Common.Entities;
using CT.Service.UserService;
using CT.Web.Models;
using Newtonsoft.Json;
using System;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CT.Web.Controllers
{
    public class DealerController : BaseController
    {
        public ActionResult Index(string SearchText, int PageNo = 1, int PageSize = 10)
        {
            UserEntity userEntity = new UserEntity
            {
                Action = "Index",
                Controller = "Dealer",
                UserID = User.UserId,
                RoleID = User.RoleId,
                PageNo = PageNo,
                PageSize = PageSize,
                SearchText = SearchText
            };
            BaseUserEntity dealers = new UserService().GetDealers(userEntity);
            return View(dealers);
        }

        public ActionResult AddDealer(long dealerID = 0)
        {
            UserEntity model = new UserEntity();
            UserService userService = new UserService();
            if (dealerID > 0)
            {
                BaseUserEntity data = userService.GetDealerByID(new UserEntity() { ID = dealerID, UserID = User.UserId, RoleID = User.RoleId });
                if (data != null)
                    model = data.UserEntity;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult AddDealer(UserEntity model, HttpPostedFileBase file)
        {
            if (ModelState.ContainsKey("ID"))
                ModelState["ID"].Errors.Clear();

            if (model.ID > 0)
                ModelState["Mobile"].Errors.Clear();

            if (ModelState.IsValid)
            {
                if (file != null && file.ContentLength > 0 && ValidateImageExtension(Path.GetExtension(file.FileName)))
                {
                    try
                    {
                        model.ProfilePic = DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName);
                        file.SaveAs(Path.Combine(Server.MapPath("~/Images/Original/"), model.ProfilePic));
                        resizeImage(Server.MapPath("~/Images/450250/"), Server.MapPath("~/Images/Original/"), model.ProfilePic, 450, 250, 450, 250);
                    }
                    catch (Exception)
                    {
                        return View(model);
                    }
                }

                UserService userService = new UserService();
                BaseEntity userInfo = new BaseEntity();
                if (model.ID == 0)
                {
                    userInfo = userService.InsertDealer(new UserEntity()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        UserName = model.UserName,
                        Password = model.Password,
                        UserID = User.UserId,
                        RoleID = 3,
                        Mobile = model.Mobile,
                        Mobile2 = model.Mobile2,
                        ProfilePic = model.ProfilePic
                    });
                }
                else
                {
                    if (string.IsNullOrEmpty(model.ProfilePic))
                    {
                        BaseUserEntity info = userService.GetDealerByID(new UserEntity()
                        {
                            ID = model.ID,
                            UserID = User.UserId,
                            RoleID = User.RoleId
                        });
                        model.ProfilePic = info.UserEntity.ProfilePic;
                    }
                    userInfo = userService.UpdateDealer(new UserEntity()
                    {
                        FirstName = model.FirstName,
                        LastName = model.LastName,
                        UserName = model.UserName,
                        Password = model.Password,
                        UserID = User.UserId,
                        RoleID = User.RoleId,
                        IsActive = model.IsActive,
                        Mobile = model.Mobile,
                        Mobile2 = model.Mobile2,
                        ID = model.ID,
                        ProfilePic = model.ProfilePic
                    });
                }
                if (userInfo.ResponseStatus.Status == 1)
                {
                    TempData[CT.Web.Common.CommonUtility.Success.ToString()] = userInfo.ResponseStatus.Message;
                    return RedirectToAction("Index");
                }
                else
                    TempData[CT.Web.Common.CommonUtility.Error.ToString()] = userInfo.ResponseStatus.Message;
            }
            return View(model);
        }

        public ActionResult DeleteDealer(long dealerID = 0)
        {
            BaseEntity dataInfo = new BaseEntity();
            if (dealerID > 0)
            {
                UserEntity model = new UserEntity { RoleID = User.RoleId, UserID = User.UserId, ID = dealerID };
                if (model.ID > 0)
                    dataInfo = new UserService().DeleteDealerByID(model);
                if (dataInfo.ResponseStatus.Status == 1)
                {
                    TempData[CT.Web.Common.CommonUtility.Success.ToString()] = dataInfo.ResponseStatus.Message;
                }
                else
                    TempData[CT.Web.Common.CommonUtility.Error.ToString()] = dataInfo.ResponseStatus.Message;
            }
            return Json(dataInfo.ResponseStatus.Status);
        }

        public ActionResult BID()
        {
            UserEntity model = new UserEntity { RoleID = User.RoleId, UserID = User.UserId };
            BaseVehicleBIDEntity baseVehicleEntity = new UserService().GetBIDS(model);
            return View(baseVehicleEntity);
        }

        public ActionResult ViewBID(long VehicleID)
        {
            if (VehicleID > 0)
            {
                VehicleBIDEntity vehicleBIDEntity = new VehicleBIDEntity { RoleID = User.RoleId, UserID = User.UserId, VehicleID = VehicleID };
                BaseVehicleBIDEntity baseVehicleBIDEntity = new UserService().ViewBID(vehicleBIDEntity);
                return View(baseVehicleBIDEntity);
            }
            return RedirectToAction("BID");
        }

        public ActionResult CloseBID(long VehicleID, long BidID)
        {
            if (VehicleID > 0)
            {
                VehicleBIDEntity vehicleBIDEntity = new VehicleBIDEntity
                {
                    RoleID = User.RoleId,
                    UserID = User.UserId,
                    VehicleID = VehicleID,
                    VechileStatus = (int)VehicleStatus.CloseDeal,
                    BidID = BidID
                };
                BaseVehicleBIDEntity baseVehicleBIDEntity = new UserService().CloseBID(vehicleBIDEntity);
                BaseUserEntity baseUserEntity = new UserService().GetUserTokenByID(BidID);
                if (!String.IsNullOrWhiteSpace(baseUserEntity.refreshedToken))
                    new FBNotification().SendDealClosedNotification(baseUserEntity.refreshedToken);
            }
            return RedirectToAction("ViewBID", new { VehicleID = VehicleID });
        }
    }
}