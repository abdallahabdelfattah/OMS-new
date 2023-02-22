// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HtmlHelper.InputExtentions.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

using Commons.Framework.Extensions;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;

namespace Commons.Framework.Mvc.Helpers
{
    #region

    using System;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;

    #endregion


    public static partial class HtmlHelperWidgetExtensions
    {






        public static IHtmlString TimePickerFor<TModel, TProperty>(
    this HtmlHelper<TModel> htmlHelper,
    Expression<Func<TModel, TProperty>> expression,
    TimePickerSettings settings = null,
    object htmlAttributes = null)
        {
            settings = settings ?? new TimePickerSettings();

            var metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            var response = HttpContext.Current.Response;

            response.RegisterWidgetCssInclude("/Widgets/TimePicker/css/jquery.timepicker.min.css");

            response.RegisterWidgetScriptInclude("/Widgets/TimePicker/js/jquery.timepicker.min.js");
            response.RegisterWidgetScriptInclude("/Widgets/TimePicker/TimePickerWidget.js");

            var clientId = "time_picker".AppendRandomString(4, true);
            settings.ContainerId = clientId;

            var fullName =
                htmlHelper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldName(
                    ExpressionHelper.GetExpressionText(expression));

            var inputBuilder = new TagBuilder("input");













            // var className = $"time ui-timepicker-input";
            inputBuilder.AddCssClass("time ui-timepicker-input");

            // inputBuilder.AddCssClass("select2-selection__rendered");

            // MergeClassAttributes(className, context, inputBuilder);
            if (inputBuilder.Attributes.ContainsKey("id"))
            {
                inputBuilder.Attributes["id"] = clientId;
            }
            else
            {
                inputBuilder.Attributes.Add("id", clientId);
            }

            if (!inputBuilder.Attributes.ContainsKey("disabled"))
            {
                if (settings.IsDisabled)
                {
                    inputBuilder.Attributes.Add("disabled", "disabled");
                }
                else
                {
                    inputBuilder.Attributes.Remove("disabled");
                }
            }
            else
            {
                if (settings.IsDisabled)
                {
                    inputBuilder.Attributes["disabled"] = "disabled";
                }
                else
                {
                    inputBuilder.Attributes.Remove("disabled");
                }
            }


            var htmlAttr = htmlAttributes as IDictionary<string, object> ?? HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            inputBuilder.MergeAttributes(htmlAttr);

            inputBuilder.MergeAttribute("data-widget-settings", settings.ToJson());
            inputBuilder.AddCssClass("time-picker-input");
            inputBuilder.MergeAttribute("type", HtmlHelper.GetInputTypeString(InputType.Text));
            inputBuilder.MergeAttribute("name", fullName, true);
            //  inputBuilder.MergeAttribute("value", value);
            inputBuilder.MergeAttribute("autocomplete", "off");
            inputBuilder.GenerateId(fullName);

            // If there are any errors for a named field, we add the css attribute.
            // if (htmlHelper.HasFieldError(fullName))
            // {
            inputBuilder.AddCssClass(HtmlHelper.ValidationInputCssClassName);

            // }
            inputBuilder.MergeAttributes(htmlHelper.GetUnobtrusiveValidationAttributes(fullName, metadata));


            var sb = new StringBuilder();
            using (var stringWriter = new StringWriter())
            {
                //inputBuilder.WriteTo(stringWriter, HtmlEncoder.Default);
                sb.AppendLine("<div class='form-group'>");
                sb.AppendLine("<div class='input-group'>");
                sb.AppendLine(stringWriter.ToString());
                sb.AppendLine("<span class=\"input-group-addon\">");
                sb.AppendLine("<span class=\"glyphicon glyphicon-calendar\"></span>");
                sb.AppendLine("</span>");
                sb.AppendLine("</div>");
                sb.AppendLine(
                    $"<input type='hidden' name='{fullName}' id='{clientId}_setter' />");
                sb.AppendLine($"<span id='{clientId}_IsValid'  class='text-danger' style='display:none;'></span>");
                sb.AppendLine("</div>");
            }



            //var viewMode = htmlHelper.ViewContext.RouteData.Route.GetVirtualPath().Remove(
            //    0,
            //    this.ViewContext.ExecutingFilePath.LastIndexOf('/') + 1);
            // { (viewMode != "Edit.cshtml" && viewMode != "View.cshtml" ? true.ToString().ToLower() : false.ToString().ToLower())},

            var initScript = $@"$(function () {{ 
                        var timePicker_{clientId} = new TimePickerWidget(
                            '{clientId}',{settings.TimePickerFormat.GetHashCode()},
                            {settings.ForceRoundTime.ToString().ToLower()},
                                {settings.StepMinutes.GetHashCode()},
                                '{settings.CompareToName}',
                                {settings.compareOperator.GetHashCode()},
                               
                                '{settings.MinTime.ToString()}',
                                '{settings.MaxTime.ToString()}');
                        timePicker_{clientId}.load(); }});";


            response.RegisterStartupScriptBlock(clientId, initScript);

            return new MvcHtmlString(sb.ToString());
        }

    }


}