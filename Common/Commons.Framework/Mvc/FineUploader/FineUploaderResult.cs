// --------------------------------------------------------------------------------------------------------------------
// <copyright file="FineUploaderResult.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Mvc.FineUploader
{
    #region

    using System.Web.Mvc;

    using Newtonsoft.Json.Linq;

    #endregion

    /// <summary>
    ///     The fine uploader result.
    /// </summary>
    /// <remarks>
    ///     Docs at https://github.com/Widen/fine-uploader/blob/master/server/readme.md
    /// </remarks>
    public class FineUploaderResult : ActionResult
    {
        /// <summary>
        ///     The response content type.
        /// </summary>
        public const string ResponseContentType = "text/plain";

        /// <summary>
        ///     The _error.
        /// </summary>
        private readonly string error;

        /// <summary>
        ///     The _other data.
        /// </summary>
        private readonly JObject otherData;

        /// <summary>
        ///     The _prevent retry.
        /// </summary>
        private readonly bool? preventRetry;

        /// <summary>
        ///     The _success.
        /// </summary>
        private readonly bool success;

        /// <summary>
        /// Initializes a new instance of the <see cref="FineUploaderResult"/> class.
        /// </summary>
        /// <param name="success">
        /// The success.
        /// </param>
        /// <param name="otherData">
        /// The other data.
        /// </param>
        /// <param name="error">
        /// The error.
        /// </param>
        /// <param name="preventRetry">
        /// The prevent retry.
        /// </param>
        public FineUploaderResult(bool success, object otherData = null, string error = null, bool? preventRetry = null)
        {
            this.success = success;
            this.error = error;
            this.preventRetry = preventRetry;

            if (otherData != null)
            {
                this.otherData = JObject.FromObject(otherData);
            }
        }

        /// <summary>
        ///     The build response.
        /// </summary>
        /// <returns>
        ///     The <see cref="string" />.
        /// </returns>
        public string BuildResponse()
        {
            var response = this.otherData ?? new JObject();
            response["success"] = this.success;

            if (!string.IsNullOrWhiteSpace(this.error))
            {
                response["error"] = this.error;
            }

            if (this.preventRetry.HasValue)
            {
                response["preventRetry"] = this.preventRetry.Value;
            }

            return response.ToString();
        }

        /// <summary>
        /// The execute result.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = ResponseContentType;

            response.Write(this.BuildResponse());
        }
    }
}