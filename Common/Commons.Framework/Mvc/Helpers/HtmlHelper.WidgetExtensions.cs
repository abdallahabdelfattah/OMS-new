// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HtmlHelper.WidgetExtensions.cs" company="Usama Nada">
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
    using System.Web.Mvc.Html;
    using System.Xml.Linq;

    using Commons.Framework.Extensions;
    using Commons.Framework.Globalization;
    using Commons.Framework.Resources;

    #endregion

    /// <summary>
    ///     The mvc helpers extensions.
    /// </summary>
    public static partial class HtmlHelperWidgetExtensions
    {

        public static IHtmlString CustomValidationSummary(this HtmlHelper htmlHelper, bool excludePropertyErrors, string message, object htmlAttributes = null)
        {
            var htmlString = htmlHelper.ValidationSummary(excludePropertyErrors, message, htmlAttributes);

            if (htmlString == null)
            {
                return new MvcHtmlString("<div id=\"CustomValidationSummary\"></div>");
            }

            var xEl = XElement.Parse(htmlString.ToHtmlString());
            var xElement = xEl.Element("ul");
            var lis = xElement?.Elements("li").ToList();

            if (lis?.Count == 1 && lis.First().Value == string.Empty)
            {
                return new MvcHtmlString("<div id=\"CustomValidationSummary\"></div>");
            }

            return new MvcHtmlString($"<div id=\"CustomValidationSummary\">{htmlString}</div>");
        }

        /// <summary>
        /// The captcha.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <param name="captchaImgUrl">
        /// The captcha img url.
        /// </param>
        /// <returns>
        /// The <see cref="IHtmlString"/>.
        /// </returns>
        public static IHtmlString CaptchaFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            object htmlAttributes = null,
            string captchaImgUrl = null)
        {
            captchaImgUrl = captchaImgUrl ?? (string.IsNullOrEmpty(CommonsSettings.ApplicationRootUrl) ? "/" : CommonsSettings.ApplicationRootUrl) + "EmbeddedResources/Captcha/Image/";
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var clientId = "captcha".AppendRandomString(4, true);
            var fullName =
                htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(
                    ExpressionHelper.GetExpressionText(expression));

            var tagBuilder = new TagBuilder("input");

            var htmlAttr = htmlAttributes as IDictionary<string, object> ?? HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            tagBuilder.MergeAttributes(htmlAttr);
            tagBuilder.AddCssClass("captcha-input");
            tagBuilder.MergeAttribute("type", HtmlHelper.GetInputTypeString(InputType.Text));
            tagBuilder.MergeAttribute("name", fullName, true);
            tagBuilder.MergeAttribute("autocomplete", "off", true);
            tagBuilder.GenerateId(clientId);
            tagBuilder.AddCssClass("form-control");
            tagBuilder.MergeAttribute("placeholder", string.Empty);
            tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
            tagBuilder.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(fullName, metadata));

            var response = HttpContext.Current.Response;

            response.RegisterWidgetScriptInclude("/Widgets/Captcha/CaptchaWidget.js");

            var initScript = $@" $(function () {{ 
                        var captchaWidLoader_{clientId} = new CaptchaWidget('divCaptchaContainer_{clientId}', '{captchaImgUrl}');
                        captchaWidLoader_{clientId}.load(); 
                    }}); ";

            response.RegisterStartupScriptBlock(clientId, initScript);

            var html = $@"<div id=""divCaptchaContainer_{clientId}"" class=""captcha-box"">
	                <div class=""col-md-6"">
		                <div class=""form-group form-group-lg"">
                            <label class=""control-label"" for=""Captcha"">{Messages.Captcha}</label>
			                {tagBuilder}
		                </div>
		                <div class=""form-group"">
			                <div class=""captcha-img"">
				                <img class=""img-captcha"" src=""{captchaImgUrl}{clientId}"" alt="""">
			                </div>
			                <a href=""#"" class=""captcha-refresh img-captcha-reload""><span class=""fa fa-refresh""></span></a>
		                </div>
{htmlHelper.ValidationMessageFor(expression, string.Empty, new { @class = "text-danger help-block captcha-danger" })}
	                </div>
                </div>";

            return new MvcHtmlString(html);
        }

        /// <summary>
        /// The ck editor for.
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
        public static IHtmlString CkEditorFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            CkEditorSettings settings = null,
            object htmlAttributes = null)
        {
            settings = settings ?? new CkEditorSettings();
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var value = metadata.Model as string;

            var response = HttpContext.Current.Response;

            response.RegisterWidgetScriptInclude("/Widgets/CKEditor/ckeditor.js");
            response.RegisterWidgetScriptInclude("/Widgets/CKEditor/CKEditorWidget.js");

            var clientId = "ckEditorTextArea".AppendRandomString(4, true);
            settings.ContainerId = clientId;

            var fullName =
                htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(
                    ExpressionHelper.GetExpressionText(expression));

            var attributes = htmlAttributes as Dictionary<string, object> ?? new Dictionary<string, object>();

            attributes.Add("data-widget-settings", settings.ToJson());

            if (attributes.ContainsKey("class"))
            {
                attributes["class"] = $"{attributes["class"]} " + "ck-text-area-input";
            }
            else
            {
                attributes.Add("class", "ck-text-area-input");
            }

            var textArea = htmlHelper.TextArea(fullName, attributes);

            var html = $@"<div id=""{clientId}"" class=""ckEditorContainer"">
                    {textArea}                                        
                  </div>";

            var initScript = $@"$(function () {{ 
                        var ckEditorWidLoader_{clientId} = new CKEditorWidget({settings.ToJson()} );
                   }});";

            response.RegisterStartupScriptBlock(clientId, initScript);

            return new MvcHtmlString(html);
        }

        /// <summary>
        /// The client id for.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString ClientIdFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression)
        {
            return MvcHtmlString.Create(
                htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(
                    ExpressionHelper.GetExpressionText(expression)));
        }

        /// <summary>
        /// The date picker for.
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
        public static IHtmlString DatePickerFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            DatePickerSettings settings = null,
            object htmlAttributes = null, bool hijriFlag = true)
        {
            settings = settings ?? new DatePickerSettings();

            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            var valueObj = (DateTime?)metadata.Model ?? DateTime.MinValue;
            var value = string.Empty;
            if (valueObj != DateTime.MinValue)
            {
                value = settings.CalendarType == CalendarType.ummalqura
                            ? valueObj.ToUmAlquraDateString(settings.DateFormat)
                            : valueObj.ToGregorianDateString(settings.DateFormat);
            }

            var response = HttpContext.Current.Response;

            response.RegisterWidgetCssInclude("/Widgets/DatePicker/css/jquery-ui.css");
            response.RegisterWidgetCssInclude("/Widgets/DatePicker/css/jquery.calendars.picker.css");
            response.RegisterWidgetCssInclude("/Widgets/DatePicker/css/ui-hot-sneaks.calendars.picker.css");
            response.RegisterWidgetCssInclude("/Widgets/DatePicker/css/highlight.css");

            response.RegisterWidgetScriptInclude("/Widgets/DatePicker/js/jquery.plugin.js");
            response.RegisterWidgetScriptInclude("/Widgets/DatePicker/js/jquery.calendars.js");
            response.RegisterWidgetScriptInclude("/Widgets/DatePicker/js/jquery.calendars-ar.js");
            response.RegisterWidgetScriptInclude("/Widgets/DatePicker/js/jquery.calendars.ummalqura.js");
            response.RegisterWidgetScriptInclude("/Widgets/DatePicker/js/jquery.calendars.ummalqura-ar.js");
            response.RegisterWidgetScriptInclude("/Widgets/DatePicker/js/jquery.calendars.plus.js");
            response.RegisterWidgetScriptInclude("/Widgets/DatePicker/js/jquery.calendars.picker.js");
            response.RegisterWidgetScriptInclude("/Widgets/DatePicker/js/jquery.calendars.picker-ar.js");
            response.RegisterWidgetScriptInclude("/Widgets/DatePicker/DatePickerWidget.js");

            var clientId = "datePickerInput".AppendRandomString(4, true);
            settings.ContainerId = clientId;

            var fullName =
                htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(
                    ExpressionHelper.GetExpressionText(expression));

            var tagBuilder = new TagBuilder("input");

            var htmlAttr = htmlAttributes as IDictionary<string, object> ?? HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            tagBuilder.MergeAttributes(htmlAttr);

            tagBuilder.MergeAttribute("data-widget-settings", settings.ToJson());
            tagBuilder.AddCssClass("date-picker-input");
            tagBuilder.MergeAttribute("type", HtmlHelper.GetInputTypeString(InputType.Text));
            tagBuilder.MergeAttribute("name", fullName, true);
            tagBuilder.MergeAttribute("value", value);
            tagBuilder.MergeAttribute("autocomplete", "off");
            tagBuilder.GenerateId(fullName);

            // If there are any errors for a named field, we add the css attribute.
            // if (htmlHelper.HasFieldError(fullName))
            // {
            tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);

            // }
            tagBuilder.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(fullName, metadata));
            var html = "";
            if (hijriFlag)
                html = $@"<div id=""{clientId}"" class=""row datePickerContainer"">
                        <div class=""form-group col-xs-9"">
                            {tagBuilder}    
                        </div>                
                        <div class=""form-group col-xs-3"" style=""padding-right: 0;"">
                            <select id=""datePickerSelect_{clientId}"" class=""date-picker-select form-control"">
                                <option value=""gregorian"">{Messages.Gregorian}</option>  
                                <option value=""ummalqura"">{Messages.UmmAlqura}</option>   
                            </select>
                        </div>                
                  </div>";
            else
            {
                html = $@"<div id=""{clientId}"" class=""row datePickerContainer"">
                        <div class=""form-group col-xs-12"">
                            {tagBuilder}    
                        </div>   
                  </div>"; 
            }

            var initScript = $@"$(function () {{ 
                        var datePickerWidLoader_{clientId} = new DatePickerWidget({settings.ToJson()});
                        datePickerWidLoader_{clientId}.load(); }});";

            response.RegisterStartupScriptBlock(clientId, initScript);

            return new MvcHtmlString(html);
        }

        /// <summary>
        /// The download link.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="attachmentId">
        /// The attachment id.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <returns>
        /// The <see cref="IHtmlString"/>.
        /// </returns>
        public static IHtmlString DownloadLink(
            this HtmlHelper htmlHelper,
            object attachmentId,
            object htmlAttributes = null)
        {
            var downloadFileUrl = "/Download/?attId=";

            var clientId = "downloadLink".AppendRandomString(4, true);

            // if (value.IsNullOrDefault<string>() || value.IsNullOrDefault<int>() || value.IsNullOrDefault<Guid>())
            // {               
            // }
            var tagBuilder = new TagBuilder("a");

            var htmlAttr = htmlAttributes as IDictionary<string, object> ?? HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            tagBuilder.MergeAttributes(htmlAttr);

            tagBuilder.MergeAttribute("href", $"{downloadFileUrl}{attachmentId}");
            tagBuilder.MergeAttribute("title", Messages.DownloadFile);
            tagBuilder.MergeAttribute("class", "download-file-link");
            tagBuilder.InnerHtml = Messages.DownloadFile;
            tagBuilder.GenerateId(clientId);

            return new MvcHtmlString(tagBuilder.ToString());
        }

        ///// <summary>
        ///// The download link for.
        ///// </summary>
        ///// <param name="htmlHelper">
        ///// The html helper.
        ///// </param>
        ///// <param name="expression">
        ///// The expression.
        ///// </param>
        ///// <param name="htmlAttributes">
        ///// The html attributes.
        ///// </param>
        ///// <typeparam name="TModel">
        ///// </typeparam>
        ///// <typeparam name="TProperty">
        ///// </typeparam>
        ///// <returns>
        ///// The <see cref="IHtmlString"/>.
        ///// </returns>
        // public static IHtmlString DownloadLinkFor<TModel, TProperty>(
        // this HtmlHelper<TModel> htmlHelper, 
        // Expression<Func<TModel, TProperty>> expression, 
        // object htmlAttributes = null)
        // {
        // var downloadFileUrl = "/Download/?attId=";

        // var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
        // var value = metadata.Model as string;

        // var clientId = "downloadLink" + StringExtensions.RandomString(4);

        // var fullName =
        // htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(
        // ExpressionHelper.GetExpressionText(expression));

        // // if (value.IsNullOrDefault<string>() || value.IsNullOrDefault<int>() || value.IsNullOrDefault<Guid>())
        // // {               
        // // }
        // var tagBuilder = new TagBuilder("a");
        // tagBuilder.MergeAttributes(HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes));

        // tagBuilder.MergeAttribute("href", $"{downloadFileUrl}{value}");
        // tagBuilder.MergeAttribute("title", Messages.DownloadFile);
        // tagBuilder.MergeAttribute("class", "download-file-link");
        // tagBuilder.InnerHtml = Messages.DownloadFile;
        // tagBuilder.GenerateId(fullName);

        // return new MvcHtmlString(tagBuilder.ToString());
        // }

        /// <summary>
        /// The drop down list 2 for.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <param name="optionLabel">
        /// The option label.
        /// </param>
        /// <param name="settings"></param>
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
        public static IHtmlString DropDownList2For<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            IEnumerable<SelectListItem> selectList,
            string optionLabel,
            object htmlAttributes = null)
        {
            var settings = new DropDownWidgetSettings();

            var htmlAttr = htmlAttributes as IDictionary<string, object> ?? HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            var clientId = htmlAttr.ContainsKey("id") ? htmlAttr["id"].ToString() : "dropDownList2".AppendRandomString(4, true);

            if (htmlAttr.ContainsKey("id"))
            {
                htmlAttr["id"] = clientId;
            }
            else
            {
                htmlAttr.Add("id", clientId);
            }

            settings.SelectId = clientId;

            var html = htmlHelper.DropDownListFor(expression, selectList, optionLabel, htmlAttr);

            var response = HttpContext.Current.Response;

            response.RegisterWidgetCssInclude("/Widgets/Dropdown/css/select2.min.css");

            response.RegisterWidgetScriptInclude("/Widgets/Dropdown/js/select2.full.min.js");
            response.RegisterWidgetScriptInclude("/Widgets/Dropdown/DropDownWidget.js");

            var initScript = $@"$(function () {{ 
                        var select2WidLoader_{clientId} = new DropDownWidget({settings.ToJson()});
                        select2WidLoader_{clientId}.load(); 
                    }});";

            response.RegisterStartupScriptBlock(clientId, initScript);

            return new MvcHtmlString($"{html}");
        }

        /// <summary>
        /// The google map input for.
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
        public static IHtmlString GoogleMapInputFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            MapWidgetSettings settings,
            object htmlAttributes = null)
        {
            // CurrentLanguage
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            var value = metadata.Model as string;

            var response = HttpContext.Current.Response;

            response.RegisterWidgetScriptInclude(
                $"https://maps.googleapis.com/maps/api/js?key={settings.GoogleMapKey}&v=3&language={CultureHelper.CurrentLanguage}&dir={CultureHelper.CurrentDirection}&region=SA");
            response.RegisterWidgetScriptInclude("/Widgets/GoogleMap/GoogleMapWidget.js");

            var clientId = "googleMap".AppendRandomString(4, true);
            settings.ContainerId = clientId;

            var fullName =
                htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(
                    ExpressionHelper.GetExpressionText(expression));

            var tagBuilder = new TagBuilder("input");
            var htmlAttr = htmlAttributes as IDictionary<string, object> ?? HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            tagBuilder.MergeAttributes(htmlAttr);
            tagBuilder.MergeAttribute("data-widget-settings", settings.ToJson());
            tagBuilder.AddCssClass("google-map-input");
            tagBuilder.MergeAttribute("type", HtmlHelper.GetInputTypeString(InputType.Text));
            tagBuilder.MergeAttribute("name", fullName, true);
            tagBuilder.MergeAttribute("value", value);
            tagBuilder.GenerateId(fullName);

            // If there are any errors for a named field, we add the css attribute.
            // if (htmlHelper.HasFieldError(fullName))
            // {
            tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);

            // }
            tagBuilder.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(fullName, metadata));

            var html = $@"<div id=""{clientId}"" class=""google-map-widget"">
                    <div width=""{settings.Width}"" height=""{settings.Height}"" style=""width:{
                    settings.Width
                }px;height:{settings.Height}px;"" class=""google-map""></div>
                    {tagBuilder}                    
                  </div>";

            var initScript = $@"google.maps.event.addDomListener(window, 'load', 
                                function(){{ 
                                    var googleMapWidLoader_{clientId} = new GoogleMapWidget({settings.ToJson()}); 
                                    googleMapWidLoader_{clientId}.load();
                                }});";

            response.RegisterStartupScriptBlock(clientId, initScript);

            return new MvcHtmlString(html);
        }

        /// <summary>
        /// The pdf viewer.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="attachmentId">
        /// The attachment id.
        /// </param>
        /// <param name="settings">
        /// The settings.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html attributes.
        /// </param>
        /// <returns>
        /// The <see cref="IHtmlString"/>.
        /// </returns>
        public static IHtmlString PdfViewer(
            this HtmlHelper htmlHelper,
            object attachmentId,
            PdfViewerSettings settings,
            object htmlAttributes = null)
        {
            settings = settings ?? new PdfViewerSettings();

            const string FileUrl = "/Download/?isInline=true&attId=";
            settings.PdfFilePath = settings.PdfFilePath ?? $"{FileUrl}{attachmentId}";

            var clientId = "pdfViewer".AppendRandomString(4, true);
            settings.ContainerId = clientId;

            var tagBuilder = new TagBuilder("div");
            var htmlAttr = htmlAttributes as IDictionary<string, object> ?? HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            tagBuilder.MergeAttributes(htmlAttr);
            tagBuilder.MergeAttribute("class", "pdf-viewer-container embed-responsive");
            tagBuilder.GenerateId(clientId);

            var response = HttpContext.Current.Response;

            response.RegisterWidgetScriptInclude("/Widgets/PdfViewer/js/pdfobject.min.js");
            response.RegisterWidgetScriptInclude("/Widgets/PdfViewer/PdfViewerWidget.js");

            var initScript = $@"$(function () {{ 
                        var pdfViewerWidLoader_{clientId} = new PdfViewerWidget({
                    settings.ToJson()
                });                        
                    }});";

            response.RegisterStartupScriptBlock(clientId, initScript);

            return new MvcHtmlString($"{tagBuilder}");
        }

        /// <summary>
        /// The tel input for.
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
        public static IHtmlString TelInputFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            TelInputSettings settings = null,
            object htmlAttributes = null)
        {
            settings = settings ?? new TelInputSettings();

            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var value = metadata.Model as string;

            if (settings.PreferredCountries != null && settings.PreferredCountries.Contains(","))
            {
                var countries = settings.PreferredCountries.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                    .Distinct().ToList();
                settings.PreferredCountries = countries.ToJson();
            }

            var response = HttpContext.Current.Response;

            response.RegisterWidgetCssInclude("/Widgets/TelInput/css/intlTelInput.css");

            response.RegisterWidgetScriptInclude("/Widgets/TelInput/js/intlTelInput.min.js");
            response.RegisterWidgetScriptInclude("/Widgets/TelInput/js/libphonenumber.utils.js");
            response.RegisterWidgetScriptInclude("/Widgets/TelInput/IntTelInputWidget.js");

            var clientId = "phoneNumInput".AppendRandomString(4, true);
            settings.ContainerId = clientId;

            var fullName =
                htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(
                    ExpressionHelper.GetExpressionText(expression));

            var tagBuilder = new TagBuilder("input");

            var htmlAttr = htmlAttributes as IDictionary<string, object> ?? HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            tagBuilder.MergeAttributes(htmlAttr);
            tagBuilder.MergeAttribute("data-widget-settings", settings.ToJson());
            tagBuilder.MergeAttribute("type", HtmlHelper.GetInputTypeString(InputType.Text));

            // tagBuilder.MergeAttribute("name", fullName, true);
            tagBuilder.MergeAttribute("value", value);

            // In the international telephone network, the format of telephone numbers is standardized by ITU-T recommendation E.164. This code specifies that the entire number should be 15 digits or shorter
            // max length of any international number 15
            tagBuilder.MergeAttribute("maxlength", "15");
            tagBuilder.MergeAttribute("style", "direction:ltr;", false);
            tagBuilder.AddCssClass("phone-number-input");

            // tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);
            tagBuilder.GenerateId(fullName);

            // tagBuilder.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(fullName, metadata));
            var tagBuilderHidden = new TagBuilder("input");
            tagBuilderHidden.MergeAttribute("type", HtmlHelper.GetInputTypeString(InputType.Hidden));
            tagBuilderHidden.MergeAttribute("id", $"hdPhoneNumber_{clientId}", true);
            tagBuilderHidden.MergeAttribute("name", fullName);
            tagBuilderHidden.MergeAttribute("value", value);
            tagBuilderHidden.AddCssClass("do-not-ignore-validation");
            tagBuilderHidden.AddCssClass(HtmlHelper.ValidationInputCssClassName);
            tagBuilderHidden.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(fullName, metadata));

            var css = $@"<style type=""text/css"">
                    .iti-flag {{background - image: url(""{
                    VirtualPathUtility.ToAbsolute("/Widgets/TelInput/img/flags.png")
                }"") !important;}}
                    .intl-tel-input input.iti-invalid-key {{background - color: #FFC7C7;}}
                    .intl-tel-input {{width: 100%;}}
                   </style>";

            var html = $@"<div id=""{clientId}"" class=""phoneInputContainer"" style=""direction:ltr;"">
                    {tagBuilder}                    
                    {tagBuilderHidden}
                  </div>";

            var initScript = $@" var telInputWidLoader_{clientId} = new IntTelInputWidget({settings.ToJson()});
                   $(function () {{ telInputWidLoader_{clientId}.setNumber('{value}'); }}); ";

            response.RegisterStartupScriptBlock(clientId, initScript);

            return new MvcHtmlString(css + html);
        }

        /// <summary>
        /// The uploader input for.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="fileId">
        /// The file Id.
        /// </param>
        /// <param name="downloadFileUrl">
        /// The download File Url.
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
        public static IHtmlString UploaderInputFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            object fileId = null,
            string downloadFileUrl = "/Files/Download/?attId=",
            object htmlAttributes = null)
        {
            // settings = settings ?? new UploaderSettings();
            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var value = metadata.Model as string;

            var clientId = "fileUploadInput".AppendRandomString(4, true);
            var containerId = clientId;

            var fullName =
                htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(
                    ExpressionHelper.GetExpressionText(expression));

            if (fileId.IsNullOrDefault<string>() || fileId.IsNullOrDefault<int>() || fileId.IsNullOrDefault<Guid>())
            {
                fileId = null;
            }

            var tagBuilder = new TagBuilder("input");
            var htmlAttr = htmlAttributes as IDictionary<string, object> ?? HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            tagBuilder.MergeAttributes(htmlAttr);

            // tagBuilder.MergeAttribute(
            // "data-widget-settings",
            // settings.ToJson());
            tagBuilder.MergeAttribute("type", "file");
            tagBuilder.AddCssClass("file-uploader-input");
            tagBuilder.MergeAttribute("name", fullName, true);
            tagBuilder.GenerateId(fullName);

            var validationAttributes = htmlHelper.GetUnobtrusiveValidationAttributes(fullName, metadata);
            if (fileId != null)
            {
                validationAttributes.Remove("data-val-required");
                foreach (var item in validationAttributes.Where(a => a.Key.Contains("data-val-requiredif")).ToList())
                {
                    validationAttributes.Remove(item.Key);
                }
            }

            tagBuilder.MergeAttributes(validationAttributes);
            tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);

            // var safeValidationAttrCollection = validationAttributes
            var isImage = validationAttributes.GetValueOrDefault("data-val-checkisvalidfile-isimage").To<bool>();
            var imageMaxHeight = validationAttributes.GetValueOrDefault("data-val-checkisvalidfile-imagemaxheight")
                .To<int>();
            var imageMaxWidth = validationAttributes.GetValueOrDefault("data-val-checkisvalidfile-imagemaxwidth")
                .To<int>();
            var allowedExtensions = validationAttributes
                .GetValueOrDefault("data-val-checkisvalidfile-allowedextensions").To<string>();
            var maxSizeInMegabytes = validationAttributes
                .GetValueOrDefault("data-val-checkisvalidfile-maxsizeinmegabytes").To<int>();

            var uploaderSettings = new UploaderSettings
            {
                IsImage = isImage,
                ImageMaxWidth = imageMaxWidth,
                ImageMaxHeight = imageMaxHeight,
                MaxSizeInMegabytes = maxSizeInMegabytes,
                AllowedExtensions = allowedExtensions,
                ContainerId = containerId,
                DownloadFileUrl = downloadFileUrl,
                FileId = fileId
            };

            var response = HttpContext.Current.Response;

            response.RegisterWidgetScriptInclude("/Widgets/Uploader/FileUploaderWidget.js");

            var initScript =
                $@" var fileUploadWidLoader_{clientId} = new FileUploaderWidget({uploaderSettings.ToJson()});
                    $(function () {{ fileUploadWidLoader_{clientId}.load();}}); ";

            response.RegisterStartupScriptBlock(clientId, initScript);

            var imgMsg = !isImage
                             ? string.Empty
                             : $@"<p><span class=""icon-angle-arrow-down"" aria-hidden=""true""></span>{
                                     Messages.MaxImageHeight
                                 }<b><span>{imageMaxHeight}</span></b> px</p>
                            <p>
                                <span class=""icon-angle-arrow-down"" aria-hidden=""true""></span>{
                                     Messages.MaxImageWidth
                                 }<b><span>{imageMaxWidth}</span></b> px
                            </p>";

            var uploaderOptionalMsg = fileId == null
                                          ? string.Empty
                                          : $@"<p class=""UploaderNotes"">{Messages.UploaderOptionalInEditMode}</p>";

            var downloadFileLink = fileId == null
                                       ? string.Empty
                                       : $@"<p><a title=""{Messages.DownloadFile}"" href=""{downloadFileUrl}{fileId}"">{
                                               Messages.DownloadFile
                                           }</a></p>";

            var msgHtml = $@"
                    <p>
                        <span class=""icon-angle-arrow-down"" aria-hidden=""true""></span>
                                {Messages.AllowedExtensions} <b><span>{allowedExtensions}</span></b>
                    </p>
                    <p>
                        <span class=""icon-angle-arrow-down"" aria-hidden=""true""></span>
                                {Messages.MaxFileSize} <b><span>{maxSizeInMegabytes}</span></b> {Messages.MB}
                     </p>
                    {imgMsg}  
                    {uploaderOptionalMsg}  
                    {downloadFileLink}";

            // string html =
            // $@"<div id=""{clientId}"" class=""uploaderInputContainer"">
            // {tagBuilder}                    
            // {msgHtml}
            // </div>";
            var html = $@"
                <span class=""icon-help upload-terms-icn""></span>
                {tagBuilder} 
                <div id=""{clientId}"" class=""uploaderInputContainer upload-terms"">
                    {msgHtml}
                </div>
                <div class=""download-file-container"">{uploaderOptionalMsg}{downloadFileLink}</div>";

            return new MvcHtmlString(html);
        }



        public static IHtmlString Captcha(
    this HtmlHelper htmlHelper,object htmlAttributes = null)
        {
            var tagBuilder = new TagBuilder("div");





            var clientId = "captcha".AppendRandomString(4, true);


            var htmlAttr = htmlAttributes as IDictionary<string, object> ?? HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

            tagBuilder.MergeAttributes(htmlAttr);
            tagBuilder.AddCssClass("captcha-input");
            tagBuilder.MergeAttribute("autocomplete", "off", true);
            tagBuilder.GenerateId(clientId);
            tagBuilder.AddCssClass("form-control");
            tagBuilder.MergeAttribute("placeholder", string.Empty);
            tagBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);

            var response = HttpContext.Current.Response;




            var initScript = $@"$(function () {{ 
                                    $('#img-captcha').click(function() {{
                                        var d = new Date();
                                        $('#img-captcha').attr('src', '/Captcha/Index?' + d.getTime());
                                    }});                            
                            }});";


            response.RegisterStartupScriptBlock(clientId, initScript);


            var html = @"<label class=""col-sm-3 col-form-label"">" + Messages.Captcha + @"</label>
                            <div class=""col-sm-9"">
                                <input 
                                        type=""text"" 
                                        name=""CaptchaCode"" 
                                        class=""form-control col-md-8"" 
                                        placeholder =""" + Messages.Captcha + @""" 
                                        autocomplete=""off""
                                        minlength = ""4""
                                        maxlength = ""4""/>
                                <img id=""img-captcha"" src=""/Captcha/Index"" class=""col-md-4"">
                            </div>";


            return new MvcHtmlString(html);

        }

    }
}