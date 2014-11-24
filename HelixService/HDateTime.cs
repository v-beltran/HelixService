using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
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
            return HDateTime.GetWeekOfYear(HDateTime.LastOfMonth(dt), DayOfWeek.Sunday) - HDateTime.GetWeekOfYear(HDateTime.FirstOfMonth(dt), DayOfWeek.Sunday) + 1;
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
        /// Get the week number (x out of 52) of the year.
        /// </summary>
        /// <param name="dt">The given date to evaluate.</param>
        /// <param name="startOfWeek">The start day of the week.</param>
        /// <returns>The week of the year.</returns>
        public static Int32 GetWeekOfYear(DateTime dt, DayOfWeek startOfWeek)
        {
            return CultureInfo.CurrentCulture.Calendar.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, startOfWeek);
        }

        /// <summary>
        /// Get holidays in a given year. More can be added here...
        /// </summary>
        /// <param name="year">The year to fetch holidays dates in.</param>
        /// <returns>A list of holiday dates.</returns>
        public static Dictionary<DateTime, String> GetHolidaysInYear(Int32 year)
        {
            Dictionary<DateTime, String> holidays = new Dictionary<DateTime, String>();
            // New Years
            holidays.Add(new DateTime(year, 1, 1), "New Year's Day");
            // Dr. Martin Luther King Day
            holidays.Add(HDateTime.GetDayFromWeekOfMonth(new DateTime(year, 1, 1), WeekOfMonth.Third, DayOfWeek.Monday), "Dr. Martin Luther King Day");
            // President's Day
            holidays.Add(HDateTime.GetDayFromWeekOfMonth(new DateTime(year, 2, 1), WeekOfMonth.Third, DayOfWeek.Monday), "President's Day");
            // Memorial Day
            holidays.Add(HDateTime.GetDayFromWeekOfMonth(new DateTime(year, 5, 1), WeekOfMonth.Last, DayOfWeek.Monday), "Memorial Day");
            // Independence Day
            holidays.Add(new DateTime(year, 7, 4), "Independence Day");
            // Labor Day
            holidays.Add(HDateTime.GetDayFromWeekOfMonth(new DateTime(year, 9, 1), WeekOfMonth.First, DayOfWeek.Monday), "Labor Day");
            // Thanksgiving Day
            holidays.Add(HDateTime.GetDayFromWeekOfMonth(new DateTime(year, 11, 1), WeekOfMonth.Fourth, DayOfWeek.Thursday), "Thanksgiving Day");
            // Christmas Day
            holidays.Add(new DateTime(year, 12, 25), "Christmas Day");

            return holidays;
        }

        /// <summary>
        /// Get holidays for a given month in a year.
        /// </summary>
        /// <param name="dt">The month date.</param>
        /// <returns>A list of holidays in a month.</returns>
        public static Dictionary<DateTime, String> GetHolidaysInMonth(DateTime dt)
        {
            return HDateTime.GetHolidaysInYear(dt.Year).Where(kvp => kvp.Key.Month.Equals(dt.Month)).ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        }

        /// <summary>
        /// Gets a specific day in a specific week in a month and day of the week.
        /// </summary>
        /// <param name="month">The month datetime to evaluate.</param>
        /// <param name="weekOfMonth">The week of the month.</param>
        /// <param name="dayOfWeek">The day of the week.</param>
        /// <returns>The new adjusted date.</returns>
        public static DateTime GetDayFromWeekOfMonth(DateTime month, WeekOfMonth weekOfMonth, DayOfWeek dayOfWeek)
        {
            DateTime currentDate = new DateTime(month.Year, month.Month, 1);
            DateTime newDate = currentDate;
            Int32 currentWeek = (int)weekOfMonth;

            // No need to do any adjustments because the first of the month is the specified date already.
            if (weekOfMonth == WeekOfMonth.First && dayOfWeek == currentDate.DayOfWeek)
                return currentDate;

            // This date already falls on the specified day of the week.
            // We can simply add the number of weeks from what was given.
            if (currentDate.DayOfWeek == dayOfWeek)
                return newDate.AddDays(currentWeek * 7);

            if (currentDate.DayOfWeek < dayOfWeek)
            {
                // When the specified day of week is greater than the current date's day of week, 
                // we can add the difference to get to the same day.
                newDate = newDate.AddDays(dayOfWeek - newDate.DayOfWeek);
            }
            else
            {
                // Go backwards to get to the first of the week.
                newDate = newDate.AddDays(-(int)newDate.DayOfWeek);

                // When we go outside the current month, make sure to increment a week.
                if (newDate.Month != currentDate.Month)
                    currentWeek++;

                // Add the number of days to get to the specified day of week.
                newDate = newDate.AddDays((int)dayOfWeek);
            }

            // Simply add the number of weeks to get to the specified date.
            newDate = newDate.AddDays(currentWeek * 7);

            // If adding too many days in the current month takes us to next month, go back a week.
            return newDate.Month != currentDate.Month ? newDate.AddDays(-7) : newDate;
        }

        /// <summary>
        /// Determines if the string is a valid date (MM/dd/yyyy).
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <param name="formats">The format(s) the date should be in.</param>
        /// <returns>True/False</returns>
        public static Boolean IsValidDate(String value, params String[] formats)
        {
            Boolean valid = false;
            DateTime dt = new DateTime();

            if (DateTime.TryParseExact(value, formats, null, DateTimeStyles.None, out dt))
            {
                valid = true;
            }

            return valid;
        }

        /// <summary>
        /// Determines if the string is a valid date between 1900 and the current date (MM/dd/yyyy).
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <param name="formats">The format(s) the date should be in.</param>
        /// <returns>True/False</returns>
        public static Boolean IsValidDateOfBirth(String value, params String[] formats)
        {
            Boolean valid = false;
            DateTime dt = new DateTime();

            if (DateTime.TryParseExact(value, formats, null, DateTimeStyles.None, out dt))
            {
                if(dt.Year >= 1900 && dt <= DateTime.Now)
                {
                    valid = true;
                }
            }

            return valid;
        }

        /// <summary>
        /// Determines if the string is a valid time in standard format.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>True/False</returns>
        public static Boolean IsValidStandardTime(String value)
        {
            return Regex.IsMatch(value, @"^(1[012]|[1-9]):[0-5][0-9](\s)?(?i)(am|pm)$");
        }

        /// <summary>
        /// Determines if the string is a valid time in military format.
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>True/False</returns>
        public static Boolean IsValidMilitaryTime(String value)
        {
            return Regex.IsMatch(value, @"^(?:[01][0-9]|2[0-3]):?[0-5][0-9]$");
        }
    }
}