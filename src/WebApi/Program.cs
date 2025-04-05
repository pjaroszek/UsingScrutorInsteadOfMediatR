using Jaroszek.CoderHouse.ScrutorReplaceMediatRPoC.Application;
using Jaroszek.CoderHouse.ScrutorReplaceMediatRPoC.Application.Common.Interfaces.Mediator;
using Jaroszek.CoderHouse.ScrutorReplaceMediatRPoC.Application.User.Commands;
using Jaroszek.CoderHouse.ScrutorReplaceMediatRPoC.Infrastructure;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddApplication();
builder.Services.AddInfrastructure();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Scrutor Replace MediatR PoC API",
        Version = "v1",
        Description = "API demonstracyjne pokazujące zastąpienie MediatR przy użyciu Scrutora"
    });
});

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Scrutor Replace MediatR PoC API v1");
        // Aby Swagger UI był dostępny na stronie głównej
        c.RoutePrefix = string.Empty;
    });
}


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast =  Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapGroup("/api/users").MapPost("/", async (CreateUserCommand command, IMediator mediator) =>
    {
        await mediator.SendAsync(command);
        return Results.CreatedAtRoute("GetUser", new { id = 1 }, null);
    })
    .WithName("CreateUser")
    .WithOpenApi();

app.Run();

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}
