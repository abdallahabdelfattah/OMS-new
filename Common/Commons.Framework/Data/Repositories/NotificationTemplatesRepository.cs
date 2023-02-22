namespace Commons.Framework.Data.Repositories
{
    using System;
    using System.Data.Entity;
    using System.Linq;

    using Commons.Framework.Notifications;

    public class NotificationTemplatesRepository : RepositoryBase<NotificationTemplate>
    {
        public NotificationTemplatesRepository(DbContext context)
            : base(context)
        {
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        public void DeleteTemplate(string key)
        {

            var item = this.DbSet.FirstOrDefault(
                c => c.Key == key && c.ApplicationId == CommonsSettings.ApplicationName);

            if (item == null)
            {
                return;
            }

            this.Delete(item);

        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="notificationTypeId">
        /// The notification Type Id.
        /// </param>
        /// <param name="applicationId">
        /// The application id.
        /// </param>
        public void DeleteTemplate(string key, int notificationTypeId, string applicationId)
        {

            var item = this.DbSet.FirstOrDefault(
                c => c.Key == key && c.NotificationTypeId == notificationTypeId && c.ApplicationId == applicationId);

            if (item == null)
            {
                return;
            }

            this.Delete(item);
        }

        public  string GetTemplate(string key, NotificationTypes notificationType)
        {
            if (string.IsNullOrEmpty(CommonsSettings.ApplicationName))
            {
                throw new Exception(
                    $@"The '{
                            key
                        }' Can not be retrieved from database. Application Name Key Sure:ApplicationName is missing from AppSettings section of your configuration file.");
            }

            return GetTemplate(key, notificationType, CommonsSettings.ApplicationName);
        }

        public string GetTemplate(string key, NotificationTypes notificationType, string applicationId)
        {
            applicationId = string.IsNullOrEmpty(applicationId) ? null : applicationId;

            var template = this.DbSet.FirstOrDefault(
                c => c.Key == key); //&& c.NotificationTypeId == (int)notificationType && c.ApplicationId == applicationId && c.IsActive);

            if (template == null)
            {
                throw new Exception($@"Template Key '{key}' Not Found for Application Id '{applicationId}'");
            }

            return template.Value; // ;
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="notificationType">
        /// The notification Type.
        /// </param>
        public void UpdateTemplate(string key, string value, NotificationTypes notificationType)
        {
            if (string.IsNullOrEmpty(CommonsSettings.ApplicationName))
            {
                throw new Exception(
                    $@"The '{
                            key
                        }' Not Updated. Application Name Key Sure:ApplicationName is missing from AppSettings section of your configuration file.");
            }


            var setting = this.DbSet.FirstOrDefault(
                c => c.Key == key && c.NotificationTypeId == (int)notificationType
                     && c.ApplicationId == CommonsSettings.ApplicationName);

            if (setting != null)
            {
                setting.Value = value;
            }
        }


        public void UpdateTemplate(string key, string value, NotificationTypes notificationType, string applicationId)
        {

            var setting = this.DbSet.FirstOrDefault(
                c => c.Key == key && c.NotificationTypeId == (int)notificationType && c.ApplicationId == applicationId);
            if (setting != null)
            {
                setting.Value = value;
            }
        }
    }
}
