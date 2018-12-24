using CT.Common.Common;
using CT.Web.Common;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace CT.Web.Controllers
{
    public class HomeController : BaseController
    {
        public async Task<ActionResult> Index()
        {
            bool status = true;//db data
            string message = "";//db data
            if (status)
            {
                TempData[CommonUtility.Success.ToString()] = message;
            }
            else
            {
                TempData[CommonUtility.Error.ToString()] = message;
            }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        [HttpPost]
        public ActionResult Contact(HttpPostedFileBase file)
        {
            ViewBag.Message = "Your contact page.";
            if (file != null)
            {
                try
                {
                    file.SaveAs(Path.Combine(Server.MapPath("~/Images/Original/"), Path.GetFileName(file.FileName)));
                    resizeImage(Server.MapPath("~/Images/450250/"), Server.MapPath("~/Images/Original/"), Path.GetFileName(file.FileName), 450, 250, 450, 250);
                }
                catch (Exception ex)
                {
                    throw;
                }
            }
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }


        private void resizeImage(string newpath, string originalpath, string originalFilename, int canvasWidth, int canvasHeight, int originalWidth, int originalHeight)
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
    }
}