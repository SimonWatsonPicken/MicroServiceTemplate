using MicroService.Domain.Aggregates;
using MicroService.Domain.SeedWork;
using System.Threading.Tasks;

namespace MicroService.Application.Services
{
    public interface IOpenLibraryService
    {
        Task<Result<Book>> GetBookDetails(string isbn);
    }
}