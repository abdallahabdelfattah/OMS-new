// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateTimeHelper.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Utils
{
    #region

    #region

    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Commons.Framework.Globalization;

    #endregion

    #endregion

    /// <summary>
    ///     Group of methods help you to manipulate with DateTime
    /// </summary>
    public static class DateTimeHelper
    {
        /// <summary>
        ///     The all formats.
        ///     14/12/1437 02:54:14 م
        /// </summary>
        private static readonly string[] allFormats =
            {
                "dd/MM/yyyy", "dd-MMM-yyyy", "dd-MMMM-yyyy", "yyyy/MM/dd", "yyyy/M/d", "dd/MM/yyyy", "d/M/yyyy",
                "dd/M/yyyy", "d/MM/yyyy", "yyyy-MM-dd", "yyyy-M-d", "dd-MM-yyyy", "d-M-yyyy", "dd-M-yyyy", "d-MM-yyyy",
                "yyyy MM dd", "yyyy M d", "dd MM yyyy", "d M yyyy", "dd M yyyy", "d MM yyyy", "dd/MM/yyyy HH:mm:ss",
                "G", "g", "yyyy/MM/dd hh:mm:ss tt", "dd/MM/yyyy hh:mm:ss tt"
            };

        /// <summary>
        /// The get um al qura date.
        /// </summary>
        /// <param name="date">
        /// The date.
        /// </param>
        /// <param name="dateFormat">
        /// The date format.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetCurrentUmAlQuraDateString(DateTime date, string dateFormat = "dd/MM/yyyy")
        {
            var culture = new CultureInfo("ar-SA") { DateTimeFormat = { Calendar = new UmAlQuraCalendar() } };
            return date.ToString(dateFormat, culture);
        }

        /// <summary>
        /// The get gregorean date.
        /// </summary>
        /// <param name="date">
        /// The date.
        /// </param>
        /// <param name="dateFormat">
        /// The date format.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetGregoreanDateString(DateTime date, string dateFormat = "dd/MM/yyyy")
        {
            var culture = new CultureInfo("en-GB") { DateTimeFormat = { Calendar = new GregorianCalendar() } };
            return date.ToString(dateFormat, culture);
        }

        /// <summary>
        /// The get month names.
        /// </summary>
        /// <param name="calendar">
        /// The calendar.
        /// </param>
        /// <returns>
        /// The <see cref="string[]"/>.
        /// </returns>
        public static string[] GetMonthNames(Calendar calendar)
        {
            return GetMonthNames(calendar, new CultureInfo(CultureInfo.CurrentCulture.ToString()));
        }

        /// <summary>
        /// The get month names.
        /// </summary>
        /// <param name="calendar">
        /// The calendar.
        /// </param>
        /// <param name="culture">
        /// The culture.
        /// </param>
        /// <returns>
        /// The <see cref="string[]"/>.
        /// </returns>
        public static string[] GetMonthNames(Calendar calendar, CultureInfo culture)
        {
            if (calendar is UmAlQuraCalendar && culture.Name.StartsWith("en-"))
            {
                return new[]
                           {
                               "Muharram", "Safar", "Rabi' al-awwal", "Rabi' al-thani", "Jumada al-awwal",
                               "Jumada al-thani", "Rajab", "Sha'aban", "Ramadan", "Shawwal", "Dhu al-Qi'dah",
                               "Dhu al-Hijjah"
                           };
            }

            culture.DateTimeFormat.Calendar = calendar;
            var monthNames = culture.DateTimeFormat.MonthNames;
            Array.Resize(ref monthNames, 12);
            return monthNames;
        }

        /// <summary>
        ///     return array of 12 months
        /// </summary>
        /// <returns>
        ///     The <see cref="string[]" />.
        /// </returns>
        public static string[] GetMonthsNumbers()
        {
            return new[] { "01", "02", "03", "04", "05", "06", "07", "08", "09", "10", "11", "12" };
        }

        /// <summary>
        /// Replace Hijry month name with its english name
        /// </summary>
        /// <param name="month">
        /// index of month (start from 1)
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetUmmAlquraMonthName(int month)
        {
            var monthsNames = CultureHelper.IsArabic
                                  ? GetMonthNames(new UmAlQuraCalendar(), new CultureInfo("ar-SA"))
                                  : GetMonthNames(new UmAlQuraCalendar(), new CultureInfo("en-US"));
            return monthsNames[month - 1];
        }

        /// <summary>
        /// return array of min-max years
        /// </summary>
        /// <param name="min">
        /// </param>
        /// <param name="max">
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public static List<string> GetYearsRange(int min, int max)
        {
            if (min > max)
            {
                throw new Exception("The min parameter must be less than max parameter");
            }

            var years = new List<string>();
            for (var i = min; i <= max; i++)
            {
                years.Add(i.ToString());
            }

            return years;
        }

        public static DateTime Min(DateTime date1, DateTime date2)
        {
            if (date1 < date2)
                return date1;
            else
                return date2;
        }

        public static DateTime Max(DateTime date1, DateTime date2)
        {
            if (date1 > date2)
                return date1;
            else
                return date2;
        }

        /// <summary>
        /// Calculates the date ranges total years.
        /// Created by The Magnificent Essam Mostafa
        /// </summary>
        /// <param name="ranges">The ranges.</param>
        /// <returns></returns>
        public static double CalculateDateRangesTotalYears(List<DateRange> ranges)
        {
            if (ranges == null)
            {
                return 0;
            }

            if (!ranges.Any())
            {
                return 0;
            }

            int totalIntersectedDates = 0;
            double totalPeriod = 0;

            for (int i = 0; i < ranges.Count; i++)
            {
                totalPeriod += ranges[i].TotalDays;
                for (int y = i + 1; y < ranges.Count; y++)
                {
                    if (y > ranges.Count)
                        break;

                    var minEndDate = Min(ranges[i].EndDate, ranges[y].EndDate);
                    var maxStartDate = Max(ranges[i].StartDate, ranges[y].StartDate);
                    totalIntersectedDates += Math.Max((int)(minEndDate - maxStartDate).TotalDays, 0);

                }
            }

            double totalDaysOfExperience = totalPeriod - totalIntersectedDates;
            double numbOfYears = totalDaysOfExperience / 365;

            return numbOfYears;
        }

        public static int GetMonthDifference(DateTime startDate, DateTime endDate)
        {
            int monthsApart = 12 * (startDate.Year - endDate.Year) + startDate.Month - endDate.Month;
            return Math.Abs(monthsApart);
        }
        public static double GetAverageMonthDifference(DateTime startDate, DateTime endDate)
        {            
            return startDate.Subtract(endDate).Days / (365.25 / 12);
        }
        /// <summary>
        /// The is gregorian.
        /// </summary>
        /// <param name="gregStr">
        /// The greg str.
        /// </param>
        /// <param name="dateFormat">
        /// The date Format.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsGregorian(string gregStr, string dateFormat = null)
        {
            var formats = !string.IsNullOrEmpty(dateFormat) ? new[] { dateFormat } : allFormats;

            var englishCultureInfo = new CultureInfo("en-GB");

            var cal = new GregorianCalendar();

            if (!string.IsNullOrEmpty(gregStr) && gregStr.Trim().Length > 0)
            {
                try
                {
                    // DateTime tmp = DateTime.Now; ;
                    // return DateTime.TryParse(gregStr, out tmp);
                    var tempDate = DateTime.ParseExact(
                        gregStr,
                        formats,
                        englishCultureInfo.DateTimeFormat,
                        DateTimeStyles.AllowWhiteSpaces);
                    if (tempDate.Year >= cal.MinSupportedDateTime.Year
                        && tempDate.Year <= cal.MaxSupportedDateTime.Year)
                    {
                        return true;
                    }

                    return false;
                }
                catch (Exception)
                {
                    return false;
                }
            }

            return false;
        }

        /// <summary>
        /// The is um alqura.
        /// </summary>
        /// <param name="hijriDateStr">
        /// The hijri date str.
        /// </param>
        /// <param name="dateFormat">
        /// The date Format.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsUmAlqura(string hijriDateStr, string dateFormat = null)
        {
            var formats = !string.IsNullOrEmpty(dateFormat) ? new[] { dateFormat } : allFormats;

            var arCul = new CultureInfo("ar-SA");
            var cal = new UmAlQuraCalendar();
            arCul.DateTimeFormat.Calendar = cal;

            if (string.IsNullOrEmpty(hijriDateStr) || hijriDateStr.Trim().Length <= 0)
            {
                return false;
            }

            try
            {
                var tempDate = DateTime.ParseExact(
                    hijriDateStr,
                    formats,
                    arCul.DateTimeFormat,
                    DateTimeStyles.AllowWhiteSpaces);
                if (tempDate.Year >= cal.MinSupportedDateTime.Year && tempDate.Year <= cal.MaxSupportedDateTime.Year)
                {
                    return true;
                }

                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// The parse gregorian date.
        /// </summary>
        /// <param name="gregStr">
        /// The greg str.
        /// </param>
        /// <param name="dateFormat">
        /// The date Format.
        /// </param>
        /// <returns>
        /// The <see cref="DateTime?"/>.
        /// </returns>
        public static DateTime? ParseGregorianDate(string gregStr, string dateFormat = null)
        {
            var formats = !string.IsNullOrEmpty(dateFormat) ? new[] { dateFormat } : allFormats;

            var englishCultureInfo = new CultureInfo("en-GB");
            var cal = new GregorianCalendar();

            DateTime? toReturn = null;
            if (!string.IsNullOrEmpty(gregStr) && gregStr.Trim().Length > 0)
            {
                try
                {
                    // DateTime tmp = DateTime.Now; ;
                    // return DateTime.TryParse(gregStr, out tmp);
                    toReturn = DateTime.ParseExact(
                        gregStr,
                        formats,
                        englishCultureInfo.DateTimeFormat,
                        DateTimeStyles.AllowWhiteSpaces);
                    if (toReturn.Value.Year >= cal.MinSupportedDateTime.Year
                        && toReturn.Value.Year <= cal.MaxSupportedDateTime.Year)
                    {
                        return toReturn.Value;
                    }

                    return null;
                }
                catch (Exception)
                {
                    return null;
                }
            }

            return null;
        }

        /// <summary>
        /// The parse um alqura date.
        /// </summary>
        /// <param name="hijriDateStr">
        /// The hijri date str.
        /// </param>
        /// <param name="dateFormat">
        /// The date Format.
        /// </param>
        /// <returns>
        /// The <see cref="DateTime?"/>.
        /// </returns>
        public static DateTime? ParseUmAlquraDate(string hijriDateStr, string dateFormat = null)
        {
            var formats = !string.IsNullOrEmpty(dateFormat) ? new[] { dateFormat } : allFormats;

            var umAlQuraCal = new UmAlQuraCalendar();
            DateTime? toRet = null;

            if (!string.IsNullOrEmpty(hijriDateStr))
            {
                var arCult = new CultureInfo("ar-SA") { DateTimeFormat = { Calendar = umAlQuraCal } };
                toRet = DateTime.ParseExact(
                    hijriDateStr,
                    formats,
                    arCult.DateTimeFormat,
                    DateTimeStyles.AllowWhiteSpaces);
            }

            return toRet;
        }
    }
}