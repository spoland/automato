using automato.Domain.Framework;

namespace automato.Domain.SFTP;

public class SftpServer : IEntity
{
    public static Result<SftpServer> Create(
        int port,
        string name,
        string hostname,
        string username,
        string password)
    {
        List<ValidationException> exceptions = new();

        if (string.IsNullOrWhiteSpace(name))
        {
            exceptions.Add(new ValidationException(
                propertyName: nameof(Name),
                message: "Name must not be null or whitespace."));
        }

        if (string.IsNullOrWhiteSpace(hostname))
        {
            exceptions.Add(new ValidationException(
                propertyName: nameof(Hostname),
                message: "Hostname must not be null or whitespace."));
        }

        if (string.IsNullOrWhiteSpace(username))
        {
            exceptions.Add(item: new ValidationException(
                nameof(Username),
                message: "Username must not be null or whitespace."));
        }

        if (string.IsNullOrWhiteSpace(password))
        {
            exceptions.Add(new ValidationException(
                propertyName: nameof(Password),
                message: "Password must not be null or whitespace."));
        }

        if (port <= 0)
        {
            exceptions.Add(new ValidationException(
                propertyName: nameof(Port),
                message: "Port must be greater than or equal to 0."));
        }

        if (!exceptions.Any())
        {
            return new SftpServer(
                port: port,
                name: name,
                hostname: hostname,
                username: username,
                password: password,
                id: Guid.NewGuid().ToString());
        }

        return exceptions;
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
