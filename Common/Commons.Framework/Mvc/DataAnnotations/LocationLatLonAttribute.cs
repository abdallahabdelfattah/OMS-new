﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LocationLatLonAttribute.cs" company="Usama Nada">
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
    ///     The arabic text only attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class LocationLatLonAttribute : RegularExpressionAttribute
    {
        /// <summary>
        ///     The pattern.
        /// </summary>
        private new const string Pattern =
            @"^[-+]?([1-8]?\d(\.\d+)?|90(\.0+)?),\s*[-+]?(180(\.0+)?|((1[0-7]\d)|([1-9]?\d))(\.\d+)?)$";

        /// <summary>
        /// Initializes static members of the <see cref="LocationLatLonAttribute"/> class. 
        ///     Initializes static members of the <see cref="ArabicTextOnlyAttribute"/> class.
        ///     Initializes static members of the <see cref="DisableScriptsAttribute"/> class.
        /// </summary>
        static LocationLatLonAttribute()
        {
            // necessary to enable client side validation
            DataAnnotationsModelValidatorProvider.RegisterAdapter(
                typeof(LocationLatLonAttribute),
                typeof(RegularExpressionAttributeAdapter));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LocationLatLonAttribute"/> class. 
        ///     Initializes a new instance of the <see cref="ArabicTextOnlyAttribute"/> class.
        ///     Initializes a new instance of the <see cref="DisableScriptsAttribute"/>
        ///     class.
        /// </summary>
        public LocationLatLonAttribute()
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
            return string.Format(Messages.LocationLatLonErrorMessage, name);
        }
    }
}