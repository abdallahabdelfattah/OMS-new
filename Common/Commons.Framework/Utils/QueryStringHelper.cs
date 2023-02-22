// --------------------------------------------------------------------------------------------------------------------
// <copyright file="QueryStringHelper.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Linq;

namespace Commons.Framework.Utils
{
    #region

    using System.Text;
    using System.Web;

    #endregion

    /// <summary>
    ///     The query string helper.
    /// </summary>
    public class QueryStringHelper
    {
        /// <summary>
        /// Get all parameters from query string
        /// </summary>
        /// <param name="exceptedKeys">
        /// The excepted Keys.
        /// </param>
        /// <returns>
        /// String value
        /// </returns>
        public static string GetQueryString(params string[] exceptedKeys)
        {
            var sb = new StringBuilder();

            foreach (string key in HttpContext.Current.Request.QueryString.Keys)
            {
                var found = false;
                if (key == null)
                {
                    continue;
                }

                var tmpKey = key.ToLower();
                
                foreach (string t in exceptedKeys)
                {
                    if (t.ToLower() == tmpKey)
                    {
                        found = true;
                        break;
                    }
                }

                if (found)
                {
                    continue;
                }

                // add to the query string
                sb.AppendFormat("{0}={1}&", key, HttpContext.Current.Request[key]);
            }

            return sb.ToString();
        }

        public static string BuildUrlWithQueryString(string baseUrl, Dictionary<string, object> parameters)
        {
            var qs = string.Join("&",
                parameters.Select(kvp =>
                    $"{kvp.Key}={kvp.Value}"));

            return $"{baseUrl}?{qs}";
        }

        /// <summary>
        /// The get value.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string GetValue(string key)
        {
            return HttpContext.Current.Request[key];
        }
    }
}