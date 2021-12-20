using MicroService.Domain.Aggregates;

namespace MicroService.Api.Application.Books
{
    public class BookCommandValidator : IValidator<BookCommand>
    {
        public BookInformation Validate(BookCommand bookCommand)
        {
            var bookInformation = new BookInformation();

            if (bookCommand == null)
            {
                bookInformation.Errors.Add("The book command cannot be null.");
            }

            if (string.IsNullOrWhiteSpace(bookCommand?.Isbn))
            {
                bookInformation.Errors.Add("The ISBN has not been provided.");
            }

            if (bookCommand?.Isbn?.Length != 10 && bookCommand?.Isbn?.Length != 13)
            {
                bookInformation.Errors.Add("The ISBN provided is not the correct length.");
            }

            bookInformation.Isbn = bookCommand?.Isbn;

            return bookInformation;
        }
    }
}