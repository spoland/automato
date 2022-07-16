using automato.Domain.Actions;

namespace automato.Domain.Unit.Tests.Actions
{
    public class ActionIdTests
    {
        [Fact]
        public void New_ShouldCreateNewInstance()
        {
            // Act
            var id = ActionId.New();

            // Assert
            Assert.NotEqual(Guid.Empty, id.Value);
        }

        [Fact]
        public void Empty_ShouldCreateEmptyInstance()
        {
            // Act
            var id = ActionId.Empty;

            // Assert
            Assert.Equal(Guid.Empty, id.Value);
        }

        [Fact]
        public void FromGuid_WhenNonEmptyGuid_ShouldCreateInstance()
        {
            // Arrange
            var guid = Guid.NewGuid();

            // Act
            var id = ActionId.FromGuid(guid);

            // Assert
            Assert.Equal(guid, id.Value);
        }

        [Fact]
        public void FromGuid_WhenEmptyGuid_ShouldThrow()
        {
            // Arrange
            var guid = Guid.Empty;

            // Assert
            Assert.Throws<ArgumentException>(() => ActionId.FromGuid(guid));
        }

        [Fact]
        public void Equality_WhenEqual_ShouldReturnTrue()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var id1 = ActionId.FromGuid(guid);
            var id2 = ActionId.FromGuid(guid);

            // Assert
            Assert.Equal(id1, id2);
            Assert.True(id1.Equals(id2));
            Assert.True(id1 == id2);
        }
    }
}