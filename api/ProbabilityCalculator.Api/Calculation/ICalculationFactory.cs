using ProbabilityCalculator.Api.Utils;

namespace ProbabilityCalculator.Api.Calculation;

public interface ICalculationFactory
{
    public Result<ICalculation> GetCalculation(string calculationName);
}
