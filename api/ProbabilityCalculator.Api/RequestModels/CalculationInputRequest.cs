using FluentValidation;

namespace ProbabilityCalculator.Api.RequestModels;

internal class CalculationInputRequest
{
    public double? ProbabilityA { get; set; }
    public double? ProbabilityB { get; set; }
    public string? CalculationType { get; set; }
}

internal class CalculationinputRequestValidator : AbstractValidator<CalculationInputRequest>
{
    public CalculationinputRequestValidator()
    {
        RuleFor(calc => calc.ProbabilityA)
            .NotNull()
            .InclusiveBetween(0, 1);

        RuleFor(calc => calc.ProbabilityB)
            .NotNull()
            .InclusiveBetween(0, 1);

        RuleFor(calc => calc.CalculationType)
            .NotNull()
            .NotEmpty();
    }
}