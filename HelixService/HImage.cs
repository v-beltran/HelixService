using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;

namespace HelixService.Utility
{
    public class HImage
    {
        #region " Main Redraw Function "

        /// <summary>
        /// Create image with the specified width and height.
        /// </summary>
        /// <param name="imageIn">The original image byte array.</param>
        /// <param name="newWidth">The width of the new image.</param>
        /// <param name="newHeight">The height of the image.</param>
        /// <returns>A byte array of the new image.</returns>
        public static Byte[] RedrawImage(Byte[] imageIn, Int32? newWidth, Int32? newHeight)
        {
            Bitmap startBitmap = GetBitmapFromBytes(imageIn);
            Byte[] newBytes = null;

            // Default to original width/height when null
            newWidth = newWidth != null ? newWidth : startBitmap.Width;
            newHeight = newHeight != null ? newHeight : startBitmap.Height;

            using (Bitmap newBitmap = new Bitmap(newWidth.Value, newHeight.Value))
            {
                using (Graphics graphics = Graphics.FromImage(newBitmap))
                {
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.DrawImage(startBitmap, 0, 0, newWidth.Value, newHeight.Value);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        newBitmap.Save(ms, startBitmap.RawFormat);
                        newBytes = ms.ToArray();
                    }
                }
            }

            return newBytes;
        }

        #endregion

        #region " Scale Functions "

        /// <summary>
        /// Scale image with the specified width and height.
        /// </summary>
        /// <param name="imageIn">The original image byte array.</param>
        /// <param name="newWidth">The width of the new image.</param>
        /// <param name="newHeight">The height of the image.</param>
        /// <returns>A byte array of the new scaled image.</returns>
        public static Byte[] RedrawImageScaleByPixels(Byte[] imageIn, Int32? newWidth, Int32? newHeight, Boolean makeSquare)
        {
            Bitmap originalImage = GetBitmapFromBytes(imageIn);

            // If no width is passed, calculate the width from the height passed
            Int32 scaledWidth = (newWidth == null || newWidth.Value == 0) ? (originalImage.Width * newHeight.Value) / originalImage.Height : newWidth.Value;

            // If no height is passed, calculate the height from the width passed
            Int32 scaledHeight = (newHeight == null || newHeight.Value == 0) ? (originalImage.Height * newWidth.Value) / originalImage.Width : newHeight.Value;

            if (makeSquare)
            {
                // Adjust for making the image perfectly square, first by width then by height
                if (newWidth != null) { scaledHeight = newWidth.Value; }
                else { scaledWidth = newHeight.Value; }
            }

            return RedrawImage(imageIn, scaledWidth, scaledHeight);
        }

        /// <summary>
        /// Scale image with the specified ratio(s).
        /// </summary>
        /// <param name="imageIn">The original image byte array.</param>
        /// <param name="scalew">Scale width ratio</param>
        /// <param name="scaleh">Scale height ratio</param>
        /// <param name="scale">Scale width/height ratio</param>
        /// <param name="makeSquare">Y/N</param>
        /// <returns>A byte array of the new scaled image.</returns>
        public static Byte[] RedrawImageScaleByRatio(Byte[] imageIn, Decimal? scalew, Decimal? scaleh, Decimal? scale, Boolean makeSquare)
        {
            Bitmap originalImage = GetBitmapFromBytes(imageIn);
            Byte[] scaleBytes = null;

            if (scalew == null && scaleh != null)
            {
                // Scale-H Only
                scaleBytes = RedrawImageScaleByPixels(imageIn, originalImage.Width, GetPixelsFromScale(originalImage.Height, scaleh), makeSquare);
            }
            else if (scaleh == null && scalew != null)
            {
                // Scale-W only
                scaleBytes = RedrawImageScaleByPixels(imageIn, GetPixelsFromScale(originalImage.Width, scalew), originalImage.Height, makeSquare);
            }
            else if (scalew != null && scaleh != null)
            {
                // Scale-W and Scale-H
                scaleBytes = RedrawImageScaleByPixels(imageIn, GetPixelsFromScale(originalImage.Width, scalew), GetPixelsFromScale(originalImage.Height, scaleh), makeSquare);
            }
            else if ((scalew == null && scaleh == null) && scale != null)
            {
                // Scale proportionately
                scaleBytes = RedrawImageScaleByPixels(imageIn, GetPixelsFromScale(originalImage.Width, scale), GetPixelsFromScale(originalImage.Height, scale), makeSquare);
            }
            else
            {
                // Do nothing
                scaleBytes = imageIn;
            }

            return scaleBytes;
        }

        #endregion

        #region " Crop Functions "

        /// <summary>
        /// Crops an image after it has been scaled.
        /// </summary>
        /// <param name="imageIn">A byte array of the scaled image.</param>
        /// <param name="crop_x">Left | Center | Right</param>
        /// <param name="crop_y">Top | Center | Bottom</param>
        /// <returns>A byte array of the new cropped image.</returns>
        public static Byte[] RedrawImageCropByGrid(Byte[] imageIn, String crop_x, String crop_y)
        {
            Bitmap originalBitmap = GetBitmapFromBytes(imageIn);

            Int32? x = GetCropX(originalBitmap, crop_x);
            Int32? y = GetCropY(originalBitmap, crop_y);
            Int32? cropWidth = GetCropWidth(originalBitmap);
            Int32? cropHeight = GetCropHeight(originalBitmap);

            Rectangle sourceRectangle = new Rectangle(0, 0, cropWidth.Value, cropHeight.Value);
            Rectangle destRectangle = new Rectangle(x.Value, y.Value, cropWidth.Value, cropHeight.Value);

            return RedrawImageCrop(sourceRectangle, destRectangle, originalBitmap, cropWidth, cropHeight);
        }

        /// <summary>
        /// Crops an image based on specific coordinates.
        /// </summary>
        /// <param name="imageIn">A byte array of the scaled image.</param>
        /// <param name="x1">Start point</param>
        /// <param name="y1">Start point</param>
        /// <param name="x2">End point</param>
        /// <param name="y2">End point</param>
        /// <returns>A byte array of the new cropped image.</returns>
        public static Byte[] RedrawImageCropByCoordinates(Byte[] imageIn, Int32? x1, Int32? y1, Int32? x2, Int32? y2)
        {
            Bitmap originalBitmap = GetBitmapFromBytes(imageIn);
            Int32? temp = 0;

            // Swap coordinates if end point < start point
            if (x2 < x1)
            {
                temp = x1;
                x1 = x2;
                x2 = temp;
            }

            // Swap coordinates if end point < start point
            if (y2 < y1)
            {
                temp = y1;
                y1 = y2;
                y2 = temp;
            }

            Int32? cropWidth = new Int32?((Math.Abs(x2.Value - x1.Value)));
            Int32? cropHeight = new Int32?((Math.Abs(y2.Value - y1.Value)));

            Rectangle sourceRectangle = new Rectangle(0, 0, cropWidth.Value, cropHeight.Value);
            Rectangle destRectangle = new Rectangle(x1.Value, y1.Value, cropWidth.Value, cropHeight.Value);

            return RedrawImageCrop(sourceRectangle, destRectangle, originalBitmap, cropWidth.Value, cropHeight.Value);
        }

        /// <summary>
        /// Crops an image's entire width based on the 'y' query.
        /// </summary>
        /// <param name="imageIn">A byte array of the scaled image.</param>
        /// <param name="crop_y">Top | Center | Bottom</param>
        /// <returns>A byte array of the new cropped image.</returns>
        public static Byte[] RedrawImageCropByGridWidth(Byte[] imageIn, String crop_y)
        {
            Bitmap originalBitmap = GetBitmapFromBytes(imageIn);

            Int32? x = 0;
            Int32? y = GetCropY(originalBitmap, crop_y);
            Int32? cropWidth = originalBitmap.Width;
            Int32? cropHeight = GetCropHeight(originalBitmap);

            Rectangle sourceRectangle = new Rectangle(0, 0, cropWidth.Value, cropHeight.Value);
            Rectangle destRectangle = new Rectangle(x.Value, y.Value, cropWidth.Value, cropHeight.Value);

            return RedrawImageCrop(sourceRectangle, destRectangle, originalBitmap, cropWidth, cropHeight);
        }

        /// <summary>
        /// Crops an image's entire height based on the 'x' query.
        /// </summary>
        /// <param name="imageIn">A byte array of the scaled image.</param>
        /// <param name="crop_x">Left | Center | Right</param>
        /// <returns>A byte array of the new cropped image.</returns>
        public static Byte[] RedrawImageCropByGridHeight(Byte[] imageIn, String crop_x)
        {
            Bitmap originalBitmap = GetBitmapFromBytes(imageIn);

            Int32? x = GetCropX(originalBitmap, crop_x);
            Int32? y = 0;
            Int32? cropWidth = GetCropWidth(originalBitmap);
            Int32? cropHeight = originalBitmap.Height;

            Rectangle sourceRectangle = new Rectangle(0, 0, cropWidth.Value, cropHeight.Value);
            Rectangle destRectangle = new Rectangle(x.Value, y.Value, cropWidth.Value, cropHeight.Value);

            return RedrawImageCrop(sourceRectangle, destRectangle, originalBitmap, cropWidth, cropHeight);
        }

        /// <summary>
        /// Creates a byte array for the image to be redrawn.
        /// </summary>
        /// <param name="sourceRectangle">The original image dimensions.</param>
        /// <param name="destRectangle">The new image dimensions.</param>
        /// <param name="scaleBitmap">A byte array of the scaled image.</param>
        /// <param name="newWidth">The width of the new image.</param>
        /// <param name="newHeight">The height of the new image.</param>
        /// <returns>A byte array of the image to be redrawn.</returns>
        public static Byte[] RedrawImageCrop(Rectangle sourceRectangle, Rectangle destRectangle, Bitmap scaleBitmap, Int32? newWidth, Int32? newHeight)
        {
            Byte[] cropBytes = null;

            using (Bitmap newBitmap = new Bitmap(newWidth.Value, newHeight.Value))
            {
                using (Graphics graphics = Graphics.FromImage(newBitmap))
                {
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.DrawImage(scaleBitmap, sourceRectangle, destRectangle, GraphicsUnit.Pixel);

                    using (MemoryStream ms = new MemoryStream())
                    {
                        newBitmap.Save(ms, scaleBitmap.RawFormat);
                        cropBytes = ms.ToArray();
                    }
                }
            }

            return cropBytes;
        }

        /// <summary>
        /// Creates a byte array for the image to be cropped.
        /// </summary>
        /// <param name="imageIn">The image to be cropped.</param>
        /// <param name="width">The width in pixels.</param>
        /// <param name="height">The height in pixels.</param>
        /// <returns>A byte array of the image to be redrawn.</returns>
        public static Byte[] RedrawImageCropByPixels(Byte[] imageIn, Int32? width, Int32? height)
        {
            Byte[] croppedImage = null;
            Bitmap originalImage = GetBitmapFromBytes(imageIn);

            if ((width == null && height == null) || (width == 0 && height == 0))
            {
                // Return back original image
                return imageIn;
            }
            else
            {
                // If no width is passed, use original width
                Int32 croppedWidth = (width == null || width == 0) ? originalImage.Width : width.Value;

                // If no height is passed, use original height
                Int32 croppedHeight = (height == null || height == 0) ? originalImage.Height : height.Value;

                // Handle height or width that is bigger than the original height or width respectively
                if (originalImage.Width < croppedWidth)
                {
                    croppedWidth = originalImage.Width;
                }

                if (originalImage.Height < croppedHeight)
                {
                    croppedHeight = originalImage.Height;
                }

                // Choose which part of the bitmap to show: the center
                Rectangle rect = new Rectangle((originalImage.Width - croppedWidth) / 2, (originalImage.Height - croppedHeight) / 2, croppedWidth, croppedHeight);

                // Bitmap object to hold the cropped image
                Bitmap destBitmap = new Bitmap(rect.Width, rect.Height);

                // Graphics object to perform the actual cropping
                using (Graphics graphics = Graphics.FromImage(destBitmap))
                {
                    graphics.DrawImage(originalImage, new Rectangle(0, 0, rect.Width, rect.Height), rect, GraphicsUnit.Pixel);

                    using (MemoryStream destStream = new MemoryStream())
                    {
                        // Save the image using the default formatting of the image file
                        destBitmap.Save(destStream, originalImage.RawFormat);
                        destStream.Close();
                        croppedImage = destStream.ToArray();
                    }
                }
            }

            //Return the scaled image binary
            return croppedImage;
        }

        /// <summary>
        /// Determine values to use for cropping and get new bytes for the cropping
        /// </summary>
        /// <param name="imageIn">The bytes of the provided image.</param>
        /// <param name="x1">Start point for cropping</param>
        /// <param name="y1">Start point for cropping</param>
        /// <param name="x2">End point for cropping</param>
        /// <param name="y2">End point for cropping</param>
        /// <returns>A byte array of the new cropped image.</returns>
        public static Byte[] RedrawImageSafeCropByCoordinates(Byte[] imageIn, Int32? x1, Int32? y1, Int32? x2, Int32? y2)
        {
            Bitmap originalBitmap = GetBitmapFromBytes(imageIn);
            Byte[] cropBytes = null;

            // Default x1 starting point to 0
            if (x1 == null || x1.Value <= 0)
            {
                x1 = 0;
            }

            // Default y1 starting point to 0
            if (y1 == null || y1.Value <= 0)
            {
                y1 = 0;
            }

            // Default x2 end point to width of image
            if (x2 == null || x2.Value == 0 || x2.Value > originalBitmap.Width)
            {
                x2 = originalBitmap.Width;
            }

            // Default y2 end point to width of image
            if (y2 == null || y2.Value == 0 || y2.Value > originalBitmap.Height)
            {
                y2 = originalBitmap.Height;
            }

            // Crop image based on coordinates if one or none were specified
            if (x1.Value != 0 || y1.Value != 0 || x2.Value != 0 || y2.Value != 0)
            {
                // Crop based on original image coordinates
                cropBytes = RedrawImageCropByCoordinates(imageIn, x1, y1, x2, y2);
            }
            else
            {
                // Do nothing
                cropBytes = imageIn;
            }

            return cropBytes;
        }

        /// <summary>
        /// Determine values to use for cropping and get new bytes for the cropping
        /// </summary>
        /// <param name="imageIn">The bytes of the provided image.</param>
        /// <param name="x">Left | Center | Right</param>
        /// <param name="y">Top | Center | Bottom</param>
        /// <returns>A byte array of the new cropped image.</returns>
        public static Byte[] RedrawImageSafeCropByGrid(Byte[] imageIn, String x, String y)
        {
            Byte[] cropBytes = null;

            if (x == String.Empty && y != String.Empty)
            {
                // y-value only
                cropBytes = RedrawImageCropByGridWidth(imageIn, y);
            }
            else if (y == String.Empty && x != String.Empty)
            {
                // x-value only
                cropBytes = RedrawImageCropByGridHeight(imageIn, x);
            }
            else if (x != String.Empty & y != String.Empty)
            {
                // Both x- and y-value
                cropBytes = RedrawImageCropByGrid(imageIn, x, y);
            }
            else
            {
                // Do nothing
                cropBytes = imageIn;
            }

            return cropBytes;
        }

        #endregion

        #region " Pad Functions "

        /// <summary>
        /// Pads an image with 1/2 its dimensions 
        /// </summary>
        /// <param name="imageIn">A byte array of a given image.</param>
        /// <returns>A byte array of the padded image.</returns>
        public static Byte[] RedrawImageWithPad(Byte[] imageIn)
        {
            Byte[] padBytes = null;
            Bitmap startBitmap = GetBitmapFromBytes(imageIn);
            Bitmap resizeBitmap = GetBitmapFromBytes(RedrawImage(imageIn, (Int32)Math.Ceiling((Decimal)(startBitmap.Width / 2)), (Int32)Math.Ceiling((Decimal)(startBitmap.Height / 2))));

            using (Bitmap newBitmap = new Bitmap(startBitmap.Width, startBitmap.Height))
            {
                using (Graphics graphics = Graphics.FromImage(newBitmap))
                {
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.FillRectangle(System.Drawing.Brushes.White, 0, 0, startBitmap.Width, startBitmap.Height);
                    graphics.DrawImage(resizeBitmap, new Point((startBitmap.Width - resizeBitmap.Width) / 2, (startBitmap.Height - resizeBitmap.Height) / 2));

                    using (MemoryStream ms = new MemoryStream())
                    {
                        newBitmap.Save(ms, startBitmap.RawFormat);
                        padBytes = ms.ToArray();
                    }
                }
            }

            return padBytes;
        }

        /// <summary>
        /// Pads an image with respect to the number of pixels specified.
        /// </summary>
        /// <param name="imageIn">A byte array of a given image.</param>
        /// <param name="pixels">The number of pixels around the image.</param>
        /// <returns>A byte array of the padded image.</returns>
        public static Byte[] RedrawImageWithPad(Byte[] imageIn, Int32? pixels)
        {
            Byte[] padBytes = null;
            Bitmap startBitmap = GetBitmapFromBytes(imageIn);
            Bitmap resizeBitmap = GetBitmapFromBytes(RedrawImage(imageIn, startBitmap.Width - pixels.Value, startBitmap.Height - pixels.Value));

            using (Bitmap newBitmap = new Bitmap(startBitmap.Width, startBitmap.Height))
            {
                using (Graphics graphics = Graphics.FromImage(newBitmap))
                {
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.FillRectangle(System.Drawing.Brushes.White, 0, 0, startBitmap.Width, startBitmap.Height);
                    graphics.DrawImage(resizeBitmap, new Point((startBitmap.Width - resizeBitmap.Width) / 2, (startBitmap.Height - resizeBitmap.Height) / 2));

                    using (MemoryStream ms = new MemoryStream())
                    {
                        newBitmap.Save(ms, startBitmap.RawFormat);
                        padBytes = ms.ToArray();
                    }
                }
            }

            return padBytes;
        }

        #endregion

        #region " Image Utility Functions "

        /// <summary>
        /// Finds the x-coordinate for cropping an image.
        /// </summary>
        /// <param name="imageIn">A byte array of the scaled image.</param>
        /// <param name="crop_x">Left | Center | Right</param>
        /// <returns>The value for the x-coordinate.</returns>
        public static Int32? GetCropX(Bitmap imageIn, String crop_x)
        {
            Int32? x = null;

            switch (crop_x)
            {
                case "left":
                    x = 0;
                    break;
                case "center":
                    x = (Int32)Math.Ceiling((Decimal)(imageIn.Width / 3));
                    break;
                case "right":
                    x = (Int32)Math.Ceiling((Decimal)(imageIn.Width / 1.5));
                    break;
                default:
                    x = 0;
                    break;
            }

            return x;
        }

        /// <summary>
        /// Finds the y-coordinate for cropping an image.
        /// </summary>
        /// <param name="imageIn">A byte array of the scaled image.</param>
        /// <param name="crop_y">Top | Center | Bottom</param>
        /// <returns>The value for the y-coordinate.</returns>
        public static Int32? GetCropY(Bitmap imageIn, String crop_y)
        {
            Int32? y = null;

            switch (crop_y)
            {
                case "top":
                    y = 0;
                    break;
                case "center":
                    y = (Int32)Math.Ceiling((Decimal)(imageIn.Height / 3));
                    break;
                case "bottom":
                    y = (Int32)Math.Ceiling((Decimal)(imageIn.Height / 1.5));
                    break;
                default:
                    y = 0;
                    break;
            }

            return y;
        }

        /// <summary>
        /// Gets the width of the image to be cropped.
        /// </summary>
        /// <param name="imageIn">A bitmap of the scaled image.</param>
        /// <returns>The width of the new image.</returns>
        public static Int32? GetCropWidth(Bitmap imageIn)
        {
            return (Int32)Math.Ceiling((Decimal)(imageIn.Width / 3));
        }

        /// <summary>
        /// Gets the height of the image to be cropped.
        /// </summary>
        /// <param name="imageIn">A bitmap of the scaled image.</param>
        /// <returns>The height of the new image.</returns>
        public static Int32? GetCropHeight(Bitmap imageIn)
        {
            return (Int32)Math.Ceiling((Decimal)(imageIn.Height / 3));
        }

        /// <summary>
        /// Converts a byte array to a bitmap.
        /// </summary>
        /// <param name="imageIn">A byte array of the original image.</param>
        /// <returns>The bitmap of the image.</returns>
        public static Bitmap GetBitmapFromBytes(Byte[] imageIn)
        {
            using (MemoryStream ms = new MemoryStream(imageIn))
            {
                Bitmap imageBitmap = (Bitmap)System.Drawing.Image.FromStream(ms);
                return imageBitmap;
            }
        }

        /// <summary>
        /// Converts a bitmap image to a byte array.
        /// </summary>
        /// <param name="imageIn">A bitmap of the image.</param>
        /// <returns>A byte array of the image.</returns>
        public static Byte[] GetBytesFromBitmap(Bitmap imageIn)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                imageIn.Save(ms, HImage.GetImageFormat(imageIn));
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Get the image format.
        /// </summary>
        /// <param name="imageIn">The bitmap of the image.</param>
        /// <returns>The image format of the image passed.</returns>
        public static ImageFormat GetImageFormat(Bitmap imageIn)
        {
            return ImageFormat.Jpeg.Equals(imageIn.RawFormat) ? ImageFormat.Jpeg :
                ImageFormat.Bmp.Equals(imageIn.RawFormat) ? ImageFormat.Bmp :
                ImageFormat.Png.Equals(imageIn.RawFormat) ? ImageFormat.Png :
                ImageFormat.Gif.Equals(imageIn.RawFormat) ? ImageFormat.Gif :
                ImageFormat.Jpeg;
        }

        /// <summary>
        /// Gets the new dimensions from given scale.
        /// </summary>
        /// <param name="currentSize">The value for the current width/height.</param>
        /// <param name="scale">The value to scale the current width/height.</param>
        /// <returns>The value of the new width/height.</returns>
        public static Int32? GetPixelsFromScale(Int32 currentSize, Decimal? scale)
        {
            return (Int32)Math.Ceiling(currentSize * scale.Value);
        }

        #endregion
    }
}