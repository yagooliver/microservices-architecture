using MediatR;

namespace MicroserviceArchitecture.Common.Models.Interfaces
{
    public interface ICommandResult<T> : IRequest<T>
    {
        bool isValid();
    }
}