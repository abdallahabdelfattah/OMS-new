// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ICache.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Caching
{
    #region

    using System;

    #endregion

    /// <summary>
    ///     Caching Interface
    ///     Any Caching Provider Implementation must Implement this Interface
    /// </summary>
    public interface ICache
    {
        /// <summary>
        ///     Flushes this Cache Instance.
        /// </summary>
        void Flush();

        /// <summary>
        /// Gets the val of specified key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        T Get<T>(string key);

        /// <summary>
        /// The get and remove.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        T GetAndRemove<T>(string key);

        /// <summary>
        /// Removes the val of specified key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        void Remove(string key);

        /// <summary>
        /// Adds the specified value with specified key.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        void Set<T>(string key, T value);

        /// <summary>
        /// Adds the specified value with specified key. With Timeout
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <param name="timeout">
        /// The timeout.
        /// </param>
        void Set<T>(string key, T value, TimeSpan timeout);
    }
}