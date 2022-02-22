using MicroService.Application.Services;
using MicroService.Domain;
using MicroService.Domain.Aggregates;
using MicroService.Domain.Extensions;
using MicroService.Domain.SeedWork;
using MicroService.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Serilog;
using SerilogTimings;
using System;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading;
using System.Threading.Tasks;

namespace MicroService.Infrastructure.OpenLibraryService
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

        public async Task<Result<Book>> GetBookDetails(string isbn)
        {
            try
            {
                var requestUri = $"{_openLibraryServiceOptions.BaseAddress}{_openLibraryServiceOptions.IsbnEndpoint}{isbn}.json";
                var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, requestUri);

                httpRequestMessage.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage httpResponseMessage;
                using (Operation.Time($"Calling Open Library at {requestUri}"))
                {
                    httpResponseMessage = await _httpClient.SendAsync(httpRequestMessage);
                }

                var bookDto = httpResponseMessage.GetResponseObject<BookDto>(out _);

                if (bookDto == null || bookDto.IsEmpty())
                {
                    Log.ForContext("ISBN", isbn).Warning("Book could not be found.");

                    return Result<Book>.Failure(Errors.NotFound());
                }

                var book = PopulateBookDetails(bookDto);

                Log.ForContext("ISBN", isbn).Information("Book found.");

                return Result<Book>.Success(book);
            }
            catch (Exception e)
            {
                Log.ForContext("ISBN", isbn).Error(e, "Error when retrieving book.");

                return Result<Book>.Failure(Errors.InternalServerError(e.Message));
            }
        }

        private static Book PopulateBookDetails(BookDto bookDto)
        {
            var book = new Book
            {
                Title = bookDto.Title,
                Publishers = bookDto.Publishers,
                PublishDate = bookDto.PublishDate,
                FirstSentence = bookDto.FirstSentence?.Value,
                Isbn = new Isbn(bookDto.Isbn10?.FirstOrDefault() ?? bookDto.Isbn13?.FirstOrDefault())
            };

            if (bookDto.Authors != null)
            {
                foreach (var author in bookDto.Authors)
                {
                    book.Authors.Add(author.Key);
                }
            }

            return book;
        }
    }
}