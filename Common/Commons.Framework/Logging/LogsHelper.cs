// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LogsHelper.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Logging
{
    #region

    using System;
    using System.Linq;

    using Commons.Framework.Data;

    #endregion

    /// <summary>
    ///     The system settings helper.
    /// </summary>
    public static class LogsHelper
    {
        /// <summary>
        /// The cleanup old log items.
        /// </summary>
        /// <param name="numOfDays">
        /// The num of days.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int CleanupOldLogItems(int numOfDays)
        {
            var dateToCleanBefore = DateTime.Now.AddDays(numOfDays * -1);
            using (var context = new CommonsDbEntities())
            {
                var items = context.Logs.Where(n => n.Date < dateToCleanBefore);
                if (!items.Any())
                {
                    return 0;
                }

                context.SaveChanges();
                return items.Count();
            }
        }
    }
}