// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NotificationTemplate.cs" company="Usama Nada">
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
    /// The notification template.
    /// </summary>
    [Table("common.NotificationTemplate")]
    public class NotificationTemplate
    {
        /// <summary>
        /// Gets or sets the application id.
        /// </summary>
        [Required]
        [StringLength(50)]
        [Column(Order = 1)]
        public string ApplicationId { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        [Required]
        [StringLength(255)]
        [Column(Order = 6)]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        [Column(TypeName = "datetime2", Order = 7)]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Column(Order = 0)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is active.
        /// </summary>
        [Column(Order = 5)]
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        [Required]
        [StringLength(100)]
        [Column(Order = 2)]
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets the notification type.
        /// </summary>
        public virtual NotificationType NotificationType { get; set; }

        /// <summary>
        /// Gets or sets the notification type id.
        /// </summary>
        [Column(Order = 4)]
        public byte NotificationTypeId { get; set; }

        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        [StringLength(255)]
        [Column(Order = 8)]
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the updated on.
        /// </summary>
        [Column(TypeName = "datetime2", Order = 9)]
        public DateTime? UpdatedOn { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [Required]
        [StringLength(4000)]
        [Column(Order = 3)]
        public string Value { get; set; }
    }
}