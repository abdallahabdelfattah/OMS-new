// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Routes.Globalization.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Mvc
{
    using System;
    using System.Drawing.Imaging;
    using System.IO;
    #region

    using System.Linq;
    using System.Web;
    using System.Web.Routing;
    using System.Web.SessionState;
    using Commons.Framework.Drawing;
    using Commons.Framework.Extensions;
    using Commons.Framework.Globalization;

    #endregion

    /// <summary>
    ///     The routes.
    /// </summary>
    public static partial class Routes
    {
        /// <summary>
        /// The register globalization routes.
        /// </summary>
        /// <param name="routes">
        /// The routes.
        /// </param>
        public static void RegisterCaptchaRoute(RouteCollection routes)
        {
            //// url to match example = /EmbeddedResources/Captcha/Image/xxxx
            routes.Add(
                new Route("EmbeddedResources/Captcha/Image/{*.*}", new CaptchaRouteHandler()));
        }
    }

    /// <summary>
    ///     The globailization resources route handler.
    /// </summary>
    public class CaptchaRouteHandler : IRouteHandler
    {
        /// <summary>
        /// The get http handler.
        /// </summary>
        /// <param name="requestContext">
        /// The request context.
        /// </param>
        /// <returns>
        /// The <see cref="IHttpHandler"/>.
        /// </returns>
        public IHttpHandler GetHttpHandler(RequestContext requestContext)
        {
            return new CaptchaHandler();
        }
    }

    /// <summary>
    ///     The globalization resources handler.
    /// </summary>
    public class CaptchaHandler : IHttpHandler, IRequiresSessionState
    {
        /// <summary>
        ///     The is reusable.
        /// </summary>
        public bool IsReusable => false;

        /// <summary>
        /// The process request.
        /// </summary>
        /// <param name="httpContext">
        /// The http context.
        /// </param>
        public void ProcessRequest(HttpContext httpContext)
        {
            var captchaImage = new CaptchaImage
            {
                Width = 200,
                NoiseFactor = 60,
                LinesFactor = 50,
                FontSize = 28,
                Length = 6,
                CharacterSet = "1234567890ABCDEFGHIJKLMNOPQRTUVWXYZ"
            };

            httpContext.Session["__CaptchaImage__"] = captchaImage.Text;
            byte[] buffer = null;
            using (var memStream = new MemoryStream())
            {
                captchaImage.Image.Save(memStream, ImageFormat.Jpeg);
                buffer = memStream.GetBuffer();
            }

            httpContext.Response.ContentType = "image/jpeg";
            httpContext.Response.BinaryWrite(buffer);
            httpContext.Response.Flush();
        }
    }
}