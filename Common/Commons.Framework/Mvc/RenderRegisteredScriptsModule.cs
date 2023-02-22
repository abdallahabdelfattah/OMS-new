// --------------------------------------------------------------------------------------------------------------------
// <copyright file="RenderRegisteredScriptsModule.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Mvc
{
    #region

    using System;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.UI;

    #endregion

    /// <summary>
    ///     The commons widget helpers module.
    /// </summary>
    public class RenderRegisteredScriptsModule : IHttpModule
    {
        /// <summary>
        ///     The dispose.
        /// </summary>
        public void Dispose()
        {
        }

        /// <summary>
        /// The init.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += this.ContextPreRequestHandlerExecute;

            // context.BeginRequest += this.ContextOnBeginRequest;
        }

        /// <summary>
        /// The context_ begin request.
        /// </summary>
        /// <param name="sender">
        /// The sender.
        /// </param>
        /// <param name="e">
        /// The e.
        /// </param>
        private void ContextOnBeginRequest(object sender, EventArgs e)
        {
            // var httpContext = new HttpContextWrapper(HttpContext.Current);
            // if (!(httpContext.Request.Url.AbsolutePath.EndsWith(".axd") || httpContext.Request.Url.AbsolutePath.EndsWith(".ashx")))
            // {
            // httpContext.Response.Filter = new HtmlHeadStream(httpContext.Response.Filter, httpContext);
            // }

            // var application = sender as HttpApplication;
            // if (application != null)
            // {
            // var gtmStream = new WidgetsScriptsStream(application.Context.Response.Filter);
            // application.Context.Response.Filter = gtmStream;
            // }
        }

        /// <summary>
        /// Handles the PreRequestHandlerExecute event of the Context control.
        /// </summary>
        /// <param name="sender">
        /// The source of the event.
        /// </param>
        /// <param name="e">
        /// The <see cref="EventArgs"/> instance containing the event data.
        /// </param>
        /// <author>Usama Nada (unada@sure.com.sa)</author>
        /// <created>23/11/2015</created>
        private void ContextPreRequestHandlerExecute(object sender, EventArgs e)
        {
            // if ((currentHandler is Page || currentHandler is System.Web.Mvc.MvcHandler) && )
            // {
            // //var gtmStream = new WidgetsScriptsStream(application.Context.Response.Filter);
            // //application.Context.Response.Filter = gtmStream;                
            // }
            var application = sender as HttpApplication;
            var httpContext = new HttpContextWrapper(HttpContext.Current);
            var currentHandler = application?.Context.CurrentHandler;

            if (httpContext.Request.Url != null && httpContext.Response.ContentType == "text/html"
                && httpContext.Server.GetLastError() == null && (currentHandler is Page || currentHandler is MvcHandler)
                && !(httpContext.Request.Url.AbsolutePath.EndsWith(".axd")
                     || httpContext.Request.Url.AbsolutePath.EndsWith(".ashx")))
            {
                httpContext.Response.Filter = new RegisteredScriptsStream(httpContext.Response.Filter, httpContext);
            }
        }
    }

    ///// <summary>
    ///// The gtm stream.
    ///// </summary>
    // internal class WidgetsScriptsStream : Stream
    // {

    // /// <summary>
    // /// The _base.
    // /// </summary>
    // private readonly Stream stream;

    // /// <summary>
    // /// Initializes a new instance of the <see cref="WidgetsScriptsStream"/> class.
    // /// </summary>
    // /// <param name="stream">
    // /// The stream.
    // /// </param>
    // public WidgetsScriptsStream(Stream stream)
    // {
    // this.stream = stream;
    // }

    // /// <summary>
    // /// Gets a value indicating whether can read.
    // /// </summary>
    // /// <exception cref="NotImplementedException">
    // /// </exception>
    // public override bool CanRead
    // {
    // get
    // {
    // throw new NotImplementedException();
    // }
    // }

    // /// <summary>
    // /// Gets a value indicating whether can seek.
    // /// </summary>
    // /// <exception cref="NotImplementedException">
    // /// </exception>
    // public override bool CanSeek => false;

    // /// <summary>
    // /// Gets a value indicating whether can write.
    // /// </summary>
    // /// <exception cref="NotImplementedException">
    // /// </exception>
    // public override bool CanWrite => true;

    // /// <summary>
    // /// Gets the length.
    // /// </summary>
    // /// <exception cref="NotImplementedException">
    // /// </exception>
    // public override long Length
    // {
    // get
    // {
    // throw new NotImplementedException();
    // }
    // }

    // /// <summary>
    // /// Gets or sets the position.
    // /// </summary>
    // /// <exception cref="NotImplementedException">
    // /// </exception>
    // public override long Position
    // {
    // get
    // {
    // throw new NotImplementedException();
    // }

    // set
    // {
    // throw new NotImplementedException();
    // }
    // }

    // /// <summary>
    // /// The flush.
    // /// </summary>
    // public override void Flush()
    // {
    // this.stream.Flush();
    // }

    // /// <summary>
    // /// The get byte array with gtm script injected.
    // /// </summary>
    // /// <param name="buffer">
    // /// The buffer.
    // /// </param>
    // /// <returns>
    // /// The <see cref="byte[]"/>.
    // /// </returns>
    // public byte[] GetByteArrayWithGtmScriptInjected(byte[] buffer)
    // {
    // var stringValue = Encoding.UTF8.GetString(buffer);
    // var scriptsToInject = HtmlHelperWidgetExtensions.RenderRegisteredScripts(null).ToString();

    // if (!string.IsNullOrWhiteSpace(stringValue) && !string.IsNullOrEmpty(scriptsToInject))
    // {
    // var indexOfBody = stringValue.IndexOf("</body>", StringComparison.InvariantCultureIgnoreCase);
    // var indexOfHtml = stringValue.IndexOf("</html>", StringComparison.InvariantCultureIgnoreCase);

    // var indexToUse = indexOfBody != -1 ? indexOfBody : indexOfHtml;                
    // var position = indexToUse;

    // if (position != -1)
    // {
    // stringValue = stringValue.Insert(position + 7, scriptsToInject);
    // return Encoding.UTF8.GetBytes(stringValue.ToCharArray());
    // }
    // }

    // return buffer;
    // }

    // /// <summary>
    // /// The read.
    // /// </summary>
    // /// <param name="buffer">
    // /// The buffer.
    // /// </param>
    // /// <param name="offset">
    // /// The offset.
    // /// </param>
    // /// <param name="count">
    // /// The count.
    // /// </param>
    // /// <returns>
    // /// The <see cref="int"/>.
    // /// </returns>
    // public override int Read(byte[] buffer, int offset, int count)
    // {
    // return this.stream.Read(buffer, offset, count);
    // }

    // /// <summary>
    // /// The seek.
    // /// </summary>
    // /// <param name="offset">
    // /// The offset.
    // /// </param>
    // /// <param name="origin">
    // /// The origin.
    // /// </param>
    // /// <returns>
    // /// The <see cref="long"/>.
    // /// </returns>
    // /// <exception cref="NotImplementedException">
    // /// </exception>
    // public override long Seek(long offset, SeekOrigin origin)
    // {
    // throw new NotImplementedException();
    // }

    // /// <summary>
    // /// The set length.
    // /// </summary>
    // /// <param name="value">
    // /// The value.
    // /// </param>
    // /// <exception cref="NotImplementedException">
    // /// </exception>
    // public override void SetLength(long value)
    // {
    // throw new NotImplementedException();
    // }

    // /// <summary>
    // /// The write.
    // /// </summary>
    // /// <param name="buffer">
    // /// The buffer.
    // /// </param>
    // /// <param name="offset">
    // /// The offset.
    // /// </param>
    // /// <param name="count">
    // /// The count.
    // /// </param>
    // public override void Write(byte[] buffer, int offset, int count)
    // {
    // var editedBuffer = this.GetByteArrayWithGtmScriptInjected(buffer);
    // this.stream.Write(editedBuffer, offset, editedBuffer.Length);
    // }
    // }
}