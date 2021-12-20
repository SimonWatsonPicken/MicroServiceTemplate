using MicroService.Domain.Aggregates;
using MicroService.Domain.Extensions;
using MicroService.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Serilog;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace MicroService.Infrastructure.ThirdPartyServices.OpenLibraryService
{
    public sealed class OpenLibraryService : IOpenLibraryService
    {
        private readonly HttpClient _httpClient;
        private readonly OpenLibraryServiceOptions _openLibraryServiceOptions;

        public OpenLibraryService(HttpClient httpClient, IOptions<OpenLibraryServiceOptions> openLibraryServiceOptions)
        {
            _httpClient = httpClient;
            _httpClient.Timeout = Timeout.InfiniteTimeSpan;

            _openLibraryServiceOptions = openLibraryServiceOptions.Value;
        }

        public async Task<BookInformation> GetBookDetails(IsbnRequest request)
        {
            BookInformation bookInformation;

            try
            {
                var requestUri = $"{_openLibraryServiceOptions.BaseAddress}{_openLibraryServiceOptions.IsbnEndpoint}{request.Isbn}.json";
                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

                httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);
                var isbnResponse = httpResponseMessage.GetResponseObject<IsbnResponse>(out var deserialiseError);

                if (isbnResponse == null)
                {
                    bookInformation = new BookInformation();
                    bookInformation.Errors.Add(deserialiseError);

                    Log
                        .ForContext("ISBN", request.Isbn)
                        .Warning("Book information could not be retrieved: {@BookInformation}", bookInformation);
                }
                else
                {
                    bookInformation = new BookInformation
                    {
                        Title = isbnResponse.Title,
                        Publishers = isbnResponse.Publishers,
                        PublishDate = isbnResponse.PublishDate,
                        FirstSentence = isbnResponse.FirstSentence?.Value,
                        Isbn = isbnResponse.Isbn10.FirstOrDefault() ?? isbnResponse.Isbn13.FirstOrDefault()
                    };
                    foreach (var author in isbnResponse.Authors)
                    {
                        bookInformation.Authors.Add(author.Key);
                    }

                    Log
                        .ForContext("Key", request.Isbn)
                        .Information("Book information successfully retrieved: {@BookInformation}", bookInformation);
                }
            }
            catch (Exception e)
            {
                bookInformation = new BookInformation();
                bookInformation.Errors.Add(e.Message);

                Log
                     .ForContext("Key", request.Isbn)
                     .Error(e, "Error when retrieving book information: {@BookInformation}", bookInformation);
            }

            return bookInformation;
        }
    }
}