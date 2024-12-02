namespace ProbabilityCalculator.Api.Utils;

public class Result<TSuccess>
{
    protected readonly bool _isSuccess;
    private readonly TSuccess? _value;
    protected readonly List<string> _errors = [];

    private Result(TSuccess value)
    {
        _isSuccess = true;
        _value = value;
    }

    private Result(string error)
    {
        _isSuccess = false;
        _errors.Add(error);
    }

    private Result(List<string> errors)
    {
        _isSuccess = false;
        _errors.AddRange(errors);
    }

    public bool IsSuccess => _isSuccess;

    public bool IsFailure => !_isSuccess;

    public List<string> Errors => _errors;

    public TSuccess Value => _value!;

    public static Result<TSuccess> Success(TSuccess value)
    {
        return new Result<TSuccess>(value);
    }

    public static Result<TSuccess> Failure(List<string> errors)
    {
        return new Result<TSuccess>(errors);
    }

    public static Result<TSuccess> Failure(string error)
    {
        return new Result<TSuccess>(error);
    }
}