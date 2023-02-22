// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RegisteredScriptsStream.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Mvc
{
    #region

    using System;
    using System.IO;
    using System.Text;
    using System.Web;

    #endregion

    /// <summary>
    ///     The html head stream.
    /// </summary>
    internal class RegisteredScriptsStream : MemoryStream
    {
        /// <summary>
        ///     The http context.
        /// </summary>
        private readonly HttpContextBase httpContext;

        /// <summary>
        ///     The output stream.
        /// </summary>
        private readonly Stream outputStream;

        /// <summary>
        ///     The closing.
        /// </summary>
        private bool closing;

        /// <summary>
        /// Initializes a new instance of the <see cref="RegisteredScriptsStream"/> class. 
        /// Initializes a new instance of the <see cref="HtmlHeadStream"/> class.
        /// </summary>
        /// <param name="outputStream">
        /// The output stream.
        /// </param>
        /// <param name="httpContext">
        /// The http context.
        /// </param>
        public RegisteredScriptsStream(Stream outputStream, HttpContextBase httpContext)
        {
            this.outputStream = outputStream;
            this.httpContext = httpContext;
        }

        /// <summary>
        ///     The close.
        /// </summary>
        public override void Close()
        {
            // Using a StreamReader to read this will cause Close to be called again
            if (this.closing)
            {
                return;
            }

            var scriptsToInject = HttpContext.Current.Response.GetRegisteredScriptsAndStyles();

            this.closing = true;
            byte[] buffer = null;
            if (this.httpContext.Response.ContentType == "text/html" && this.httpContext.Server.GetLastError() == null)
            {
                var html = this.ReadOriginalHtml();

                if (!string.IsNullOrEmpty(scriptsToInject) && !string.IsNullOrEmpty(html))
                {
                    var indexOfBody = html.IndexOf("</body>", StringComparison.InvariantCultureIgnoreCase);
                    var indexOfHtml = html.IndexOf("</html>", StringComparison.InvariantCultureIgnoreCase);

                    var indexToUse = indexOfBody != -1 ? indexOfBody : indexOfHtml;
                    var position = indexToUse;

                    if (position != -1)
                    {
                        html = html.Insert(position, scriptsToInject);
                    }
                }

                buffer = this.httpContext.Response.ContentEncoding.GetBytes(html);
            }
            else
            {
                var contentType = this.httpContext.Response.ContentType;
                if (contentType == "application/json" || contentType == "application/x-javascript"
                    || contentType == "text/plain" || contentType == "text/javascript" || contentType == "text/x-json"
                    || contentType == "application/javascript" || contentType == "text/x-javascript")
                {
                    var html = this.ReadOriginalHtml();
                    buffer = Encoding.UTF8.GetBytes(html.ToCharArray());
                }
                else
                {
                    buffer = this.GetBuffer();
                }

                this.Position = 0;

                // this.GetBuffer();
            }

            this.outputStream.Write(buffer, 0, buffer.Length);
            base.Close();
        }

        /// <summary>
        ///     The read original html.
        /// </summary>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        private string ReadOriginalHtml()
        {
            var html = string.Empty;
            this.Position = 0;
            using (var reader = new StreamReader(this))
            {
                html = reader.ReadToEnd();
            }

            return html;
        }
    }
}