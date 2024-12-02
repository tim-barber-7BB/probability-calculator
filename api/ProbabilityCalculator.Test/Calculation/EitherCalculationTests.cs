using ProbabilityCalculator.Api.Calculation;
using ProbabilityCalculator.Api.Models;

namespace ProbabilityCalculator.Test.Calculation;
public class EitherCalculationTests
{
    [Theory]
    [InlineData(0.5, 0.5, 0.75)]
    [InlineData(0.25, 0.25, 0.4375)]
    [InlineData(0.25, 0.75, 0.8125)]
    [InlineData(1.0, 0.0, 1.0)]
    public void Calculates_Correctly(double probabilityAValue, double probabilityBValue, double expectedResult)
    {
        // Arrange
        var probabilityA = Probability.Create(probabilityAValue).Value;
        var probabilityB = Probability.Create(probabilityBValue).Value;

        var eitherCalculation = new EitherCalculation();

        // Act
        var result = eitherCalculation.Calculate(probabilityA, probabilityB);

        // Assert
        Assert.Equal(expectedResult, result);
    }
}
