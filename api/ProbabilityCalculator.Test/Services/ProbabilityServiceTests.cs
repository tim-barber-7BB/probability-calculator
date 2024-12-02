using Moq;
using ProbabilityCalculator.Api.Calculation;
using ProbabilityCalculator.Api.Models;
using ProbabilityCalculator.Api.Services;
using ProbabilityCalculator.Api.Utils;

namespace ProbabilityCalculator.Test.Services;
public class ProbabilityServiceTests
{
    [Fact]
    public void Calculate_CallsFactory_AndCalculator()
    {
        // Arrange
        var calculationName = "Test";
        var probabilityA = Probability.Create(0.25).Value;
        var probabilityB = Probability.Create(0.5).Value;
        var calculationInput = new CalculationInput(probabilityA, probabilityB, calculationName);

        var mockCalculation = new Mock<ICalculation>();
        var calculationResult = 0.55;
        mockCalculation.Setup(calc => calc.Calculate(probabilityA, probabilityB)).Returns(calculationResult);
        var fakeResult = Result<ICalculation>.Success(mockCalculation.Object);

        var mockFactory = new Mock<ICalculationFactory>();
        mockFactory.Setup(factory => factory.GetCalculation(calculationName)).Returns(fakeResult);

        var probabilityService = new ProbabilityService(mockFactory.Object);

        // Act
        var result = probabilityService.Calculate(calculationInput);

        // Assert
        mockFactory.Verify(factory => factory.GetCalculation(calculationName), Times.Once());
        Assert.True(result.IsSuccess);
        Assert.Equal(calculationResult, result.Value);
    }

    [Fact]
    public void Calculate_CallsFactory_AndPassesError()
    {
        // Arrange
        var calculationName = "Test";
        var probabilityA = Probability.Create(0.25).Value;
        var probabilityB = Probability.Create(0.5).Value;
        var calculationInput = new CalculationInput(probabilityA, probabilityB, calculationName);

        var testFailureMessage = "Test Failure";
        var fakeResult = Result<ICalculation>.Failure(testFailureMessage);

        var mockFactory = new Mock<ICalculationFactory>();
        mockFactory.Setup(factory => factory.GetCalculation(calculationName)).Returns(fakeResult);

        var probabilityService = new ProbabilityService(mockFactory.Object);

        // Act
        var result = probabilityService.Calculate(calculationInput);

        // Assert
        mockFactory.Verify(factory => factory.GetCalculation(calculationName), Times.Once());
        Assert.True(result.IsFailure);
        Assert.Equal(testFailureMessage, result.Errors.Single());
    }
}