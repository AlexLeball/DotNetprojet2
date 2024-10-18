using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.Extensions.Localization;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using P2FixAnAppDotNetCode.Controllers;


namespace P2FixAnAppDotNetCode.Models
{
    // Custom validation attribute for localized required fields
    public class RequiredLocalizedAttribute : RequiredAttribute
    {
        private readonly string _resourceKey;

        // Constructor with parameters for localization
        public RequiredLocalizedAttribute(string resourceKey)
        {
            _resourceKey = resourceKey;
        }
        // Override the IsValid method to check for required fields
        //this is to check if the value is null or empty

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // Get the localizer service from the validation context
            var localizer = (IStringLocalizer<OrderController>)validationContext.GetService(typeof(IStringLocalizer<OrderController>));
            // Check if this logs the expected value
            Console.WriteLine(localizer["ErrorMissingName"]); 

            if (localizer == null)
            {
                throw new InvalidOperationException("IStringLocalizer not found in service provider.");
            }

            if (string.IsNullOrWhiteSpace(value?.ToString()))
            {
                var errorMessage = localizer[_resourceKey].Value;
                return new ValidationResult(errorMessage);
            }

            return ValidationResult.Success;
        }

    }

    public class Order
    {    
        [BindNever]
        public int OrderId { get; set; }

        [BindNever]
        public ICollection<CartLine> Lines { get; set; }

        // Using the custom RequiredLocalized attribute for all required fields
        [RequiredLocalized("ErrorMissingName")]
        public string Name { get; set; }

        [RequiredLocalized("ErrorMissingAddress")]
        public string Address { get; set; }

        [RequiredLocalized("ErrorMissingCity")]
        public string City { get; set; }

        public string Zip { get; set; }

        [RequiredLocalized("ErrorMissingCountry")]
        public string Country { get; set; }

        [BindNever]
        public DateTime Date { get; set; }
    }
}

