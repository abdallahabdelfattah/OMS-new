using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace OMS.Web.Utilities
{
    public class BooleanModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            // var dt = DateTime.Parse(value.AttemptedValue);
            if (!string.IsNullOrEmpty(value.AttemptedValue))
            {//2016-01-10T18:35:50.466Z
                string s = value.AttemptedValue.Trim();
                if (s.ToLower() == "true" || s.ToLower() == "1")
                    return true;
                else
                    if (s.ToLower() == "false" || s.ToLower() == "0")
                        return false;
                    else
                        return null;
            }
            else
                return null;
        }
    }
}
