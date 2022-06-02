namespace automato.Domain.Actions;

public class LocalDirectory : ValueObject
{
    public static LocalDirectory FromString(string value)
    {
        if (!Directory.Exists(value))
        {
            throw new ArgumentException($"The specified directory cannot be found.", nameof(value));
        }

        return new LocalDirectory { Value = Path.GetFullPath(value) };
    }

    public static LocalDirectory Empty => new() { Value = string.Empty };

    public string Value { get; private set; } = string.Empty;

    public static implicit operator LocalDirectory(string value) => FromString(value);
    public static implicit operator string(LocalDirectory value) => value.Value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private LocalDirectory()
    {
        // Hide default constructor
    }
}
