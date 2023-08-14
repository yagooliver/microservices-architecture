using MediatR;
using MicroserviceArchitecture.Common.Models.Handler;
using MicroserviceArchitecture.Services.Customer.Model.Model.Dto;

namespace MicroserviceArchitecture.Services.Customer.Application.Commands
{
    public class GetAllCustomersCommand : CommandBase<List<CustomerDto>>
    {
    }
}
