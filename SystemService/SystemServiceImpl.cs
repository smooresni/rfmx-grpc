using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using NationalInstruments.ModularInstruments.SystemServices.DeviceServices;
using System;
using System.Threading.Tasks;

namespace NationalInstruments.ApplicationsEngineering.Services.System
{
    public class SystemServiceImpl : SystemService.SystemServiceBase
    {
        public override Task<SystemIdentity> Identify(Empty request, ServerCallContext context)
        {
            var identity = new SystemIdentity()
            {
                MachineName = Environment.MachineName,
                OSVersion = Environment.OSVersion.ToString()
            };
            using (ModularInstrumentsSystem sys = new ModularInstrumentsSystem())
            {
                foreach (DeviceInfo device in sys.DeviceCollection)
                    identity.InstrumentIdentities.Add(new InstrumentIdentity()
                    {
                        Model = device.Model,
                        Name = device.Name,
                        SerialNumber = device.SerialNumber,
                        SlotNumber = device.SlotNumber
                    });
            }
            return Task.FromResult(identity);
        }
    }
}
