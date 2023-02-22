// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HtmlHelper.ImageLinkExtensions.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Mvc.Helpers
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    ///     This represents the extension methods entity for the <c>HtmlHelper</c> class.
    /// </summary>
    public static class HtmlHelperImageLinkExtensions
    {
        /// <summary>
        /// Renders the &lt;a&gt; tag with image.
        /// </summary>
        /// <param name="htmlHelper">
        /// <c>HtmlHelper</c> instance.
        /// </param>
        /// <param name="src">
        /// Image file location.
        /// </param>
        /// <param name="href">
        /// Link URL.
        /// </param>
        /// <returns>
        /// Returns the &lt;a&gt; tag with image.
        /// </returns>
        public static MvcHtmlString ImageLink(this HtmlHelper htmlHelper, string src, string href, string additionalKOattributes)
        {
            if (string.IsNullOrWhiteSpace(src))
            {
                throw new ArgumentNullException(nameof(src));
            }

            if (string.IsNullOrWhiteSpace(href))
            {
                throw new ArgumentNullException(nameof(href));
            }

            return ImageLink(htmlHelper, src, href, additionalKOattributes, null);
        }

        /// <summary>
        /// Renders the &lt;a&gt; tag with image.
        /// </summary>
        /// <param name="htmlHelper">
        /// <c>HtmlHelper</c> instance.
        /// </param>
        /// <param name="src">
        /// Image file location.
        /// </param>
        /// <param name="href">
        /// Link URL.
        /// </param>
        /// <param name="htmlAttributes">
        /// List of attributes used for the {a} tag.
        /// </param>
        /// <param name="imageAttributes">
        /// List of attributes for the {img} tag.
        /// </param>
        /// <returns>
        /// Returns the &lt;a&gt; tag with image.
        /// </returns>
        public static MvcHtmlString ImageLink(
            this HtmlHelper htmlHelper,
            string src,
            string href,
            object htmlAttributes,
            object imageAttributes)
        {
            if (string.IsNullOrWhiteSpace(src))
            {
                throw new ArgumentNullException(nameof(src));
            }

            if (string.IsNullOrWhiteSpace(href))
            {
                throw new ArgumentNullException(nameof(href));
            }

            return ImageLink(
                htmlHelper,
                src,
                href,
                HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes),
                HtmlHelper.AnonymousObjectToHtmlAttributes(imageAttributes));
        }

        /// <summary>
        /// Renders the &lt;a&gt; tag with image.
        /// </summary>
        /// <param name="htmlHelper">
        /// <c>HtmlHelper</c> instance.
        /// </param>
        /// <param name="src">
        /// Image file location.
        /// </param>
        /// <param name="href">
        /// Link URL.
        /// </param>
        /// <param name="htmlAttributes">
        /// List of attributes used for the {a} tag.
        /// </param>
        /// <param name="imageAttributes">
        /// List of attributes for the {img} tag.
        /// </param>
        /// <returns>
        /// Returns the &lt;a&gt; tag with image.
        /// </returns>
        public static MvcHtmlString ImageLink(
            this HtmlHelper htmlHelper,
            string src,
            string href,
            IDictionary<string, object> htmlAttributes,
            IDictionary<string, object> imageAttributes)
        {
            if (string.IsNullOrWhiteSpace(src))
            {
                throw new ArgumentNullException(nameof(src));
            }

            if (string.IsNullOrWhiteSpace(href))
            {
                throw new ArgumentNullException(nameof(href));
            }

            var link = htmlHelper.Link(".", href, htmlAttributes);
            var image = htmlHelper.Image(src, imageAttributes);
            return new MvcHtmlString(link.ToHtmlString().Replace(">.</a>", ">" + image.ToHtmlString() + "</a>"));
        }
    }
}