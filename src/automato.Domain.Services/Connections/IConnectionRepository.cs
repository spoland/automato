using automato.Domain.Connections;

namespace automato.Domain.Services.Connections;

public interface IConnectionRepository
{
    Task<IEnumerable<Connection>> GetAll(CancellationToken cancellationToken);

    Task<Connection> Get(ConnectionId id, CancellationToken cancellationToken);

    Task Create(Connection connection, CancellationToken cancellationToken);
}
