// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommonsUnitOfWork.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Data
{
    #region

    using System;

    using Commons.Framework.Data.Repositories;

    #endregion

    /// <summary>
    /// The commons unit of work.
    /// </summary>
    public sealed class CommonsUnitOfWork : UnitOfWorkBase, IUnitOfWork
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CommonsUnitOfWork"/> class.
        /// </summary>
        public CommonsUnitOfWork()
        {
            this.context = new CommonsDbEntities();
            this.Navigations = new NavigationRepository(this.context);
            this.SystemSettings = new SystemSettingsRepository(this.context);
            this.AttachmentTypes = new AttachmentTypeRepository(this.context);
            this.NotificationQueues = new NotificationQueuesRepository(this.context);
            this.NotificationTemplates = new NotificationTemplatesRepository(this.context);
        }

        public NotificationTemplatesRepository NotificationTemplates { get; set; }

        public NotificationQueuesRepository NotificationQueues { get; set; }

        /// <summary>
        /// Finalizes an instance of the <see cref="CommonsUnitOfWork"/> class. 
        /// </summary>
        ~CommonsUnitOfWork()
        {
            this.Dispose(false);
        }

        /// <summary>
        /// Gets or sets the navigations.
        /// </summary>
        public NavigationRepository Navigations { get; set; }

        /// <summary>
        /// Gets or sets the system settings.
        /// </summary>
        public SystemSettingsRepository SystemSettings { get; set; }

        /// <summary>
        /// Gets or sets the attachment types.
        /// </summary>
        internal AttachmentTypeRepository AttachmentTypes { get; set; }

        /// <summary>
        /// The dispose.
        /// </summary>
        public new void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// The dispose.
        /// </summary>
        /// <param name="disposing">
        /// The disposing.
        /// </param>
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.context?.Dispose();
                var dbContext = this.context;
                dbContext?.Dispose();
            }
        }
    }
}