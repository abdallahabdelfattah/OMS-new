// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Bundling.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Mvc
{
    #region

    using System.Collections.Generic;
    using System.Web.Optimization;

    #endregion

    /// <summary>
    ///     The as is bundle orderer.
    /// </summary>
    public class AsIsBundleOrderer : IBundleOrderer
    {
        /// <summary>
        /// The order files.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        /// <param name="files">
        /// The files.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        public virtual IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }

    /// <summary>
    ///     The bundle extensions.
    /// </summary>
    public static class BundleExtensions
    {
        /// <summary>
        /// The force ordered.
        /// </summary>
        /// <param name="sb">
        /// The sb.
        /// </param>
        /// <returns>
        /// The <see cref="Bundle"/>.
        /// </returns>
        public static Bundle ForceOrdered(this Bundle sb)
        {
            sb.Orderer = new AsIsBundleOrderer();
            return sb;
        }
    }
}