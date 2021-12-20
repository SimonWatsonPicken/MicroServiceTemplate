using MicroService.Infrastructure.Options;
using MicroService.Infrastructure.ThirdPartyServices.OpenLibraryService;
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
            var bookInformation = await service.GetBookDetails(new IsbnRequest
            {
                Isbn = "0451526538"
            });

            // Assert.
            Assert.NotNull(bookInformation);
            Assert.Equal("The adventures of Tom Sawyer", bookInformation.Title);
            Assert.Equal("0451526538", bookInformation.Isbn);
            Assert.Empty(bookInformation.Errors);
        }
    }
}