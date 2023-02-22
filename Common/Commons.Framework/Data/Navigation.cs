// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Navigation.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Data
{
    #region

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Diagnostics.CodeAnalysis;

    #endregion

    /// <summary>
    /// The navigation.
    /// </summary>
    [Table("common.Navigation")]
    public sealed class Navigation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Navigation"/> class.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Navigation()
        {
            this.Children = new HashSet<Navigation>();
        }

        /// <summary>
        /// Gets or sets the application id.
        /// </summary>
        [Required]
        [StringLength(100)]
        [Column(Order = 1)]
        public string ApplicationId { get; set; }

        /// <summary>
        /// Gets or sets the children.
        /// </summary>
        [SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public ICollection<Navigation> Children { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        [Required]
        [StringLength(256)]
        [Column(Order = 10)]
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the created on.
        /// </summary>
        [Column(TypeName = "datetime2", Order = 9)]
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Column(Order = 0)]
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is active.
        /// </summary>
        [Column(Order = 8)]
        public bool IsActive { get; set; }

        /// <summary>
        /// Gets or sets the item order.
        /// </summary>
        [Column(Order = 7)]
        public int? ItemOrder { get; set; }

        /// <summary>
        /// Gets or sets the link url.
        /// </summary>
        [StringLength(500)]
        [Column(Order = 4)]
        public string LinkUrl { get; set; }

        /// <summary>
        /// Gets or sets the name ar.
        /// </summary>
        [Required]
        [StringLength(100)]
        [Column(Order = 2)]
        public string NameAr { get; set; }

        /// <summary>
        /// Gets or sets the name en.
        /// </summary>
        [Required]
        [StringLength(100)]
        [Column(Order = 3)]
        public string NameEn { get; set; }

        /// <summary>
        /// Gets or sets the parent.
        /// </summary>
        public Navigation Parent { get; set; }

        /// <summary>
        /// Gets or sets the parent id.
        /// </summary>
        [Column(Order = 5)]
        public int? ParentId { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        [StringLength(500)]
        [Column(Order = 6)]
        public string Roles { get; set; }

        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        [StringLength(256)]
        [Column(Order = 12)]
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the updated on.
        /// </summary>
        [Column(TypeName = "datetime2", Order = 11)]
        public DateTime? UpdatedOn { get; set; }
    }
}