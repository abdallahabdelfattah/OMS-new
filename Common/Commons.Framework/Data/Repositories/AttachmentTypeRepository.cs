// --------------------------------------------------------------------------------------------------------------------
// <copyright file="AttachmentTypeRepository.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Data.Repositories
{
    #region

    using System.Data.Entity;

    #endregion

    /// <summary>
    /// The attachment type repository.
    /// </summary>
    internal class AttachmentTypeRepository : RepositoryBase<AttachmentType>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AttachmentTypeRepository"/> class.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public AttachmentTypeRepository(DbContext context)
            : base(context)
        {
        }
    }
}