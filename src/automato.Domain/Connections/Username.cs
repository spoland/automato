namespace automato.Domain.Connections;

public class Username : ValueObject
{
    public static Username FromString(string value)
    {
        if (!string.IsNullOrEmpty(value))
        {
            return new Username { Value = value };
        }

        throw new ArgumentException($"A username must be a non empty string value.", nameof(value));
    }

    public string Value { get; private set; } = string.Empty;

    public static Username Empty => new();

    public static implicit operator Username(string value) => FromString(value);
    public static implicit operator string(Username value) => value.Value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private Username()
    {
        // Hide default constructor
    }
}
