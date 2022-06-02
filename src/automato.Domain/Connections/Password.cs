namespace automato.Domain.Connections;

public class Password : ValueObject
{
    public static Password FromString(string value)
    {
        return new Password { Value = value };
    }

    public string Value { get; private set; } = string.Empty;

    public static Password Empty => new();

    public static implicit operator Password(string value) => FromString(value);
    public static implicit operator string(Password value) => value.Value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private Password()
    {
        // Hide default constructor
    }
}
