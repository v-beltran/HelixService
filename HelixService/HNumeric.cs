using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelixService.Utility
{
    public class HNumeric
    {
        /// <summary>
        /// Gets an integer that can store null.
        /// </summary>
        /// <param name="value">The integer to be acquired from object.</param>
        /// <returns>The integer from object passed or a null.</returns>
        public static Int32? GetNullableInteger(Object value)
        {
            Int32 n;
            if (value == null)
            {
                return null;
            }
            else
            {
                if (Int32.TryParse(value.ToString(), out n))
                {
                    return new Int32?(n);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Gets a decimal that can store null.
        /// </summary>
        /// <param name="value">The decimal to be acquired from object.</param>
        /// <returns>The decimal from object passed or a null.</returns>
        public static Decimal? GetNullableDecimal(Object value)
        {
            Decimal d;
            if (value == null)
            {
                return null;
            }
            else
            {
                if (Decimal.TryParse(value.ToString(), out d))
                {
                    return new Decimal?(d);
                }
                else
                {
                    return null;
                }
            }
        }
    }
}