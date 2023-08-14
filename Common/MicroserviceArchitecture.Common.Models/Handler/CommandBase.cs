using MediatR;

namespace MicroserviceArchitecture.Common.Models.Handler
{
    public class CommandBase<T> : IRequest<T>
    {
        public bool isValid()
        {
            throw new NotImplementedException();
        }
    }
}