using System.ComponentModel.DataAnnotations;
using lib;

public class RomanOrNumberParseable : ValidationAttribute
{
    public RomanOrNumberParseable() : base("Must be either a number or a Roman numeral.") { }

    protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
            return new ValidationResult("Cannot be empty.", [validationContext.MemberName]);
        if (value is int || int.TryParse(value?.ToString(), out _))
            return ValidationResult.Success;
        if (value is string strValue)
        {
            try
            {
                RomanNumeralParser.RomanToInt(strValue);
                return ValidationResult.Success;
            }
            catch (ArgumentException ex)
            {
                return new ValidationResult(ex.Message, [validationContext.MemberName]);
            }
        }
        return new ValidationResult("Must be either a number or a Roman numeral.", [validationContext.MemberName]);
    }

}