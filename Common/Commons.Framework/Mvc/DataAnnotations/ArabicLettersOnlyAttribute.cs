// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ArabicTextOnlyAttribute.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Mvc.DataAnnotations
{
    #region

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    using Commons.Framework.Resources;

    #endregion

    /// <summary>
    ///     The arabic letters only attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class ArabicLettersOnlyAttribute : RegularExpressionAttribute
    {
        /// <summary>
        /// The pattern.
        /// </summary>
        private new const string Pattern = @"^[\u0600-\u065F\u066A-\u06EF\u06FA-\u06FF]*$";

        /// <summary>
        ///     Initializes static members of the <see cref="ArabicLettersOnlyAttribute"/> class.
        ///     Initializes static members of the <see cref="DisableScriptsAttribute"/> class.
        /// </summary>
        static ArabicLettersOnlyAttribute()
        {
            // necessary to enable client side validation
            DataAnnotationsModelValidatorProvider.RegisterAdapter(
                typeof(ArabicTextOnlyAttribute),
                typeof(RegularExpressionAttributeAdapter));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ArabicLettersOnlyAttribute" /> class.
        ///     Initializes a new instance of the <see cref="DisableScriptsAttribute" />
        ///     class.
        /// </summary>
        public ArabicLettersOnlyAttribute()
            : base(Pattern)
        {
        }

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
            return string.Format(Messages.ArabicLettersOnlyErrorMessage, name);
        }
    }
}