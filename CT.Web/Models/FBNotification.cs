using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;

namespace CT.Web.Models
{
    public class FBNotification
    {
        public void SendDealClosedNotification(string token)
        {
            string fcm_url = ConfigurationManager.AppSettings["fcm_url"];
            string Authorization = ConfigurationManager.AppSettings["Authorization"];
            string Sender = ConfigurationManager.AppSettings["Sender"];
            string title = ConfigurationManager.AppSettings["NotificationTitle"];
            string dealClosedMessage = ConfigurationManager.AppSettings["DealClosedMessage"];

            WebRequest tRequest = WebRequest.Create(fcm_url);
            tRequest.Method = "post";
            //serverKey - Key from Firebase cloud messaging server  
            tRequest.Headers.Add(string.Format("Authorization: key={0}", Authorization));
            //Sender Id - From firebase project setting  
            tRequest.Headers.Add(string.Format("Sender: id={0}", Sender));
            tRequest.ContentType = "application/json";
            var payload = new
            {
                to = token,
                priority = "high",
                content_available = true,
                notification = new
                {
                    body = dealClosedMessage,
                    title = title,
                    badge = 1
                }
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
                            }
                        }
                    }
                }
            }
        }
    }
}