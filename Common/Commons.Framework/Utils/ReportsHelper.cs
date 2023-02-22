// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ReportsHelper.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Utils
{
    #region

    using System.Data;
    using System.Linq;

    using Microsoft.Reporting.WebForms;

    #endregion

    /// <summary>
    /// The reports helper.
    /// </summary>
    public class ReportsHelper
    {
        /// <summary>
        /// The report type.
        /// </summary>
        public enum ReportType
        {
            /// <summary>
            /// The pdf.
            /// </summary>
            PDF,

            /// <summary>
            /// The excel.
            /// </summary>
            Excel,

            /// <summary>
            /// The excelopenxml.
            /// </summary>
            EXCELOPENXML,

            /// <summary>
            /// The image.
            /// </summary>
            IMAGE,

            /// <summary>
            /// The word.
            /// </summary>
            WORD,

            /// <summary>
            /// The wordopenxml.
            /// </summary>
            WORDOPENXML
        }

        /// <summary>
        /// The build report.
        /// </summary>
        /// <param name="dataSetName">
        /// The data set name.
        /// </param>
        /// <param name="reportPath">
        /// The report path.
        /// </param>
        /// <param name="repotType">
        /// The repot type.
        /// </param>
        /// <param name="reportParameters">
        /// The report parameters.
        /// </param>
        /// <param name="reportData">
        /// The report data.
        /// </param>
        /// <returns>
        /// The <see cref="byte[]"/>.
        /// </returns>
        public static byte[] BuildReport(
            string dataSetName,
            string reportPath,
            ReportType repotType,
            ReportParameter[] reportParameters,
            DataTable reportData)
        {
            var rds = new ReportDataSource(dataSetName, reportData);

            // rds.Name = dataSetName; //This refers to the dataset name in the RDLC file
            // rds.Value = reportData;
            var report = new LocalReport { ReportPath = reportPath, EnableExternalImages = true };

            // background image            

            // language, align and direction
            // reportLanguage = !string.IsNullOrEmpty(reportLanguage) ? reportLanguage.ToLower() : "ar";
            // string textDirection = reportLanguage == "ar" ? "RTL" : "LTR";
            // string textAlign = reportLanguage == "ar" ? "Right" : "Left";
            // reportLanguage = reportLanguage == "ar" ? "ar-SA" : "en-US";

            // apply parameters
            if (reportParameters != null && reportParameters.Any())
            {
                report.SetParameters(reportParameters);
            }

            report.DataSources.Clear();
            report.DataSources.Add(rds);
            report.Refresh();
            return report.Render(repotType.ToString());
        }
    }
}