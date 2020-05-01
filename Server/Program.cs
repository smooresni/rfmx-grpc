using Grpc.Core;
using NationalInstruments.ApplicationsEngineering.Services.NIRfsgGrpc;
using NationalInstruments.ApplicationsEngineering.Services.RFmxGrpc;
using System;
using Grpc.Core.Interceptors;

namespace NationalInstruments.ApplicationsEngineering.Services
{
    class Program
    {
        const int Port = 50051;

        static void Main(string[] args)
        {
            Server server = new Server
            {
                Services = { RFmxService.BindService(new RFmxServiceImpl()).Intercept(new GrpcInterceptor()), 
                    NIRfsgService.BindService(new RfsgServiceImpl()) },
                Ports = { new ServerPort(Environment.MachineName, Port, ServerCredentials.Insecure) },
            };
            server.Start();

            Console.WriteLine($"Server listening on {Environment.MachineName}:{Port}");
            Console.WriteLine("Press any key to stop the server...");
            Console.ReadKey();

            server.ShutdownAsync().Wait();
        }
    }
}
