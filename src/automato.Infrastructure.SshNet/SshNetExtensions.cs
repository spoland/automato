using Renci.SshNet;
using Renci.SshNet.Sftp;

namespace automato.Infrastructure.SshNet;

public static class SshNetExtensions
{
    /// <summary>
    /// Asynchronously retrieve list of files in remote directory
    /// </summary>
    /// <param name="client">The <see cref="SftpClient"/> instance</param>
    /// <param name="path">The path.</param>
    /// <param name="listCallback">The list callback.</param>
    /// <returns>List of directory entries</returns>
    public static Task<IEnumerable<SftpFile>> ListDirectoryAsync(
        this SftpClient client,
        string path,
        Action<int>? listCallback = null)
    {
        return Task<IEnumerable<SftpFile>>.Factory.FromAsync(
            asyncResult: client.BeginListDirectory(path, null, null, listCallback),
            endMethod: client.EndListDirectory);
    }

    public static Task DownloadFileAsync(
        this SftpClient client,
        string path,
        Stream output,
        Action<ulong>? downloadCallback = null)
    {
        return Task.Factory.FromAsync(
            asyncResult: client.BeginDownloadFile(path, output, null, null, downloadCallback),
            endMethod: client.EndDownloadFile);
    }
}
