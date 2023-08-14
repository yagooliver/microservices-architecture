using MicroserviceArchitecture.Services.Customer.Infra.SqlServer.ContextDb;
using MicroserviceArchitecture.Services.Customer.Model.Entities;
using MicroserviceArchitecture.Services.Customer.Contracts.Interfaces;

namespace MicroserviceArchitecture.Services.Customer.Infra.SqlServer.Repositories
{
    public class CustomerRepository : RepositoryBase<Client>, ICustomerRepository
    {
        public CustomerRepository(CustomerDbContext db) : base(db)
        {
        }
    }
}