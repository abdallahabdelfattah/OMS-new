// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CustomAuthorizeAttribute.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Mvc
{
    #region

    using System.Web.Mvc;

    #endregion

    /// <summary>
    ///     The custom authorize attribute.
    /// </summary>
    public class CustomAuthorizeAttribute : AuthorizeAttribute
    {
        /// <summary>
        /// The handle unauthorized request.
        /// </summary>
        /// <param name="filterContext">
        /// The filter context.
        /// </param>
        protected override void HandleUnauthorizedRequest(AuthorizationContext filterContext)
        {
            filterContext.Result = filterContext.HttpContext.User.Identity.IsAuthenticated
                                       ? new HttpStatusCodeResult(403)
                                       : new HttpUnauthorizedResult();
        }
    }
}