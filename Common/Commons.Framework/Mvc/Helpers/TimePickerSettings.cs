// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DatePickerSettings.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Mvc.Helpers
{
    #region Usings

    using System;

    #endregion

    /// <summary>
    ///     The time picker settings.
    /// </summary>
    public class TimePickerSettings : WidgetSettingsBase
    {
        /// <summary>
        /// The compare operator.
        /// </summary>
        public enum CompareOperator
        {
            /// <summary>
            /// The greater than.
            /// </summary>
            GreaterThan = 1,

            /// <summary>
            /// The smaller than.
            /// </summary>
            SmallerThan = 2,

            /// <summary>
            /// The greaer than or equal.
            /// </summary>
            GreaerThanOrEqual = 3,

            /// <summary>
            /// The smaller than or equal.
            /// </summary>
            SmallerThanOrEqual = 4
        }

        /// <summary>
        /// The minutes step.
        /// </summary>
        public enum MinutesStep
        {
            /// <summary>
            /// The five.
            /// </summary>
            Five = 5,

            /// <summary>
            /// The ten.
            /// </summary>
            Ten = 10,

            /// <summary>
            /// The fifteen.
            /// </summary>
            Fifteen = 15,

            /// <summary>
            /// The thirty.
            /// </summary>
            Thirty = 30,

            /// <summary>
            /// The sixty.
            /// </summary>
            Sixty = 60
        }

        /// <summary>
        /// The time format.
        /// </summary>
        public enum TimeFormat
        {
            /// <summary>
            /// The twenty four hour.
            /// </summary>
            TwentyFourHour = 0,

            /// <summary>
            /// The twelve hour.
            /// </summary>
            TwelveHour = 1
        }

        /// <summary>
        /// Gets or sets the compare operator.
        /// </summary>
        public CompareOperator compareOperator { get; set; } = CompareOperator.GreaterThan;

        /// <summary>
        /// Gets or sets the compare to name.
        /// </summary>
        public string CompareToName { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether force round time.
        /// </summary>
        public bool ForceRoundTime { get; set; } = false;

        /// <summary>
        /// Gets or sets a value indicating whether is disabled.
        /// </summary>
        public bool IsDisabled { get; set; } = false;

        /// <summary>
        /// Gets or sets the max time.
        /// </summary>
        public TimeSpan MaxTime { get; set; } = new TimeSpan();

        /// <summary>
        /// Gets or sets the min time.
        /// </summary>
        public TimeSpan MinTime { get; set; } = new TimeSpan();

        /// <summary>
        /// Gets or sets the step minutes.
        /// </summary>
        public MinutesStep StepMinutes { get; set; } = MinutesStep.Thirty;

        /// <summary>
        /// Gets or sets the time picker format.
        /// </summary>
        public TimeFormat TimePickerFormat { get; set; }



    }

}