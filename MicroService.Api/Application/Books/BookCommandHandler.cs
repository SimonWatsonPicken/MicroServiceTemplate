using MicroService.Infrastructure.ThirdPartyServices.OpenLibraryService;
using System.Threading.Tasks;

namespace MicroService.Api.Application.Books
{
    public class BookCommandHandler : ICommandHandler<BookCommand, BookUiEntity>
    {
        private readonly IValidator<BookCommand> _bookCommandValidator;
        private readonly IOpenLibraryService _openLibraryService;

        public BookCommandHandler(IValidator<BookCommand> bookCommandValidator, IOpenLibraryService openLibraryService)
        {
            _bookCommandValidator = bookCommandValidator;
            _openLibraryService = openLibraryService;
        }

        public async Task<BookUiEntity> Handle(BookCommand bookCommand)
        {
            var bookInformation = _bookCommandValidator.Validate(bookCommand);
            if (bookInformation.IsValid)
            {
                var isbnRequest = new IsbnRequest { Isbn = bookInformation.Isbn };
                bookInformation = await _openLibraryService.GetBookDetails(isbnRequest);
            }

            return new BookUiEntity(bookInformation);
        }
    }
}
