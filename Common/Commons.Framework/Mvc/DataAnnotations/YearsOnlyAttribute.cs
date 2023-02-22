// --------------------------------------------------------------------------------------------------------------------
// <copyright file="YearsOnlyAttribute.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Mvc.DataAnnotations
{
    #region

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Globalization;
    using System.Web.Mvc;

    using Commons.Framework.Extensions;
    using Commons.Framework.Mvc.Helpers;
    using Commons.Framework.Resources;

    #endregion

    /// <summary>
    ///     The years only attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class YearsOnlyAttribute : RegularExpressionAttribute
    {
        /// <summary>
        ///     The pattern.
        /// </summary>
        private new const string Pattern = @"^\d{4}$";

        /// <summary>
        ///     Initializes static members of the <see cref="YearsOnlyAttribute" /> class.
        ///     Initializes static members of the <see cref="ArabicTextOnlyAttribute" />
        ///     class.
        ///     Initializes static members of the <see cref="DisableScriptsAttribute" /> class.
        /// </summary>
        static YearsOnlyAttribute()
        {
            // necessary to enable client side validation
            DataAnnotationsModelValidatorProvider.RegisterAdapter(
                typeof(YearsOnlyAttribute),
                typeof(RegularExpressionAttributeAdapter));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="YearsOnlyAttribute" /> class.
        /// </summary>
        public YearsOnlyAttribute()
            : base(Pattern)
        {
        }

        /// <summary>
        ///     Gets or sets the culture.
        /// </summary>
        public CalendarType CalendarType { get; set; } = CalendarType.gregorian;

        /// <summary>
        /// The format error message.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public override string FormatErrorMessage(string name)
        {
            return string.Format(Messages.YearOnlyErrorMessage, name);
        }

        /// <summary>
        /// The is valid.
        /// </summary>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public override bool IsValid(object value)
        {
            if (string.IsNullOrEmpty(value.To<string>()))
            {
                return true;
            }

            try
            {
                var date = DateTime.ParseExact(
                    value.To<string>(),
                    "yyyy",
                    this.CalendarType == CalendarType.gregorian ? new CultureInfo("en-US") : new CultureInfo("ar-SA"));
                return date.Year != DateTime.MinValue.Year;
            }
            catch
            {
                return false;
            }
        }
    }
}