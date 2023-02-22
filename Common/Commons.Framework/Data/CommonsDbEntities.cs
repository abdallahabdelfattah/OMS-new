// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CommonsDbEntities.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Data
{
    #region

    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.Core.EntityClient;
    using System.Data.Entity.Migrations;
    using System.Linq;

    using Commons.Framework.EmbeddedResources;
    using Commons.Framework.Extensions;

    #endregion

    /// <summary>
    ///     The sure db entities.
    /// </summary>
    internal class CommonsDbEntities : DbContext
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Data.CommonsDbEntities" /> class.
        /// </summary>
        public CommonsDbEntities()
            : base(CommonsSettings.ConnectionStringValue)
        {
        }

        /// <summary>
        /// Gets or sets the activation codes.
        /// </summary>
        public virtual DbSet<ActivationCode> ActivationCodes { get; set; }

        /// <summary>
        /// Gets or sets the attachment types.
        /// </summary>
        public virtual DbSet<AttachmentType> AttachmentTypes { get; set; }

        /// <summary>
        /// Gets or sets the logs.
        /// </summary>
        public virtual DbSet<Log> Logs { get; set; }

        /// <summary>
        /// Gets or sets the navigations.
        /// </summary>
        public virtual DbSet<Navigation> Navigations { get; set; }

        /// <summary>
        /// Gets or sets the notification queues.
        /// </summary>
        public virtual DbSet<NotificationQueue> NotificationQueues { get; set; }

        /// <summary>
        /// Gets or sets the notification send status.
        /// </summary>
        public virtual DbSet<NotificationSendStatu> NotificationSendStatus { get; set; }

        /// <summary>
        /// Gets or sets the notification templates.
        /// </summary>
        public virtual DbSet<NotificationTemplate> NotificationTemplates { get; set; }

        /// <summary>
        /// Gets or sets the notification types.
        /// </summary>
        public virtual DbSet<NotificationType> NotificationTypes { get; set; }

        /// <summary>
        /// Gets or sets the system settings.
        /// </summary>
        public virtual DbSet<SystemSetting> SystemSettings { get; set; }

        /// <summary>
        /// The on model creating.
        /// </summary>
        /// <param name="modelBuilder">
        /// The model builder.
        /// </param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Log>().Property(e => e.Thread).IsUnicode(false);

            modelBuilder.Entity<Log>().Property(e => e.Level).IsUnicode(false);

            modelBuilder.Entity<Log>().Property(e => e.Logger).IsUnicode(false);

            modelBuilder.Entity<Log>().Property(e => e.Url).IsUnicode(false);

            modelBuilder.Entity<Log>().Property(e => e.Browser).IsUnicode(false);

            modelBuilder.Entity<Log>().Property(e => e.User).IsUnicode(false);

            modelBuilder.Entity<Log>().Property(e => e.Message).IsUnicode(false);

            modelBuilder.Entity<Log>().Property(e => e.Exception).IsUnicode(false);

            modelBuilder.Entity<Log>().Property(e => e.ExceptionType).IsUnicode(false);

            modelBuilder.Entity<Log>().Property(e => e.ExceptionData).IsUnicode(false);

            modelBuilder.Entity<Log>().Property(e => e.AllXml).IsUnicode(false);

            modelBuilder.Entity<Navigation>().HasMany(e => e.Children).WithOptional(e => e.Parent)
                .HasForeignKey(e => e.ParentId);

            modelBuilder.Entity<NotificationSendStatu>().HasMany(e => e.NotificationQueues)
                .WithRequired(e => e.NotificationSendStatu).HasForeignKey(e => e.NotificationSendStatusId)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<NotificationType>().HasMany(e => e.NotificationQueues)
                .WithRequired(e => e.NotificationType).WillCascadeOnDelete(false);

            modelBuilder.Entity<NotificationType>().HasMany(e => e.NotificationTemplates)
                .WithRequired(e => e.NotificationType).WillCascadeOnDelete(false);

            modelBuilder.Entity<SystemSetting>().Property(e => e.ValueType).IsUnicode(false);
        }
    }

    /// <summary>
    ///     The configuration.
    /// </summary>
    internal sealed class Configuration : DbMigrationsConfiguration<CommonsDbEntities>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Configuration" /> class.
        /// </summary>
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
            this.ContextKey = "DataLayer.CommonsDbEntities";
        }

        /// <summary>
        /// The seed.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        protected override void Seed(CommonsDbEntities context)
        {
            if (context.NotificationTypes.Any())
            {
                return;
            }

            var sql = EmbeddedResourceReader.ReadAsString("Data/InitalizeDatabase.sql");
            context.Database.ExecuteSqlCommand(sql);

            base.Seed(context);
            context.SaveChanges();
        }
    }
}