using Microsoft.AspNetCore.Mvc;

namespace MicroserviceArchitecture.Services.Shipping.Service.Controllers
{
    [ApiController]
    [Route("shippment")]
    public class ShippingController : ControllerBase
    {
        private readonly ILogger<ShippingController> _logger;

        public ShippingController(ILogger<ShippingController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "ping")]
        public IActionResult Get()
        {
            return Ok("pong");
        }
    }
}