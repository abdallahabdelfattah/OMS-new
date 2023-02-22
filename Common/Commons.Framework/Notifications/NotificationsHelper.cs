// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationsHelper.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Notifications
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Validation;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading;

    using Commons.Framework.Data;
    using Commons.Framework.Extensions;

    #endregion

    /// <summary>
    ///     The system settings helper.
    /// </summary>
    public static class NotificationsHelper
    {
        /// <summary>
        ///     The cache key name.
        /// </summary>
        ////private static readonly string cacheKeyName = "Sure:NotificationsTemplates";
        /// <summary>
        ///     The current lang suffix.
        /// </summary>
        public static string CurrentLangSuffix => Thread.CurrentThread.CurrentCulture.Name.Split('-')[0].ToPascalCase();

        public static void UpdateNotificationQueueItemStatus(
            Guid id,
            NotificationSendStatus nss,
            string updatedBy = null, int? maxRetryCount = null)
        {
            using (var uow = new CommonsUnitOfWork())
            {
                var nq = uow.NotificationQueues.GetById(id);

                if (nq != null)
                {
                    if (nss == NotificationSendStatus.Sent)
                    {
                        nq.SentOn = DateTime.Now;
                    }

                    if(nq.RetryCount == null)
                    {
                        nq.RetryCount = 1;
                    }

                    if(nss == NotificationSendStatus.Failed)
                    {
                        nq.RetryCount = Convert.ToByte(nq.RetryCount.To<int>() + 1);
                    }

                    if(nq.RetryCount >= maxRetryCount)
                    {
                        nq.NotificationSendStatusId = (int)NotificationSendStatus.RetryThresholdExceeded;
                    }

                    nq.UpdatedBy = updatedBy;
                    nq.UpdatedOn = DateTime.Now;
                    nq.NotificationSendStatusId = (byte)nss;
                }

                uow.Save(updatedBy);
            }
        }

        ////public static void IncrementNotificationQueueItemRetryCount(Guid id, string updatedBy = null)
        ////{
        ////    using (var uow = new CommonsUnitOfWork())
        ////    {
        ////        var nq = uow.NotificationQueues.GetById(id);

        ////        if (nq != null)
        ////        {
        ////            nq.UpdatedOn = DateTime.Now;
        ////            nq.UpdatedBy = updatedBy;
        ////            nq.RetryCount++;

        ////            uow.Save();
        ////        }
        ////    }
        ////}

        public static int CleanupOldQueueItems(int numOfDays)
        {
            using (var uow = new CommonsUnitOfWork())
            {
                var count = uow.NotificationQueues.CleanupOldQueueItems(numOfDays);
                uow.Save();
                return count;
            }
        }

        public static PagedList<NotificationQueue> GetUnderProcssingNotificationQueueItems(
            int pageNum,
            int pageSize,
            int timeInSeconds)
        {
            using (var uow = new CommonsUnitOfWork())
            {
                return uow.NotificationQueues.GetUnderProcssingNotificationQueueItems(pageNum, pageSize, timeInSeconds);
            }
        }

        public static PagedList<NotificationQueue> GetNotificationQueueItems(
            NotificationSendStatus status,
            int pageNum,
            int pageSize)
        {
            // get failed
            using (var uow = new CommonsUnitOfWork())
            {
                return uow.NotificationQueues.GetNotificationQueueItems(status, pageNum, pageSize);
            }
        }

        public static void AddNotificationToQueue(EmailMessage mail, string userPreferredCommunicationLang)
        {
            using (var uow = new CommonsUnitOfWork())
            {
                uow.NotificationQueues.Add(mail, userPreferredCommunicationLang);
                uow.Save();
            }
        }
        public static void AddMobileNotificationToQueue(MobileNotification mobileNotification, string userPreferredCommunicationLang)
        {
            using (var uow = new CommonsUnitOfWork())
            {
                uow.NotificationQueues.AddMobileNotification(mobileNotification, userPreferredCommunicationLang);
                uow.Save();
            }
        }
    }
}