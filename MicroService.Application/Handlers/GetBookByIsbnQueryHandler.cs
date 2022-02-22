using MediatR;
using MicroService.Application.Queries;
using MicroService.Application.Services;
using MicroService.Application.ViewModels;
using MicroService.Domain;
using MicroService.Domain.SeedWork;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MicroService.Application.Handlers
{
    public class GetBookByIsbnQueryHandler : IRequestHandler<GetBookByIsbnQuery, Result<BookViewModel>>
    {
        private readonly IOpenLibraryService _openLibraryService;

        public GetBookByIsbnQueryHandler(IOpenLibraryService openLibraryService)
        {
            _openLibraryService = openLibraryService ?? throw new ArgumentNullException(nameof(openLibraryService));
        }

        public async Task<Result<BookViewModel>> Handle(GetBookByIsbnQuery query, CancellationToken cancellationToken)
        {
            if (query == null)
            {
                return Result<BookViewModel>.Failure(Errors.ValueIsRequired(nameof(query)));
            }

            var result = await _openLibraryService.GetBookDetails(query.Isbn);

            return result.IsSuccess
                ? Result<BookViewModel>.Success(new BookViewModel(result.Value))
                : Result<BookViewModel>.Failure(result.Error);
        }
    }
}