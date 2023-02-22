// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Log.cs" company="Usama Nada">
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
    /// The log.
    /// </summary>
    [Table("common.Log")]
    public class Log
    {
        /// <summary>
        /// Gets or sets the all xml.
        /// </summary>
        [StringLength(6000)]
        [Column(Order = 15)]
        public string AllXml { get; set; }

        /// <summary>
        /// Gets or sets the application.
        /// </summary>
        [Required]
        [StringLength(60)]
        [Column(Order = 1)]
        public string Application { get; set; }

        /// <summary>
        /// Gets or sets the browser.
        /// </summary>
        [StringLength(255)]
        [Column(Order = 9)]
        public string Browser { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        [Column(TypeName = "datetime2", Order = 3)]
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the exception.
        /// </summary>
        [StringLength(6000)]
        [Column(Order = 12)]
        public string Exception { get; set; }

        /// <summary>
        /// Gets or sets the exception data.
        /// </summary>
        [StringLength(500)]
        [Column(Order = 14)]
        public string ExceptionData { get; set; }

        /// <summary>
        /// Gets or sets the exception type.
        /// </summary>
        [StringLength(255)]
        [Column(Order = 13)]
        public string ExceptionType { get; set; }

        /// <summary>
        /// Gets or sets the host.
        /// </summary>
        [StringLength(50)]
        [Column(Order = 2)]
        public string Host { get; set; }

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        [Column(Order = 0)]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the level.
        /// </summary>
        [Required]
        [StringLength(50)]
        [Column(Order = 5)]
        public string Level { get; set; }

        /// <summary>
        /// Gets or sets the logger.
        /// </summary>
        [Required]
        [StringLength(255)]
        [Column(Order = 6)]
        public string Logger { get; set; }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        [Required]
        [StringLength(4000)]
        [Column(Order = 11)]
        public string Message { get; set; }

        /// <summary>
        /// Gets or sets the status code.
        /// </summary>
        [Column(Order = 8)]
        public int? StatusCode { get; set; }

        /// <summary>
        /// Gets or sets the thread.
        /// </summary>
        [Required]
        [StringLength(255)]
        [Column(Order = 4)]
        public string Thread { get; set; }

        /// <summary>
        /// Gets or sets the url.
        /// </summary>
        [StringLength(500)]
        [Column(Order = 7)]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        [StringLength(255)]
        [Column(Order = 10)]
        public string User { get; set; }
    }
}