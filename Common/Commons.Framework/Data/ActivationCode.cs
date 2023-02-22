// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ActivationCode.cs" company="Usama Nada">
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
    /// The activation code.
    /// </summary>
    [Table("common.ActivationCode")]
    public class ActivationCode
    {
        /// <summary>
        /// Gets or sets the attempt count.
        /// </summary>
        [Column(Order = 3)]
        public int AttemptCount { get; set; }

        /// <summary>
        /// Gets or sets the code.
        /// </summary>
        [Column(Order = 4)]
        public int Code { get; set; }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        [Required]
        [StringLength(256)]
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
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the sent date time.
        /// </summary>
        [Column(TypeName = "datetime2", Order = 5)]
        public DateTime SentDateTime { get; set; }

        /// <summary>
        /// Gets or sets the updated by.
        /// </summary>
        [StringLength(256)]
        [Column(Order = 8)]
        public string UpdatedBy { get; set; }

        /// <summary>
        /// Gets or sets the updated on.
        /// </summary>
        [Column(TypeName = "datetime2", Order = 9)]
        public DateTime? UpdatedOn { get; set; }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        [Column(Order = 2)]
        public Guid UserId { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        [Required]
        [StringLength(256)]
        [Column(Order = 1)]
        public string UserName { get; set; }
    }
}