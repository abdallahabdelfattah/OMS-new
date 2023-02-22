// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NavigationRepository.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Data.Repositories
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;

    using Commons.Framework.Extensions;
    using Commons.Framework.Notifications;
    
    #endregion

    /// <summary>
    /// The navigation repository.
    /// </summary>
    public class NotificationQueuesRepository : RepositoryBase<NotificationQueue>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public NotificationQueuesRepository(DbContext context)
            : base(context)
        {
        }

        private string CurrentLangSuffix => Thread.CurrentThread.CurrentCulture.Name.Split('-')[0].ToPascalCase();

        /// <summary>
        /// The add notification to queue.
        /// </summary>
        /// <param name="email">
        /// The email.
        /// </param>
        /// <param name="enforceArabic">
        /// The enforce Arabic.
        /// </param>
        /// <exception cref="Exception">
        /// </exception>
        public void Add(object message, string preferredLanguage = null)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            if (!(message is EmailMessage) && !(message is SmsMessage))
            {
                throw new ArgumentException("message type must be EmailMessage or SmsMessage");
            }

            var notificationType = message is EmailMessage ? NotificationTypes.Email : NotificationTypes.SMS;

            var notificationTemplatesRepo = new NotificationTemplatesRepository(this.Context);

            var mainContainerTemplate = string.Empty;
            var templateName = string.Empty;
            var applicationId = string.Empty;
            var subject = string.Empty;
            var createdBy = string.Empty;
            var recipients = string.Empty;
            var data = new Dictionary<string, string>();

            if (notificationType == NotificationTypes.Email)
            {
                var email = (EmailMessage)message;
                recipients = string.Join(";", email.To.Select(a => a.Address));
                subject = email.Subject;
                mainContainerTemplate =
                    $"CommonEmailStructure{(string.IsNullOrEmpty(preferredLanguage) ? this.CurrentLangSuffix : preferredLanguage.ToCamelCase())}";
                templateName = email.TemplateName + (string.IsNullOrEmpty(preferredLanguage)
                                                         ? this.CurrentLangSuffix
                                                         : preferredLanguage.ToCamelCase());
                applicationId = email.ApplicationId;
                data = email.Data;
                createdBy = email.CreatedBy;
            }

            if (notificationType == NotificationTypes.SMS)
            {
                var sms = (SmsMessage)message;
                recipients = sms.PhoneNumber;
                subject = sms.Title;
                mainContainerTemplate =
                    $"CommonSmsStructure{(string.IsNullOrEmpty(preferredLanguage) ? this.CurrentLangSuffix : preferredLanguage.ToCamelCase())}";
                templateName = sms.TemplateName + (string.IsNullOrEmpty(preferredLanguage)
                                                       ? this.CurrentLangSuffix
                                                       : preferredLanguage.ToCamelCase());
                applicationId = sms.ApplicationId;
                data = sms.Data;
                createdBy = sms.CreatedBy;
            }

            var commonMessageContent = notificationTemplatesRepo.GetTemplate(
                mainContainerTemplate,
                NotificationTypes.Email,
                CommonsSettings.ApplicationName);

            if (string.IsNullOrEmpty(commonMessageContent))
            {
                throw new Exception(
                    $"The template {mainContainerTemplate} is not available, Check table common.NotificationTemplate");
            }

            var messageContent = notificationTemplatesRepo.GetTemplate(templateName, notificationType, applicationId);

            if (string.IsNullOrEmpty(messageContent))
            {
                throw new Exception(
                    $"The template '{templateName}' is not available, Check table common.NotificationTemplate");
            }

            // replace values in design template
            messageContent = data.Keys.Aggregate(
                messageContent,
                (current, key) => current.Replace($"{"{" + key + "}"}", data[key]));

            // replace values in common template
            commonMessageContent = data.Keys.Aggregate(
                commonMessageContent,
                (current, key) => current.Replace($"{"{" + key + "}"}", data[key]));

            var content = commonMessageContent.Replace("{Body}", messageContent);

            var nq = new NotificationQueue
                         {
                             Id = Guid.NewGuid().AsSequentialGuid(),
                             NotificationTypeId = (byte)notificationType,
                             NotificationSendStatusId = (int)NotificationSendStatus.NotProcessed,
                             Subject = subject,
                             // Sender = sender, 
                             Recipients = recipients,
                             Content = content,
                             CreatedBy = createdBy,
                             CreatedOn = DateTime.Now
                         };


            base.Add(nq);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="preferredLanguage"></param>
        public void AddMobileNotification(object message, string preferredLanguage = null)
        {
            if (message == null)
            {
                throw new ArgumentNullException(nameof(message));
            }

            var notificationTemplatesRepo = new NotificationTemplatesRepository(this.Context);

            var templateName = string.Empty;
            var applicationId = string.Empty;
            var subject = string.Empty;
            var createdBy = string.Empty;
            var recipients = string.Empty;
            var data = new Dictionary<string, string>();

            var mobileNotification = (MobileNotification) message;
            recipients = mobileNotification.MobileToken;
            subject = mobileNotification.Title;

            templateName = mobileNotification.TemplateName + (string.IsNullOrEmpty(preferredLanguage)
                               ? this.CurrentLangSuffix
                               : preferredLanguage.ToCamelCase());

            applicationId = mobileNotification.ApplicationId;
            data = mobileNotification.Data;
            createdBy = mobileNotification.CreatedBy;

            var messageContent =
                notificationTemplatesRepo.GetTemplate(templateName, NotificationTypes.MobileNotification,
                    applicationId);

            if (string.IsNullOrEmpty(messageContent))
            {
                throw new Exception(
                    $"The template '{templateName}' is not available, Check table common.NotificationTemplate");
            }

            //get requestId
            var requestId = data["RequestId"];

            //get  message content
            var item = data.Where(x => !x.Key.Contains("RequestId")).ToDictionary();

            // replace values in design template
            messageContent = item.Keys.Aggregate(
                messageContent,
                (current, key) => current.Replace($"{"{" + key + "}"}", item[key]));

            var nq = new NotificationQueue
            {
                Id = Guid.NewGuid().AsSequentialGuid(),
                NotificationTypeId = (byte) NotificationTypes.MobileNotification,
                NotificationSendStatusId = (int) NotificationSendStatus.NotProcessed,
                Subject = subject,
                // Sender = sender, 
                Recipients = recipients,
                Content = messageContent + "@" + requestId,
                CreatedBy = createdBy,
                CreatedOn = DateTime.Now
            };


            base.Add(nq);
        }


        //private static void AddNotificationToQueue(NotificationQueue nq)
        //{
        //    using (var context = new CommonsDbEntities())
        //    {
        //        nq.NotificationSendStatusId = (int)NotificationSendStatus.NotProcessed;
        //        nq.CreatedOn = DateTime.Now;
        //        nq.Id = Guid.NewGuid().AsSequentialGuid();
        //        context.NotificationQueues.Add(nq);
        //        try
        //        {
        //            context.SaveChanges();
        //        }
        //        catch (DbEntityValidationException dbEx)
        //        {
        //            foreach (var validationErrors in dbEx.EntityValidationErrors)
        //            {
        //                foreach (var validationError in validationErrors.ValidationErrors)
        //                {
        //                    Trace.TraceInformation(
        //                        "Property: {0} Error: {1}",
        //                        validationError.PropertyName,
        //                        validationError.ErrorMessage);
        //                }
        //            }
        //        }
        //    }
        //}

        /// <summary>
        /// The cleanup old queue items.
        /// </summary>
        /// <param name="numOfDays">
        /// The num of days.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int CleanupOldQueueItems(int numOfDays)
        {
            var dateToCleanBefore = DateTime.Now.AddDays(numOfDays * -1);

            var items = this.DbSet.Where(n => n.CreatedOn < dateToCleanBefore).ToList();

            if (!items.Any())
            {
                return 0;
            }

            this.Delete(items);

            return items.Count;
        }

        /// <summary>
        /// The get notification queue items.
        /// </summary>
        /// <param name="status">
        /// The status.
        /// </param>
        /// <param name="pageNum">
        /// The page num.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <returns>
        /// The <see cref="PagedList"/>.
        /// </returns>
        public PagedList<NotificationQueue> GetNotificationQueueItems(
            NotificationSendStatus status,
            int pageNum,
            int pageSize)
        {

            return this.DbSet.Where(n => n.NotificationSendStatusId == (int)status)
                .GetPaged(o => o.CreatedOn, true, pageNum, pageSize);
        }

        /// <summary>
        /// The get under procssing notification queue items.
        /// </summary>
        /// <param name="pageNum">
        /// The page num.
        /// </param>
        /// <param name="pageSize">
        /// The page size.
        /// </param>
        /// <param name="timeInSeconds">
        /// The time in seconds.
        /// </param>
        /// <returns>
        /// The <see cref="PagedList"/>.
        /// </returns>
        public PagedList<NotificationQueue> GetUnderProcssingNotificationQueueItems(
            int pageNum,
            int pageSize,
            int timeInSeconds)
        {
            var datetimeToCheckBefore = DateTime.Now.AddSeconds(-timeInSeconds);

            return this.DbSet
                .Where(
                    n => n.NotificationSendStatusId == (int)NotificationSendStatus.UnderProcessing
                         && n.CreatedOn < datetimeToCheckBefore).GetPaged(o => o.CreatedOn, true, pageNum, pageSize);
        }

        /// <summary>
        /// The increment notification queue item retry count.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="updatedBy">
        /// The updated By.
        /// </param>
        public void IncrementNotificationQueueItemRetryCount(Guid id, string updatedBy = null)
        {
            var nq = this.DbSet.FirstOrDefault(c => c.Id == id);

            if (nq != null)
            {
                nq.UpdatedOn = DateTime.Now;
                nq.UpdatedBy = updatedBy;
                nq.RetryCount++;
            }
        }


        /// <summary>
        /// The update notification queue item status.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="nss">
        /// The nss.
        /// </param>
        /// <param name="updatedBy">
        /// The updated By.
        /// </param>
        public void UpdateNotificationQueueItemStatus(Guid id, NotificationSendStatus nss, string updatedBy = null)
        {
            var nq = this.DbSet.FirstOrDefault(c => c.Id == id);

            if (nq != null)
            {
                if (nss == NotificationSendStatus.Sent)
                {
                    nq.SentOn = DateTime.Now;
                }

                nq.UpdatedBy = updatedBy;
                nq.UpdatedOn = DateTime.Now;
                nq.NotificationSendStatusId = (byte)nss;
            }
        }
    }
}