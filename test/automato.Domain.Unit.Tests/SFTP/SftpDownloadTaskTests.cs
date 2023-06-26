using automato.Domain.SFTP;

namespace automato.Domain.Unit.Tests.SFTP;

public class SftpDownloadTaskTests
{
    [Fact]
    public void Create_WhenValidParameters_ShouldReturnSuccessfulResult()
    {
        // Arrange
        var result = SftpDownloadTask.Create(
            name: "name",
            localPath: "local",
            remotePath: "/remote",
            sftpServerId: "serverid",
            searchPattern: @"\b[M]\w+");

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value?.Name.Should().Be("name");
        result.Value?.LocalPath.Should().Be("local");
        result.Value?.RemotePath.Should().Be("/remote");
        result.Value?.SftpServerId.Should().Be("serverid");
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData(null)]
    public void Create_WhenInvalid_ShouldReturnFailedResult(string value)
    {
        // Arrange
        var result = SftpDownloadTask.Create(
            name: value,
            localPath: value,
            remotePath: value,
            searchPattern: "[",
            sftpServerId: value);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Exceptions.Should().HaveCount(5);
    }
}