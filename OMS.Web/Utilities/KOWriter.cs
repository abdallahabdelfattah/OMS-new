using OMS.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OMS.Web.Utilities
{
    public class KOWriter
    {
        public static HtmlString WriteTextbox(string value, string additionalKOattributes = null,string PlaceHolder="", string disalbed = "")
        {
            return new HtmlString(string.Format("<input type='text' class='form-control' data-bind='value:{0}{1}' placeholder='{2}'  {3}/>",
                value, additionalKOattributes == null ? "" : additionalKOattributes,PlaceHolder, disalbed));
        }
        public static HtmlString WriteTextbox(string value, int MaxLength)
        {
            return new HtmlString(string.Format("<input type='text' maxlength={1} class='form-control' data-bind='value:{0}' />",
                value, MaxLength));
        }
        public static HtmlString WritePasswordTextbox(string value, string additionalKOattributes = null)
        {
            return new HtmlString(string.Format("<input type='password'  class='form-control' data-bind='value:{0}{1}' />",
                value, additionalKOattributes == null ? "" : additionalKOattributes));
        }
        public static HtmlString WriteLabel(string value, string additionalKOattributes = null)
        {
            return new HtmlString(string.Format("<label type='text'  class='form-control' data-bind='text:{0}{1}' />",
                value, additionalKOattributes == null ? "" : additionalKOattributes));
        }
        public static HtmlString WriteTextArea(string value, string additionalKOattributes = null)
        {
            return new HtmlString(string.Format("<textarea type='text'  class='form-control' rows='8' cols='35' data-bind='value:{0}{1}'></textarea>",
                value, additionalKOattributes == null ? "" : additionalKOattributes));
        }
        public static HtmlString WriteRichTextArea(string value, string additionalKOattributes = null)
        {
            return new HtmlString(string.Format("<rich-editor params='Value: {0}'></rich-editor>",
                value, additionalKOattributes == null ? "" : additionalKOattributes));
        }
        public static HtmlString WriteIntegerTextbox(string value, string additionalKOattributes = null)
        {
            return new HtmlString(string.Format("<input type='number'  class='form-control' min='0' data-bind='numeric,value:{0}{1}' />",
                value, additionalKOattributes == null ? "" : additionalKOattributes));
        }
        public static HtmlString WriteDecimalTextbox(string value, string additionalKOattributes = null)
        {
            return new HtmlString(string.Format("<input type='text' class='form-control NumericOnly' min='0' data-bind='value:{0}{1}' />",
                value, additionalKOattributes == null ? "" : additionalKOattributes));
        }
        //public static HtmlString WriteDecimalTextbox(string value, string additionalKOattributes = null)
        //{
        //    return new HtmlString(string.Format("<input type='text'  class='form-control' data-bind='numeric,value:{0}{1}' />",
        //        value, additionalKOattributes == null ? "" : additionalKOattributes));
        //}
        public static HtmlString WriteCheckbox(string value, string additionalKOattributes = null)
        {
            return new HtmlString(string.Format("<input type='checkbox' id='checkbox1'  data-bind='checked:{0}{1}' />",
                value, additionalKOattributes == null ? "" : additionalKOattributes));
        }

        public static HtmlString WriteRadioButton(string groupName, string additionalKOattributes = null)
        {
            return new HtmlString(string.Format("<input type='radio' name={1}  data-bind='checked:{0}' />",
                groupName, additionalKOattributes == null ? "" : additionalKOattributes));
        }
        public static HtmlString WriteDropDown(string value, string values, string ItemText = "Name", string ItemValue = "Id", string additionalKOattributes = null)
        {
            return new HtmlString(string.Format(@"<select class='form-control m-b' data-bind='value:{0},options:{1},optionsText:""{2}"",optionsValue:""{3}""{4}'></select>", value, values, ItemText, ItemValue, additionalKOattributes == null ? "" : additionalKOattributes));
        }
        public static HtmlString WriteDropDown2(string value, string values, string ItemText = "Name", string ItemValue = "Id", string additionalKOattributes = null)
        {
            return new HtmlString(string.Format(@"<select class='form-control m-b select2' data-bind='value:{0},options:{1},optionsText:""{2}"",optionsValue:""{3}""{4}'></select>", value, values, ItemText, ItemValue, additionalKOattributes == null ? "" : additionalKOattributes));
        }
        public static HtmlString WriteDropDown3(string value, string values,string id, string ItemText = "Name", string ItemValue = "Id", string additionalKOattributes = null)
        {
            return new HtmlString(string.Format(@"<select class='form-control m-b select2' id={0} data-bind='value:{1},options:{2},optionsText:""{3}"",optionsValue:""{4}""{5}'></select>", id, value, values, ItemText, ItemValue, additionalKOattributes == null ? "" : additionalKOattributes));
        }
        public static HtmlString WriteToolbar()
        {
            return new HtmlString($"<div class='pull-right'><div class='doc-buttons'><a href = '#' class='btn btn-success btn-xs toolbarBtn'  data-bind='click:New'>{AppResource.New}</a><a href = '#' class='btn btn-primary btn-xs toolbarBtn' data-bind='click:Save'>{AppResource.Save}</a><a href = '#' class='btn btn-danger btn-xs toolbarBtn' data-bind='visible:Model.Id()>0,click:Delete'>{AppResource.Remove}</a></div></div>");
        }
        public static HtmlString WriteMedicalLensToolbar()
        {
            return new HtmlString($"<div class='pull-right'><div class='doc-buttons'><a href = '#' class='btn btn-success btn-xs toolbarBtn'  data-bind='visible:Model.Id()>0,click:AddLensDtls'>{AppResource.AddLensDetails}</a></div></div>");
        }
        
        public static HtmlString WriteButton(string bindValue,string value)
        {
            return new HtmlString($"<div class='pull-right'><div class='doc-buttons'><a href = '#' class='btn btn-primary btn-xs toolbarBtn' data-bind='click:{bindValue}'>{value}</a></div></div>");
        }
        public static HtmlString WriteButton()
        {
            return new HtmlString("<div class='pull-right'><div class='doc-buttons'><a href = '#' class='btn btn-primary btn-xs toolbarBtn' data-bind='click:Update'>Update</a></div></div>");
        }
        public static HtmlString WriteErrorMessage()
        {
            return new HtmlString(@"<div id='errorDiv' class='alert alert-danger' style ='display: none' data-bind='style:{ display:Errors().length > 0 ? ""block"" : ""none"" }'><ul data-bind='foreach:Errors'><li><i class='fa fa-ban-circle'></i><span data-bind='text:$data'></span> </li></ul></div>");
        }
        public static HtmlString Writeuploder(string callbackfunction, string url, string fileType = "")
        {
            return new HtmlString("<form enctype = 'multipart/form-data'><input class='upload' id='upload' multiple  name='upload' type='file' data-bind='fileUpload: { callback:" + callbackfunction + ", property:\"filename\", url: \"" + url + "\", fileType: \"" + fileType + "\" }' /></form>");
        }
        public static HtmlString Writeuploder2(string callbackfunction, string url, string fileType = "")
        {
            return new HtmlString("<input id='upload' multiple  name='upload' type='file' data-bind='fileUpload: { callback:" + callbackfunction + ", property:\"filename\", url: \"" + url + "\", fileType: \"" + fileType + "\" }' />");
        }
        public static HtmlString WriteDatePicker(string value, string additionalKOattributes = null, string placeholder = "Select Date")
        {
            return new HtmlString(string.Format("<div class='input-group date'><div class='input-group-addon'><i class='fa fa-calendar'></i></div><input type='text' class='form-control pull-right datepicker2' data-bind='value:{0}{1}' /></div>",
                value, additionalKOattributes == null ? "" : additionalKOattributes));


            //  return new HtmlString(string.Format("<input type='text'  class='form-control' data-bind='datepicker:{0},datepickerOptions: { changeMonth: true, changeYear: true, buttonImageOnly: true ,dateFormat: 'dd/MM/yy', minDate: new Date(), buttonImage: 'images/calendar.gif',buttonImageOnly: true }' />",
        }
        public static HtmlString WriteStartDatePicker(string value, string additionalKOattributes = null, string startDate = null)
        {
            if (startDate != null)
            {
                return new HtmlString(string.Format("<input type='text'  class='form-control' data-bind='Value:{0},datepicker:{1},startDate:{0}' />",
                    value, additionalKOattributes == null ? "" : additionalKOattributes, startDate));
            }
            else
            {
                return new HtmlString(string.Format("<input type='text'  class='form-control' data-bind='Value:{0},datepicker:{1}' />",
                value, additionalKOattributes == null ? "" : additionalKOattributes));
            }

            //  return new HtmlString(string.Format("<input type='text'  class='form-control' data-bind='datepicker:{0},datepickerOptions: { changeMonth: true, changeYear: true, buttonImageOnly: true ,dateFormat: 'dd/MM/yy', minDate: new Date(), buttonImage: 'images/calendar.gif',buttonImageOnly: true }' />",

        }

    }
}