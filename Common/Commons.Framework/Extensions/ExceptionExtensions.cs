// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ExceptionExtensions.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Extensions
{
    #region

    using System;
    using System.Collections;
    using System.Text;
    using System.Web;

    using Commons.Framework.Mvc;

    #endregion

    /// <summary>
    ///     The exception extensions.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// The to formatted string.
        /// </summary>
        /// <param name="exception">
        /// The exception.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ToFormattedString(this Exception exception)
        {
            if (HttpContext.Current != null)
            {
                exception.Data["Page Url"] = HttpContext.Current.Request.RawUrl;
                exception.Data["User Agent"] = HttpContext.Current.Request.UserAgent();
                exception.Data["Current User"] = HttpContext.Current?.User?.Identity?.Name ?? "anonymous";
            }

            var exceptionString = new StringBuilder();
            exceptionString.AppendFormat("{0}\n", exception.Message);
            exceptionString.Append(exception);
            exceptionString.Append("\nData:\n");

            foreach (DictionaryEntry dictionaryEntry in exception.Data)
            {
                exceptionString.AppendFormat("\t{0}: {1}\n", dictionaryEntry.Key, dictionaryEntry.Value);
            }

            exceptionString.Append("--------- End of the exception ---------");

            return exceptionString.ToString();
        }
    }
}