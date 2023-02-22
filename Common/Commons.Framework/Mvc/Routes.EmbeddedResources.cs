// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Routes.EmbeddedResources.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Mvc
{
    #region

    using System.IO;
    using System.Linq;
    using System.Web;
    using System.Web.Routing;

    using Commons.Framework.EmbeddedResources;

    #endregion

    /// <summary>
    ///     The routes.
    /// </summary>
    public static partial class Routes
    {
        /// <summary>
        /// The register embedded reources routes.
        /// </summary>
        /// <param name="routes">
        /// The routes.
        /// </param>
        public static void RegisterEmbeddedReourcesRoutes(RouteCollection routes)
        {
            routes.Add(
                new Route("EmbeddedResources/v{versionNumber}/{folderName}/{*.*}", new EmbeddedReourcesRouteHandler()));
        }
    }

    /// <summary>
    ///     The embedded reources route handler.
    /// </summary>
    public class EmbeddedReourcesRouteHandler : IRouteHandler
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
            return new EmbeddedReourcesRoutesHandler();
        }
    }

    /// <summary>
    ///     The embedded reources routes handler.
    /// </summary>
    public class EmbeddedReourcesRoutesHandler : IHttpHandler
    {
        /// <summary>
        ///     The _valid extensions.
        /// </summary>
        private static readonly string[] _validExtensions = { "jpg", "bmp", "gif", "png" }; // etc

        /// <summary>
        ///     The is reusable.
        /// </summary>
        public bool IsReusable => false;

        /// <summary>
        /// The is image extension.
        /// </summary>
        /// <param name="ext">
        /// The ext.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsImageExtension(string ext)
        {
            return _validExtensions.Contains(ext);
        }

        /// <summary>
        /// The process request.
        /// </summary>
        /// <param name="httpContext">
        /// The http context.
        /// </param>
        public void ProcessRequest(HttpContext httpContext)
        {
            var filePath = httpContext.Request.Path;
            var extension = Path.GetExtension(filePath);

            var fileContents = EmbeddedResourceReader.Read(filePath);

            if (fileContents == null)
            {
                httpContext.Response.StatusCode = 404;
            }

            httpContext.Response.ContentType = MimeTypeMap.GetMimeType(extension);

            if (!IsImageExtension(extension))
            {
                httpContext.Response.Headers.Add("charset", "utf-8");
            }

            httpContext.Response.BinaryWrite(fileContents);
            httpContext.Response.End();
        }
    }
}