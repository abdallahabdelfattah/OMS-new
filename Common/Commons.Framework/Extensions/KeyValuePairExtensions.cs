// --------------------------------------------------------------------------------------------------------------------
// <copyright file="KeyValuePairExtensions.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Extensions
{
    #region

    using System.Collections.Generic;

    #endregion

    /// <summary>
    ///     KeyValuePair Extensions
    /// </summary>
    public static class KeyValuePairExtensions
    {
        /// <summary>
        /// Withes the key.
        /// </summary>
        /// <typeparam name="TK">
        /// </typeparam>
        /// <typeparam name="TV">
        /// </typeparam>
        /// <param name="kv">
        /// The kv.
        /// </param>
        /// <param name="newKey">
        /// The new key.
        /// </param>
        public static KeyValuePair<TK, TV> WithKey<TK, TV>(this KeyValuePair<TK, TV> kv, TK newKey)
        {
            return new KeyValuePair<TK, TV>(newKey, kv.Value);
        }

        /// <summary>
        /// Withes the value.
        /// </summary>
        /// <typeparam name="TK">
        /// </typeparam>
        /// <typeparam name="TV">
        /// </typeparam>
        /// <param name="kv">
        /// The kv.
        /// </param>
        /// <param name="newValue">
        /// The new value.
        /// </param>     
        public static KeyValuePair<TK, TV> WithValue<TK, TV>(this KeyValuePair<TK, TV> kv, TV newValue)
        {
            return new KeyValuePair<TK, TV>(kv.Key, newValue);
        }
    }
}