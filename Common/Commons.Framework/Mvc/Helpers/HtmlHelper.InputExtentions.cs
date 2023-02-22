// --------------------------------------------------------------------------------------------------------------------
// <copyright file="HtmlHelper.InputExtentions.cs" company="Usama Nada">
//   No Copy Rights. Free To Use and Share. Enjoy
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Commons.Framework.Mvc.Helpers
{
    #region

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Text;
    using System.Web;
    using System.Web.Mvc;
    using System.Web.Mvc.Html;
    using System.Web.Routing;

    #endregion

    /// <summary>
    ///     The html helper extensions.
    /// </summary>
    public static class HtmlHelperExtensions
    {
        /// <summary>
        /// Returns a checkbox for each of the provided <paramref name="items"/>.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html Helper.
        /// </param>
        /// <param name="listName">
        /// The list Name.
        /// </param>
        /// <param name="items">
        /// The items.
        /// </param>
        /// <param name="direction">
        /// The direction.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html Attributes.
        /// </param>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString CheckBoxList(
            this HtmlHelper htmlHelper,
            string listName,
            IEnumerable<SelectListItem> items,
            RepeatDirection direction,
            object htmlAttributes = null)
        {
            var htmlAttr = htmlAttributes as IDictionary<string, object> ?? HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
            var container = new TagBuilder("div");
            container.MergeAttributes(htmlAttr);

            foreach (var item in items)
            {
                var label = new TagBuilder("label");

                // commented by Ahmed Bilall
                // label.MergeAttribute(
                // "class", 
                // "checkbox" + (direction == RepeatDirection.Horizonal ? " inline" : string.Empty));
                // ======================================================================================
                // default class
                label.MergeAttributes(new RouteValueDictionary(htmlAttributes), true);
                label.AddCssClass("checkbox-inline");

                var cb = new TagBuilder("input");
                cb.MergeAttribute("type", "checkbox");

                var chkHtmlAttr = htmlAttributes as IDictionary<string, object> ?? HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);

                cb.MergeAttributes(chkHtmlAttr);
                cb.MergeAttribute("name", listName);
                cb.MergeAttribute("value", item.Value ?? item.Text);

                if (item.Selected)
                {
                    cb.MergeAttribute("checked", "checked");
                }

                label.InnerHtml = cb.ToString(TagRenderMode.SelfClosing) + item.Text;

                container.InnerHtml += label.ToString();
            }

            return new MvcHtmlString(container.ToString());
        }

        /// <summary>
        /// Returns a checkbox for each of the provided <paramref name="items"/>.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html Helper.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="items">
        /// The items.
        /// </param>
        /// <param name="direction">
        /// The direction.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html Attributes.
        /// </param>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString CheckBoxListFor<TModel, TValue>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TValue>> expression,
            IEnumerable<SelectListItem> items,
            RepeatDirection direction,
            object htmlAttributes = null)
        {
            var listName = ExpressionHelper.GetExpressionText(expression);
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);

            items = GetCheckboxListWithDefaultValues(metaData.Model, items);
            return htmlHelper.CheckBoxList(listName, items, direction, htmlAttributes);
        }

        /// <summary>
        /// Returns a description for this model property
        /// </summary>
        /// <param name="html">
        /// The html.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <returns>
        /// The <see cref="IHtmlString"/>.
        /// </returns>
        public static IHtmlString DescriptionFor<TModel, TValue>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TValue>> expression)
        {
            var metadata = ModelMetadata.FromLambdaExpression(expression, html.ViewData);
            return new HtmlString(metadata.Description);
        }

        /// <summary>
        /// Creates a checkbox list for an Enum property
        /// </summary>
        /// <param name="html">
        /// The html.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="direction">
        /// The direction.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html Attributes.
        /// </param>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString EnumCheckBoxListFor<TModel, TEnum>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, IEnumerable<TEnum>>> expression,
            RepeatDirection direction,
            object htmlAttributes = null)
        {
            return html.CheckBoxListFor(expression, GetEnumSelectList<TEnum>(), direction, htmlAttributes);
        }

        /// <summary>
        /// Creates a dropdown list for an Enum property
        /// </summary>
        /// <param name="html">
        /// The html.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="emptyItemText">
        /// The empty Item Text.
        /// </param>
        /// <param name="emptyItemValue">
        /// The empty Item Value.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html Attributes.
        /// </param>
        /// <exception cref="System.ArgumentException">
        /// If the property type is not a valid Enum
        /// </exception>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString EnumDropDownListFor<TModel, TEnum>(
            this HtmlHelper<TModel> html,
            Expression<Func<TModel, TEnum>> expression,
            string emptyItemText = "",
            string emptyItemValue = "",
            object htmlAttributes = null)
        {
            return html.DropDownListFor(
                expression,
                GetEnumSelectList<TEnum>(true, emptyItemText, emptyItemValue),
                htmlAttributes);
        }

        /// <summary>
        /// Returns an image element for the specified <paramref name="src"/>
        /// </summary>
        /// <param name="html">
        /// The html.
        /// </param>
        /// <param name="src">
        /// The src.
        /// </param>
        /// <param name="alt">
        /// The alt.
        /// </param>
        /// <param name="htmlAttributes">
        /// The html Attributes.
        /// </param>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString Image(
            this HtmlHelper html,
            string src,
            string alt = "",
            object htmlAttributes = null)
        {
            if (src.StartsWith("~"))
            {
                src = VirtualPathUtility.ToAbsolute(src);
            }

            var tb = new TagBuilder("img");
            tb.MergeAttribute("src", src);
            tb.MergeAttribute("alt", alt);
            tb.MergeAttributes(new RouteValueDictionary(htmlAttributes));
            return MvcHtmlString.Create(tb.ToString(TagRenderMode.SelfClosing));
        }

        /// <summary>
        /// The radio button list for.
        /// </summary>
        /// <param name="htmlHelper">
        /// The html helper.
        /// </param>
        /// <param name="expression">
        /// The expression.
        /// </param>
        /// <param name="listOfValues">
        /// The list of values.
        /// </param>
        /// <typeparam name="TModel">
        /// </typeparam>
        /// <typeparam name="TProperty">
        /// </typeparam>
        /// <returns>
        /// The <see cref="MvcHtmlString"/>.
        /// </returns>
        public static MvcHtmlString RadioButtonListFor<TModel, TProperty>(
            this HtmlHelper<TModel> htmlHelper,
            Expression<Func<TModel, TProperty>> expression,
            IEnumerable<SelectListItem> listOfValues)
        {
            var metaData = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
            var sb = new StringBuilder();

            if (listOfValues != null)
            {
                foreach (var item in listOfValues)
                {
                    var id = $"{metaData.PropertyName}_{item.Value}";

                    var radio = htmlHelper.RadioButtonFor(expression, item.Value, new { id }).ToHtmlString();
                    sb.AppendFormat(
                        "<label class=\"radio-inline\" for=\"{0}\">{2} {1}</label> ",
                        id,
                        HttpUtility.HtmlEncode(item.Text),
                        radio);
                }
            }

            return MvcHtmlString.Create(sb.ToString());
        }

        /// <summary>
        /// The get checkbox list with default values.
        /// </summary>
        /// <param name="defaultValues">
        /// The default values.
        /// </param>
        /// <param name="selectList">
        /// The select list.
        /// </param>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        private static IEnumerable<SelectListItem> GetCheckboxListWithDefaultValues(
            object defaultValues,
            IEnumerable<SelectListItem> selectList)
        {
            var defaultValuesList = defaultValues as IEnumerable;

            if (defaultValuesList == null)
            {
                return selectList;
            }

            var values = from object value in defaultValuesList
                         select Convert.ToString(value, CultureInfo.CurrentCulture);

            var selectedValues = new HashSet<string>(values, StringComparer.OrdinalIgnoreCase);
            var newSelectList = new List<SelectListItem>();

            foreach (var selectListItem in selectList)
            {
                selectListItem.Selected = selectListItem.Value != null
                                              ? selectedValues.Contains(selectListItem.Value)
                                              : selectedValues.Contains(selectListItem.Text);

                newSelectList.Add(selectListItem);
            }

            return newSelectList;
        }

        ///// <summary>
        ///// Renders an Alert if one exists in TempData (requires a partial view named _Alert)
        ///// </summary>
        // public static MvcHtmlString Alert(this HtmlHelper html)
        // {
        // var alert = html.ViewContext.TempData[typeof(Alert).FullName] as Alert;
        // if (alert != null)
        // return html.Partial("_Alert", alert);

        // return MvcHtmlString.Empty;
        // }

        /// <summary>
        /// The get enum select list.
        /// </summary>
        /// <param name="addEmptyItemForNullable">
        /// The add empty item for nullable.
        /// </param>
        /// <param name="emptyItemText">
        /// The empty item text.
        /// </param>
        /// <param name="emptyItemValue">
        /// The empty item value.
        /// </param>
        /// <typeparam name="TEnum">
        /// </typeparam>
        /// <returns>
        /// The <see cref="IEnumerable"/>.
        /// </returns>
        /// <exception cref="ArgumentException">
        /// </exception>
        private static IEnumerable<SelectListItem> GetEnumSelectList<TEnum>(
            bool addEmptyItemForNullable = false,
            string emptyItemText = "",
            string emptyItemValue = "")
        {
            var enumType = typeof(TEnum);
            var nullable = false;

            if (!enumType.IsEnum)
            {
                enumType = Nullable.GetUnderlyingType(enumType);

                if (enumType != null && enumType.IsEnum)
                {
                    nullable = true;
                }
                else
                {
                    throw new ArgumentException("Not a valid Enum type");
                }
            }

            var selectListItems = (from v in Enum.GetValues(enumType).Cast<TEnum>()
                                   select new SelectListItem
                                              {
                                                  Text = v.ToString(), // .SeparatePascalCase(),
                                                  Value = v.ToString()
                                              }).ToList();

            if (nullable && addEmptyItemForNullable)
            {
                selectListItems.Insert(0, new SelectListItem { Text = emptyItemText, Value = emptyItemValue });
            }

            return selectListItems;
        }
    }

    /// <summary>
    ///     The repeat direction.
    /// </summary>
    public enum RepeatDirection
    {
        /// <summary>
        ///     The horizonal.
        /// </summary>
        Horizonal,

        /// <summary>
        ///     The vertical.
        /// </summary>
        Vertical
    }
}