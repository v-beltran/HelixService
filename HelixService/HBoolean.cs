using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelixService.Utility
{
    public class HBoolean
    {
        /// <summary>
        /// Converts Y/N value to boolean.
        /// </summary>
        /// <param name="value">Y/N</param>
        /// <returns></returns>
        public static Boolean ToBooleanFromYN(String value)
        {
            return value.ToUpper().Equals("Y") ? true : false;
        }

        /// <summary>
        /// Converts a Yes/No value to boolean.
        /// </summary>
        /// <param name="value">Yes/No</param>
        /// <returns></returns>
        public static Boolean ToBooleanFromYesNo(String value)
        {
            return value.ToUpper().Equals("YES") ? true : false;
        }

        /// <summary>
        /// Converts a T/F value to boolean.
        /// </summary>
        /// <param name="value">T/F</param>
        /// <returns></returns>
        public static Boolean ToBooleanFromTF(String value)
        {
            return value.ToUpper().Equals("T") ? true : false;
        }

        /// <summary>
        /// Converts a True/False value to boolean.
        /// </summary>
        /// <param name="value">True/False</param>
        /// <returns></returns>
        public static Boolean ToBooleanFromTrueFalse(String value)
        {
            return value.ToUpper().Equals("TRUE") ? true : false;
        }

        /// <summary>
        /// Converts a boolean to a Y/N string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String ToYNFromBoolean(Boolean value)
        {
            return value ? "Y" : "N";
        }

        /// <summary>
        /// Converts a boolean to a Yes/No string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String ToYesNoFromBoolean(Boolean value)
        {
            return value ? "Yes" : "No";
        }

        /// <summary>
        /// Converts a boolean to a T/F string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String ToTFFromBoolean(Boolean value)
        {
            return value ? "T" : "F";
        }

        /// <summary>
        /// Converts a boolean to a True/False string.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static String ToTrueFalseFromBoolean(Boolean value)
        {
            return value ? "True" : "False";
        }
    }
}
