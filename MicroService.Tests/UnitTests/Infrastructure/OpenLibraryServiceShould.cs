using MicroService.Infrastructure.OpenLibraryService;
using MicroService.Infrastructure.Options;
using Microsoft.Extensions.Options;
using System.Net.Http;
using Xunit;

namespace MicroService.Tests.UnitTests.Infrastructure
{
    public sealed class OpenLibraryServiceShould
    {
        [Fact]
        [Trait("Category", "UnitTest")]
        public async void Succeed_When_Using_Valid_Data()
        {
            // Arrange.
            var service = new OpenLibraryService(new HttpClient(), new OptionsWrapper<OpenLibraryServiceOptions>(
                new OpenLibraryServiceOptions
                {
                    BaseAddress = "https://openlibrary.org/",
                    IsbnEndpoint = "isbn"
                }));

            // Act.
            var result = await service.GetBookDetails("0451526538");

            // Assert.
            Assert.Equal("The adventures of Tom Sawyer", result.Value.Title);
            Assert.Equal("0451526538", result.Value.Isbn.Isbn10);
            Assert.Equal("9780451526533", result.Value.Isbn.Isbn13);
            Assert.Null(result.Error);
        }
    }
}