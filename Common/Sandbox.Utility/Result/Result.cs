namespace Sandbox.Utility.Result;

public class Result<T>
{
    private Result(T value)
    {
        IsSuccess = true;
        Value = value;
        Error = Error.None;
    }

    private Result(Error error)
    {
        IsSuccess = false;
        Value = default;
        Error = error;
    }

    public bool IsSuccess { get; }
    public T? Value { get; set; }
    public Error Error { get; set; }

    public static Result<T> Success(T value)
    {
        return new Result<T>(value);
    }

    public static Result<T> Failure(Error error)
    {
        return new Result<T>(error);
    }
}