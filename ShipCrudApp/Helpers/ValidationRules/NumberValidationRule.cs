using System.Globalization;
using System.Windows.Controls;

namespace ShipCrudApp.Helpers.ValidationRules
{
    public class NumberValidationRule : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!(value is double userInput))
            {
                return new ValidationResult(false, "Value must be of type double.");
            }

            if (userInput <= 0)
            {
                return new ValidationResult(false, "The number cannot be less than or equal to 0.");
            }

            return ValidationResult.ValidResult;
        }
    }
}
