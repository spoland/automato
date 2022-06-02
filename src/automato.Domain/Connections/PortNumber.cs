namespace automato.Domain.Connections;

public class PortNumber : ValueObject
{
    public static PortNumber FromInt(int value)
    {
        if (value > 0)
        {
            return new() { Value = value };
        }

        throw new ArgumentException($"{value} is not a valid port number, port numbers must be greater than 0.");
    }

    public int Value { get; private set; }

    public static PortNumber Empty => new();

    public static implicit operator PortNumber(int value) => FromInt(value);
    public static implicit operator int(PortNumber value) => value.Value;

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    private PortNumber()
    {
        // Hide default constructor
    }
}
