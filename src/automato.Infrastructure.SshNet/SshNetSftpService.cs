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

        try
        {
            using var sftpClient = new SftpClient(GetConnectionInfo(server));

            sftpClient.ErrorOccurred += (sender, args) =>
                _logger.LogError(args.Exception, "An SFTP error occurred while processing task '{TaskName}': {Message}", task.Name, args.Exception.Message);

            sftpClient.Connect();

            var remoteFiles = await ListDirectoryAsync(
                sftpClient: sftpClient,
                remotePath: task.RemotePath,
                searchPattern: task.SearchPattern);

            await DownloadFiles(
                task: task,
                sftpClient: sftpClient,
                remoteFiles: remoteFiles,
                cancellationToken: cancellationToken);

            await DeleteEmptyDirectories(
                task: task,
                sftpClient: sftpClient,
                remoteFiles: remoteFiles);

            sftpClient.Disconnect();

            _logger.LogInformation("Successfully completed SFTP download task '{TaskName}'", task.Name);
        }
        catch (Exception e)
        {
            _logger.LogError(e, "An error occurred while processing task '{TaskName}': {Message}", task.Name, e.Message);
        }
    }

    private async Task DownloadFiles(SftpDownloadTask task, SftpClient sftpClient, IEnumerable<SftpFile> remoteFiles, CancellationToken cancellationToken)
    {
        foreach (var file in remoteFiles.Where(rf => !rf.IsDirectory))
        {
            cancellationToken.ThrowIfCancellationRequested();

            var localFilePath = Path.Combine(task.LocalPath, file.FullName.Replace(task.RemotePath, ""));

            if (!Directory.Exists(Path.GetDirectoryName(localFilePath)))
            {
                _ = Directory.CreateDirectory(Path.GetDirectoryName(localFilePath)!);
            }

            using var fileStream = File.Create(localFilePath);

            await sftpClient.DownloadFileAsync(file.FullName, fileStream, (downloaded) => PrintProgress((int)downloaded, file));

            if (task.DeleteDownloadedFiles)
            {
                sftpClient.DeleteFile(file.FullName);
                _logger.LogInformation("Deleted downloaded file '{FileName}' from server.", file.FullName);
            }
        }
    }

    private async Task DeleteEmptyDirectories(SftpDownloadTask task, SftpClient sftpClient, IEnumerable<SftpFile> remoteFiles)
    {
        if (task.DeleteEmptyDirectories)
        {
            foreach (var directory in remoteFiles.Where(rf => rf.IsDirectory))
            {
                var filesInDirectory = await ListDirectoryAsync(sftpClient, directory.FullName, task.SearchPattern);

                if (!filesInDirectory.Any())
                {
                    sftpClient.DeleteDirectory(directory.FullName);
                    _logger.LogInformation("Deleted empty directory '{Directory}' from server.", directory.FullName);
                }
            }
        }
    }

    private static async Task<IEnumerable<SftpFile>> ListDirectoryAsync(SftpClient sftpClient, string remotePath, string? searchPattern)
    {
        var directoryList = await sftpClient.ListDirectoryAsync(remotePath);

        directoryList = directoryList
            .Where(f => f.Name != "." && f.Name != "..")
            .Where(f => Regex.IsMatch(f.Name, searchPattern ?? string.Empty));

        foreach (var file in directoryList)
        {
            if (file.IsDirectory)
            {
                directoryList = directoryList.Concat(await ListDirectoryAsync(sftpClient, file.FullName, searchPattern));
            }
        }

        return directoryList;
    }

    private void PrintProgress(int downloaded, SftpFile sftpFile)
    {
        _logger.LogInformation("Downloading file {FileName}, progress: {Downloaded}%", sftpFile.Name, (downloaded / (double)sftpFile.Attributes.Size * 100).ToString("0.00"));
    }

    private ConnectionInfo GetConnectionInfo(SftpServer serverConfiguration)
    {
        return new ConnectionInfo(
            port: serverConfiguration.Port,
            host: serverConfiguration.Hostname,
            username: serverConfiguration.Username,
            authenticationMethods: new PasswordAuthenticationMethod(username: serverConfiguration.Username, password: serverConfiguration.Password ?? string.Empty));
    }
}
