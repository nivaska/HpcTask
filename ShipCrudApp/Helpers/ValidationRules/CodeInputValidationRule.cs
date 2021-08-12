using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace ShipCrudApp.Helpers.ValidationRules
{
    public class CodeInputValidationRule: ValidationRule
    {
        private string codeRegexPattern = @"^[A-Za-z]{4}-\d{4}-\w{1}\d{1}$";
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (!(value is string userInput))
            {
                return new ValidationResult(false, "Value must be of type string.");
            }

            if (!Regex.Match(userInput, codeRegexPattern, RegexOptions.IgnoreCase).Success)
            {
                return new ValidationResult(false, "The value for code should be in the format AAAA-1111-A1");
            }

            return ValidationResult.ValidResult;
        }
    }
}

