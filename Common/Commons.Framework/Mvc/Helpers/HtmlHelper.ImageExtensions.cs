// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HtmlHelper.ImageExtensions.cs" company="Usama Nada">
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
    ///     This represents the extension methods entity for the <c>System.Web.Mvc.HtmlHelper</c> class.
    /// </summary>
    public static class HtmlHelperImageExtensions
    {
        /// <summary>
        /// Renders the &lt;img&gt; tag.
        /// </summary>
        /// <param name="htmlHelper">
        /// <c>HtmlHelper</c> instance.
        /// </param>
        /// <param name="src">
        /// Image file location.
        /// </param>
        /// <returns>
        /// Returns the &lt;img&gt; tag in HTML format.
        /// </returns>
        public static MvcHtmlString Image(this HtmlHelper htmlHelper, string src)
        {
            if (string.IsNullOrWhiteSpace(src))
            {
                throw new ArgumentNullException(nameof(src));
            }

            return Image(htmlHelper, src, null);
        }

        /// <summary>
        /// Renders the &lt;img&gt; tag.
        /// </summary>
        /// <param name="htmlHelper">
        /// <c>HtmlHelper</c> instance.
        /// </param>
        /// <param name="src">
        /// Image file location.
        /// </param>
        /// <param name="imageAttributes">
        /// List of attributes for the {img} tag.
        /// </param>
        /// <returns>
        /// Returns the &lt;img&gt; tag in HTML format.
        /// </returns>
        public static MvcHtmlString Image(this HtmlHelper htmlHelper, string src, object imageAttributes)
        {
            if (string.IsNullOrWhiteSpace(src))
            {
                throw new ArgumentNullException(nameof(src));
            }

            return Image(htmlHelper, src, HtmlHelper.AnonymousObjectToHtmlAttributes(imageAttributes));
        }

        /// <summary>
        /// Renders the &lt;img&gt; tag.
        /// </summary>
        /// <param name="htmlHelper">
        /// <c>HtmlHelper</c> instance.
        /// </param>
        /// <param name="src">
        /// Image file location.
        /// </param>
        /// <param name="imageAttributes">
        /// List of attributes for the {img} tag.
        /// </param>
        /// <returns>
        /// Returns the &lt;img&gt; tag in HTML format.
        /// </returns>
        public static MvcHtmlString Image(
            this HtmlHelper htmlHelper,
            string src,
            IDictionary<string, object> imageAttributes)
        {
            if (string.IsNullOrWhiteSpace(src))
            {
                throw new ArgumentNullException(nameof(src));
            }

            var tagBuilder = new TagBuilder("img");
            tagBuilder.MergeAttribute("src", src);
            tagBuilder.MergeAttributes(imageAttributes);

            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.SelfClosing));
        }
    }
}