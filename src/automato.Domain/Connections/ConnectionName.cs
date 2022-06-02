namespace automato.Domain.Connections;

public class ConnectionName : ValueObject
{
    public static ConnectionName FromString(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException("A connection name can not be empty.", nameof(value));
        }

        return new ConnectionName { Value = value };
    }

    public string Value { get; private set; } = string.Empty;

    public static ConnectionName Empty => new();

    public static implicit operator ConnectionName(string value) => FromString(value);
    public static implicit operator string(ConnectionName value) => value.Value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private ConnectionName()
    {
        // Hide default constructor
    }
}
