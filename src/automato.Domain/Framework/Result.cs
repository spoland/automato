namespace automato.Domain.Framework;

public class Result<T> where T : class
{
    public T? Value { get; }

    public bool IsSuccess { get; }

    public IEnumerable<ValidationException>? Exceptions { get; }

    public Result(IEnumerable<ValidationException> exceptions)
    {
        IsSuccess = false;
        Exceptions = exceptions;
    }

    public Result(T value)
    {
        Value = value;
        IsSuccess = true;
    }

    public static implicit operator Result<T>(T value) => new(value);
    public static implicit operator Result<T>(List<ValidationException> exceptions) => new(exceptions);
}
