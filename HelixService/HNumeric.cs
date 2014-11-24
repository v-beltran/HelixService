using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace HelixService.Utility
{
    public class HNumeric
    {
        /// <summary>
        /// Gets an integer from object, which will default to '0' from an invalid value.
        /// </summary>
        /// <param name="value">The integer to be acquired from object.</param>
        /// <returns>The integer from object passed or zero.</returns>
        public static Int32 GetSafeInteger(Object value)
        {
            Int32 n;
            if (value == null)
            {
                return 0;
            }
            else
            {
                if (Int32.TryParse(value.ToString(), out n))
                {
                    return n;
                }
                else
                {
                    return 0;
                }
            }
        }

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
        /// Gets a decimal from object, which will default to '0' from an invalid value.
        /// </summary>
        /// <param name="value">The decimal to be acquired from object.</param>
        /// <returns>The decimal from object passed or zero.</returns>
        public static Decimal GetSafeDecimal(Object value)
        {
            Decimal d;
            if (value == null)
            {
                return 0;
            }
            else
            {
                if (Decimal.TryParse(value.ToString(), out d))
                {
                    return d;
                }
                else
                {
                    return 0;
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

        /// <summary>
        /// Gets a currency value, which will default to '0' from an invalid value.
        /// </summary>
        /// <param name="value">The decimal to be parsed.</param>
        /// <param name="cultureInfo">The currency of a culture.</param>
        /// <returns>The currency from the given culture.</returns>
        public static Decimal GetSafeCurrency(Object value, CultureInfo cultureInfo)
        {
            Decimal currency = 0;
            if (value == null)
            {
                return 0;
            }
            else
            {
                if (Decimal.TryParse(value.ToString(), NumberStyles.Currency, cultureInfo, out currency))
                {
                    return currency;
                }
                else
                {
                    return 0;
                }
            }
        }

        /// <summary>
        /// Gets a currency value, which will default to null from an invalid value.
        /// </summary>
        /// <param name="value">The decimal to be parsed.</param>
        /// <param name="cultureInfo">The currency of a culture.</param>
        /// <returns>The currency from the given culture.</returns>
        public static Decimal? GetNullableCurrency(Object value, CultureInfo cultureInfo)
        {
            Decimal currency = 0;
            if (value == null)
            {
                return null;
            }
            else
            {
                if (Decimal.TryParse(value.ToString(), NumberStyles.Currency, cultureInfo, out currency))
                {
                    return new Decimal?(currency);
                }
                else
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// Determine if a number is divisble by another number.
        /// </summary>
        /// <param name="a">The number to divide.</param>
        /// <param name="b">The number to divide by.</param>
        /// <returns></returns>
        public static Boolean IsDivisible(Int32 a, Int32 b)
        {
            return (a % b) == 0;
        }

    }
}