using System.ComponentModel.DataAnnotations;

namespace MomAndBaby.Utilities.Helper
{
    public class PasswordMatchAttribute : ValidationAttribute
    {
        private readonly string _comparisonProperty;

        public PasswordMatchAttribute(string comparisonProperty)
        {
            _comparisonProperty = comparisonProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var currentValue = value as string;
            var property = validationContext.ObjectType.GetProperty(_comparisonProperty);

            if (property == null)
            {
                return new ValidationResult($"Property {_comparisonProperty} not found.");
            }

            var comparisonValue = property.GetValue(validationContext.ObjectInstance) as string;

            if (string.IsNullOrEmpty(currentValue) || string.IsNullOrEmpty(comparisonValue))
            {
                return ValidationResult.Success;
            }

            if (currentValue != comparisonValue)
            {
                return new ValidationResult("The passwords do not match.");
            }

            return ValidationResult.Success;
        }
    }
}
