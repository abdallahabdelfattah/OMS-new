using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

using Commons.Framework.Extensions;
using Commons.Framework.Utils;

namespace Commons.Framework.Mvc.DataAnnotations
{
    public enum DateRestrictionType
    {
        FutureOnly,
        FutureIncludingToday,
        PastOnly,
        PastIncludingToday
    }

    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class DateRestrictionAttribute : ValidationAttribute, IClientValidatable
    {
        public DateRestrictionType DateRestrictionType { get; set; }

        public bool ValidateClientSideOnly { get; set; }


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
            if (this.ValidateClientSideOnly)
            {
                return ValidationResult.Success;
            }

            if (!(value is DateTime))
            {
                return ValidationResult.Success;
            }

            var selectedDate = value.To<DateTime>().Date;
            var today = DateTime.Now.Date;

            switch (DateRestrictionType)
            {
                case DateRestrictionType.FutureOnly:
                    if (selectedDate > today)
                    {
                        return ValidationResult.Success;
                    }
                    break;
                case DateRestrictionType.FutureIncludingToday:
                    if (selectedDate >= today)
                    {
                        return ValidationResult.Success;
                    }
                    break;
                case DateRestrictionType.PastOnly:
                    if (selectedDate < today)
                    {
                        return ValidationResult.Success;
                    }
                    break;
                case DateRestrictionType.PastIncludingToday:
                    if (selectedDate <= today)
                    {
                        return ValidationResult.Success;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(DateRestrictionType));
            }

            return new ValidationResult(this.FormatErrorMessage(validationContext.DisplayName));
        }

        public IEnumerable<ModelClientValidationRule> GetClientValidationRules(
            ModelMetadata metadata,
            ControllerContext context)
        {
            yield return new ModelClientValidationRule
            {
                ErrorMessage = this.FormatErrorMessage(metadata.DisplayName),
                ValidationType = "daterestriction",
                ValidationParameters =
                                     {
                                         { "todaydate", DateTimeHelper.GetGregoreanDateString(DateTime.Now.Date) },
                                         {
                                             "daterestrictiontype",
                                             this.DateRestrictionType.ToString().ToCamelCase()
                                         }
                                     }
            };
        }

        public override string FormatErrorMessage(string currentPropDisplayName)
        {
            return string.Format(
                CultureInfo.CurrentCulture,
                this.ErrorMessageString,
                currentPropDisplayName);
        }
    }
}