// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Routes.Globalization.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Mvc
{
    using System;
    #region

    using System.Linq;
    using System.Web;
    using System.Web.Routing;

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
        public static void RegisterGlobalizationRoutes(RouteCollection routes)
        {
            //// url to match example = /Globalization/Resources/v1.1/Mci.Regulations.Localization/Mci.Regulations.Localization.AppResources/ar-SA/javascript/
            routes.Add(
                new Route("EmbeddedResources/Resources/v{versionNumber}/{*.*}", new GlobailizationResourcesRouteHandler()));
        }
    }

    /// <summary>
    ///     The globailization resources route handler.
    /// </summary>
    public class GlobailizationResourcesRouteHandler : IRouteHandler
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
            return new GlobalizationResourcesHandler();
        }
    }

    /// <summary>
    ///     The globalization resources handler.
    /// </summary>
    public class GlobalizationResourcesHandler : IHttpHandler
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
            const int AssemblyNameIndex = 4;
            const int ResourceTypeFullNameIndex = 5;
            const int CultureNameIndex = 6;
            const int FormatIndex = 7;

            var strIndex = httpContext.Request.Path.IndexOf("/EmbeddedResource", StringComparison.Ordinal);
            var newKey = httpContext.Request.Path.Substring(strIndex);

            var arr = newKey.Split('/');
            var assemblyName = arr.SafeGet(AssemblyNameIndex);
            var resourceTypeFullName = arr.SafeGet(ResourceTypeFullNameIndex);
            var cultureName = arr.SafeGet(CultureNameIndex);
            var format = !string.IsNullOrEmpty(arr.SafeGet(FormatIndex)) ? arr.SafeGet(FormatIndex) : "javascript";

            string resourceJavaScriptVarName = null;
            if (resourceTypeFullName != null && resourceTypeFullName.Contains('.') && format == "javascript")
            {
                resourceJavaScriptVarName = resourceTypeFullName.Substring(resourceTypeFullName.LastIndexOf('.') + 1);
            }

            resourceJavaScriptVarName = resourceJavaScriptVarName ?? "applicationResources";

            var fileContents = ResxConverter.ToJson(assemblyName, resourceTypeFullName, cultureName);

            if (format == "javascript")
            {
                fileContents = $"var ${resourceJavaScriptVarName.ToCamelCase()} = {fileContents};";
            }

            httpContext.Response.ContentType = "application/json";
            httpContext.Response.Headers.Add("charset", "utf-8");
            httpContext.Response.Write(fileContents);
            httpContext.Response.End();
        }
    }
}