// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Enums.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Notifications
{
    /// <summary>
    ///     The notification type.
    /// </summary>
    public enum NotificationTypes
    {
        /// <summary>
        ///     The email.
        /// </summary>
        Email = 1,

        /// <summary>
        ///     The sms.
        /// </summary>
        SMS = 2,
        
        /// <summary>
        /// The mobile notification
        /// </summary>
        MobileNotification = 4,
    }

    /// <summary>
    ///     The notification send status.
    /// </summary>
    public enum NotificationSendStatus
    {
        /// <summary>
        ///     The not processed.
        /// </summary>
        NotProcessed = 1,

        /// <summary>
        ///     The under processing.
        /// </summary>
        UnderProcessing = 2,

        /// <summary>
        ///     The sent.
        /// </summary>
        Sent = 3,

        /// <summary>
        ///     The failed.
        /// </summary>
        Failed = 4,

        /// <summary>
        ///     The retry threshold exceeded.
        /// </summary>
        RetryThresholdExceeded = 5
    }
}