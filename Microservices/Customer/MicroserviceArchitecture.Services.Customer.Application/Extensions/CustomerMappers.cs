using MicroserviceArchitecture.Services.Customer.Application.Commands;
using MicroserviceArchitecture.Services.Customer.Model.Entities;
using MicroserviceArchitecture.Services.Customer.Model.ValueObjects;
using MicroserviceArchitecture.Services.Customer.Model.Model.Dto;

namespace MicroserviceArchitecture.Services.Customer.Application.Extensions
{
    public static class CustomerMappers
    {
        public static Client AddCustomerCommandToClient(this AddCustomerCommand command)
        {
            Client client = new(Guid.NewGuid(), command.FirstName, command.LastName, command.Email);
            return client;
        }
    }
}