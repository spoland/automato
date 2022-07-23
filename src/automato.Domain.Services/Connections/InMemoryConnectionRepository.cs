using automato.Domain.Connections;

namespace automato.Domain.Services.Connections;

public class InMemoryConnectionRepository : IConnectionRepository
{
    private readonly Dictionary<ConnectionId, Connection> _connections = new Dictionary<ConnectionId, Connection>();

    public Task Create(Connection connection, CancellationToken cancellationToken)
    {
        _connections.Add(connection.Id, connection);

        return Task.CompletedTask;
    }

    public Task<Connection> Get(ConnectionId id, CancellationToken cancellationToken)
    {
        return Task.FromResult(_connections[id]);
    }

    public Task<IEnumerable<Connection>> GetAll(CancellationToken cancellationToken)
    {
        return Task.FromResult(_connections.Values.AsEnumerable());
    }
}
