using MicroService.Domain.SeedWork;

namespace MicroService.Domain
{
    public static class Errors
    {
        public static Error NotFound() =>
            new("book.not.found", "Book not found.");

        public static Error InvalidIsbn(string isbn) =>
            new("invalid.isbn", $"ISBN {(string.IsNullOrWhiteSpace(isbn) ? string.Empty : isbn)} is invalid.");

        public static Error ValueIsRequired(string parameterName) =>
            new("value.is.required", $"Value is required for '{parameterName}'.");

        public static Error InternalServerError(string message) =>
            new("internal.server.error", message);
    }
}