using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.Framework.Notifications
{
    public class SmsMessage
    {
        public string PhoneNumber { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }

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
}
