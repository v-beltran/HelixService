using HelixService.Utility;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelixServiceUI.ImageTransform
{
    /// <summary>
    /// Resize, scale, crop, and pad images
    /// </summary>
    public class Image : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            // Possible query strings to transform an image
            Int32? width = HNumeric.GetNullableInteger(context.Request.QueryString["width"]);
            Int32? height = HNumeric.GetNullableInteger(context.Request.QueryString["height"]);
            Decimal? scale = HNumeric.GetNullableDecimal(context.Request.QueryString["scale"]);
            Decimal? scalew = HNumeric.GetNullableDecimal(context.Request.QueryString["scalew"]);
            Decimal? scaleh = HNumeric.GetNullableDecimal(context.Request.QueryString["scaleh"]);
            Int32? pad = HNumeric.GetNullableInteger(context.Request.QueryString["pad"]);
            Int32? x1 = HNumeric.GetNullableInteger(context.Request.QueryString["x1"]);
            Int32? y1 = HNumeric.GetNullableInteger(context.Request.QueryString["y1"]);
            Int32? x2 = HNumeric.GetNullableInteger(context.Request.QueryString["x2"]);
            Int32? y2 = HNumeric.GetNullableInteger(context.Request.QueryString["y2"]);
            String cropx = HString.SafeTrim(context.Request.QueryString["x"]);
            String cropy = HString.SafeTrim(context.Request.QueryString["y"]);
            Boolean square = HBoolean.ToBooleanFromYN(HString.SafeTrim(context.Request.QueryString["square"]));

            // Grab image from database--just set one for testing
            Bitmap image = (Bitmap)System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath("image.gif"));
            Byte[] imageBytes;

            /* 1. Resize image */
            imageBytes = HImage.RedrawImage(HImage.GetBytesFromBitmap(image), width, height);

            /* 2. Scale the resized image */
            imageBytes = HImage.RedrawImageScaleByRatio(imageBytes, scalew, scaleh, scale, square);

            /* 3. Crop the scaled image */
            if (!String.IsNullOrEmpty(cropx) || !String.IsNullOrEmpty(cropy))
                imageBytes = HImage.RedrawImageSafeCropByGrid(imageBytes, cropx, cropy);
            else
                imageBytes = HImage.RedrawImageSafeCropByCoordinates(imageBytes, x1, y1, x2, y2);

            /* 4. Add padding to image */
            if (pad > 0)
                imageBytes = HImage.RedrawImageWithPad(imageBytes, pad);

            context.Response.Clear();
            context.Response.ContentType = "image/" + HImage.GetImageFormat(image).ToString();
            context.Response.BinaryWrite(imageBytes);
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}