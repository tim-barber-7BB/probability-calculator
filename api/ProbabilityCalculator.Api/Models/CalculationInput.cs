namespace ProbabilityCalculator.Api.Models;

public class CalculationInput
{
    public CalculationInput(Probability probabilityA, Probability probabilityB, string calculationType)
    {
        ProbabilityA = probabilityA;
        ProbabilityB = probabilityB;
        CalculationType = calculationType;
    }

    public Probability ProbabilityA { get; }
    public Probability ProbabilityB { get; }
    public string CalculationType { get; }
}
