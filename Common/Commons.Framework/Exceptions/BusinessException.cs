// --------------------------------------------------------------------------------------------------------------------
// <copyright file="BusinessException.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Exceptions
{
    #region

    using System;

    #endregion

    /// <summary>
    ///     The business exception.
    /// </summary>
    [Serializable]
    public class BusinessException : BaseException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="BusinessException" /> class.
        /// </summary>
        public BusinessException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BusinessException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public BusinessException(string message)
            : base(message)
        {
        }
    }
}