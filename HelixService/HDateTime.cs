using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace HelixService.Utility
{
    public class HDateTime
    {
        /// <summary>
        /// Try to get the datetime from an object.
        /// </summary>
        /// <param name="value">Object to try to parse.</param>
        /// <returns>The parsed datetime or null.</returns>
        public static DateTime? GetDateTimeNullable(object value)
        {
            DateTime currentDateTime = DateTime.UtcNow;
            DateTime newDateTime = DateTime.UtcNow;
            Boolean parse = DateTime.TryParse(HString.ToString(value), out newDateTime);
            return parse ? (DateTime?)newDateTime : null;
        }

        /// <summary>
        /// Try to get the datetime from an object.
        /// </summary>
        /// <param name="value">Object to try to parse.</param>
        /// <returns>The parsed datetime or the current Utc datetime.</returns>
        public static DateTime GetDateTime(object value)
        {
            DateTime? newDateTime = HDateTime.GetDateTimeNullable(value);
            return newDateTime != null ? newDateTime.Value : DateTime.UtcNow;
        }

        /// <summary>
        /// Get the first day in the given year.
        /// </summary>
        /// <param name="dt">The datetime to parse.</param>
        /// <returns>The first of the year.</returns>
        public static DateTime FirstOfYear(DateTime dt)
        {
            return new DateTime(dt.Year, 1, 1);
        }

        /// <summary>
        /// Get the first day in the given month.
        /// </summary>
        /// <param name="dt">The datetime to parse.</param>
        /// <returns>The first of the month.</returns>
        public static DateTime FirstOfMonth(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, 1);
        }

        /// <summary>
        /// Get the first day in the given week.
        /// </summary>
        /// <param name="dt">The datetime to parse.</param>
        /// <param name="startOfWeek">The start day of the week.</param>
        /// <returns>The first of the week.</returns>
        public static DateTime FirstOfWeek(DateTime dt, DayOfWeek startOfWeek)
        {
            return dt.AddDays(startOfWeek - dt.DayOfWeek);
        }

        /// <summary>
        /// Get the end day in the given year.
        /// </summary>
        /// <param name="dt">The datetime to parse.</param>
        /// <returns>The end of the year.</returns>
        public static DateTime LastOfYear(DateTime dt)
        {
            return new DateTime(dt.Year, 12, 31);
        }

        /// <summary>
        /// Get the end day in the given month.
        /// </summary>
        /// <param name="dt">The datetime to parse.</param>
        /// <returns>The end of the month.</returns>
        public static DateTime LastOfMonth(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, DateTime.DaysInMonth(dt.Year, dt.Month));
        }

        /// <summary>
        /// Get the end day in the given week.
        /// </summary>
        /// <param name="dt">The datetime to parse.</param>
        /// <param name="startOfWeek">The start day of the week.</param>
        /// <returns>The end of the week.</returns>
        public static DateTime LastOfWeek(DateTime dt, DayOfWeek startOfWeek)
        {
            return HDateTime.FirstOfWeek(dt, startOfWeek).AddDays(6);
        }

        /// <summary>
        /// Get the total number of weeks in a month.
        /// </summary>
        /// <param name="dt">The datetime to evaluate.</param>
        /// <param name="startOfWeek">The start day of the week.</param>
        /// <returns>The total number of weeks in a month.</returns>
        public static Int32 GetNumberOfWeeks(DateTime dt, DayOfWeek startOfWeek)
        {
            return HDateTime.GetWeekNumberOfYear(HDateTime.LastOfMonth(dt), DayOfWeek.Sunday) - HDateTime.GetWeekNumberOfYear(HDateTime.FirstOfMonth(dt), DayOfWeek.Sunday) + 1;
        }

        /// <summary>
        /// Gets the name of the month from integer value.
        /// </summary>
        /// <param name="num">The integer of the month.</param>
        /// <returns>The month name.</returns>
        public static String GetMonthName(Int32 num)
        {
            return CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(num);
        }

        /// <summary>
        /// Convert to UTC from a given datetime.
        /// </summary>
        /// <param name="dt">The datetime to convert.</param>
        /// <returns>The UTC datetime.</returns>
        public static DateTime ToUtc(DateTime dt)
        {
            return dt.ToUniversalTime();
        }

        /// <summary>
        /// Convert UTC datetime to the given timezone.
        /// </summary>
        /// <param name="dt">The datetime to convert.</param>
        /// <param name="tz">The timezone to convert to.</param>
        /// <returns>The local time in the timezone.</returns>
        public static DateTime FromUtc(DateTime dt, TimeZoneInfo tz)
        {
            return TimeZoneInfo.ConvertTimeFromUtc(dt, tz);
        }

        /// <summary>
        /// Subtracts a year from the given date.
        /// </summary>
        /// <param name="dt">The year to go back from.</param>
        /// <returns>The previous year.</returns>
        public static DateTime GetPreviousYear(DateTime dt)
        {
            return dt.AddYears(-1);
        }

        /// <summary>
        /// Subtracts a month from the given date.
        /// </summary>
        /// <param name="dt">The month to go back from.</param>
        /// <returns>The previous month.</returns>
        public static DateTime GetPreviousMonth(DateTime dt)
        {
            return dt.AddMonths(-1);
        }

        /// <summary>
        /// Subtracts a week from the given date.
        /// </summary>
        /// <param name="dt">The week to go back from.</param>
        /// <returns>The previous week.</returns>
        public static DateTime GetPreviousWeek(DateTime dt)
        {
            return dt.AddDays(-7);
        }

        /// <summary>
        /// Subtracts a day from the given date.
        /// </summary>
        /// <param name="dt">The day to go back from.</param>
        /// <returns>The previous day.</returns>
        public static DateTime GetPreviousDay(DateTime dt)
        {
            return dt.AddDays(-1);
        }

        /// <summary>
        /// Adds a year from the given date.
        /// </summary>
        /// <param name="dt">The year to go back from.</param>
        /// <returns>The next year.</returns>
        public static DateTime GetNextYear(DateTime dt)
        {
            return dt.AddYears(1);
        }

        /// <summary>
        /// Adds a month from the given date.
        /// </summary>
        /// <param name="dt">The month to go back from.</param>
        /// <returns>The next month.</returns>
        public static DateTime GetNextMonth(DateTime dt)
        {
            return dt.AddMonths(1);
        }

        /// <summary>
        /// Adds a week from the given date.
        /// </summary>
        /// <param name="dt">The week to go back from.</param>
        /// <returns>The next week.</returns>
        public static DateTime GetNextWeek(DateTime dt)
        {
            return dt.AddDays(7);
        }

        /// <summary>
        /// Adds a day from the given date.
        /// </summary>
        /// <param name="dt">The day to go back from.</param>
        /// <returns>The next day.</returns>
        public static DateTime GetNextDay(DateTime dt)
        {
            return dt.AddDays(1);
        }

        /// <summary>
        /// Create datetime with the specific year, month, and day. 
        /// </summary>
        /// <param name="year">An integer representation of the year.</param>
        /// <param name="month">An integer representation of the month.</param>
        /// <param name="day">An integer representation of the day.</param>
        /// <returns>If valid, a new datetime object, otherwise the current datetime.</returns>
        public static DateTime GetDateTime(Int32 year, Int32 month, Int32 day)
        {
            DateTime result = new DateTime();

            if (DateTime.TryParse(String.Format("{0}/{1}/{2}", year, month, day), out result))
                return result.Date;
            else
                return DateTime.Now.Date;
        }

        /// <summary>
        /// Get the week number in the year.
        /// </summary>
        /// <param name="dt">The given date to evaluate.</param>
        /// <param name="startOfWeek">The start day of the week.</param>
        /// <returns></returns>
        public static Int32 GetWeekNumberOfYear(DateTime dt, DayOfWeek startOfWeek)
        {
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, startOfWeek);
        }
    }
}