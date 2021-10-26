using Xunit;

namespace SPA.UnitTests
{
    public class NameReverseServiceTests
    {
        [Fact]
        public void Reverse_JASON_Returns_NOSAJ()
        {
            // Arrange
            var nameReverseService = new NameReverseService();

            // Act
            var reversedName = nameReverseService.Reverse("JASON");

            // Assert
            Assert.Equal("NOSAJ", reversedName);
        }
    }
}
