using System.Text.RegularExpressions;
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

        using var sftpClient = new SftpClient(connectionInfo);

        sftpClient.Connect();
        var remoteFiles = await ListDirectoryAsync(sftpClient, task);

        foreach (var file in remoteFiles)
        {
            var localFilePath = Path.Combine(task.LocalPath, file.FullName.Replace(task.RemotePath, ""));

            if (!Directory.Exists(Path.GetDirectoryName(localFilePath)))
            {
                _ = Directory.CreateDirectory(Path.GetDirectoryName(localFilePath)!); // use a directory value object
            }

            using var fileStream = File.Create(localFilePath);

            await sftpClient.DownloadFileAsync(file.FullName, fileStream, (downloaded) => PrintProgress((int)downloaded, file));
        }
    }

    private static async Task<IEnumerable<SftpFile>> ListDirectoryAsync(SftpClient sftpClient, SftpDownloadTask task)
    {
        var files = await sftpClient.ListDirectoryAsync(task.RemotePath);

        return files
            .Where(f => f.FullName != "." && f.FullName != "..")
            .Where(f => Regex.IsMatch(f.Name, task.SearchPattern ?? string.Empty));
    }

    private void PrintProgress(int downloaded, SftpFile sftpFile)
    {
        _logger.LogInformation("Downloading file {FileName}, progress: {Downloaded}%", sftpFile.Name, (downloaded / (double)sftpFile.Attributes.Size * 100).ToString("0.00"));
    }
}
