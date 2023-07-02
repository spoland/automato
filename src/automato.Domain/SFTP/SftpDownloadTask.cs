using System.Text.RegularExpressions;
using automato.Domain.Framework;

namespace automato.Domain.SFTP;

public class SftpDownloadTask : Framework.Task
{
    public static Result<SftpDownloadTask> Create(
        string name,
        string localPath,
        string remotePath,
        string sftpServerId,
        bool deleteDownloadedFiles,
        bool deleteEmptyDirectories,
        string? searchPattern = default)
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

        if (string.IsNullOrWhiteSpace(remotePath) || !Path.IsPathRooted(remotePath))
        {
            exceptions.Add(new ValidationException(
                propertyName: nameof(remotePath),
                message: $"{nameof(RemotePath)} must not be null or whitespace and must contain a root."));
        }

        if (string.IsNullOrWhiteSpace(sftpServerId))
        {
            exceptions.Add(new ValidationException(
                propertyName: nameof(sftpServerId),
                message: $"{nameof(SftpServerId)} must not be null or whitespace."));
        }

        // validate search pattern
        if (searchPattern is not null)
        {
            try
            {
                _ = new Regex(searchPattern);
            }
            catch (ArgumentException)
            {
                exceptions.Add(new ValidationException(
                    propertyName: nameof(searchPattern),
                    message: $"{nameof(SearchPattern)} is not a valid regular expression."));
            }
        }

        if (!exceptions.Any())
        {
            return new SftpDownloadTask(
                name: name,
                localPath: localPath,
                remotePath: remotePath,
                sftpServerId: sftpServerId,
                searchPattern: searchPattern,
                id: Guid.NewGuid().ToString(),
                deleteDownloadedFiles: deleteDownloadedFiles,
                deleteEmptyDirectories: deleteEmptyDirectories);
        }

        return exceptions;
    }

    public string LocalPath { get; }

    public string RemotePath { get; }

    public string SftpServerId { get; }

    /// <summary>
    /// A regular expression that can be used to filter files on the remote server.
    /// </summary>
    public string? SearchPattern { get; }

    /// <summary>
    /// Delete files that have been downloaded from the server.
    /// </summary>
    public bool DeleteDownloadedFiles { get; }

    /// <summary>
    /// Deletes empty directories after downloading files.
    /// </summary>
    public bool DeleteEmptyDirectories { get; }

    private SftpDownloadTask(
        string id,
        string name,
        string localPath,
        string remotePath,
        string sftpServerId,
        string? searchPattern,
        bool deleteDownloadedFiles,
        bool deleteEmptyDirectories) : base(id, name)
    {
        LocalPath = localPath;
        RemotePath = remotePath;
        SftpServerId = sftpServerId;
        SearchPattern = searchPattern;
        DeleteDownloadedFiles = deleteDownloadedFiles;
        DeleteEmptyDirectories = deleteEmptyDirectories;
    }
}
