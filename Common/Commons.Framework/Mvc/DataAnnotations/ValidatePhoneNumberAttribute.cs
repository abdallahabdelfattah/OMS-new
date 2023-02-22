// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidatePhoneNumberAttribute.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Mvc.DataAnnotations
{
    #region

    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Commons.Framework.Extensions;
    using Commons.Framework.Utils;

    using NumberType = Commons.Framework.Mvc.Helpers.NumberType;

    #endregion

    /// <summary>
    ///     The phone number attribute.
    /// </summary>
    public class ValidatePhoneNumberAttribute : ValidationAttribute, IClientValidatable
    {
        // public override string FormatErrorMessage(string name)
        // {           
        // return base.FormatErrorMessage(name);
        // }

        /// <summary>
        ///     The default error message.
        /// </summary>
        /// <summary>
        ///     Gets or sets the country code.
        /// </summary>
        public string CountryCode { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the number type.
        /// </summary>
        public NumberType NumberType { get; set; } = NumberType.MOBILE;

        /// <summary>
        /// The get client validation rules.
        /// </summary>
        /// <param name="metadata">
        /// The metadata.
        /// </param>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
            ModelMetadata metadata,
            ControllerContext context)
        {
            var modelClientValRule =
                new ModelClientValidationRule
                    {
                        ValidationType = "checkisvalidnumber",
                        ErrorMessage = this.ErrorMessage ?? this.ErrorMessageString,
                        ValidationParameters =
                            {
                                { "countrycode", this.CountryCode },
                                { "numbertype", this.NumberType }
                            }
                    };

            return new List<ModelClientValidationRule> { modelClientValRule };
        }

        // public override bool
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
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var numberType = this.NumberType.ToString().ToEnum<Utils.NumberType>();

            var isValid = PhoneNumbers.IsValidNumber(value as string, numberType, this.CountryCode);

            if (isValid)
            {
                validationContext.ObjectType.GetProperty(validationContext.MemberName).SetValue(
                    validationContext.ObjectInstance,
                    PhoneNumbers.FormatPhoneNumber(value.ToString(), this.CountryCode),
                    null);
                return ValidationResult.Success;
            }

            return new ValidationResult(this.ErrorMessage);
        }
    }
}