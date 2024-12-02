using FluentValidation;
using ProbabilityCalculator.Api.Calculation;
using ProbabilityCalculator.Api.Models;
using ProbabilityCalculator.Api.RequestModels;
using ProbabilityCalculator.Api.Services;
using Serilog;

namespace ProbabilityCalculator.Api;

internal class Program
{
    private static void Main(string[] args)
    {
        Log.Logger = new LoggerConfiguration()
        .WriteTo.File("log.txt", rollingInterval: RollingInterval.Day)
        .CreateLogger();

        Log.Information("Starting ProbabilityCalculator....");

        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddScoped<IProbabilityService, ProbabilityService>();
        builder.Services.AddScoped<IValidator<CalculationInputRequest>, CalculationinputRequestValidator>();
        builder.Services.AddScoped<ICalculationFactory, CalculationFactory>();

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddCors(options =>
        {
            options.AddDefaultPolicy(
                policy =>
                {
                    policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
        });

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }
        app.UseCors();

        app.MapPost("/api/calculate", (CalculationInputRequest calculationRequest, IValidator<CalculationInputRequest> validator, IProbabilityService calculator) =>
        {
            // Validate Request Model
            var validationResult = validator.Validate(calculationRequest);

            if (!validationResult.IsValid)
                return Results.BadRequest(validationResult.Errors.Select(x => x.ErrorMessage));

            // Create Domain Model
            var probabilityAResult = Probability.Create(calculationRequest.ProbabilityA!.Value);
            if(probabilityAResult.IsFailure)
                return Results.BadRequest(probabilityAResult.Errors);

            var probabilityBResult = Probability.Create(calculationRequest.ProbabilityB!.Value);
            if (probabilityBResult.IsFailure)
                return Results.BadRequest(probabilityBResult.Errors);

            var calculationInput = new CalculationInput(probabilityA: probabilityAResult.Value, probabilityB: probabilityBResult.Value, calculationType: calculationRequest.CalculationType!);
            
            // Calculate Result
            var result = calculator.Calculate(calculationInput);

            if (result.IsSuccess)
                return Results.Json(result.Value);

            return Results.BadRequest(result.Errors);

        })
        .WithName("CalculateProbabilities")
        .WithOpenApi();

        app.Run();
    }
}