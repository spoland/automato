using automato.Domain.Actions;

namespace automato.Domain.Sftp;

public class SftpFileDownloadAction : AutomaterAction
{
    public SftpFileDownloadAction(
        ActionId id,
        ActionName name,
        RemoteDirectory remoteDirectory,
        LocalDirectory localDirectory,
        bool removeDownloaded,
        string fileMask) : base(id, name)
    {
        RemoteDirectory = remoteDirectory;
        LocalDirectory = localDirectory;
        RemoveDownloaded = removeDownloaded;
        FileMask = fileMask;
    }

    /// <summary>
    /// The remote directory that should be searched for files.
    /// </summary>
    /// <value>The remote directory.</value>
    public RemoteDirectory RemoteDirectory { get; private set; } = RemoteDirectory.Empty;

    /// <summary>
    /// The directory that files should be saved to.
    /// </summary>
    /// <value>The local directory.</value>
    public LocalDirectory LocalDirectory { get; private set; } = LocalDirectory.Empty;

    /// <summary>
    /// Gets a value indicating whether [remove downloaded] files from server.
    /// </summary>
    /// <value><c>true</c> if [remove downloaded]; otherwise, <c>false</c>.</value>
    public bool RemoveDownloaded { get; private set; }

    /// <summary>
    /// The file mask to apply when determining what files should be downloaded.
    /// </summary>
    /// <value>The file mask.</value>
    public string? FileMask { get; private set; }

    private SftpFileDownloadAction() : base()
    {
    }
}
