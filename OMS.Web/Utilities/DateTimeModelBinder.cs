using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OMS.Web.Utilities
{
    public class DateTimeModelBinder : DefaultModelBinder
    {
        private string[] _customFormats = { "dd/MM/yyyy", "yyyy-MM-ddTHH:mm"};

     
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
           // var dt = DateTime.Parse(value.AttemptedValue);
            if (!string.IsNullOrEmpty(value.AttemptedValue))
            {//2016-01-10T18:35:50.466Z
                string s = value.AttemptedValue;
                if (value.AttemptedValue.EndsWith("Z"))
                {
                    s = value.AttemptedValue.Substring(0, value.AttemptedValue.LastIndexOf(':'));
                }
                //2016-01-10T07:07:43.307
                if (s.Contains('.'))
                    s = s.Substring(0, s.LastIndexOf('.'));
                try
                {
                    var date = DateTime.ParseExact(s, _customFormats, CultureInfo.InvariantCulture, DateTimeStyles.None);
                    return date;
                }
                catch { return null; }
            }
            else
                return null;
        }
    }
}