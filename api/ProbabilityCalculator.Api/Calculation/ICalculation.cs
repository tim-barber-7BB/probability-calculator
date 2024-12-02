using ProbabilityCalculator.Api.Models;

namespace ProbabilityCalculator.Api.Calculation;

public interface ICalculation
{
    double Calculate(Probability probabilityA, Probability probabilityB);
}
