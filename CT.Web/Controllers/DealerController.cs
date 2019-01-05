using CT.Common.Common;
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
        public ActionResult Index()
        {
            BaseUserEntity dealers = new UserService().GetDealers(User.UserId, User.RoleId);
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
                if (file != null && file.ContentLength > 0)
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