using CT.Common.Common;
using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Text;
using System.Web.Http;

namespace CT.APIService.Controllers
{
    public class BaseApiController : ApiController
    {
        public readonly CTApiResponse cTApiResponse;
        string extensions = ConfigurationManager.AppSettings["extensions"];
        public string pathOriginal = System.Web.Hosting.HostingEnvironment.MapPath("~/Images/Original/").Replace(@"\ctapi", string.Empty);
        public string path450250 = System.Web.Hosting.HostingEnvironment.MapPath("~/Images/450250/").Replace(@"\ctapi", string.Empty);

        public BaseApiController()
        {
            cTApiResponse = new CTApiResponse();
        }

        [NonAction]
        public IHttpActionResult RunInSafe(Func<IHttpActionResult> fn)
        {
            try
            {
                return fn();
            }
            catch (Exception ex)
            {
                throw;
            }
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
                encoderParameters.Param[0] = new EncoderParameter(System.Drawing.Imaging.Encoder.Quality, 100L);
                //thumbnail.Save(newpath + originalFilename, info[1], encoderParameters);
                using (MemoryStream memory = new MemoryStream())
                {
                    using (FileStream fs = new FileStream(newpath + originalFilename, FileMode.Create, FileAccess.ReadWrite))
                    {
                        thumbnail.Save(memory, ImageFormat.Jpeg);
                        byte[] bytes = memory.ToArray();
                        fs.Write(bytes, 0, bytes.Length);
                    }
                }
            }
            catch (Exception ex)
            {
                string error = ex.Message + (ex.InnerException != null ? ex.InnerException.Message : string.Empty) + ex.StackTrace;
                WriteLog(error);
            }
        }

        public bool ValidateImageExtension(string ext)
        {
            return extensions.Contains(ext);
        }

        public void WriteLog(string log)
        {
            string path = System.Web.Hosting.HostingEnvironment.MapPath("~/Images/Original/") + "log.txt";
            StringBuilder sb = new StringBuilder();
            sb.Append("------------" + Environment.NewLine);
            sb.Append(log + Environment.NewLine);
            sb.Append("------------" + Environment.NewLine);
            File.AppendAllText(path, sb.ToString());
        }
    }
}