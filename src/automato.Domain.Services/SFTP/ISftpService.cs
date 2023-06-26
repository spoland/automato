using automato.Domain.SFTP;

namespace automato.Domain.Services.SFTP;

public interface ISftpService
{
    Task DownloadAsync(SftpServer server, SftpDownloadTask task, CancellationToken cancellationToken);
}
