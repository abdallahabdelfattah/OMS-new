// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BaseException.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Exceptions
{
    #region

    using System;

    #endregion

    /// <summary>
    ///     The base exception.
    /// </summary>
    [Serializable]
    public class BaseException : Exception
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BaseException" /> class.
        /// </summary>
        public BaseException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public BaseException(string message)
            : base(message)
        {
        }

        public BaseException(Exception innerException)
            : base(string.Empty,innerException)
        {
        }

        public BaseException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}