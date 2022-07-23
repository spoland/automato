using automato.Domain.Connections;

namespace automato.Domain.Unit.Tests.Actions;

public class ConnectionIdTests
{
    [Fact]
    public void New_ShouldCreateNewInstance()
    {
        // Act
        var id = ConnectionId.New();

        // Assert
        Assert.NotEqual(Guid.Empty, id.Value);
    }

    [Fact]
    public void Empty_ShouldCreateEmptyInstance()
    {
        // Act
        var id = ConnectionId.Empty;

        // Assert
        Assert.Equal(Guid.Empty, id.Value);
    }

    [Fact]
    public void FromGuid_WhenNonEmptyGuid_ShouldCreateInstance()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        var id = ConnectionId.FromGuid(guid);

        // Assert
        Assert.Equal(guid, id.Value);
    }

    [Fact]
    public void FromGuid_WhenEmptyGuid_ShouldThrow()
    {
        // Arrange
        var guid = Guid.Empty;

        // Assert
        Assert.Throws<ArgumentException>(() => ConnectionId.FromGuid(guid));
    }

    [Fact]
    public void Equality_WhenEqual_ShouldReturnTrue()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var id1 = ConnectionId.FromGuid(guid);
        var id2 = ConnectionId.FromGuid(guid);

        // Assert
        Assert.Equal(id1, id2);
        Assert.True(id1.Equals(id2));
        Assert.True(id1 == id2);
    }
}