namespace automato.Domain.Connections;

public record ConnectionDetails
{
    public static ConnectionDetails FromString(string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            throw new ArgumentException("A connection name can not be empty.", nameof(value));
        }

        return new ConnectionDetails { Value = value };
    }

    public string Value { get; private set; } = string.Empty;

    public static ConnectionDetails Empty => new();

    public override string ToString() => Value;

    public static implicit operator ConnectionDetails(string value) => FromString(value);
    public static implicit operator string(ConnectionDetails value) => value.Value;

    private ConnectionDetails()
    {
        // Hide default constructor
    }
}
