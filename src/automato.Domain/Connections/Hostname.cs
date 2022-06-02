namespace automato.Domain.Connections;

public class Hostname : ValueObject
{
    public static Hostname FromString(string value)
    {
        if (Uri.CheckHostName(value) != UriHostNameType.Unknown)
        {
            return new Hostname { Value = value };
        }

        throw new ArgumentException($"'{value}' doesn't appear to be a valid hostname/IP address.", nameof(value));
    }

    public string Value { get; private set; } = string.Empty;

    public static Hostname Empty => new();

    public static implicit operator Hostname(string value) => FromString(value);
    public static implicit operator string(Hostname value) => value.Value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private Hostname()
    {
        // Hide default constructor
    }
}
