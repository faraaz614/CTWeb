﻿using CT.Common.Common;
using CT.Common.Entities;
using CT.Service.UserService;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CT.Web.Controllers
{
    public class DealerController : BaseController
    {
        public ActionResult Index(string SearchText)
        {
            BaseUserEntity dealers = new BaseUserEntity();
            dealers.SearchText = SearchText;
            if (!String.IsNullOrWhiteSpace(dealers.SearchText) && dealers.SearchText != "Search ...")
                dealers = new UserService().GetDealers(User.UserId, User.RoleId, dealers.SearchText);
            else
                dealers = new UserService().GetDealers(User.UserId, User.RoleId);
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
                VehicleBIDEntity vehicleBIDEntity = new VehicleBIDEntity { RoleID = User.RoleId, ID = User.UserId, VehicleID = VehicleID };
                BaseVehicleBIDEntity baseVehicleBIDEntity = new UserService().ViewBID(vehicleBIDEntity);
                return View(baseVehicleBIDEntity);
            }
            return RedirectToAction("BID");
        }
    }
}