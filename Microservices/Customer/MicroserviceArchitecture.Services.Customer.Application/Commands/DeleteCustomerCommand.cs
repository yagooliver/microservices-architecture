using MicroserviceArchitecture.Common.Models.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceArchitecture.Services.Customer.Application.Commands
{
    public class DeleteCustomerCommand : CommandBase<bool>
    {
        public Guid Id { get; }

        public DeleteCustomerCommand(Guid id)
        {
            Id = id;
        }
    }
}
