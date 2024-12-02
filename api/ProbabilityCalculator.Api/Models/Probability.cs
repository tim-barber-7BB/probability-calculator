using ProbabilityCalculator.Api.Utils;

namespace ProbabilityCalculator.Api.Models;

public class Probability
{
    private readonly double _value;

    private Probability(double value)
    {
        _value = value;
    }

    public double Value => _value;

    public static Result<Probability> Create(double value)
    {
        if (value < 0 || value > 1)
            return Result<Probability>.Failure("Probability must be between 0 and 1.");

        return Result<Probability>.Success(new Probability(value));
    }
}
