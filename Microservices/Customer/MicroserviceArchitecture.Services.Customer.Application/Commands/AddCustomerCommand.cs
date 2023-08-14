using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MicroserviceArchitecture.Common.Models.Handler;
using MicroserviceArchitecture.Services.Customer.Model.Model.Dto;

namespace MicroserviceArchitecture.Services.Customer.Application.Commands
{
    public class AddCustomerCommand : CommandBase<CustomerDto>
    {
        public string? FirstName {get;set;}
        public string? LastName {get;set;}
        public string? Email {get;set;}
    }
}