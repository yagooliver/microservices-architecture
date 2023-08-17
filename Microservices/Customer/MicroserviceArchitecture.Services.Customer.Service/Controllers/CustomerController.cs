using MicroserviceArchitecture.Common.Models.Interfaces;
using Microsoft.AspNetCore.Mvc;
using MicroserviceArchitecture.Services.Customer.Model.Model.Dto;
using MicroserviceArchitecture.Services.Customer.Application.Commands;

namespace MicroserviceArchitecture.Services.Customer.Service.Controllers
{
    [ApiController]
    [Route("/api/customers")]
    [Produces("application/json")]
    public class CustomerController : Controller
    {
        private readonly IMediatorHandler mediator;

        public CustomerController(IMediatorHandler mediator)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] AddCustomerCommand request)
        {
            CustomerDto? dto = await mediator.Send(request);

            return Ok(dto);
        }

        [HttpGet]
        [Route("{id}")]
        [ProducesResponseType(typeof(CustomerDto), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get(string id)
        {
            CustomerDto? dto = await mediator.Send(new GetCustomerByIdCommand(){ Id = Guid.Parse(id) });

            return Ok(dto);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<CustomerDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Get()
        {
            List<CustomerDto> dto = await mediator.Send(new GetAllCustomersCommand());

            return Ok(dto);
        }

        [HttpGet("ping")]
        public IActionResult Ping()
        {
            return Ok("Pong");
        }
    }
}