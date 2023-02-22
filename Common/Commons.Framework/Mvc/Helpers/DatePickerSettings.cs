// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DatePickerSettings.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Mvc.Helpers
{
    #region Usings

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;
    using System;

    #endregion

    /// <summary>
    ///     The date picker settings.
    /// </summary>
    public class DatePickerSettings : WidgetSettingsBase
    {
        /// <summary>
        ///     Gets or sets the calendar type.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public CalendarType CalendarType { get; set; } = CalendarType.gregorian;

        /// <summary>
        ///     Gets or sets the date format.
        /// </summary>
        public string DateFormat { get; set; } = "dd/MM/yyyy";

        /// <summary>
        ///     Gets or sets the picker year range.
        /// </summary>
        public string PickerYearRange { get; set; } = "c-100:c+50";

        /// <summary>
        /// Gets or sets the range type.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public PickerRangeType? RangeType { get; set; }

        /// <summary>
        /// Gets or sets the max date.
        /// </summary>
        private DateTime? MaxDate { get; set; }

        /// <summary>
        /// Gets or sets the min date.
        /// </summary>
        private DateTime? MinDate { get; set; }
    }

    /// <summary>
    ///     The calendar type.
    /// </summary>
    public enum CalendarType
    {
        /// <summary>
        ///     The ummalqura.
        /// </summary>
        ummalqura,

        /// <summary>
        ///     The gregorian.
        /// </summary>
        gregorian
    }

    /// <summary>
    /// The picker range type.
    /// </summary>
    public enum PickerRangeType
    {
        /// <summary>
        /// The future only.
        /// </summary>
        futureOnly,

        /// <summary>
        /// The future including today.
        /// </summary>
        futureIncludingToday,

        /// <summary>
        /// The past only.
        /// </summary>
        pastOnly,

        /// <summary>
        /// The past including today.
        /// </summary>
        pastIncludingToday,

        /// <summary>
        /// The specific dates.
        /// </summary>
        specificDates
    }
}