using CT.Common.Common;
using CT.Web.Common;
using CT.Web.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CT.Web.Controllers
{
    [CTAuthorizeAttribute]
    public class BaseController : Controller
    {
        string apival = ConfigurationManager.AppSettings["ApiUrl"];
        string extensions = ConfigurationManager.AppSettings["extensions"];

        protected virtual new CustomPrincipal User
        {
            get { if (HttpContext != null) return HttpContext.User as CustomPrincipal; else return null; }
        }

        public async Task<CTApiResponse> Get(string url)
        {
            var apiResponse = new CTApiResponse(false, "Unable to connect to api");
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apival);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                SetClientAuthentication(client);

                HttpResponseMessage response = await client.GetAsync("api" + url);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    HttpContext.Response.RedirectToRoute("login");

                if (response.IsSuccessStatusCode)
                    apiResponse = await response.Content.ReadAsAsync<CTApiResponse>();

                return apiResponse;
            }
        }

        protected virtual async Task<string> Post(string url, Dictionary<string, string> formdata)
        {

            if (formdata == null)
                return string.Empty;

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apival);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                SetClientAuthentication(client);

                var postData = new List<KeyValuePair<string, string>>();

                foreach (var param in formdata)
                    postData.Add(new KeyValuePair<string, string>(param.Key, param.Value));

                HttpContent content = new FormUrlEncodedContent(postData);
                HttpResponseMessage response = await client.PostAsync("api" + url, content);

                string apiResponse = string.Empty;
                if (response.IsSuccessStatusCode)
                    apiResponse = await response.Content.ReadAsStringAsync();

                return apiResponse;
            }
        }

        protected virtual async Task<CTApiResponse> Post<T>(string url, T formdata)
        {
            if (formdata == null)
                throw new ArgumentException("cannot create post request without formdata");

            var apiResponse = new CTApiResponse(false, "Unable to connect to api");
            using (HttpClient client = new HttpClient())
            {

                client.BaseAddress = new Uri(apival);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                SetClientAuthentication(client);

                HttpResponseMessage response = await client.PostAsJsonAsync("api" + url, formdata);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    HttpContext.Response.RedirectToRoute("login");

                if (response.IsSuccessStatusCode)
                    apiResponse = await response.Content.ReadAsAsync<CTApiResponse>();

                return apiResponse;
            }
        }

        protected virtual async Task<CTApiResponse> Put<T>(string url, T formdata)
        {
            if (formdata == null)
                throw new ArgumentException("cannot create post request without formdata");

            var apiResponse = new CTApiResponse(false, "Unable to connect to api");
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apival);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                SetClientAuthentication(client);

                HttpResponseMessage response = await client.PutAsJsonAsync("api" + url, formdata);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    HttpContext.Response.RedirectToRoute("login");

                if (response.IsSuccessStatusCode)
                    apiResponse = await response.Content.ReadAsAsync<CTApiResponse>();

                return apiResponse;
            }
        }

        protected virtual async Task<CTApiResponse> Delete(string url)
        {
            var apiResponse = new CTApiResponse(false, "Unable to connect to api");
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(apival);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                SetClientAuthentication(client);

                HttpResponseMessage response = await client.DeleteAsync("api" + url);

                if (response.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                    HttpContext.Response.RedirectToRoute("login");

                if (response.IsSuccessStatusCode)
                    apiResponse = await response.Content.ReadAsAsync<CTApiResponse>();

                return apiResponse;
            }
        }

        void SetClientAuthentication(HttpClient client)
        {
            //var token =  HttpContext.User.Identity.GetToken();

            //if (string.IsNullOrWhiteSpace(token))
            //    return;

            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
        }

        public void resizeImage(string newpath, string originalpath, string originalFilename, int canvasWidth, int canvasHeight, int originalWidth, int originalHeight)
        {
            try
            {
                Image image = Image.FromFile(originalpath + originalFilename);
                System.Drawing.Image thumbnail = new Bitmap(canvasWidth, canvasHeight); // changed parm names
                System.Drawing.Graphics graphic = System.Drawing.Graphics.FromImage(thumbnail);
                graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphic.SmoothingMode = SmoothingMode.HighQuality;
                graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
                graphic.CompositingQuality = CompositingQuality.HighQuality;

                // Figure out the ratio
                double ratioX = (double)canvasWidth / (double)originalWidth;
                double ratioY = (double)canvasHeight / (double)originalHeight;
                // use whichever multiplier is smaller
                double ratio = ratioX < ratioY ? ratioX : ratioY;

                // now we can get the new height and width
                int newHeight = Convert.ToInt32(originalHeight * ratio);
                int newWidth = Convert.ToInt32(originalWidth * ratio);

                // Now calculate the X,Y position of the upper-left corner 
                // (one of these will always be zero)
                int posX = Convert.ToInt32((canvasWidth - (originalWidth * ratio)) / 2);
                int posY = Convert.ToInt32((canvasHeight - (originalHeight * ratio)) / 2);

                graphic.Clear(Color.White); // white padding
                graphic.DrawImage(image, posX, posY, newWidth, newHeight);

                System.Drawing.Imaging.ImageCodecInfo[] info = ImageCodecInfo.GetImageEncoders();
                EncoderParameters encoderParameters;
                encoderParameters = new EncoderParameters(1);
                encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, 100L);
                thumbnail.Save(newpath + originalFilename, info[1], encoderParameters);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public bool ValidateImageExtension(string ext)
        {
            return extensions.Contains(ext);
        }
    }
}