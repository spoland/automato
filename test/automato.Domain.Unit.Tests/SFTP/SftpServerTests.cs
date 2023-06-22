using automato.Domain.Framework;
using automato.Domain.SFTP;

namespace automato.Domain.Unit.Tests.SFTP;

public class SftpServerTests
{
    [Fact]
    public void Create_WhenValidParameters_ShouldCreate()
    {
        // Arrange
        var result = SftpServer.Create(
            name: "name",
            hostname: "hostname",
            username: "username",
            password: "password",
            port: 22);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().NotBeNull();
        result.Value?.Port.Should().Be(22);
        result.Value?.Name.Should().Be("name");
        result.Value?.Password.Should().Be("password");
        result.Value?.Username.Should().Be("username");
        result.Value?.Hostname.Should().Be("hostname");
    }

    [Theory]
    [InlineData(null, 0)]
    [InlineData("", -1)]
    [InlineData(" ", 0)]
    public void Create_WhenInvalid_ShouldReturnFailedResult(string badString, int port)
    {
        // Arrange
        var result = SftpServer.Create(
            name: badString,
            hostname: badString,
            username: badString,
            password: badString,
            port: port);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.Exceptions.Should().NotBeEmpty();
        result.Exceptions.Should().HaveCount(5);
    }
}