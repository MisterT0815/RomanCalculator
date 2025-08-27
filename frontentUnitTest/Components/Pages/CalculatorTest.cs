using Xunit;
using Bunit;
using frontend.Components.Pages;
using AngleSharp.Dom;

public class CalculatorComponentTest : TestContext
{
    [Theory]
    [InlineData("10", "+", "5")]
    [InlineData("20", "-", "10")]
    [InlineData("X", "+", "II")]
    [InlineData("X", "+", "2")]
    public void Calculator_SubmitsValidNumbers_ShowsResult(string firstNumber, string operation, string secondNumber)
    {
        var cut = RenderComponent<Calculator>();

        // Fill in form fields
        cut.Find("input[name='calcModel.FirstNumber']").Change(firstNumber);
        cut.Find("select[name='calcModel.Operation']").Change(operation);
        cut.Find("input[name='calcModel.SecondNumber']").Change(secondNumber);

        // Submit form
        cut.Find("button[type='submit']").Click();

        // Assert suceeds
        Assert.Contains("Result:", cut.Markup);
    }

    [Theory]
    [InlineData("", "+", "5", "First number is required.")]
    [InlineData("10", "+", "", "Second number is required.")]
    [InlineData("VIVqIVIA", "+", "5", "Invalid Roman numeral: \"VIVqIVIA\"")]
    [InlineData("10", "+", "ABC", "Invalid Roman numeral: \"ABC\"")]
    public void Calculator_SubmitsInvalidInput_ShowsValidation(string firstNumber, string operation, string secondNumber, string errorMessage)
    {
        var cut = RenderComponent<Calculator>();

        // Leave FirstNumber empty
        cut.Find("input[name='calcModel.FirstNumber']").Change(firstNumber);
        cut.Find("select[name='calcModel.Operation']").Change(operation);
        cut.Find("input[name='calcModel.SecondNumber']").Change(secondNumber);

        // Submit form
        cut.Find("button[type='submit']").Click();

        // Assert validation message is shown
        Assert.Contains(errorMessage, cut.Find("div[class='text-danger']").TextContent);
    }

    [Theory]
    [InlineData("VIVqIVIA", "+", "5", "Invalid Roman numeral: \"VIVqIVIA\"")]
    [InlineData("10", "+", "ABC", "Invalid Roman numeral: \"ABC\"")]
    public void CalculatorInvalidRomanNumerals_ShowsValidation_WithoutSubmit(string firstNumber, string operation, string secondNumber, string errorMessage)
    {
        var cut = RenderComponent<Calculator>();

        // Fill in form fields with invalid Roman numerals
        cut.Find("input[name='calcModel.FirstNumber']").Change(firstNumber);
        cut.Find("select[name='calcModel.Operation']").Change(operation);
        cut.Find("input[name='calcModel.SecondNumber']").Change(secondNumber);

        // Assert validation message is shown
        Assert.Contains(errorMessage, cut.Find("div[class='text-danger']").TextContent);
    }
}