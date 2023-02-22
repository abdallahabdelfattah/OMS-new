// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CkEditorSettings.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Mvc.Helpers
{
    #region

    using Newtonsoft.Json;
    using Newtonsoft.Json.Converters;

    #endregion

    /// <summary>
    ///     The toolbar mode.
    /// </summary>
    public enum ToolbarMode
    {
        /// <summary>
        ///     The full.
        /// </summary>
        Full,

        /// <summary>
        ///     The basic.
        /// </summary>
        Basic
    }

    /// <summary>
    ///     The ck editor settings.
    /// </summary>
    public class CkEditorSettings : WidgetSettingsBase
    {
        /// <summary>
        ///     Gets or sets the height.
        /// </summary>
        public int Height { get; set; } = 150;

        /// <summary>
        ///     Gets or sets the toolbar mode.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public ToolbarMode ToolbarMode { get; set; } = ToolbarMode.Basic;

        /// <summary>
        ///     Gets or sets the ui color.
        /// </summary>
        public string UiColor { get; set; } = "#9AB8F3";
    }
}