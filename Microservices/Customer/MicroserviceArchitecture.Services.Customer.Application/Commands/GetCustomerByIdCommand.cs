using MicroserviceArchitecture.Common.Models.Handler;
using MicroserviceArchitecture.Services.Customer.Model.Model.Dto;

namespace MicroserviceArchitecture.Services.Customer.Application.Commands
{
    public class GetCustomerByIdCommand : CommandBase<CustomerDto>
    {
        public Guid Id { get; set; }
    }
}