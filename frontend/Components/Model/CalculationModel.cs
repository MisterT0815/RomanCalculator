using System.ComponentModel.DataAnnotations;

public class CalculationModel
{
    [Required(ErrorMessage = "First number is required."), RomanOrNumberParseable]
    public string? FirstNumber { get; set; }
    public string Operation { get; set; } = "+";
    [Required(ErrorMessage = "Second number is required."), RomanOrNumberParseable]
    public string? SecondNumber { get; set; }
}