using MediatR;
using MicroserviceArchitecture.Services.Customer.Application.Commands;
using MicroserviceArchitecture.Services.Customer.Contracts.Interfaces;
using MicroserviceArchitecture.Services.Customer.Model.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceArchitecture.Services.Customer.Application.CommandHandlers
{
    public class DeleteCustomerCommandHandler : IRequestHandler<DeleteCustomerCommand, bool>
    {
        private readonly ICustomerRepository customerRepository;

        public DeleteCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            this.customerRepository = customerRepository;
        }

        public async Task<bool> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            await customerRepository.DeleteAsync(new Client(request.Id));

            return true;
        }
    }
}
