// --------------------------------------------------------------------------------------------------------------------
// <copyright file="NavigationRepository.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Data.Repositories
{
    #region

    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;

    #endregion

    /// <summary>
    /// The navigation repository.
    /// </summary>
    public class NavigationRepository : RepositoryBase<Navigation>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public NavigationRepository(DbContext context)
            : base(context)
        {
        }

        /// <summary>
        /// The add.
        /// </summary>
        /// <param name="navNode">
        /// The nav node.
        /// </param>
        /// <returns>
        /// The <see cref="ReturnResult"/>.
        /// </returns>
        public new ReturnResult Add(Navigation navNode)
        {
            var result = new ReturnResult();

            if (this.DbSet.Any(r => r.NameAr == navNode.NameAr && r.ParentId == navNode.ParentId))
            {
                result.AddErrorItem("NameAr", "Name Ar Already Exist");
            }

            if (this.DbSet.Any(r => r.NameEn == navNode.NameEn && r.ParentId == navNode.ParentId))
            {
                result.AddErrorItem("NameEn", "Name En Already Exist");
            }

            this.DbSet.Add(navNode);

            return result;
        }
    }
}