using automato.Domain.Framework;

namespace automato.Domain.SFTP;

public class SftpDownloadTask : IEntity
{
    public static Result<SftpDownloadTask> Create(
        string name,
        string localPath,
        string remotePath,
        string sftpServerId)
    {
        List<ValidationException> exceptions = new();

        if (string.IsNullOrWhiteSpace(name))
        {
            exceptions.Add(new ValidationException(
                propertyName: nameof(Name),
                message: $"{nameof(Name)} must not be null or whitespace."));
        }

        if (string.IsNullOrWhiteSpace(localPath))
        {
            exceptions.Add(new ValidationException(
                propertyName: nameof(localPath),
                message: $"{nameof(LocalPath)} must not be null or whitespace."));
        }

        if (string.IsNullOrWhiteSpace(remotePath))
        {
            exceptions.Add(new ValidationException(
                propertyName: nameof(remotePath),
                message: $"{nameof(RemotePath)} must not be null or whitespace."));
        }

        if (string.IsNullOrWhiteSpace(sftpServerId))
        {
            exceptions.Add(new ValidationException(
                propertyName: nameof(sftpServerId),
                message: $"{nameof(SftpServerId)} must not be null or whitespace."));
        }

        if (!exceptions.Any())
        {
            return new SftpDownloadTask(
                name: name,
                localPath: localPath,
                remotePath: remotePath,
                sftpServerId: sftpServerId,
                id: Guid.NewGuid().ToString());
        }

        return exceptions;
    }

    public string Id { get; }

    public string Name { get; }

    public string LocalPath { get; }

    public string RemotePath { get; }

    public string SftpServerId { get; }

    private SftpDownloadTask(
        string id,
        string name,
        string localPath,
        string remotePath,
        string sftpServerId)
    {
        Id = id;
        Name = name;
        LocalPath = localPath;
        RemotePath = remotePath;
        SftpServerId = sftpServerId;
    }
}
