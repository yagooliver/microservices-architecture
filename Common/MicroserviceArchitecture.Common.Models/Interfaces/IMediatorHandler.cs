using MicroserviceArchitecture.Common.Models.Handler;

namespace MicroserviceArchitecture.Common.Models.Interfaces
{
    public interface IMediatorHandler
    {
        Task<TResult> Send<TResult>(CommandBase<TResult> command);
        Task RaiseEvent<T>(T @event) where T : class;
    }
}