// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CultureHelper.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Globalization
{
    #region

    using System;
    using System.Globalization;
    using System.Threading;
    using System.Web;

    #endregion

    #region

    #endregion

    /// <summary>
    ///     The culture helper.
    /// </summary>
    public static class CultureHelper
    {
        /// <summary>
        /// The current culture name.
        /// </summary>
        public static string CurrentCultureName => Thread.CurrentThread.CurrentCulture.Name;

        /// <summary>
        ///     The current direction.
        /// </summary>
        public static string CurrentDirection => IsRightToLeft ? "rtl" : "ltr";

        /// <summary>
        ///     The current language.
        /// </summary>
        public static string CurrentLanguage => Thread.CurrentThread.CurrentCulture.Name.Substring(0, 2);

        /// <summary>
        ///     The is arabic.
        /// </summary>
        public static bool IsArabic => Thread.CurrentThread.CurrentCulture.LCID == 1025;

        /// <summary>
        ///     The is right to left.
        /// </summary>
        public static bool IsRightToLeft => Thread.CurrentThread.CurrentCulture.TextInfo.IsRightToLeft;

        /// <summary>
        /// The get culture info.
        /// </summary>
        /// <param name="cultureName">
        /// The culture name.
        /// </param>
        /// <returns>
        /// The <see cref="CultureInfo"/>.
        /// </returns>
        public static CultureInfo GetCultureInfo(string cultureName = "en-GB")
        {
            // The default date in the system should be gregorian.
            var dateTimeFormat = new CultureInfo("en-GB").DateTimeFormat;

            // The default currency should be SAR
            var numberFormat = new CultureInfo("ar-SA").NumberFormat;
            numberFormat.CurrencyPositivePattern = 3;
            numberFormat.CurrencyNegativePattern = 3;

            return new CultureInfo(cultureName) { NumberFormat = numberFormat, DateTimeFormat = dateTimeFormat };
        }

        /// <summary>
        /// The initialize culture from cookie.
        /// </summary>
        public static void InitializeCultureFromCookie()
        {
            var defaultCulture = CommonsSettings.DefaultCulture; // USAMA READ FROM FRAMEWORK SETTINGS
            const string CookieName = "user-culture";
            var cultureCookie = HttpContext.Current.Request.Cookies[CookieName];
            if (cultureCookie == null)
            {
                cultureCookie =
                    new HttpCookie(
                        CookieName,
                        defaultCulture.ToString(CultureInfo.InvariantCulture)) {
                                                                                  Expires = DateTime.Now.AddYears(1) 
                                                                               };
                HttpContext.Current.Response.Cookies.Add(cultureCookie);
            }

            var clutureValFromCookie = cultureCookie.Value ?? defaultCulture;
            var cultureToSet = GetCultureInfo(clutureValFromCookie);
            Thread.CurrentThread.CurrentCulture = cultureToSet;
            Thread.CurrentThread.CurrentUICulture = cultureToSet;
        }
    }
}