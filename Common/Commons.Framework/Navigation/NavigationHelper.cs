// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NavigationHelper.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Navigation
{
    #region

    using System;
    using System.Linq;

    using Commons.Framework.Data;

    #endregion

    /// <summary>
    ///     The system settings helper.
    /// </summary>
    public static class NavigationHelper
    {
        /// <summary>
        /// The add navigation node.
        /// </summary>
        /// <param name="navNode">
        /// The nav node.
        /// </param>
        public static void AddNavigationNode(Navigation navNode)
        {
            // user name
            using (var context = new CommonsDbEntities())
            {
                context.Navigations.Add(navNode);
                context.SaveChanges();

                //////// CacheManager.Current.Expire(cacheKeyName);
            }
        }

        /// <summary>
        /// The cache key name.
        /// </summary>
        /// <summary>
        /// The delete.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        public static void Delete(int id)
        {
            //////// CacheManager.Current.Expire(cacheKeyName);
            using (var context = new CommonsDbEntities())
            {
                var toDel = context.Navigations.FirstOrDefault(c => c.Id == id);
                if (toDel != null)
                {
                    context.Navigations.Remove(toDel);
                    context.SaveChanges();
                }
            }
        }

        /// <summary>
        /// The get matching node.
        /// </summary>
        /// <param name="url">
        /// The url.
        /// </param>
        /// <returns>
        /// The <see cref="Navigation"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public static Navigation GetMatchingNode(string url)
        {
            if (url == null)
            {
                throw new ArgumentNullException();
            }

            var uri = new Uri(url);

            using (var context = new CommonsDbEntities())
            {
                return context.Navigations.FirstOrDefault(c => c.LinkUrl == uri.AbsolutePath);
            }
        }

        /// <summary>
        /// The rename node.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="nameAr">
        /// The name ar.
        /// </param>
        /// <param name="nameEn">
        /// The name en.
        /// </param>
        public static void RenameNode(int id, string nameAr, string nameEn)
        {
            using (var context = new CommonsDbEntities())
            {
                var node = context.Navigations.Find(id);
                if (node == null)
                {
                    return;
                }

                node.NameAr = nameAr;
                node.NameEn = nameEn;
                context.SaveChanges();

                // //// CacheManager.Current.Expire(cacheKeyName);
            }
        }

        /// <summary>
        /// The update.
        /// </summary>
        /// <param name="navNode">
        /// The nav node.
        /// </param>
        public static void Update(Navigation navNode)
        {
            using (var context = new CommonsDbEntities())
            {
                var obj = context.Navigations.Find(navNode.Id);
                context.Entry(obj).CurrentValues.SetValues(navNode);

                context.SaveChanges();

                //////// CacheManager.Current.Expire(cacheKeyName);
            }
        }

        ////        var c = context.Navigations.Where(n => n.ParentId == id);
        ////    {
        ////    using (var context = new SureDbEntities())
        ////{           

        ////public static void DeleteNode(int id)
        ////        context.Navigations.Where(n => n.Id == id).Delete();                
        ////        context.SaveChanges();
        ////        //////// CacheManager.Current.Expire(cacheKeyName);
        ////    }
        ////}        
    }
}