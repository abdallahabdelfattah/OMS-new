// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DisableScriptsAttribute.cs" company="Usama Nada">
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
    ///     The disable scripts attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class DisableScriptsAttribute : RegularExpressionAttribute
    {
        /// <summary>
        ///     The pattern.
        /// </summary>
        private new const string Pattern = @"^[^<>{}]+$";

        /// <summary>
        ///     Initializes static members of the <see cref="DisableScriptsAttribute" /> class.
        /// </summary>
        static DisableScriptsAttribute()
        {
            // necessary to enable client side validation
            DataAnnotationsModelValidatorProvider.RegisterAdapter(
                typeof(DisableScriptsAttribute),
                typeof(RegularExpressionAttributeAdapter));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="DisableScriptsAttribute" /> class.
        /// </summary>
        public DisableScriptsAttribute()
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
            return string.Format(Messages.ScriptsNotAllowedErrorMessage, name);
        }
    }
}