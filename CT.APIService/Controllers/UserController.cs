using CT.APIService.Models;
using CT.Common.Common;
using CT.Common.Entities;
using CT.Service.UserService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace CT.APIService.Controllers
{
    public class UserController : BaseApiController
    {
        UserService _userService = null;

        public UserController()
        {
            _userService = new UserService();
        }

        public IHttpActionResult GetDealers(int UserID, int RoleID, string SearchText, int PageNo = 1)
        {
            UserEntity model = new UserEntity { UserID = UserID, RoleID = RoleID, SearchText = SearchText, PageNo = PageNo, PageSize = 10, Sort = "" };
            var data = _userService.GetDealers(model);
            return Ok(data);
        }

        public IHttpActionResult GetDealerByID(int DealerID, int UserID, int RoleID)
        {
            UserEntity model = new UserEntity { ID = DealerID, UserID = UserID, RoleID = RoleID };
            BaseUserEntity baseUserEntity = _userService.GetDealerByID(model);
            return Ok(baseUserEntity);
        }

        [HttpPost]
        public IHttpActionResult InsertUpdateDealer()
        {
            string result = string.Empty;
            BaseEntity data = new BaseEntity();
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }

                var provider = Request.Content.ReadAsMultipartAsync<InMemoryMultipartFormDataStreamProvider>(new InMemoryMultipartFormDataStreamProvider()).Result;
                //access form data  
                NameValueCollection formData = provider.FormData;
                var formDictionary = formData.AllKeys.Where(p => formData[p] != "null").ToDictionary(p => p, p => formData[p]);
                string json = JsonConvert.SerializeObject(formDictionary);
                var model = JsonConvert.DeserializeObject<UserEntity>(json);
                //access files  
                IList<HttpContent> files = provider.Files;
                if (files.Count > 0)
                {
                    HttpContent file1 = files[0];
                    var thisFileName = file1.Headers.ContentDisposition.FileName.Trim('\"');
                    string filename = DateTime.Now.Ticks.ToString() + Path.GetExtension(thisFileName);
                    string path = Path.Combine(pathOriginal, filename);
                    Stream input = file1.ReadAsStreamAsync().Result;
                    using (Stream file = File.OpenWrite(path))
                    {
                        input.CopyTo(file);
                        file.Close();
                    }
                    resizeImage(path450250, pathOriginal, filename, 450, 250, 450, 250);
                    model.ProfilePic = filename;
                }

                if (model.ID > 0)
                    data = _userService.UpdateDealer(model);
                else
                    data = _userService.InsertDealer(model);
                return Json(data);
            }
            catch (Exception ex)
            {
                string error = ex.Message + (ex.InnerException != null ? ex.InnerException.Message : string.Empty) + ex.StackTrace;
                WriteLog(error);
                return Json(error);
            }
        }

        public IHttpActionResult GetBIDSByUserID(int UserID, int RoleID, string SearchText, int PageNo = 1)
        {
            UserEntity model = new UserEntity { UserID = UserID, RoleID = RoleID, SearchText = SearchText, PageNo = PageNo, PageSize = 10 };
            BaseVehicleEntity baseVehicleEntity = _userService.GetBIDSByUserID(model);
            return Ok(baseVehicleEntity);
        }

        [HttpGet]
        public IHttpActionResult SaveBIDByUserID(int VehicleID, decimal BidAmount, int UserID, int RoleID)
        {
            VehicleBIDEntity model = new VehicleBIDEntity { VehicleID = VehicleID, BIDAmount = BidAmount, UserID = UserID, RoleID = RoleID };
            BaseVehicleBIDEntity baseVehicleBIDEntity = _userService.SaveBIDByUserID(model);
            if (baseVehicleBIDEntity.ResponseStatus.Status == 1 && baseVehicleBIDEntity.VehicleEntity != null)
            {
                BidNotification(VehicleID, BidAmount, baseVehicleBIDEntity.VehicleEntity.BidTimeMilliSecs);
            }
            return Ok(baseVehicleBIDEntity);
        }

        //to update current bid in android app
        private static void BidNotification(int VehicleID, decimal BidAmount, long? BidTimeMilliSecs)
        {
            try
            {
                int result = new FBNotification().SendDealClosedNotification("/topics/cartimez", string.Empty, false, (VehicleID + "~" + BidAmount + "~" + BidTimeMilliSecs));
            }
            catch (Exception)
            {

            }
        }

        [HttpGet]
        public IHttpActionResult SaveRegistration(string refreshedToken, int UserID, int RoleID)
        {
            VehicleBIDEntity model = new VehicleBIDEntity { refreshedToken = refreshedToken, UserID = UserID, RoleID = RoleID };
            BaseVehicleBIDEntity baseVehicleBIDEntity = _userService.SaveRegistration(model);
            return Ok(baseVehicleBIDEntity);
        }

        [HttpGet]
        public IHttpActionResult CloseDealByTimer()
        {
            BaseUserEntity baseUserEntity = new UserService().CloseBID(0);
            return Ok(baseUserEntity);
        }

        //---------------------------------------------------------------------------------------------------------//

        public IHttpActionResult DeleteDealerByID(int DealerID, int RoleID)
        {
            return RunInSafe(() =>
            {
                UserEntity model = new UserEntity { ID = DealerID, RoleID = RoleID };
                var data = _userService.DeleteDealerByID(model);
                cTApiResponse.Data = data;
                cTApiResponse.IsSuccess = true;
                return Ok(cTApiResponse);
            });
        }

        public IHttpActionResult GetBIDS(int UserID, int RoleID)
        {
            return RunInSafe(() =>
            {
                UserEntity model = new UserEntity { UserID = UserID, RoleID = RoleID };
                var data = _userService.GetBIDS(model);
                cTApiResponse.Data = data;
                cTApiResponse.IsSuccess = true;
                return Ok(cTApiResponse);
            });
        }

        [HttpGet]
        public IHttpActionResult ViewBID(int VehicleID, int UserID, int RoleID)
        {
            return RunInSafe(() =>
            {
                VehicleBIDEntity vehicleEntity = new VehicleBIDEntity { VehicleID = VehicleID, UserID = UserID, RoleID = RoleID };
                var data = _userService.ViewBID(vehicleEntity);
                cTApiResponse.Data = data;
                cTApiResponse.IsSuccess = true;
                return Ok(cTApiResponse);
            });
        }
    }
}
