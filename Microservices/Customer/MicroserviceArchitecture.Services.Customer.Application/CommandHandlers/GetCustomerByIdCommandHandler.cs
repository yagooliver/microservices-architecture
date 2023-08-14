using MediatR;
using MicroserviceArchitecture.Services.Customer.Application.Commands;
using MicroserviceArchitecture.Services.Customer.Application.Extensions;
using MicroserviceArchitecture.Services.Customer.Contracts.Interfaces;
using MicroserviceArchitecture.Services.Customer.Model.Entities;
using MicroserviceArchitecture.Services.Customer.Model.Model.Dto;

namespace MicroserviceArchitecture.Services.Customer.Application.CommandHandlers
{
    public class GetCustomerByIdCommandHandler : IRequestHandler<GetCustomerByIdCommand, CustomerDto>
    {
        private readonly ICustomerRepository customerRepository;

        public GetCustomerByIdCommandHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }
        /// <summary>
        /// Command handler to get customer by id
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<CustomerDto> Handle(GetCustomerByIdCommand request, CancellationToken cancellationToken)
        {
            Client? client = await customerRepository.GetByIdAsync(request.Id);

            if(client is null)
                throw new Exception("Customer not found");

            return client.ClientToCustomerDto();
        }
    }
}