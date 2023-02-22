using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace OMS.Web.Utilities
{
    public class StringModelBinder : DefaultModelBinder
    {
        public override object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
        {
            var value = bindingContext.ValueProvider.GetValue(bindingContext.ModelName);
            if (value == null)
                return null;
            if (!string.IsNullOrEmpty(value.AttemptedValue))
            {
                return value.AttemptedValue.Trim();
               
            }
            else
                return null;
        }
    }
}
