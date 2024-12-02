using ProbabilityCalculator.Api.Calculation;
using ProbabilityCalculator.Api.Models;

namespace ProbabilityCalculator.Test.Calculation;
public class CombinedWithCalculationTests
{
    [Theory]
    [InlineData(0.5, 0.5, 0.25)]
    [InlineData(0.25, 0.25, 0.0625)]
    [InlineData(0.25, 0.75, 0.1875)]
    [InlineData(1.0, 0.0, 0.0)]
    public void Calculates_Correctly(double probabilityAValue, double probabilityBValue, double expectedResult)
    {
        // Arrange
        var probabilityA = Probability.Create(probabilityAValue).Value;
        var probabilityB = Probability.Create(probabilityBValue).Value;

        var combinedWithCalculation = new CombinedWithCalculation();

        // Act
        var result = combinedWithCalculation.Calculate(probabilityA, probabilityB);

        // Assert
        Assert.Equal(expectedResult, result);
    }
}
