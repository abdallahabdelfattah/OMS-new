// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CompareDatesAttribute.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Mvc.DataAnnotations
{
    #region

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Linq;
    using System.Web.Mvc;

    using Commons.Framework.Extensions;

    #endregion

    /// <summary>
    /// The compare dates attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public sealed class IsDateAfterAttribute : ValidationAttribute, IClientValidatable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IsDateAfterAttribute"/> class.
        /// </summary>
        /// <param name="from">
        /// The from.
        /// </param>
        public IsDateAfterAttribute(string from)
        {
            this.Start = from;
        }

        /// <summary>
        /// Gets the start.
        /// </summary>
        private string Start { get; }

        private string StartDisplayName { get; set; }

        public bool AllowEqualDates { get; set; }

        /// <summary>
        /// The is valid.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="validationContext">
        /// The validation context.
        /// </param>
        /// <returns>
        /// The <see cref="ValidationResult"/>.
        /// </returns>
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {

            var propertyInfo = validationContext.ObjectType.GetProperty(this.Start);
            if (propertyInfo == null)
            {
                return new ValidationResult($"unknown property {this.Start}");
            }

            var startDateValue = propertyInfo.GetValue(validationContext.ObjectInstance, null);

            if (!(value is DateTime) || !(startDateValue is DateTime))
            {
                return ValidationResult.Success;
            }

            // Compare values
            if ((DateTime)value >= (DateTime)startDateValue)
            {
                if (this.AllowEqualDates)
                {
                    return ValidationResult.Success;
                }

                if ((DateTime)value > (DateTime)startDateValue)
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));            
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
            ModelMetadata metadata,
            ControllerContext context)
        {
            var type = metadata.ContainerType;
            var model = Activator.CreateInstance(type);

            var provider = new DataAnnotationsModelMetadataProvider();
            var otherMetaData = provider.GetMetadataForProperty(() => model, type, this.Start);

            this.StartDisplayName = otherMetaData.DisplayName;
            
            yield return new ModelClientValidationRule
                             {
                                 ErrorMessage = this.FormatErrorMessage(metadata.DisplayName),
                                 ValidationType = "isdateafter",
                                 ValidationParameters =
                                     {
                                         { "propertytested", this.Start },
                                         {
                                             "allowequaldates",
                                             this.AllowEqualDates
                                         }
                                     }
                             };
        }

        public override string FormatErrorMessage(string currentPropDisplayName)
        {


            return string.Format(
                CultureInfo.CurrentCulture,
                this.ErrorMessageString,
                currentPropDisplayName,
                this.StartDisplayName);
        }
    }
}