namespace automato.Domain.Actions;

public class RemoteDirectory : ValueObject
{
    public static RemoteDirectory FromString(string value)
    {
        if (!Path.IsPathRooted(value))
        {
            throw new ArgumentException($"The remote path must be fully qualified (not relative).", nameof(value));
        }

        return new RemoteDirectory { Value = value };
    }

    public static RemoteDirectory Empty => new() { Value = string.Empty };

    public string Value { get; private set; } = string.Empty;

    public static implicit operator RemoteDirectory(string value) => FromString(value);
    public static implicit operator string(RemoteDirectory value) => value.Value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private RemoteDirectory()
    {
        // Hide default constructor
    }
}
