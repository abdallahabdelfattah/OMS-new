// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DateModelBinder.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Mvc
{
    #region

    using System;
    using System.Web.Mvc;

    using Commons.Framework.Resources;
    using Commons.Framework.Utils;

    #endregion

    /// <summary>
    ///     The date model binder.
    /// </summary>
    public class DateModelBinder : IModelBinder
    {
        /// <summary>
        /// The bind model.
        /// </summary>
        /// <param name="controllerContext">
        /// The controller context.
        /// </param>
        /// <param name="bindingContext">
        /// The binding context.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (value != null && string.IsNullOrWhiteSpace(value.AttemptedValue))
            {
                return null;
            }

            var dateTime = DateTime.MinValue;

            try
            {
                // var dateParts = value.AttemptedValue.Split(new[] { "/" }, StringSplitOptions.RemoveEmptyEntries);
                if (DateTimeHelper.IsUmAlqura(value.AttemptedValue))
                {
                    dateTime = DateTimeHelper.ParseUmAlquraDate(value.AttemptedValue).Value;
                }
                else if (DateTimeHelper.IsGregorian(value.AttemptedValue))
                {
                    dateTime = DateTimeHelper.ParseGregorianDate(value.AttemptedValue).Value;
                }

                if (dateTime == DateTime.MinValue)
                {
                    bindingContext.ModelState.AddModelError(bindingContext.ModelName, Messages.InvalidDateTime);
                    return null;
                }

                return dateTime;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}