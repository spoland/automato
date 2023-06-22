using automato.Domain.SFTP;

namespace automato.Domain.Unit.Tests.SFTP;

public class SftpServerTests
{
    [Fact]
    public void Create_WhenValidParameters_ShouldCreate()
    {
        // Arrange
        var sftpServer = SftpServer.Create(
            name: "name",
            hostname: "hostname",
            username: "username",
            password: "password",
            port: 22);

        // Assert
        sftpServer.Port.Should().Be(22);
        sftpServer.Name.Should().Be("name");
        sftpServer.Password.Should().Be("password");
        sftpServer.Username.Should().Be("username");
        sftpServer.Hostname.Should().Be("hostname");
    }

    [Fact]
    public void Create_WhenNameIsNullOrWhitespace_ShouldThrow()
    {
        // Arrange
        Action act = () => SftpServer.Create(
            name: null!,
            hostname: "hostname",
            username: "username",
            password: "password",
            port: 22);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Create_WhenHostnameIsNullOrWhitespace_ShouldThrow()
    {
        // Arrange
        Action act = () => SftpServer.Create(
            name: "name",
            hostname: null!,
            username: "username",
            password: "password",
            port: 22);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Create_WhenUsernameIsNullOrWhitespace_ShouldThrow()
    {
        // Arrange
        Action act = () => SftpServer.Create(
            name: "name",
            hostname: "hostname",
            username: null!,
            password: "password",
            port: 22);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Create_WhenPasswordIsNullOrWhitespace_ShouldThrow()
    {
        // Arrange
        Action act = () => SftpServer.Create(
            name: "name",
            hostname: "hostname",
            username: "username",
            password: null!,
            port: 22);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-1)]
    public void Create_WhenPortIsLessThanOrEqualToZero_ShouldThrow(int port)
    {
        // Arrange
        Action act = () => SftpServer.Create(
            port: port,
            name: "name",
            hostname: "hostname",
            username: "username",
            password: "password");

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }
}