using Dapr.AppCallback.Autogen.Grpc.v1;
using Dapr.Client;
using Dapr.Client.Autogen.Grpc.v1;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using MicroService.GrpcA;

namespace MicroService.GrpcB.Services
{
    public class GreeterBService : AppCallback.AppCallbackBase
    {
        private readonly ILogger<GreeterBService> _logger;
        private readonly DaprClient _daprClient;
        public GreeterBService(DaprClient daprClient, ILogger<GreeterBService> logger)
        {
            _daprClient = daprClient;
            _logger = logger;
        }

        public override async Task<InvokeResponse> OnInvoke(InvokeRequest request, ServerCallContext context)
        {
            var response = new InvokeResponse();

            switch (request.Method)
            {
                case "getGreeterB":
                    var input = request.Data.Unpack<HelloRequest>();

                    var output = new HelloReply
                    {
                        Message = $"Hello GreeterB-{input.Name}"
                    };
                    response.Data = Any.Pack(output);

                    break;
                case "getGreeterA":
                    var inputA = request.Data.Unpack<HelloRequest>();

                    var requestA = new HelloRequestA { Name = inputA.Name };

                    var ret = await _daprClient.InvokeMethodGrpcAsync<HelloRequestA, HelloReplyA>("grpca", "getGreeterA", requestA);

                    var outputA = new HelloReply
                    {
                        Message = ret.Message
                    };

                    response.Data = Any.Pack(outputA);

                    break;
                case "getHttpB":
                    var httpbRet = await _daprClient.InvokeMethodAsync<IEnumerable<WeatherForecast>>(HttpMethod.Get, "httpb", "weatherforecast");

                    var outputhttpB = new HelloReply
                    {
                        Message = System.Text.Json.JsonSerializer.Serialize(httpbRet)
                    };

                    response.Data = Any.Pack(outputhttpB);

                    break;
                default:
                    break;
            }
            return response;
        }
    }
}