﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NumbersOnlyAttribute.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


namespace Commons.Framework.Mvc.DataAnnotations
{
    #region

    using Commons.Framework.Extensions;
    using Commons.Framework.Resources;
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// The numbers only attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class NumbersOnlyAttribute : RegularExpressionAttribute
    {
        /// <summary>
        ///     The pattern.
        /// </summary>
        private new const string Pattern = @"^[0-9]*$";

        private int? Minimum { get; set; }

        private int? Maximum { get; set; }
        /// <summary>
        /// Initializes static members of the <see cref="NumbersOnlyAttribute"/> class. 
        ///     Initializes static members of the <see cref="ArabicTextOnlyAttribute"/> class.
        ///     Initializes static members of the <see cref="DisableScriptsAttribute"/> class.
        /// </summary>
        static NumbersOnlyAttribute()
        {
            // necessary to enable client side validation
            DataAnnotationsModelValidatorProvider.RegisterAdapter(
                typeof(NumbersOnlyAttribute),
                typeof(RegularExpressionAttributeAdapter));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="NumbersOnlyAttribute"/> class. 
        ///     Initializes a new instance of the <see cref="ArabicTextOnlyAttribute"/> class.
        ///     Initializes a new instance of the <see cref="DisableScriptsAttribute"/>
        ///     class.
        /// </summary>
        public NumbersOnlyAttribute()
            : base(Pattern)
        {
        }

        public NumbersOnlyAttribute(int minimum, int maximum)
            : base(Pattern)
        {
            this.Minimum = minimum;
            this.Maximum = maximum;
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
            return string.Format(Messages.NumberOnlyErrorMessage, name);
        }

        public override bool IsValid(object value)
        {
            var obj = value.To<int>();
            if (Maximum != null && Minimum != null)
            {
                return obj < Maximum && obj > Minimum;
            }
            if (Minimum != null)
            {
                return obj > Minimum;
            }
            if (Maximum != null)
            {
                return obj > Minimum;
            }
            return base.IsValid(value);
        }
    }
}