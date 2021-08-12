using System.Globalization;
using System.Windows.Controls;

namespace ShipCrudApp.Helpers.ValidationRules
{
    public class UserInputValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!(value is string userInput))
            {
                return new ValidationResult(false, "Value must be of type string.");
            }

            if (string.IsNullOrEmpty(userInput.Trim()))
            {
                return new ValidationResult(false, "The field cannot be empty.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
