using GoogleBooksService;
using System.Linq;
using Xunit;

namespace GoogleBooksServiceTests
{
    public class VolumeServiceTests
    {
        [Fact]
        public async void CanSearchByAuthor()
        {
            // Arrange
            var service = new VolumeService();

            // Act
            var searchResult = await service.SearchByAuthorAsync("Isaac Asimov");

            // Assert
            Assert.NotNull(searchResult);
            Assert.NotEqual(0, searchResult.TotalItems);
            Assert.Equal("Isaac Asimov", searchResult.Items.First().VolumeInfo.Authors.First());
        }

        [Fact]
        public async void CanGetMoreThan40Books()
        {
            // Arrange
            var service = new VolumeService();
            const int maxBooks = 120;

            // Act
            var searchResult = await service.SearchByAuthorAsync("Terry Pratchett", maxBooks);

            // Assert
            Assert.NotNull(searchResult);
            Assert.NotEqual(0, searchResult.TotalItems);
            Assert.InRange(searchResult.Items.Count(), 41, maxBooks);
        }
    }
}
