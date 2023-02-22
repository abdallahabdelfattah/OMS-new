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
    ///     The Active Director yException.
    /// </summary>
    [Serializable]
    public class ActiveDirectoryException : BaseException
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="ActiveDirectoryException" /> class.
        /// </summary>
        public ActiveDirectoryException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ActiveDirectoryException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message.
        /// </param>
        public ActiveDirectoryException(string message)
            : base(message)
        {
        }


        public ActiveDirectoryException(Exception innerException)
            : base(innerException)
        {
           
        }
    }
}