using CT.APIService.Models;
using CT.Common.Common;
using CT.Common.Entities;
using CT.Service.VehicleService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace CT.APIService.Controllers
{
    public class VehicleController : BaseApiController
    {
        VehicleService _VehicleService = null;

        public VehicleController()
        {
            _VehicleService = new VehicleService();
        }

        [HttpPost]
        public IHttpActionResult InsertUpdateVehicle()
        {
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
                var model = JsonConvert.DeserializeObject<VehicleEntity>(json);
                if (model.ID > 0)
                    data = _VehicleService.UpdateVehicle(model);
                else
                    data = _VehicleService.InsertVehicle(model);

                //access files  
                IList<HttpContent> files = provider.Files;
                foreach (var carimage in files)
                {
                    HttpContent file1 = carimage;
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
                    model.VehicleImage.Add(new VehicleImageEntity { ImageName = filename, VehicleID = model.ID });
                    data = _VehicleService.AddVehicleImages(model);
                }
                return Json(data);
            }
            catch (Exception ex)
            {
                string error = ex.Message + (ex.InnerException != null ? ex.InnerException.Message : string.Empty) + ex.StackTrace;
                WriteLog(error);
                return Json(error);
            }
        }

        public IHttpActionResult DeleteVehicleByID(int ID, int UserID, int RoleID)
        {
            return RunInSafe(() =>
            {
                VehicleEntity model = new VehicleEntity { ID = ID, UserID = UserID, RoleID = RoleID };
                var data = _VehicleService.DeleteVehicleByID(model);
                cTApiResponse.Data = data;
                cTApiResponse.IsSuccess = true;
                return Ok(cTApiResponse);
            });
        }

        public IHttpActionResult GetVehicleByID(int ID, int UserID, int RoleID)
        {
            VehicleEntity model = new VehicleEntity { ID = ID, UserID = UserID, RoleID = RoleID };
            BaseVehicleEntity baseVehicleEntity = _VehicleService.GetVehicleByID(model);
            return Ok(baseVehicleEntity);
        }

        public IHttpActionResult GetVehicles(int UserID, int RoleID, string SearchText, int PageNo = 1)
        {
            VehicleEntity model = new VehicleEntity { UserID = UserID, RoleID = RoleID, SearchText = SearchText, PageNo = PageNo, PageSize = 10 };
            var baseVehicleEntity = _VehicleService.GetVehicles(model);
            return Ok(baseVehicleEntity);
        }

        [HttpPost]
        public IHttpActionResult AddVehicleDetails(VehicleEntity model)
        {
            return RunInSafe(() =>
            {
                BaseEntity data = new BaseEntity();
                data = _VehicleService.AddVehicleDetails(model);
                cTApiResponse.Data = data;
                cTApiResponse.IsSuccess = true;
                return Ok(cTApiResponse);
            });
        }

        [HttpPost]
        public IHttpActionResult AddVehicleDocument(VehicleEntity model)
        {
            return RunInSafe(() =>
            {
                BaseEntity data = new BaseEntity();
                data = _VehicleService.AddVehicleDocument(model);
                cTApiResponse.Data = data;
                cTApiResponse.IsSuccess = true;
                return Ok(cTApiResponse);
            });
        }

        [HttpPost]
        public IHttpActionResult AddVehicleTechnical(VehicleEntity model)
        {
            return RunInSafe(() =>
            {
                BaseEntity data = new BaseEntity();
                data = _VehicleService.AddVehicleTechnical(model);
                cTApiResponse.Data = data;
                cTApiResponse.IsSuccess = true;
                return Ok(cTApiResponse);
            });
        }

        [HttpPost]
        public IHttpActionResult AddVehicleImages(VehicleEntity model)
        {
            return RunInSafe(() =>
            {
                BaseEntity data = new BaseEntity();
                data = _VehicleService.AddVehicleImages(model);
                cTApiResponse.Data = data;
                cTApiResponse.IsSuccess = true;
                return Ok(cTApiResponse);
            });
        }

        public IHttpActionResult DeleteVehicleImage(string ImageName, int VehicleID, int UserID, int RoleID)
        {
            return RunInSafe(() =>
            {
                VehicleImageEntity model = new VehicleImageEntity { ImageName = ImageName, VehicleID = VehicleID, UserID = UserID, RoleID = RoleID };
                BaseEntity data = new BaseEntity();
                data = _VehicleService.DeleteVehicleImage(model);
                cTApiResponse.Data = data;
                cTApiResponse.IsSuccess = true;
                return Ok(cTApiResponse);
            });
        }

        public IHttpActionResult CloseDeal(int ID, int UserID, int RoleID)
        {
            return RunInSafe(() =>
            {
                VehicleEntity model = new VehicleEntity { ID = ID, UserID = UserID, RoleID = RoleID };
                BaseEntity data = new BaseEntity();
                data = _VehicleService.CloseDeal(model);
                cTApiResponse.Data = data;
                cTApiResponse.IsSuccess = true;
                return Ok(cTApiResponse);
            });
        }
    }
}