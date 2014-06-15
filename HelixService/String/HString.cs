using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelixService.Utility
{
    public class HString
    {
        public static Boolean IsString(Object value)
        {
            return typeof(Object) == typeof(String) ? true : false;
        }

        public static String SafeTrim(Object value)
        {
            return IsString(value) ? value.ToString() : String.Empty;
        }

        public static String NullableTrim(Object value)
        {
            return IsString(value) ? value.ToString() : null;
        }

        public static String Truncate(String value, Int32 maxChars)
        {
            return value == null ? null
                : value.Length <= maxChars ? value
                : value.Substring(0, maxChars) + " ..";
        }
    }
}