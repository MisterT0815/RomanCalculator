using Bunit;
using frontend.Components.Pages;
using AngleSharp.Dom;
using frontend.Services;
using Moq;
using Microsoft.Extensions.DependencyInjection;

public class CalculatorComponentTest : TestContext
{
    private readonly Mock<ICalculationService> CalculationServiceMock;

    public CalculatorComponentTest()
    {
        CalculationServiceMock = new Mock<ICalculationService>();
        Services.AddSingleton<ICalculationService>(CalculationServiceMock.Object);
    }

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

        // Assert succeeds
        Assert.Contains("Result:", cut.Markup);
        // Assert Calculation Service is called
        CalculationServiceMock.Verify(m => m.Calculate(It.IsAny<CalculationModel>()), Times.Once);
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
        // Assert no calculation is performed
        CalculationServiceMock.Verify(m => m.Calculate(It.IsAny<CalculationModel>()), Times.Never);
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
        // Assert no calculation is performed
        CalculationServiceMock.Verify(m => m.Calculate(It.IsAny<CalculationModel>()), Times.Never);
    }
}