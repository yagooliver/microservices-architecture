using MicroserviceArchitecture.Services.Customer.Model.Entities;
using MicroserviceArchitecture.Services.Customer.Model.ValueObjects;
using MicroserviceArchitecture.Services.Customer.Model.Model.Dto;

namespace MicroserviceArchitecture.Services.Customer.Application.Extensions
{
    public static class CustomerMappers
    {
        public static CustomerDto ClientToCustomerDto(this Client client)
        {
            return new CustomerDto
            {
                Id = client.Id,
                Name = $"{client.FirstName} {client.LastName}",
                Email = client.Email
            };
        }

        public static List<CustomerDto> ClientsToCustomerDtos(this IEnumerable<Client> client)
        {
            return client.Select(x => new CustomerDto
            {
                Id = x.Id,
                Name = $"{x.FirstName} {x.LastName}",
                Email = x.Email
            }).ToList();
        }
    }
}