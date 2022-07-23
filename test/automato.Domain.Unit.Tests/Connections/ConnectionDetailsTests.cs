using automato.Domain.Connections;

namespace automato.Domain.Unit.Tests.Actions;

public class ConnectionDetailsTests
{
    [Fact]
    public void FromString_ShouldCreateNewInstance()
    {
        // Act
        var sut = ConnectionDetails.FromString("details");

        // Assert
        Assert.Equal("details", sut.Value);
    }

    [Fact]
    public void Empty_ShouldCreateEmptyInstance()
    {
        // Act
        var sut = ConnectionDetails.Empty;

        // Assert
        Assert.Equal(string.Empty, sut.Value);
    }

    [Fact]
    public void ImplicitStringOperator_WhenValidString_ShouldCreateInstance()
    {
        // Arrange
        ConnectionDetails sut = "details";

        // Assert
        Assert.Equal("details", sut.Value);
    }
}