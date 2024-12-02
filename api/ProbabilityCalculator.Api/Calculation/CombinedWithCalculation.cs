using ProbabilityCalculator.Api.Models;

namespace ProbabilityCalculator.Api.Calculation;

public class CombinedWithCalculation : ICalculation
{
    public double Calculate(Probability probabilityA, Probability probabilityB)
    {
        return probabilityA.Value * probabilityB.Value;
    }
}
