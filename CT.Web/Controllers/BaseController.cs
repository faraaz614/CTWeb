using CT.Common.Common;
using CT.Web.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace CT.Web.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {
        string apival = ConfigurationManager.AppSettings["ApiUrl"];

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
    }
}