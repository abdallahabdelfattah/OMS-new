// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MemoryCacheManager.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Caching
{
    #region

    using System;
    using System.Runtime.Caching;

    using Commons.Framework.Extensions;

    #endregion

    /// <summary>
    ///     The memory cache manager.
    /// </summary>
    public class MemoryCacheManager : ICache
    {
        /// <summary>
        /// The cache.
        /// </summary>
        private static readonly MemoryCache Cache = MemoryCache.Default;

        /// <summary>
        /// The flush.
        /// </summary>
        /// <exception cref="NotImplementedException">
        /// </exception>
        public void Flush()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <returns>
        /// The <see cref="T"/>.
        /// </returns>
        public T Get<T>(string key)
        {
            return Cache.Get(key).To<T>();
        }

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
        public T GetAndRemove<T>(string key)
        {
            var val = Cache.Get(key);
            Cache.Remove(key);
            return val.To<T>();
        }

        /// <summary>
        /// The remove.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        public void Remove(string key)
        {
            Cache.Remove(key);
        }

        /// <summary>
        /// The set.
        /// </summary>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="value">
        /// The value.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        public void Set<T>(string key, T value)
        {
            Cache.Set(key, value, new CacheItemPolicy { Priority = CacheItemPriority.Default });
        }

        /// <summary>
        /// The set.
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
        /// <typeparam name="T">
        /// </typeparam>
        public void Set<T>(string key, T value, TimeSpan timeout)
        {
            Cache.Set(key, value, new CacheItemPolicy { AbsoluteExpiration = DateTimeOffset.Now.Add(timeout) });
        }
    }
}