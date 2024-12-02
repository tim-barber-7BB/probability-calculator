using ProbabilityCalculator.Api.Models;

namespace ProbabilityCalculator.Api.Calculation;

public class EitherCalculation : ICalculation
{
    public double Calculate(Probability probabilityA, Probability probabilityB)
    {
        return probabilityA.Value + probabilityB.Value - (probabilityA.Value * probabilityB.Value);
    }
}

