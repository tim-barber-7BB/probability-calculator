using ProbabilityCalculator.Api.Calculation;
using ProbabilityCalculator.Api.Models;

namespace ProbabilityCalculator.Test.Calculation;
public class CalculationFactoryTests
{
    [Fact]
    public void GetCalculation_WithNoCalculation_Fails()
    {
        // Arrange
        var calculationFactory = new CalculationFactory();
        var unknownCalculationName = "Unknown";
        var expectedErrorMessage = "No calculation for type: Unknown";

        // Act
        var result = calculationFactory.GetCalculation(unknownCalculationName);

        // Assert
        Assert.True(result.IsFailure);
        Assert.Single(result.Errors);
        Assert.Equal(expectedErrorMessage, result.Errors.Single());
    }

    [Fact]
    public void GetCalculation_WithValidCalculation_Succeeds()
    {
        // Arrange
        var calculationFactory = new CalculationFactory();
        var calculationName = CalculationTypes.Either;
        var expectedCalculationType = typeof(EitherCalculation);

        // Act
        var result = calculationFactory.GetCalculation(calculationName);
        var resultType = result.Value;

        // Assert
        Assert.True(result.IsSuccess);
        Assert.IsType<EitherCalculation>(resultType);
    }
}