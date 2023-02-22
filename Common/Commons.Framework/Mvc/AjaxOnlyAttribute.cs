// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AjaxOnlyAttribute.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Mvc
{
    #region Usings

    using System;
    using System.Globalization;
    using System.Web.Mvc;

    #endregion

    /// <summary>
    /// The ajax only attribute.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class AjaxOnlyAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// The on action executing.
        /// </summary>
        /// <param name="filterContext">
        /// The filter context.
        /// </param>
        /// <exception cref="InvalidOperationException">
        /// </exception>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.Request.IsAjaxRequest())
            {
                ////filterContext.HttpContext.Response.StatusCode = 404;
                ////filterContext.Result = new HttpNotFoundResult();
                
                throw new InvalidOperationException(
                    string.Format(
                        CultureInfo.CurrentCulture,
                        "The action '{0}' is accessible only by an ajax request.",
                        filterContext.ActionDescriptor.ActionName));
            }

            base.OnActionExecuting(filterContext);
        }
    }
}