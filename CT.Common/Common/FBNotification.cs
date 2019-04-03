using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace CT.Common.Common
{
    public class FBNotification
    {
        public int SendDealClosedNotification(string token, string body, bool status, string data = null)
        {
            int result = 0;
            string fcm_url = ConfigurationManager.AppSettings["fcm_url"];
            string Authorization = ConfigurationManager.AppSettings["Authorization"];
            string Sender = ConfigurationManager.AppSettings["Sender"];
            string title = ConfigurationManager.AppSettings["NotificationTitle"];
            string dealClosedMessage = string.Empty;
            if (String.IsNullOrWhiteSpace(body))
            {
                title = ConfigurationManager.AppSettings["DealTitle"];
                if (status)
                    body = ConfigurationManager.AppSettings["DealClosedWon"];
                else
                    body = ConfigurationManager.AppSettings["DealClosedLost"];
            }
            
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
                    body = body,
                    title = title,
                    badge = 1
                }
            };
            string postbody = JsonConvert.SerializeObject(payload).ToString();
            if (!String.IsNullOrWhiteSpace(data))
            {
                var payloaddata = new
                {
                    to = "/topics/cartimez",
                    priority = "high",
                    content_available = true,
                    notification = new
                    {
                        body = "",
                        title = "bid",
                        badge = 1
                    },
                    data = new
                    {
                        list = data,
                    },
                };
                postbody = JsonConvert.SerializeObject(payloaddata).ToString();
            }
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
                                    result = 1;
                            }
                        }
                    }
                }
            }
            return result;
        }
    }
}
