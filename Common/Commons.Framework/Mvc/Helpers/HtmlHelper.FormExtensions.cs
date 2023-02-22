// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HtmlHelper.FormExtensions.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Mvc.Helpers
{
    #region

    using System;
    using System.Collections.Specialized;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using System.Web.Routing;

    #endregion

    /// <summary>
    /// The form extensions.
    /// </summary>
    public static class FormExtensions
    {
        /// <summary>
        /// The begin form query string.
        /// </summary>
        /// <param name="helper">
        /// The helper.
        /// </param>
        /// <param name="action">
        /// The action.
        /// </param>
        /// <param name="controller">
        /// The controller.
        /// </param>
        /// <param name="method">
        /// The method.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <returns>
        /// The <see cref="IDisposable"/>.
        /// </returns>
        public static IDisposable BeginFormQueryString(
            this HtmlHelper helper,
            string action,
            string controller,
            FormMethod method,
            object htmlAttributes)
        {
            var query = helper.ViewContext.RequestContext.HttpContext.Request.QueryString;
            var routeValues = query.ToRouteValueDictionary();
            return helper.BeginForm(action, controller, routeValues, method, htmlAttributes);
        }

        /// <summary>
        /// The to route value dictionary.
        /// </summary>
        /// <param name="collection">
        /// The collection.
        /// </param>
        /// <returns>
        /// The <see cref="RouteValueDictionary"/>.
        /// </returns>
        public static RouteValueDictionary ToRouteValueDictionary(this NameValueCollection collection)
        {
            var routeValues = new RouteValueDictionary();
            foreach (string key in collection)
            {
                routeValues[key] = collection[key];
            }

            return routeValues;
        }
    }
}