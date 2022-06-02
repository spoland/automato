namespace automato.Domain.Connections;

public class ConnectionId : ValueObject
{
    /// <summary>
    /// Creates a new <see cref="ConnectionId"/> instance.
    /// </summary>
    /// <returns>ConnectionId.</returns>
    public static ConnectionId New() => new() { Value = Guid.NewGuid() };

    internal static ConnectionId Empty => new() { Value = Guid.Empty };

    public static ConnectionId FromGuid(Guid guid)
    {
        if (guid == Guid.Empty)
        {
            throw new ArgumentException("A connection ID cannot be empty.", nameof(guid));
        }

        return new() { Value = guid };
    }

    /// <summary>
    /// Gets the value.
    /// </summary>
    /// <value>The value.</value>
    public Guid Value { get; private set; }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }

    /// <summary>
    /// Prevents a default instance of the <see cref="ConnectionId"/> class from being created.
    /// </summary>
    private ConnectionId()
    {
        // Hide default constructor
    }
}