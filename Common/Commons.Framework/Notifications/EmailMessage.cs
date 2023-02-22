// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EmailMessage.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Specialized;
using System.IO;
using System.Net.Mime;
using System.Text;

namespace Commons.Framework.Notifications
{
    #region

    using System.Collections.Generic;
    using System.Net.Mail;

    #endregion

    /// <summary>
    ///     The email message.
    /// </summary>
    /// 
   // [Serializable]
    public class EmailMessage : MailMessage
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="EmailMessage" /> class.
        ///     Initializes a new instance of the <see cref="EMail" /> class.
        /// </summary>
        public EmailMessage()
        {
            this.Data = new Dictionary<string, string>();
        }

        /// <summary>
        ///     Gets or sets the application id.
        /// </summary>
        public string ApplicationId { get; set; }

        /// <summary>
        ///     Gets or sets the created by.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        ///     Gets or sets the data.
        /// </summary>
        public Dictionary<string, string> Data { get; set; }

        /////// <summary>
        ///////     Gets or sets the language lcid.
        /////// </summary>
        ////public int LanguageLcid { get; set; }

        /// <summary>
        ///     Gets or sets the template name.
        /// </summary>
        public string TemplateName { get; set; }
    }

    [Serializable]
    public class SimpleMailMessage
    {
        public string From { get; set; }
        public string DisplayName { get; set; }
        public string To { get; set; }
        public string ReplyTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }

        public MailMessage ToMailMessage()
        {
            return new MailMessage
            {
                Body = Body,
                From = new MailAddress(From, DisplayName),
                IsBodyHtml = true,
                Subject = Subject,
            };
        }
    }


}