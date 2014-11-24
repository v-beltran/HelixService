using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
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

        /// <summary>
        /// Determines if the string is a valid password. A valid password is between 8-32
        /// characters and contains at least 1 digit, lowercase and uppercase character.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns></returns>
        public static Boolean IsValidPassword(String value)
        {
            return Regex.IsMatch(value, @"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])[0-9a-zA-Z]{8,32}$");
        }

        /// <summary>
        /// Determines if the string is a valid e-mail address.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns></returns>
        public static Boolean IsValidEmail(String value)
        {
            return Regex.IsMatch(value, @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" +
                @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$");
        }

        /// <summary>
        /// Determines if the string is a valid phone number (U.S. only).
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns></returns>
        public static Boolean IsValidPhoneNumber(String value)
        {
            return Regex.IsMatch(value, @"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$");
        }

        /// <summary>
        /// Determines if the string is a valid ZIP code (U.S. only).
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns></returns>
        public static Boolean IsValidZIPCode(String value)
        {
            return Regex.IsMatch(value, @"^\d{5}(?:[-\s]\d{4})?$");
        }

        /// <summary>
        /// Determines if the string is a valid URL.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns></returns>
        public static Boolean IsValidURL(String value)
        {
            return Regex.IsMatch(value, @"^(((ht|f)tp(s?)\:\/\/)|(www\.))[0-9a-zA-Z]([-.\w]*[0-9a-zA-Z])*(:(0-9)*)*(\/?)([a-zA-Z0-9\-\.\?\,\'\/\\\+&amp;%\$#_]*)?$");
        }
    }
}