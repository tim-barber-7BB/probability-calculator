using ProbabilityCalculator.Api.Models;
using ProbabilityCalculator.Api.Utils;

namespace ProbabilityCalculator.Api.Calculation;

public class CalculationFactory : ICalculationFactory
{
    private readonly Dictionary<string, Type> Calculations = new()
    {
        { CalculationTypes.Either, typeof(EitherCalculation) },
        { CalculationTypes.CombinedWith, typeof(CombinedWithCalculation) }
    };

    public CalculationFactory()
    {
    }

    public Result<ICalculation> GetCalculation(string calculationName)
    {
        if (!Calculations.TryGetValue(calculationName, out var calculationType))
            return Result<ICalculation>.Failure($"No calculation for type: {calculationName}");

        return Result<ICalculation>.Success((ICalculation)Activator.CreateInstance(calculationType)!);
    }
}
