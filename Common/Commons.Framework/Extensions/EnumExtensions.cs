// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EnumExtensions.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Extensions
{
    #region

    using System;

    #endregion

    /// <summary>
    ///     The enum extensions.
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// The has.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool Has<T>(this Enum type, T value)
        {
            try
            {
                return ((int)(object)type & (int)(object)value) == (int)(object)value;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// The is.
        /// </summary>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool Is<T>(this Enum type, T value)
        {
            try
            {
                return (int)(object)type == (int)(object)value;
            }
            catch
            {
                return false;
            }
        }
    }
}