using CT.APIService.Models;
using CT.Common.Common;
using CT.Common.Entities;
using CT.Service.UserService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
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

        [HttpPost]
        public async Task<IHttpActionResult> InsertUpdateDealer()
        {
            BaseEntity data = new BaseEntity();
            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {
                    throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
                }

                var provider = await Request.Content.ReadAsMultipartAsync<InMemoryMultipartFormDataStreamProvider>(new InMemoryMultipartFormDataStreamProvider());
                //access form data  
                NameValueCollection formData = provider.FormData;
                var formDictionary = formData.AllKeys.Where(p => formData[p] != "null").ToDictionary(p => p, p => formData[p]);
                string json = JsonConvert.SerializeObject(formDictionary);
                var model = JsonConvert.DeserializeObject<UserEntity>(json);
                //access files  
                IList<HttpContent> files = provider.Files;
                HttpContent file1 = files[0];
                var thisFileName = file1.Headers.ContentDisposition.FileName.Trim('\"');
                string filename = DateTime.Now.Ticks.ToString() + Path.GetExtension(thisFileName);
                string path = Path.Combine(System.Web.Hosting.HostingEnvironment.MapPath("~/Images/Original/"), filename);
                Stream input = await file1.ReadAsStreamAsync();
                using (Stream file = File.OpenWrite(path))
                {
                    input.CopyTo(file);
                    file.Close();
                }
                resizeImage(System.Web.Hosting.HostingEnvironment.MapPath("~/Images/450250/"), System.Web.Hosting.HostingEnvironment.MapPath("~/Images/Original/"), filename, 450, 250, 450, 250);
                model.ProfilePic = filename;

                if (model.ID > 0)
                    data = _userService.UpdateDealer(model);
                else
                    data = _userService.InsertDealer(model);
            }
            catch (Exception ex)
            {
                return Ok(ex.Message + ex.InnerException);
            }
            var result = JsonConvert.SerializeObject(data);
            return Ok(result);
        }

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

        public IHttpActionResult GetDealerByID(int DealerID, int UserID, int RoleID)
        {
            UserEntity model = new UserEntity { ID = DealerID, UserID = UserID, RoleID = RoleID };
            BaseUserEntity baseUserEntity = _userService.GetDealerByID(model);
            return Ok(baseUserEntity);
        }

        public IHttpActionResult GetDealers(int UserID, int RoleID)
        {
            UserEntity model = new UserEntity { ID = UserID, RoleID = RoleID };
            var data = _userService.GetDealers(model);
            return Ok(data);
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

        public IHttpActionResult GetBIDSByUserID(int UserID, int RoleID)
        {
            UserEntity model = new UserEntity { UserID = UserID, RoleID = RoleID };
            BaseVehicleEntity baseVehicleEntity = _userService.GetBIDSByUserID(model);
            return Ok(baseVehicleEntity);
        }

        [HttpGet]
        public IHttpActionResult SaveBIDByUserID(int VehicleID, decimal BidAmount, int UserID, int RoleID)
        {
            VehicleBIDEntity model = new VehicleBIDEntity { VehicleID = VehicleID, BIDAmount = BidAmount, UserID = UserID, RoleID = RoleID };
            BaseVehicleBIDEntity baseVehicleBIDEntity = _userService.SaveBIDByUserID(model);
            return Ok(baseVehicleBIDEntity);
        }

        //[HttpPost]
        //public async Task<IHttpActionResult> InsertUpdateDealer()
        //{
        //    if (!Request.Content.IsMimeMultipartContent())
        //    {
        //        throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
        //    }

        //    var provider = await Request.Content.ReadAsMultipartAsync<InMemoryMultipartFormDataStreamProvider>(new InMemoryMultipartFormDataStreamProvider());
        //    NameValueCollection formData = provider.FormData;
        //    IList<HttpContent> files = provider.Files;

        //    BaseEntity data = new BaseEntity();
        //    UserEntity model = new UserEntity();
        //    //foreach (string kvp in formData.AllKeys)
        //    //{
        //    //    PropertyInfo pi = model.GetType().GetProperty(kvp, BindingFlags.Public | BindingFlags.Instance);
        //    //    if (pi != null)
        //    //    {
        //    //        pi.SetValue(model, formData[kvp], null);
        //    //    }
        //    //}
        //    var formDictionary = formData.AllKeys.Where(p => formData[p] != "null").ToDictionary(p => p, p => formData[p]);
        //    string json = JsonConvert.SerializeObject(formDictionary);
        //    model = JsonConvert.DeserializeObject<UserEntity>(json);

        //    var httpRequest = HttpContext.Current.Request;
        //    if (httpRequest.Files.Count < 1)
        //    {
        //        Request.CreateResponse(HttpStatusCode.BadRequest);
        //    }

        //    foreach (string httpfile in httpRequest.Files)
        //    {
        //        var file = httpRequest.Files[httpfile];
        //        if (file != null && file.ContentLength > 0 && ValidateImageExtension(Path.GetExtension(file.FileName)))
        //        {
        //            try
        //            {
        //                model.ProfilePic = DateTime.Now.Ticks.ToString() + Path.GetExtension(file.FileName);
        //                file.SaveAs(Path.Combine(HttpContext.Current.Server.MapPath("~/Images/Original/"), model.ProfilePic));
        //                resizeImage(HttpContext.Current.Server.MapPath("~/Images/450250/"), HttpContext.Current.Server.MapPath("~/Images/Original/"), model.ProfilePic, 450, 250, 450, 250);
        //            }
        //            catch (Exception)
        //            {

        //            }
        //        }
        //    }

        //    if (model.ID > 0)
        //        data = _userService.UpdateDealer(model);
        //    else
        //        data = _userService.InsertDealer(model);

        //    return Ok(data);
        //}
    }
}
