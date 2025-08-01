using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace BookRental.WebAPI.Utils
{
    public class ValidateRegexAttribute(RegexValidationType type) : ValidationAttribute("{0} is not valid")
    {
        public RegexValidationType RegexType { get; }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            switch (RegexType)
            {
                case RegexValidationType.ISBN:
                    return ValidateISBN(value, validationContext);
                default:
                    throw new Exception("Invalid regex type specified.");
            }
        }

        private ValidationResult ValidateISBN(object? value, ValidationContext validationContext)
        {
            var stringValue = value as string;
            string? regex;
            switch (stringValue.Length)
            {
                case 10:
                    regex = @"^[0-9]{9}[0-9X]$";
                    break;
                case 13:
                    regex = @"^97[89][0-9]{10}$";
                    break;
                default:
                    return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));
            }

            if (value == null || !Regex.IsMatch(stringValue!, regex))
                return new ValidationResult(FormatErrorMessage(validationContext.DisplayName));

            return ValidationResult.Success;
        }
    }
}
