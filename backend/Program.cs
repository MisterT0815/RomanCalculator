using backend.Models;
using lib;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapPost("/calculate", (Calculation calculation) =>
{
    var resultInt = calculation.calculate();
    var resultRoman = RomanNumeralParser.IntToRoman(resultInt);
    return Results.Ok<string>(resultInt + " or " + resultRoman);
})
.AddEndpointFilter(async (context, next) =>
{
    var calculationArgument = context.GetArgument<Calculation>(0);
    try
    {
        calculationArgument.Validate();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
    return await next(context);
});

app.Run();
