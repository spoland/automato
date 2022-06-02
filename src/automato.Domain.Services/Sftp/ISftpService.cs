using automato.Domain.Sftp;

namespace automato.Domain.Services.Sftp;

public interface ISftpService
{
    /// <summary>
    /// Downloads the specified files.
    /// </summary>
    /// <param name="remotePaths">Full path to remote directory(s) followed by slash and wildcard to select files or subdirectories to download</param>
    /// <param name="localPath">The local path that files should be downloaded to.</param>
    /// <param name="removeDownloaded">if set to <c>true</c> [remove downloaded] from server.</param>
    /// <returns>Task.</returns>
    public Task DownloadFiles(
        SftpConnection connection,
        SftpFileDownloadAction action,
        CancellationToken cancellationToken);
}
