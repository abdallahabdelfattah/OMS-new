// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UIException.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Exceptions
{
    #region

    using System;

    #endregion

    /// <summary>
    ///     The ui exception.
    /// </summary>
    [Serializable]
    public class UiException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UiException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <param name="exception">
        /// The exception.
        /// </param>
        public UiException(string message, Exception exception)
            : base(message, exception)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UiException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public UiException(string message)
            : base(message)
        {
        }
    }
}