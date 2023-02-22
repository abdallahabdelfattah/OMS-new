using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Commons.Framework.Mvc.DataAnnotations
{
    using System.ComponentModel.DataAnnotations;
    using System.Web.Mvc;

    /// <summary>Provides an adapter for the <see cref="T:System.Runtime.CompilerServices.RequiredAttributeAttribute" /> attribute.</summary>
    public class RequiredAttributeAdapter : DataAnnotationsModelValidator<RequiredAttribute>
    {
        /// <summary>Initializes a new instance of the <see cref="T:System.Runtime.CompilerServices.RequiredAttributeAttribute" /> class.</summary>
        /// <param name="metadata">The model metadata.</param>
        /// <param name="context">The controller context.</param>
        /// <param name="attribute">The required attribute.</param>
        public RequiredAttributeAdapter(ModelMetadata metadata, ControllerContext context, RequiredAttribute attribute)
            : base(metadata, context, attribute)
        {
        }
        /// <summary>Gets a list of required-value client validation rules.</summary>
        /// <returns>A list of required-value client validation rules.</returns>
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            var rule = new ModelClientValidationRequiredRule(base.ErrorMessage);
            if (base.Attribute.AllowEmptyStrings)
            {
                //setting "true" rather than bool true which is serialized as "True"
                rule.ValidationParameters["allowempty"] = "true";
            }

            return new ModelClientValidationRequiredRule[] { rule };
        }
    }
}
