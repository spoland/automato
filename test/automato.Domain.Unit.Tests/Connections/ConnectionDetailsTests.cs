using automato.Domain.Connections;

namespace automato.Domain.Unit.Tests.Actions;

public class ConnectionDetailsTests
{
    [Fact]
    public void FromString_WhenValidString_ShouldCreateNewInstance()
    {
        // Act
        var sut = ConnectionDetails.FromString("details");

        // Assert
        Assert.Equal("details", sut.Value);
    }

    [Theory]
    [InlineData("")]
    [InlineData("    ")]
    [InlineData(null)]
    public void FromString_WhenInvalidString_ShouldThrow(string? value)
    {
        // Assert
        Assert.Throws<ArgumentException>(() => ConnectionDetails.FromString(value!));
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

    [Fact]
    public void ToString_ShouldReturnValueString()
    {
        // Arrange
        ConnectionDetails sut = "details";

        // Assert
        Assert.Equal("details", sut.ToString());
    }
}