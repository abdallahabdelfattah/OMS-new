// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ValidateCaptchaAttribute.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Mvc.DataAnnotations
{
    #region

    using System;
    using System.ComponentModel.DataAnnotations;
    using System.Web;

    using Commons.Framework.Extensions;

    #endregion

    /// <summary>
    ///     The validate captcha.
    /// </summary>
    public class ValidateCaptchaAttribute : ValidationAttribute
    {
        /// <summary>
        ///     To be used by captcha control
        /// </summary>
        public const string CaptchaId = "__CaptchaImage__";

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
            try
            {
                var captchaVal = value as string;

                if (string.IsNullOrEmpty(captchaVal)) return true;

                var imageText = HttpContext.Current.Session[CaptchaId] as string;
                if (imageText == null || string.IsNullOrEmpty(captchaVal))
                {
                    return false;
                }

                var result = string.CompareOrdinal(imageText.ToLower(), captchaVal.ReplaceArabicByEnglishNumbers().ToLower()) == 0;

                return result;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}