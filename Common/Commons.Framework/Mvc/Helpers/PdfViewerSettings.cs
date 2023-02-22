// --------------------------------------------------------------------------------------------------------------------
// <copyright file="PdfViewerSettings.cs" company="Usama Nada">
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
    ///     The pdf viewer settings.
    /// </summary>
    public class PdfViewerSettings : WidgetSettingsBase
    {
        /// <summary>
        ///     Gets or sets a value indicating whether enable nav pans.
        /// </summary>
        public bool EnableNavPans { get; set; } = true;

        /// <summary>
        ///     Gets or sets a value indicating whether enable status bar.
        /// </summary>
        public bool EnableStatusBar { get; set; } = true;

        /// <summary>
        ///     Gets or sets a value indicating whether enable toolbar.
        /// </summary>
        public bool EnableToolbar { get; set; } = true;

        /// <summary>
        ///     Gets or sets a value indicating whether force pdf js.
        /// </summary>
        public bool ForcePdfJs { get; set; } = true;

        /// <summary>
        ///     Gets or sets the height.
        /// </summary>
        public int Height { get; set; } = 500;

        /// <summary>
        ///     Gets or sets the page mode.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public PageMode PageMode { get; set; } = PageMode.none;

        /// <summary>
        ///     Gets or sets the page number.
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        ///     Gets or sets the pdf file path.
        /// </summary>
        public string PdfFilePath { get; set; }

        /// <summary>
        ///     Gets or sets the pdf js url.
        /// </summary>
        public string PdfJsUrl { get; set; } = "/Widgets/PdfViewer/js/pdfjs/web/viewer.html";

        /// <summary>
        ///     Gets or sets the search.
        /// </summary>
        public string Search { get; set; } = string.Empty;

        /// <summary>
        ///     Gets or sets the view.
        /// </summary>
        [JsonConverter(typeof(StringEnumConverter))]
        public ViewMode View { get; set; } = ViewMode.FitV;

        /// <summary>
        ///     Gets or sets the width.
        /// </summary>
        public int Width { get; set; }

        /// <summary>
        ///     Gets or sets the zoom.
        /// </summary>
        public int Zoom { get; set; } = 100;
    }

    /// <summary>
    ///     The page mode.
    /// </summary>
    public enum PageMode
    {
        /// <summary>
        ///     The bookmarks.
        /// </summary>
        bookmarks,

        /// <summary>
        ///     The thumbs.
        /// </summary>
        thumbs,

        /// <summary>
        ///     The none.
        /// </summary>
        none
    }

    /// <summary>
    ///     The view mode.
    /// </summary>
    public enum ViewMode
    {
        /// <summary>
        ///     The fit v.
        /// </summary>
        FitV,

        /// <summary>
        ///     The fit h.
        /// </summary>
        FitH,

        /// <summary>
        ///     The fit.
        /// </summary>
        Fit,

        /// <summary>
        ///     The fit b.
        /// </summary>
        FitB,

        /// <summary>
        ///     The fit bh.
        /// </summary>
        FitBH,

        /// <summary>
        ///     The fit bv.
        /// </summary>
        FitBV,

        /// <summary>
        ///     The fit r.
        /// </summary>
        FitR
    }
}