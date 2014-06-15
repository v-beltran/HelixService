using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace HelixService.Utility
{
    public class HEncoding
    {
        /// <summary>
        /// Returns a byte array from a string value.
        /// </summary>
        /// <param name="value">A string value to get bytes from.</param>
        /// <returns></returns>
        public static Byte[] StringToBytes(String value)
        {
            return Encoding.UTF8.GetBytes(value);
        }
    }
}