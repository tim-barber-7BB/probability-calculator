using ProbabilityCalculator.Api.Calculation;
using ProbabilityCalculator.Api.Models;
using ProbabilityCalculator.Api.Utils;
using Serilog;

namespace ProbabilityCalculator.Api.Services;

public class ProbabilityService : IProbabilityService
{
    private readonly ICalculationFactory _calculationFactory;

    public ProbabilityService(ICalculationFactory calculationFactory)
    {
        _calculationFactory = calculationFactory;
    }

    public Result<double> Calculate(CalculationInput input)
    {
        var getCalculationResult = _calculationFactory.GetCalculation(input.CalculationType);

        if (getCalculationResult.IsFailure)
            return Result<double>.Failure(getCalculationResult.Errors);

        var calculation = getCalculationResult.Value;
        var calculationResult = calculation.Calculate(input.ProbabilityA, input.ProbabilityB);


        // Simple log of the successful calculation details
        var logCalculationDetails = new { Date = DateTime.UtcNow, CalculationInputs = input, Result = calculationResult };
        Log.Information("{@LogCalculationDetails}", logCalculationDetails);

        return Result<double>.Success(calculationResult);
    }
}