using MediatR;
using MicroserviceArchitecture.Services.Customer.Application.Commands;
using MicroserviceArchitecture.Services.Customer.Contracts.Interfaces;
using MicroserviceArchitecture.Services.Customer.Model.Entities;
using MicroserviceArchitecture.Services.Customer.Model.Model.Dto;
using MicroserviceArchitecture.Services.Customer.Application.Extensions;

namespace MicroserviceArchitecture.Services.Customer.Application.CommandHandlers
{
    public class GetAllCustomersCommandHandler : IRequestHandler<GetAllCustomersCommand, List<CustomerDto>>
    {
        private readonly ICustomerRepository customerRepository;

        public GetAllCustomersCommandHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<List<CustomerDto>> Handle(GetAllCustomersCommand request, CancellationToken cancellationToken)
        {
            IEnumerable<Client>? client = await customerRepository.GetAllAsync();

            if (client is null)
                throw new Exception("Customer not found");

            return client.ClientsToCustomerDtos();
        }
    }
}
