using MicroService.Domain.Aggregates;
using System.Threading.Tasks;

namespace MicroService.Infrastructure.ThirdPartyServices.OpenLibraryService
{
    public interface IOpenLibraryService
    {
        Task<BookInformation> GetBookDetails(IsbnRequest request);
    }
}