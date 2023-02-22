// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GreaterThanAttribute.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Mvc.DataAnnotations
{
    #region Usings

    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// The greater than attribute.
    /// </summary>
    public class GreaterThanAttribute : ValidationAttribute, IClientValidatable
    {

        private string MinDisplayName { get; set; }

        public bool AllowEqualValues { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GreaterThanAttribute"/> class.
        /// </summary>
        /// <param name="minPropertyName">
        /// The other property.
        /// </param>
        public GreaterThanAttribute(string minPropertyName)
        {
            this.MinPropertyName = minPropertyName;
        }

        /// <summary>
        /// Gets or sets the other property.
        /// </summary>
        public string MinPropertyName { get; set; }


        /// <summary>
        /// The get second comparable.
        /// </summary>
        /// <param name="validationContext">
        /// The validation context.
        /// </param>
        /// <returns>
        /// The <see cref="IComparable"/>.
        /// </returns>
        protected IComparable GetSecondComparable(ValidationContext validationContext)
        {
            var propertyInfo = validationContext.ObjectType.GetProperty(this.MinPropertyName);
            if (propertyInfo != null)
            {
                var secondValue = propertyInfo.GetValue(validationContext.ObjectInstance, null);
                return secondValue as IComparable;
            }

            return null;
        }

        /// <summary>
        /// The is valid.
        /// </summary>
        /// <param name="firstValue">
        /// The first value.
        /// </param>
        /// <param name="validationContext">
        /// The validation context.
        /// </param>
        /// <returns>
        /// The <see cref="ValidationResult"/>.
        /// </returns>
        protected override ValidationResult IsValid(object firstValue, ValidationContext validationContext)
        {
            var propertyInfo = validationContext.ObjectType.GetProperty(this.MinPropertyName);
            if (propertyInfo == null)
            {
                return new ValidationResult($"unknown property {this.MinPropertyName}");
            }

            var firstComparable = firstValue as IComparable;
            var secondComparable = this.GetSecondComparable(validationContext);

            if (firstComparable == null || secondComparable == null)
            {
                return ValidationResult.Success;
            }

            if (this.AllowEqualValues && firstComparable.CompareTo(secondComparable) == 0)
            {
                return ValidationResult.Success;
            }

            if (firstComparable.CompareTo(secondComparable) < 0)
            {
                return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
            }

            return ValidationResult.Success;
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(ModelMetadata metadata, ControllerContext context)
        {
            var type = metadata.ContainerType;
            var model = Activator.CreateInstance(type);

            var provider = new DataAnnotationsModelMetadataProvider();
            var otherMetaData = provider.GetMetadataForProperty(() => model, type, this.MinPropertyName);

            this.MinDisplayName = otherMetaData.DisplayName;

            yield return new ModelClientValidationRule
                             {
                                 ErrorMessage = this.FormatErrorMessage(metadata.DisplayName),
                                 ValidationType = "greaterthan",
                                 ValidationParameters =
                                     {
                                         { "propertytested", this.MinPropertyName },
                                         {
                                             "allowequalvalues",
                                             this.AllowEqualValues
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
                this.MinDisplayName);
        }
    }
}