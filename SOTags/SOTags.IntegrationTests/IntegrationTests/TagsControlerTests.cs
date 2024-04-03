using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net;

namespace SOTags.IntegrationTests.IntegrationTests
{
    public class TagsControlerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly WebApplicationFactory<Program> _factory;

        public TagsControlerTests(WebApplicationFactory<Program> factory)
        {
            _factory = factory;
        }

        [Fact]
        public async Task GetAllTags_ReturnsCollectionOfAllTags()
        {
            // Arrange

            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/Tags");

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("PageSize=10&Page=1&sortByName=desc")]
        [InlineData("PageSize=10&Page=1&sortByName=asc")]
        [InlineData("PageSize=10&Page=1&sortByCount=desc")]
        [InlineData("PageSize=10&Page=1&sortByCount=asc")]
        [InlineData("PageSize=10&Page=2")]
        [InlineData("PageSize=10&Page=3&sortByName=desc&sortByCount=desc")]
        public async Task GetPagedTags_ReturnsPagedResult(string queryParameters)
        {
            // Arrange

            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/Tags/Paged?" + queryParameters);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }

        [Theory]
        [InlineData("Page=1&sortByName=desc")]
        [InlineData("Page=2&sortByName=desc")]
        [InlineData("PageSize=10&Page=1&sortByName=unsupportedText")]
        [InlineData("PageSize=10&Page=1&sortByCount=unsupportedText")]
        [InlineData("Page=1000000&Pagesize=100")]
        public async Task GetPagedTags_WithInvalidParameters_ReturnsBadRequest(string queryParameters)
        {
            // Arrange

            var client = _factory.CreateClient();

            // Act
            var response = await client.GetAsync("/Tags/Paged?" + queryParameters);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public async Task UpdateDatabase_UpdatesDatabaseWithDataDownloadedFromStackOverflow()
        {
            // Arrange

            var client = _factory.CreateClient();
            var request = new HttpRequestMessage(HttpMethod.Put, "/Tags");

            // Act
            var response = await client.SendAsync(request);

            // Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}

