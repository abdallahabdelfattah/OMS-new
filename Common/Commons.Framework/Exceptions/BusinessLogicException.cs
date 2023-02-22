// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BusinessLogicException.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Exceptions
{
    #region

    using System;

    #endregion

    /// <summary>
    ///     The business logic exception.
    /// </summary>
    [Serializable]
    public class BusinessLogicException : BaseException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BusinessLogicException" /> class.
        /// </summary>
        public BusinessLogicException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessLogicException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public BusinessLogicException(string message)
            : base(message)
        {
        }
    }
}