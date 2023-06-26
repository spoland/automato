using automato.Domain.Services.SFTP;
using automato.Domain.SFTP;
using Microsoft.Extensions.Logging;
using Renci.SshNet;
using Renci.SshNet.Sftp;

namespace automato.Infrastructure.SshNet;

public class SshNetSftpService : ISftpService
{
    private readonly ILogger _logger;

    public SshNetSftpService(ILogger<SshNetSftpService> logger)
    {
        _logger = logger;
    }

    public async Task DownloadAsync(SftpServer server, SftpDownloadTask task, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Starting SFTP download task '{TaskName}'", task.Name);

        var connectionInfo = new ConnectionInfo(
            port: server.Port,
            host: server.Hostname,
            username: server.Username,
            authenticationMethods: new PasswordAuthenticationMethod(username: server.Username, password: server.Password ?? string.Empty));

        var remotePath = Path.GetDirectoryName(task.RemotePath)?.Replace('\\', '/') ?? "/";

        using var sftpClient = new SftpClient(connectionInfo);

        sftpClient.Connect();
        var remoteFiles = GetFiles(sftpClient, remotePath).ToList();

        foreach (var file in remoteFiles)
        {
            var localFilePath = Path.Combine(task.LocalPath, file.FullName.Replace(task.RemotePath, ""));

            if (!Directory.Exists(Path.GetDirectoryName(localFilePath)))
            {
                Directory.CreateDirectory(Path.GetDirectoryName(localFilePath)!); // use a directory value object
            }

            using var fileStream = File.Create(localFilePath);

            await Task.Factory.FromAsync(
                asyncResult: sftpClient.BeginDownloadFile(
                    path: file.FullName,
                    output: fileStream,
                    asyncCallback: null,
                    state: null,
                    downloadCallback: (downloaded) => PrintProgress((int)downloaded, (int)file.Attributes.Size, file.Name)),
                endMethod: sftpClient.EndDownloadFile);
        }
    }

    private void PrintProgress(int downloaded, int total, string fileName)
    {
        _logger.LogInformation("Downloading file {FileName}, progress: {Downloaded}%", fileName, (downloaded / (double)total * 100).ToString("0.00"));
    }

    private IEnumerable<SftpFile> GetFiles(SftpClient sftpClient, string remotePath)
    {
        var files = sftpClient.ListDirectory(remotePath);

        foreach (var file in files.Where(f => f.Name != "." && f.Name != ".."))
        {
            if (file.IsDirectory)
            {
                var subFiles = GetFiles(sftpClient, file.FullName);
                foreach (var subFile in subFiles)
                {
                    yield return subFile;
                }
            }
            else
            {
                yield return file;
            }
        }
    }
}
