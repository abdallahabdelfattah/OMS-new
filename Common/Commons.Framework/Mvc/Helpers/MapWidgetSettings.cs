// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MapWidgetSettings.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Mvc.Helpers
{
    /// <summary>
    ///     The map widget settings.
    /// </summary>
    public class MapWidgetSettings : WidgetSettingsBase
    {
        /// <summary>
        ///     Gets or sets the center latitude.
        /// </summary>
        public string CenterLatitude { get; set; } = "23.8859";

        /// <summary>
        ///     Gets or sets the center longitude.
        /// </summary>
        public string CenterLongitude { get; set; } = "45.0792";

        /// <summary>
        ///     Gets or sets the google map key.
        /// </summary>
        public string GoogleMapKey { get; set; }

        /// <summary>
        ///     Gets or sets the height.
        /// </summary>
        public int Height { get; set; } = 500;

        /// <summary>
        ///     Gets or sets the width.
        /// </summary>
        public int Width { get; set; } = 500;

        /// <summary>
        ///     Gets or sets the zoom level.
        /// </summary>
        public int ZoomLevel { get; set; } = 5;
    }
}