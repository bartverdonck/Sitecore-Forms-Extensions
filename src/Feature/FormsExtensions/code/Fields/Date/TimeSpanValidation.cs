using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;
using Sitecore.ExperienceForms.Mvc.Models.Validation;

namespace Feature.FormsExtensions.Fields.Date
{
    public class TimeSpanValidation:ValidationElement<TimeSpanValidationParameters>
    {
        
        private readonly List<string> validUnits = new List<string> {"years", "months", "days"};        

        public TimeSpanValidation(ValidationDataModel validationItem) : base(validationItem)
        {
        }
        
        public override ValidationResult Validate(object value)
        {            
            if (!validUnits.Contains(Parameters.Unit))
                throw new ArgumentException($"The unit parameter must contain one of these values: {string.Join(", ", validUnits)}");


            if (!(value is DateTime dateToValidate))
                return new ValidationResult(FormatMessage());

            return IsValidDate(dateToValidate);            
        }

        public override IEnumerable<ModelClientValidationRule> ClientValidationRules
        {
            get
            {
                var rule = new ModelClientValidationRule
                {
                    ErrorMessage = FormatMessage(),
                    ValidationType = Parameters.ValidationType?? "timespan"
                };

                if (Parameters.MinValue.HasValue)
                    rule.ValidationParameters["min"] = Parameters.MinValue;

                if (Parameters.MaxValue.HasValue)
                    rule.ValidationParameters["max"] = Parameters.MaxValue;

                rule.ValidationParameters["unit"] = Parameters.Unit;

                yield return rule;
            }
        }

        private ValidationResult IsValidDate(DateTime date)
        {            
            var valueToValidate = 0;

            switch (Parameters.Unit)
            {
                case "days":
                    valueToValidate = GetDays(date);
                    break;
                case "months":
                    valueToValidate = GetMonths(date);
                    break;
                case "years":
                    valueToValidate = GetYears(date);
                    break;

            }

            var isValid = !(Parameters.MinValue.HasValue && valueToValidate < Parameters.MinValue.Value);

            if (Parameters.MaxValue.HasValue && valueToValidate > Parameters.MaxValue.Value)
                isValid = false;

            return !isValid
                ? new ValidationResult(FormatMessage()) 
                : ValidationResult.Success;
        }

        private static int GetDays(DateTime date)
        {
            return (int) DateTime.Now.Date.Subtract(date.Date).TotalDays;
        }

        private static int GetMonths(DateTime date)
        {
            var today = DateTime.Now;

            return ((today.Year - date.Year) * 12) + today.Month - date.Month;
        }

        private static int GetYears(DateTime date)
        {
            var today = DateTime.Now;
            var diffYears = today.Year - date.Year;
            if (date.Date > today.AddYears(-diffYears))
                diffYears--;

            return diffYears;
        }
    }
}