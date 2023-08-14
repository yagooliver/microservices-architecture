using System;
using MediatR;
using MicroserviceArchitecture.Common.Models.Interfaces;

namespace MicroserviceArchitecture.Common.Models.Handler
{
    public class MediatorHandler : IMediatorHandler
    {
        private readonly IMediator mediator;

        public MediatorHandler(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public Task RaiseEvent<T>(T @event) where T : class
        {
            return mediator.Publish(@event);
        }
        
        public async Task<TResult> Send<TResult>(CommandBase<TResult> command)
        {
            return await mediator.Send(command);
        }
    }
}