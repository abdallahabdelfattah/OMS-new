// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ServiceSettings.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.WinServices
{
    #region

    using System.ServiceProcess;

    #endregion

    /// <summary>
    ///     The service settings.
    /// </summary>
    public class ServiceSettings
    {
        /// <summary>
        ///     Gets or sets the service account.
        /// </summary>
        public ServiceAccount ServiceAccount { get; set; }

        /// <summary>
        ///     Gets or sets the service account password.
        /// </summary>
        public string ServiceAccountPassword { get; set; }

        /// <summary>
        ///     Gets or sets the service account user name.
        /// </summary>
        public string ServiceAccountUserName { get; set; }

        /// <summary>
        ///     Gets or sets the service description.
        /// </summary>
        public string ServiceDescription { get; set; }

        /// <summary>
        ///     Gets or sets the service display name.
        /// </summary>
        public string ServiceDisplayName { get; set; }

        /// <summary>
        ///     Gets or sets the service executale path.
        /// </summary>
        public string ServiceExecutalePath { get; set; }

        /// <summary>
        ///     Gets or sets the service name.
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        ///     Gets or sets the service start mode.
        /// </summary>
        public ServiceStartMode ServiceStartMode { get; set; }

        /// <summary>
        ///     Gets or sets the service timeout.
        /// </summary>
        public int ServiceTimeout { get; set; }
    }
}