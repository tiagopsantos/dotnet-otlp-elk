using Microsoft.AspNetCore.Mvc;

namespace ApiApplication1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ThrowExController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

        private readonly ILogger<ThrowExController> _logger;

        public ThrowExController(ILogger<ThrowExController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetThrowEx")]
        public void Get()
        {
            throw new Exception("Error in API1");
        }
    }
}