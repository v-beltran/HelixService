using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelixService.Utility
{
    public class HString
    {
        /// <summary>
        /// Convert any object to a string.
        /// </summary>
        /// <param name="value">The object to convert to string.</param>
        /// <returns></returns>
        public static String ToString(Object value)
        {
            return value != null ? value.ToString() : String.Empty;
        }

        /// <summary>
        /// Safely trims a string.
        /// </summary>
        /// <param name="value">The string to trim.</param>
        /// <returns></returns>
        public static String SafeTrim(String value)
        {
            return value != null ? value.Trim() : String.Empty;
        }

        /// <summary>
        /// Safely trims a string from object.
        /// </summary>
        /// <param name="value">The object to set string and trim.</param>
        /// <returns></returns>
        public static String SafeTrim(Object value)
        {
            return value != null ? HString.ToString(value).Trim() : String.Empty;
        }

        /// <summary>
        /// Trims a string when null is an acceptable output.
        /// </summary>
        /// <param name="value">The string to trim.</param>
        /// <returns></returns>
        public static String NullableTrim(String value)
        {
            return value != null ? value.Trim() : null;
        }

        /// <summary>
        /// Removes characters from the beginning of the string.
        /// </summary>
        /// <param name="value">The string to be shortened.</param>
        /// <param name="subtractChars">The number of characters to be removed from the beginning of value.</param>
        /// <returns></returns>
        public static String TruncateLeft(String value, Int32 subtractChars)
        {
            return String.Join(String.Empty, value.Skip(subtractChars));
        }

        /// <summary>
        /// Truncates a string and replaces the end with ellipses. 
        /// </summary>
        /// <param name="value">The string to truncate.</param>
        /// <param name="maxChars">The number of characters of the new string.</param>
        /// <returns></returns>
        public static String TruncateRight(String value, Int32 maxChars)
        {
            return value == null ? null
                : value.Length <= maxChars ? value
                : value.Substring(0, maxChars) + "...";
        }
    }
}