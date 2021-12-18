using Dapr.AppCallback.Autogen.Grpc.v1;
using Dapr.Client;
using Dapr.Client.Autogen.Grpc.v1;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
namespace MicroService.GrpcA.Services
{
    public class GreeterAService : AppCallback.AppCallbackBase
    {
        private readonly ILogger<GreeterAService> _logger;
        private readonly DaprClient _daprClient;
        public GreeterAService(DaprClient daprClient, ILogger<GreeterAService> logger)
        {
            _daprClient = daprClient;
            _logger = logger;
        }

        public override Task<InvokeResponse> OnInvoke(InvokeRequest request, ServerCallContext context)
        {
            var response = new InvokeResponse();

            switch (request.Method)
            {
                case "getGreeterA":
                    var input = request.Data.Unpack<HelloRequestA>();

                    var output = new HelloReplyA
                    {
                        Message = $"Hello GreeterA-{input.Name}"
                    };
                    response.Data = Any.Pack(output);

                    break;
                default:
                    break;
            }
            return Task.FromResult(response);
        }
    }
}