// --------------------------------------------------------------------------------------------------------------------
// <copyright file="SystemSetting.cs" company="Usama Nada">
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
    /// The system setting.
    /// </summary>
    [Table("common.SystemSettings")]
    public class SystemSetting
    {
        /// <summary>
        /// Gets or sets the application id.
        /// </summary>
        [StringLength(100)]
        [Column(Order = 1)]
        public string ApplicationId { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        [StringLength(255)]
        [Column(Order = 9)]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        [Column(TypeName = "datetime2", Order = 10)]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the group name.
        /// </summary>
        [Required]
        [StringLength(50)]
        [Column(Order = 6)]
        public string GroupName { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Column(Order = 0)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is active.
        /// </summary>
        [Column(Order = 8)]
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is sticky.
        /// </summary>
        [Column(Order = 7)]
        public bool IsSticky { get; set; }

        /// <summary>
        /// Gets or sets the key.
        /// </summary>
        [Required]
        [StringLength(100)]
        [Column(Order = 3)]
        public string Key { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether secure.
        /// </summary>
        [Column(Order = 2)]
        public bool Secure { get; set; }

        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        [StringLength(255)]
        [Column(Order = 11)]
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the updated on.
        /// </summary>
        [Column(TypeName = "datetime2", Order = 12)]
        public DateTime? UpdatedOn { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [Required]
        [StringLength(255)]
        [Column(Order = 5)]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the value type.
        /// </summary>
        [Required]
        [StringLength(30)]
        [Column(Order = 4)]
        public string ValueType { get; set; }
    }
}