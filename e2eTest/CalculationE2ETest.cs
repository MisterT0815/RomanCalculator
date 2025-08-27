using System.Reflection;
using Microsoft.Playwright;
using Microsoft.Playwright.Xunit;
using Xunit.Sdk;

namespace e2eTest
{
    [WithTestName]
    public class CalculationE2ETest : PageTest
    {
        public override async Task InitializeAsync()
        {
            await base.InitializeAsync().ConfigureAwait(false);
            await Context.Tracing.StartAsync(new()
            {
                Title = $"{WithTestNameAttribute.CurrentClassName}.{WithTestNameAttribute.CurrentTestName}",
                Screenshots = true,
                Snapshots = true,
                Sources = true
            });
        }

        public override async Task DisposeAsync()
        {
            await Context.Tracing.StopAsync(new()
            {
                Path = Path.Combine(
                    Environment.CurrentDirectory,
                    "playwright-traces",
                $"{WithTestNameAttribute.CurrentClassName}.{WithTestNameAttribute.CurrentTestName}.zip"
                )
            });
            await base.DisposeAsync().ConfigureAwait(false);
        }

        [Theory]
        [InlineData("123", "V", "+", "128")]
        [InlineData("123", "III", "-", "120")]
        public async Task Successfull_Run(string firstNumber, string secondNumber, string operation, string expectedResult)
        {
            await Page.GotoAsync("http://localhost:5156/");

            await Page.GetByText("Calculator").ClickAsync();
            await Page.Locator("input[name=\"calcModel.FirstNumber\"]").FillAsync("123");
            await Page.Locator("input[name=\"calcModel.SecondNumber\"]").FillAsync("V");
            await Page.GetByText("Calculate").PressAsync("Enter");

            await Expect(Page.Locator("text=Result: " + expectedResult)).ToBeVisibleAsync();
        }

        [Theory]
        [InlineData("", "V", "-", "First number is required.")]
        [InlineData("123", "", "-", "Second number is required.")]
        public async Task FailsWithMessage(string firstNumber, string secondNumber, string operation, string expectedMessage)
        {
            await Page.GotoAsync("http://localhost:5156/");

            await Page.GetByText("Calculator").ClickAsync();
            await Page.Locator("input[name=\"calcModel.FirstNumber\"]").FillAsync(firstNumber);
            await Page.Locator("select[name=\"calcModel.Operation\"]").SelectOptionAsync(operation);
            await Page.Locator("input[name=\"calcModel.SecondNumber\"]").FillAsync(secondNumber);
            await Page.GetByText("Calculate").PressAsync("Enter");

            await Expect(Page.Locator($"text={expectedMessage}")).ToBeVisibleAsync();
        }
    }
}

public class WithTestNameAttribute : BeforeAfterTestAttribute
{
    public static string CurrentTestName = string.Empty;
    public static string CurrentClassName = string.Empty;
    private static int counter = 0;

    public override void Before(MethodInfo methodInfo)
    {
        CurrentTestName = methodInfo.Name + counter.ToString();
        CurrentClassName = methodInfo.DeclaringType!.Name;
        counter = counter + 1;
    }

    public override void After(MethodInfo methodInfo)
    {
    }
}