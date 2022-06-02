using automato.Domain.Connections;
using automato.Domain.Services.Connections;

namespace automato.Infrastructure.LiteDB.Connections;

public class ConnectionRepository : IConnectionRepository
{
    public Task<IEnumerable<Connection>> GetAll(CancellationToken cancellationToken)
    {
        using var db = new LiteDatabase(@"Data/Automato.db");
        var collection = db.GetCollection<Connection>("connections");

        var results = collection.Query().ToEnumerable();

        return Task.FromResult(results);
    }

    public Task<Connection> Get(ConnectionId id, CancellationToken cancellationToken)
    {
        using var db = new LiteDatabase(@"Data/MyData.db");
        var collection = db.GetCollection<Connection>("connections");

        var connection = collection.FindOne(x => x.Id == id);

        return Task.FromResult(connection);
    }

    public Task Create(Connection connection, CancellationToken cancellationToken)
    {
        using var db = new LiteDatabase(@"Data/MyData.db");
        var collection = db.GetCollection<Connection>("connections");

        collection.Insert(connection);

        return Task.CompletedTask;
    }
}
