// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OperationFailedException.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Exceptions
{
    #region

    using System;

    #endregion

    /// <summary>
    ///     Operation Failed exception.
    /// </summary>
    /// <author>Ahmed Magdy (amagdy@sure.com.sa)</author>
    /// <created>01/10/2015</created>
    [Serializable]
    public sealed class OperationFailedException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OperationFailedException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message that describes the error.
        /// </param>
        /// <author>Ahmed Magdy (amagdy@sure.com.sa)</author>
        /// <created>01/10/2015</created>
        public OperationFailedException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OperationFailedException"/> class.
        /// </summary>
        /// <param name="operationName">
        /// Name of the operation.
        /// </param>
        /// <param name="message">
        /// The message.
        /// </param>
        /// <author>Ahmed Magdy (amagdy@sure.com.sa)</author>
        /// <created>01/10/2015</created>
        public OperationFailedException(string operationName, string message)
            : base(message)
        {
            this.Data["OperationName"] = operationName;
        }
    }
}