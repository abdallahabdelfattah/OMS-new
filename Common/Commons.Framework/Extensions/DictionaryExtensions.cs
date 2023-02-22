// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DictionaryExtensions.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Extensions
{
    #region

    using System;
    using System.Dynamic;

    #region

    using System.Collections.Generic;

    #endregion

    #endregion

    /// <summary>
    ///     Extends dictionary classes with XML
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// The get value or default.
        /// </summary>
        /// <param name="dictionary">
        /// The dictionary.
        /// </param>
        /// <param name="key">
        /// The key.
        /// </param>
        /// <param name="defaultValue">
        /// The default value.
        /// </param>
        /// <typeparam name="TKey">
        /// </typeparam>
        /// <typeparam name="TValue">
        /// </typeparam>
        /// <returns>
        /// The <see cref="TValue"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// </exception>
        public static TValue GetValueOrDefault<TKey, TValue>(
            this IDictionary<TKey, TValue> dictionary,
            TKey key,
            TValue defaultValue = default(TValue))
        {
            if (dictionary == null)
            {
                throw new ArgumentNullException(nameof(dictionary));
            }

            TValue value;
            return dictionary.TryGetValue(key, out value) ? value : defaultValue;
        }

        public static object ToAnonymousObject(this IDictionary<string, object> @this)
        {
            var obj = new ExpandoObject();
            var dic = (IDictionary<string, object>)obj;

            foreach (var keyValuePair in @this)
            {
                dic.Add(keyValuePair);
            }

            return dic;
        }
    }
}