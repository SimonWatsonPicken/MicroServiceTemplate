using System.Threading.Tasks;

namespace MicroService.Api.Application
{
    public interface ICommandHandler<in TIn, TOut>
    {
        Task<TOut> Handle(TIn command);
    }
}