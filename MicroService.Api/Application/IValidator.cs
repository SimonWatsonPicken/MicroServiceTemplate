using MicroService.Domain.Aggregates;

namespace MicroService.Api.Application
{
    public interface IValidator<in T>
    {
        BookInformation Validate(T command);
    }
}