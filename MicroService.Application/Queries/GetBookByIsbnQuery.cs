using MediatR;
using MicroService.Application.ViewModels;
using MicroService.Domain.SeedWork;

namespace MicroService.Application.Queries
{
    public sealed class GetBookByIsbnQuery : IRequest<Result<BookViewModel>>
    {
        public string Isbn { get; }

        public GetBookByIsbnQuery(string isbn)
        {
            Isbn = isbn;
        }
    }
}