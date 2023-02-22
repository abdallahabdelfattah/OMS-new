// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HtmlHelper.FineUploaderWidget.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Mvc.Helpers
{
    #region

    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Web;
    using System.Web.Mvc;

    using Commons.Framework.Extensions;
    using Commons.Framework.Resources;

    #endregion

    /// <summary>
    /// The html helper widget extensions.
    /// </summary>
    public static partial class HtmlHelperWidgetExtensions
    {
        /// <summary>
        /// The fine uploader for.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="settings">
        /// The settings.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IHtmlString"/>.
        /// </returns>
        public static IHtmlString FineUploaderFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            UploaderSettings settings,
            object htmlAttributes = null)
        {
            settings = settings ?? new UploaderSettings();
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var value = metadata.Model as string;

            var clientId = "fileUploadInput".AppendRandomString(4, true);
            settings.ContainerId = clientId;

            var fullName =
                htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(
                    ExpressionHelper.GetExpressionText(expression));

            var fieldId = settings.FileId;
            if (fieldId.IsNullOrDefault<string>() || fieldId.IsNullOrDefault<int>() || fieldId.IsNullOrDefault<Guid>())
            {
                settings.FileId = null;
            }

            var tagBuilder = new TagBuilder("input");

            var htmlAttr = htmlAttributes as IDictionary<string, object> ?? HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            tagBuilder.MergeAttributes(htmlAttr);
            tagBuilder.MergeAttribute("data-widget-settings", settings.ToJson());
            tagBuilder.MergeAttribute("type", "file");
            tagBuilder.AddCssClass("file-uploader-input");
            tagBuilder.MergeAttribute("name", fullName, true);
            tagBuilder.GenerateId(fullName);

            var validationAttributes = htmlHelper.GetUnobtrusiveValidationAttributes(fullName, metadata);
            if (settings.FileId != null)
            {
                validationAttributes.Remove("data-val-required");

                // var todelete = );
                foreach (var item in validationAttributes.Where(a => a.Key.Contains("data-val-requiredif")).ToList())
                {
                    validationAttributes.Remove(item.Key);
                }
            }

            tagBuilder.MergeAttributes(validationAttributes);
            tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);

            var response = HttpContext.Current.Response;
            response.RegisterWidgetCssInclude("/Widgets/FineUploader/css/fine-uploader-new.min.css");
            response.RegisterWidgetScriptInclude("/Widgets/FineUploader/js/fine-uploader.min.js");
            response.RegisterWidgetScriptInclude("/Widgets/FineUploader/FineUploaderWidget.js");

            var initScript = $@" var fineUploadWidLoader_{clientId} = new FineUploaderWidget({settings.ToJson()});
                    $(function () {{ fineUploadWidLoader_{clientId}.load();}}); ";

            response.RegisterStartupScriptBlock(clientId, initScript);

            var imgMsg = !settings.IsImage
                             ? string.Empty
                             : $@"<p><span class=""fa fa-angle-left"" aria-hidden=""true""></span>{
                                     Messages.MaxImageHeight
                                 }<b><span>{settings.ImageMaxHeight}</span></b> px</p>
                            <p>
                                <span class=""fa fa-angle-left"" aria-hidden=""true""></span>{
                                     Messages.MaxImageWidth
                                 }<b><span>{settings.ImageMaxWidth}</span></b> px
                            </p>";

            var uploaderOptionalMsg = settings.FileId == null
                                          ? string.Empty
                                          : $@"<p><span class=""fa fa-angle-left"" aria-hidden=""true""></span>{
                                                  Messages.UploaderOptionalInEditMode
                                              }</p>";

            var downloadFileLink = settings.FileId == null
                                       ? string.Empty
                                       : $@"<p><span class=""fa fa-angle-left"" aria-hidden=""true""></span><a title=""{
                                               Messages.DownloadFile
                                           }"" href=""{settings.DownloadFileUrl}{settings.FileId}"">{
                                               Messages.DownloadFile
                                           }</a></p>";

            var msgHtml = $@"
                    <p>
                        <span class=""fa fa-angle-left"" aria-hidden=""true""></span>
                                {Messages.AllowedExtensions} <b><span>{settings.AllowedExtensions}</span></b>
                    </p>
                    <p>
                        <span class=""fa fa-angle-left"" aria-hidden=""true""></span>
                                {Messages.MaxFileSize} <b><span>{settings.MaxSizeInMegabytes}</span></b> {Messages.MB}
                     </p>
                    {imgMsg}  
                    {uploaderOptionalMsg}  
                    {downloadFileLink}";

            var html = $@"<div id='{clientId}' class='uploaderInputContainer file-upload-terms'>
                                
<div class='fine-uploader-input'></div>
<script type='text/template' id='qq-template-validation'>
        <div class='qq-uploader-selector qq-uploader' qq-drop-area-text='Drop files here'>
            <div class='qq-total-progress-bar-container-selector qq-total-progress-bar-container'>
                <div role='progressbar' aria-valuenow='0' aria-valuemin='0' aria-valuemax='100' class='qq-total-progress-bar-selector qq-progress-bar qq-total-progress-bar'></div>
            </div>
            <div class='qq-upload-drop-area-selector qq-upload-drop-area' qq-hide-dropzone>
                <span class='qq-upload-drop-area-text-selector'></span>
            </div>
            <div class='qq-upload-button-selector qq-upload-button'>
                <div>{Messages.SelectFiles}</div>
            </div>
            <span class='qq-drop-processing-selector qq-drop-processing'>
                <span>{Messages.ProcessingDroppedFiles}</span>
                <span class='qq-drop-processing-spinner-selector qq-drop-processing-spinner'></span>
            </span>
            <ul class='qq-upload-list-selector qq-upload-list' aria-live='polite' aria-relevant='additions removals'>
                <li>
                    <div class='qq-progress-bar-container-selector'>
                        <div role='progressbar' aria-valuenow='0' aria-valuemin='0' aria-valuemax='100' class='qq-progress-bar-selector qq-progress-bar'></div>
                    </div>
                    <span class='qq-upload-spinner-selector qq-upload-spinner'></span>
                    <img class='qq-thumbnail-selector' qq-max-size='100' qq-server-scale>
                    <span class='qq-upload-file-selector qq-upload-file'></span>
                    <span class='qq-upload-size-selector qq-upload-size'></span>
                    <button type='button' class='qq-btn qq-upload-cancel-selector qq-upload-cancel'>Cancel</button>
                    <button type='button' class='qq-btn qq-upload-retry-selector qq-upload-retry'>Retry</button>
                    <button type='button' class='qq-btn qq-upload-delete-selector qq-upload-delete'>Delete</button>
                    <span role='status' class='qq-upload-status-text-selector qq-upload-status-text'></span>
                </li>
            </ul>

            <dialog class='qq-alert-dialog-selector'>
                <div class='qq-dialog-message-selector'></div>
                <div class='qq-dialog-buttons'>
                    <button type='button' class='qq-cancel-button-selector'>{Messages.Close}</button>
                </div>
            </dialog>

            <dialog class='qq-confirm-dialog-selector'>
                <div class='qq-dialog-message-selector'></div>
                <div class='qq-dialog-buttons'>
                    <button type='button' class='qq-cancel-button-selector'>{Messages.No}</button>
                    <button type='button' class='qq-ok-button-selector'>{Messages.Yes}</button>
                </div>
            </dialog>

            <dialog class='qq-prompt-dialog-selector'>
                <div class='qq-dialog-message-selector'></div>
                <input type='text'>
                <div class='qq-dialog-buttons'>
                    <button type='button' class='qq-cancel-button-selector'>{Messages.Cancel}</button>
                    <button type='button' class='qq-ok-button-selector'>{Messages.Ok}</button>
                </div>
            </dialog>
        </div>
    </script>






                        {msgHtml}
                      </div>";
            return new MvcHtmlString(html); // {tagBuilder}
        }
    }
}