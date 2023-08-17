using MediatR;
using MicroserviceArchitecture.Common.Models.Handler;
using MicroserviceArchitecture.Common.Models.Interfaces;
using MicroserviceArchitecture.Services.Customer.Application.CommandHandlers;
using MicroserviceArchitecture.Services.Customer.Application.Commands;
using MicroserviceArchitecture.Services.Customer.Contracts.Interfaces;
using MicroserviceArchitecture.Services.Customer.Infra.Redis.Repositories;
using MicroserviceArchitecture.Services.Customer.Infra.SqlServer.Repositories;
using MicroserviceArchitecture.Services.Customer.Model.Model.Dto;

namespace MicroserviceArchitecture.Services.Customer.Service
{
    public static class ServiceCollectionExtension
    {
        /// <summary>
        /// Inject the repositories
        /// </summary>
        /// <param name="services"></param>
        public static void AddRepositories(this IServiceCollection services)
        { 
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.Decorate<ICustomerRepository, CustomerCacheRepository>();
        }
        
        /// <summary>
        /// inject the command handlers
        /// </summary>
        /// <param name="services"></param>
        public static void AddCommands(this IServiceCollection services)
        {
            services.AddScoped<IMediatorHandler, MediatorHandler>();

            //Add requests
            services.AddScoped<IRequestHandler<AddCustomerCommand, CustomerDto>, AddCustomerCommandHandler>();
            services.AddScoped<IRequestHandler<GetCustomerByIdCommand, CustomerDto>, GetCustomerByIdCommandHandler>();
            services.AddScoped<IRequestHandler<GetAllCustomersCommand, List<CustomerDto>>, GetAllCustomersCommandHandler>();
            services.AddScoped<IRequestHandler<DeleteCustomerCommand, bool>, DeleteCustomerCommandHandler>();
        }
    }
}