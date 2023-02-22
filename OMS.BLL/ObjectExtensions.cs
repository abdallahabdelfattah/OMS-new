using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OMS.BLL
{
    public static class ObjectExtensions
    {
        // <summary>
        /// The is null or default.
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool IsNullOrDefault<T>(this object obj)
        {
            if (obj == null)
            {
                return true;
            }

            if (Equals(obj, default(T)))
            {
                return true;
            }

            return false;
        }
    }
}
