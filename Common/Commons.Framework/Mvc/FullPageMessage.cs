// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FullPageMessage.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Mvc

{
    #region

    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// The message icon.
    /// </summary>
    public enum MessageIcon
    {
        /// <summary>
        /// The success.
        /// </summary>
        Success,

        /// <summary>
        /// The error.
        /// </summary>
        Error,

        /// <summary>
        /// The security.
        /// </summary>
        Security,

        /// <summary>
        /// The exclamation.
        /// </summary>
        Exclamation
    }

    /// <summary>
    /// The full page message.
    /// </summary>
    public class FullPageMessage
    {
        /// <summary>
        /// Gets or sets the back link text.
        /// </summary>
        public string BackLinkText { get; set; }

        /// <summary>
        /// Gets or sets the back link url.
        /// </summary>
        public string BackLinkUrl { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the handle error info.
        /// </summary>
        public HandleErrorInfo HandleErrorInfo { get; set; }

        /// <summary>
        /// Gets or sets the icon.
        /// </summary>
        public MessageIcon Icon { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the page title.
        /// </summary>
        public string PageTitle { get; set; }
    }
}