using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MiBankWebsiteA2
{
    public class LoginValidator : ValidationAttribute
    {
        public string[] PropertyNames { get; private set; }
        public LoginValidator(params string[] propertyNames) {
            this.PropertyNames = propertyNames;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var properties = this.PropertyNames.Select(validationContext.ObjectType.GetProperty);

            return base.IsValid(value, validationContext);
        }
    }
}
