// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ItemNotFoundException.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Exceptions
{
    #region

    using System;

    #endregion

    /// <summary>
    ///     Item not found eexception
    /// </summary>
    /// <author>Ahmed Magdy (amagdy@sure.com.sa)</author>
    /// <created>20/09/2015</created>
    [Serializable]
    public class ItemNotFoundException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ItemNotFoundException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        /// <author>Ahmed Magdy (amagdy@sure.com.sa)</author>
        /// <created>20/09/2015</created>
        public ItemNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ItemNotFoundException"/> class.
        /// </summary>
        /// <param name="message">
        /// The error message that explains the reason for the exception.
        /// </param>
        /// <param name="innerException">
        /// The exception that is the cause of the current exception, or a null reference (Nothing in
        ///     Visual Basic) if no inner exception is specified.
        /// </param>
        /// <author>Ahmed Magdy (amagdy@sure.com.sa)</author>
        /// <created>20/09/2015</created>
        public ItemNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}