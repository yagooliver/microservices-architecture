using Microsoft.AspNetCore.Mvc;

namespace MicroserviceArchitecture.Services.Order.Service.Controllers
{
    [ApiController]
    [Route("order")]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;

        public OrderController(ILogger<OrderController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Get()
        {
            return Ok("pong");
        }
    }
}