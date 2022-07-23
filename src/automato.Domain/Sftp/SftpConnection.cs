using automato.Domain.Connections;

namespace automato.Domain.Sftp;

public class SftpConnection : Connection
{
    public SftpConnection(
        ConnectionId id,
        Hostname hostname,
        Username username,
        Password password,
        ConnectionName name,
        PortNumber portNumber,
        ConnectionDetails? details,
        SshHostKeyFingerprint? sshHostKeyFingerprint) : base(id, name, details)
    {
        Details = details;
        Hostname = hostname;
        Username = username;
        Password = password;
        PortNumber = portNumber;
        SshHostKeyFingerprint = sshHostKeyFingerprint;
    }

    public Hostname Hostname { get; private set; } = Hostname.Empty;

    public PortNumber PortNumber { get; private set; } = PortNumber.Empty;

    public Username Username { get; private set; } = Username.Empty;

    public Password Password { get; private set; } = Password.Empty;

    public SshHostKeyFingerprint? SshHostKeyFingerprint { get; private set; }

    private SftpConnection() : base()
    {

    }
}
