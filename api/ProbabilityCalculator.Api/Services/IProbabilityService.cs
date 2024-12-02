using ProbabilityCalculator.Api.Models;
using ProbabilityCalculator.Api.Utils;

namespace ProbabilityCalculator.Api.Services;

internal interface IProbabilityService
{
    Result<double> Calculate(CalculationInput input);
}