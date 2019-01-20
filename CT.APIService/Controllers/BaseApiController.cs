using CT.Common.Common;
using System;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Web.Http;

namespace CT.APIService.Controllers
{
    public class BaseApiController : ApiController
    {
        public readonly CTApiResponse cTApiResponse;
        string extensions = ConfigurationManager.AppSettings["extensions"];

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