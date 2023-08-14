using MicroserviceArchitecture.Services.Customer.Model.Entities;
using MicroserviceArchitecture.Common.Models.Interfaces.Repositories;

namespace MicroserviceArchitecture.Services.Customer.Contracts.Interfaces
{
    public interface ICustomerRepository : IRepositoryBase<Client>
    {
        
    }
}