using automato.Domain.Connections;
using automato.Domain.Sftp;

namespace automato.Domain.Services.Connections;

public class InMemoryConnectionRepository : IConnectionRepository
{
    private readonly Dictionary<ConnectionId, Connection> _connections = new Dictionary<ConnectionId, Connection>()
    {
        {
            ConnectionId.FromGuid(Guid.Parse("0d7b7f38-7345-49c3-8eb8-9855b08ee37d")),
            new SftpConnection(id: ConnectionId.FromGuid(Guid.Parse("0d7b7f38-7345-49c3-8eb8-9855b08ee37d")), hostname: "1234", username: "spoland", password: "password", name: "Rapidseedbox", portNumber: 2222, details: default, sshHostKeyFingerprint: default)
        },
        {
            ConnectionId.FromGuid(Guid.Parse("0d7b7f38-7345-49c3-8eb8-9855b08ee37e")),
            new SftpConnection(id: ConnectionId.FromGuid(Guid.Parse("0d7b7f38-7345-49c3-8eb8-9855b08ee37e")), hostname: "1234", username: "spoland", password: "password", name: "Appserver", portNumber: 2222, details: default, sshHostKeyFingerprint: default)
        }
    };

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
