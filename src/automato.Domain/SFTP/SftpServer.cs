using automato.Domain.Abstractions;

namespace automato.Domain.SFTP;

public class SftpServer : IEntity
{
    public static SftpServer Create(
        int port,
        string name,
        string hostname,
        string username,
        string password)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentNullException(nameof(name), "Name must not be null or whitespace.");
        }

        if (string.IsNullOrWhiteSpace(hostname))
        {
            throw new ArgumentNullException(nameof(hostname), "Hostname must not be null or whitespace.");
        }

        if (string.IsNullOrWhiteSpace(username))
        {
            throw new ArgumentNullException(nameof(username), "Username must not be null or whitespace.");
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            throw new ArgumentNullException(nameof(password), "Password must not be null or whitespace.");
        }

        if (port <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(port), "Port must be greater than or equal to 0.");
        }

        return new SftpServer(
            port: port,
            name: name,
            hostname: hostname,
            username: username,
            password: password,
            id: Guid.NewGuid().ToString());
    }

    public string Id { get; }

    public string Name { get; }

    public string Hostname { get; }

    public string Username { get; }

    public string Password { get; }

    public int Port { get; }

    private SftpServer(string id, string name, string hostname, string username, string password, int port)
    {
        Id = id;
        Port = port;
        Name = name;
        Hostname = hostname;
        Username = username;
        Password = password;
    }
}
