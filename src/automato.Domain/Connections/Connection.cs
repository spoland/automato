namespace automato.Domain.Connections;

/// <summary>
/// Represents a Connection.
/// </summary>
public abstract class Connection : IEntity<ConnectionId>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Connection"/> class.
    /// </summary>
    /// <param name="id">The identifier.</param>
    protected Connection(ConnectionId id, ConnectionName name)
    {
        Id = id;
        Name = name;
    }

    protected Connection()
    {
    }

    public ConnectionId Id { get; protected set; } = ConnectionId.Empty;

    public ConnectionName Name { get; protected set; } = ConnectionName.Empty;
}