// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommonsSettings.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework
{
    #region

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Configuration;
    using System.Data.Entity.Core.EntityClient;
    using System.Web.Mvc;

    using Commons.Framework.Extensions;
    using Commons.Framework.Mvc;
    using Commons.Framework.Mvc.DataAnnotations;

    using RequiredAttributeAdapter = Commons.Framework.Mvc.DataAnnotations.RequiredAttributeAdapter;

    #endregion

    /// <summary>
    /// The commons settings.
    /// </summary>
    public static class CommonsSettings
    {
        /// <summary>
        /// Gets the application name.
        /// </summary>
        public static string ApplicationName { get; internal set; } ////= "MyApplicationName";
        public static string UsersMgmtApplicationUrl { get; internal set; }
        
        /// <summary>
        /// Gets the connection string name.
        /// </summary>
        public static string ConnectionStringName { get; internal set; } = "CommonsDbEntities";

        public static string ApplicationRootUrl { get; internal set; }

        /// <summary>
        /// The connstring val.
        /// </summary>
        private static string connstringVal;

        /// <summary>
        /// Gets the connection string value.
        /// </summary>
        public static string ConnectionStringValue {
            get
            {
                if (connstringVal != null)
                {
                    return connstringVal;
                }

                ConnectionStringName = ConfigurationManager.AppSettings.GetValue<string>("Commons:DbConnectionStringName") ?? ConnectionStringName;
               

                connstringVal = ConfigurationManager.ConnectionStrings[ConnectionStringName].ConnectionString;

                if (connstringVal.Contains("metadata=res"))
                {
                    connstringVal = new EntityConnectionStringBuilder(connstringVal).ProviderConnectionString;
                }

                return connstringVal;

            }

            internal set => connstringVal = value;
        }

        /// <summary>
        /// Gets the default culture.
        /// </summary>
        public static string DefaultCulture { get; internal set; } = "en-GB";

        /// <summary>
        /// Gets the widgets static content version.
        /// </summary>
        public static double WidgetsStaticContentVersion { get; internal set; } = 1.0;

        /// <summary>
        /// The configure.
        /// </summary>
        public static void Configure()
        {
            ////RouteTable.Routes.Add(new Route("EmbeddedResources/Widgets/v{versionNumber}/{*filePath}", new CustomRouteHandler()));
            ////RouteTable.Routes.Add(new Route("EmbeddedResources/Widgets", new CustomRouteHandler()));
            
            var conf = ConfigurationManager.AppSettings;
            DefaultCulture = conf.GetValue<string>("Commons:DefaultCulture") ?? "en-GB";
            ApplicationName = conf.GetValue<string>("Commons:ApplicationName");
            UsersMgmtApplicationUrl = conf.GetValue<string>("UserMgmtSiteUrl");
            
            WidgetsStaticContentVersion = conf.GetValue<double>("Commons:WidgetsStaticContentVersion", 1.0);

            ApplicationRootUrl = conf.GetValue<string>("Commons:ApplicationRootUrl", string.Empty);
            ////ApplicationRootUrl = string.IsNullOrEmpty(ApplicationRootUrl) ? "/" : ApplicationRootUrl;

            // binders
            ModelBinders.Binders.Add(typeof(DateTime), new DateModelBinder());
            ModelBinders.Binders.Add(typeof(DateTime?), new DateModelBinder());

            // required attribute change to not allow empty string
            DataAnnotationsModelValidatorProvider.RegisterAdapterFactory(
                typeof(RequiredAttribute),
                (metadata, controllerContext, attribute) => new RequiredAttributeAdapter(
                    metadata,
                    controllerContext,
                    (RequiredAttribute)attribute));
        }
    }
}