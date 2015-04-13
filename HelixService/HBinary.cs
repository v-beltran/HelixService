using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace HelixService.Utility
{
    public class HBinary
    {
        /// <summary>
        /// Get bytes from a stream object.
        /// </summary>
        /// <param name="input">The stream to get the byte array from.</param>
        /// <returns></returns>
        public static Byte[] GetBytes(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        /// <summary>
        /// Optimized method to get a human-readable file size.
        /// </summary>
        /// <param name="size">The size in bytes.</param>
        /// <param name="precision">The number of digits after the decimal.</param>
        /// <returns></returns>
        public static String GetBytesReadable(Int64 size, Int32 precision = 2)
        {
            // Get absolute value
            Int64 absolute_size = (size < 0 ? -size : size);
            Int32 precisionValue = (precision >= 0 && precision <= 4) ? precision : 2;

            // Determine the suffix and readable value
            String suffix;
            Double readable;

            if (absolute_size >= 0x1000000000000000) // Exabyte
            {
                suffix = "EB";
                readable = (size >> 50);
            }
            else if (absolute_size >= 0x4000000000000) // Petabyte
            {
                suffix = "PB";
                readable = (size >> 40);
            }
            else if (absolute_size >= 0x10000000000) // Terabyte
            {
                suffix = "TB";
                readable = (size >> 30);
            }
            else if (absolute_size >= 0x40000000) // Gigabyte
            {
                suffix = "GB";
                readable = (size >> 20);
            }
            else if (absolute_size >= 0x100000) // Megabyte
            {
                suffix = "MB";
                readable = (size >> 10);
            }
            else if (absolute_size >= 0x400) // Kilobyte
            {
                suffix = "KB";
                readable = size;
            }
            else
            {
                return size.ToString("0 B"); // Byte
            }

            // Divide by 1024 to get fractional value
            readable = (readable / 1024);

            // Return formatted number with suffix
            return String.Format("{0} {1}", readable.ToString("F" + precisionValue), suffix);
        }
    }
}