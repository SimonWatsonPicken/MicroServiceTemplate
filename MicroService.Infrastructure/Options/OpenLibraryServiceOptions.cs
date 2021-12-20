using MicroService.Domain.Extensions;

namespace MicroService.Infrastructure.Options
{
    public sealed class OpenLibraryServiceOptions
    {
        public const string OpenLibraryServiceSectionName = "OpenLibraryService";

        private string _baseAddress;
        private string _isbnEndpoint;

        public string BaseAddress
        {
            get => _baseAddress;
            set => _baseAddress = value.ForceTrailingCharacter('/');
        }

        public string IsbnEndpoint
        {
            get => _isbnEndpoint;
            set => _isbnEndpoint = value.StripLeadingCharacter('/').ForceTrailingCharacter('/');
        }
    }
}