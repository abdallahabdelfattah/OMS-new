// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationQueue.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Data
{
    #region

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    #endregion

    /// <summary>
    /// The notification queue.
    /// </summary>
    [Table("common.NotificationQueue")]
    public class NotificationQueue
    {
        /// <summary>
        /// Gets or sets the base request id.
        /// </summary>
        [Column(Order = 9)]
        public Guid? BaseRequestId { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        [Required]
        [Column(Order = 2)]
        public string Content { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        [Required]
        [StringLength(256)]
        [Column(Order = 11)]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        [Column(TypeName = "datetime2", Order = 10)]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Column(Order = 0)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the notification send statu.
        /// </summary>
        public virtual NotificationSendStatu NotificationSendStatu { get; set; }

        /// <summary>
        /// Gets or sets the notification send status id.
        /// </summary>
        [Column(Order = 4)]
        public byte NotificationSendStatusId { get; set; }

        /// <summary>
        /// Gets or sets the notification type.
        /// </summary>
        public virtual NotificationType NotificationType { get; set; }

        /// <summary>
        /// Gets or sets the notification type id.
        /// </summary>
        [Column(Order = 3)]
        public byte NotificationTypeId { get; set; }

        /// <summary>
        /// Gets or sets the recipients.
        /// </summary>
        [StringLength(500)]
        [Column(Order = 7)]
        public string Recipients { get; set; }

        /// <summary>
        /// Gets or sets the retry count.
        /// </summary>
        [Column(Order = 8)]
        public byte? RetryCount { get; set; }

        /// <summary>
        /// Gets or sets the sender.
        /// </summary>
        [StringLength(256)]
        [Column(Order = 6)]
        public string Sender { get; set; }

        /// <summary>
        /// Gets or sets the sent on.
        /// </summary>
        [Column(TypeName = "datetime2", Order = 5)]
        public DateTime? SentOn { get; set; }

        /// <summary>
        /// Gets or sets the subject.
        /// </summary>
        [StringLength(256)]
        [Column(Order = 1)]
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        [StringLength(256)]
        [Column(Order = 13)]
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the updated on.
        /// </summary>
        [Column(TypeName = "datetime2", Order = 12)]
        public DateTime? UpdatedOn { get; set; }
    }
}