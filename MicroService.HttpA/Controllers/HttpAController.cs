using Dapr.Client;
using MicroService.GrpcB;
using Microsoft.AspNetCore.Mvc;

namespace MicroService.HttpA.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class HttpAController : ControllerBase
    {
        private readonly ILogger<WeatherForecastController> _logger;
        private readonly DaprClient _daprClient;
        public HttpAController(DaprClient daprClient, ILogger<WeatherForecastController> logger)
        {
            _daprClient = daprClient;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetGrpcBAsync(string name)
        {
            var request = new HelloRequest { Name = name };

            var ret = await _daprClient.InvokeMethodGrpcAsync<HelloRequest, HelloReply>("grpcb", "getGreeterB", request);
            return this.Ok(ret.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetGrpcAByBAsync(string name)
        {
            var request = new HelloRequest { Name = name };

            var ret = await _daprClient.InvokeMethodGrpcAsync<HelloRequest, HelloReply>("grpcb", "getGreeterA", request);

            return this.Ok(ret.Message);
        }

        [HttpGet]
        public async Task<IActionResult> GetHttpBAsync()
        {
            var ret = await _daprClient.InvokeMethodAsync<IEnumerable<WeatherForecast>>(HttpMethod.Get, "httpb", "weatherforecast");

            return this.Ok(ret);
        }
    }
}
