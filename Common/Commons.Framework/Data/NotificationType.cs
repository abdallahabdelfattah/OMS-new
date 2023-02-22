// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationType.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Data
{
    #region

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;

    #endregion

    /// <summary>
    /// The notification type.
    /// </summary>
    [Table("common.NotificationType")]
    public sealed class NotificationType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotificationType"/> class.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public NotificationType()
        {
            this.NotificationQueues = new HashSet<NotificationQueue>();
            this.NotificationTemplates = new HashSet<NotificationTemplate>();
        }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Column(Order = 0)]
        public byte Id { get; set; }

        /// <summary>
        /// Gets or sets the name ar.
        /// </summary>
        [Required]
        [StringLength(256)]
        [Column(Order = 1)]
        public string NameAr { get; set; }

        /// <summary>
        /// Gets or sets the name en.
        /// </summary>
        [Column(Order = 2)]
        [StringLength(256)]
        public string NameEn { get; set; }

        /// <summary>
        /// Gets or sets the notification queues.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<NotificationQueue> NotificationQueues { get; set; }

        /// <summary>
        /// Gets or sets the notification templates.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<NotificationTemplate> NotificationTemplates { get; set; }
    }
}