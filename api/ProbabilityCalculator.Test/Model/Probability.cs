using ProbabilityCalculator.Api.Models;

namespace ProbabilityCalculator.Test.Models;
public class ProbabilityTests
{
    [Theory]
    [InlineData(1.1)]
    [InlineData(-0.1)]
    public void Create_Probabilities_OutOfRange_Fails(double probability)
    {
        // Arrange
        var expectedErrorMessage = "Probability must be between 0 and 1.";

        // Act
        var result = Probability.Create(probability);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Single(result.Errors);
        Assert.Equal(expectedErrorMessage, result.Errors.Single());
    }

    [Theory]
    [InlineData(0.5)]
    [InlineData(0.0)]
    [InlineData(1.0)]
    public void Create_Probabilities_InRange_Success(double probability)
    {
        // Act
        var result = Probability.Create(probability);

        // Assert
        Assert.True(result.IsSuccess);
        Assert.Empty(result.Errors);
    }
}