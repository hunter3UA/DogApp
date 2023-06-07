using DogApp.Application.Helpers;
using FluentAssertions;
using Xunit;

namespace Application.Tests.Helpers
{
    public class PaginationHelperTest
    {
        [Theory]
        [InlineData(0, 10, 100, 0, 10, 10)]
        [InlineData(2, 100, 10, 0, 30, 1)]
        [InlineData(5, 15, 100, 4, 15, 7)]
        [InlineData(-1, 25, 75, 0, 25, 3)]
        [InlineData(2, 50, 20, 0, 30, 1)]
        public void FilterSettings_ReturnsCorrectValues(
            int pageNumber, int pageSize, int totalItems,
            int expectedPageNumber, int expectedPageSize, int expectedTotalPages)
        {
            var result = PaginationHelper.FilterSettings(pageNumber, pageSize, totalItems);

            result.PageNumber.Should().Be(expectedPageNumber);
            result.PageSize.Should().Be(expectedPageSize);
            result.TotalPages.Should().Be(expectedTotalPages);
        }
    }
}