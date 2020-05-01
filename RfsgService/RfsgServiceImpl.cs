using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using NationalInstruments.ModularInstruments.NIRfsg;
using NationalInstruments.ModularInstruments.NIRfsgPlayback;
using NationalInstruments.ReferenceDesignLibraries;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NationalInstruments.ApplicationsEngineering.Services.NIRfsgGrpc
{
    public class RfsgServiceImpl : NIRfsgService.NIRfsgServiceBase
    {
        private readonly Dictionary<IntPtr, NIRfsg> sessionMap = new Dictionary<IntPtr, NIRfsg>();

        public override Task<RfsgServiceSession> Initialize(RfsgServiceResource request, ServerCallContext context)
        {
            var rfsg = new NIRfsg(request.Name, request.IdQuery, request.Reset, request.OptionString);
            var handle = rfsg.GetInstrumentHandle().DangerousGetHandle();
            sessionMap.Add(handle, rfsg);
            return Task.FromResult(new RfsgServiceSession()
            {
                Handle = (long)handle
            });
        }

        public override Task<RfsgServiceInstrumentConfiguration> GetDefaultInstrumentConfiguration(RfsgServiceSession request, ServerCallContext context)
        {
            var rfsg = sessionMap[(IntPtr)request.Handle];
            var instrConfig = SG.InstrumentConfiguration.GetDefault(rfsg);
            return Task.FromResult(new RfsgServiceInstrumentConfiguration()
            {
                CarrierFrequencyHz = instrConfig.CarrierFrequency_Hz,
                DutAverageInputPowerDBm = instrConfig.DutAverageInputPower_dBm,
                ExternalAttenuationDB = instrConfig.ExternalAttenuation_dB,
                LOSharingMode = (RfsgServiceInstrumentConfiguration.Types.LocalOscillatorSharingMode)instrConfig.LOSharingMode,
                ReferenceClockSource = instrConfig.ReferenceClockSource,
                SelectedPorts = instrConfig.SelectedPorts,
                Session = request
            });
        }

        public override Task<Empty> ConfigureInstrument(RfsgServiceInstrumentConfiguration request, ServerCallContext context)
        {
            var rfsg = sessionMap[(IntPtr)request.Session.Handle];
            var instrConfig = new SG.InstrumentConfiguration()
            {
                CarrierFrequency_Hz = request.CarrierFrequencyHz,
                DutAverageInputPower_dBm = request.DutAverageInputPowerDBm,
                ExternalAttenuation_dB = request.ExternalAttenuationDB,
                ReferenceClockSource = request.ReferenceClockSource,
                SelectedPorts = request.SelectedPorts,
                LOSharingMode = (LocalOscillatorSharingMode)request.LOSharingMode
            };
            SG.ConfigureInstrument(rfsg, instrConfig);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> ReadAndDownloadWaveformFromFile(RfsgServiceWaveformDownloadConfiguration request, ServerCallContext context)
        {
            var handle = (IntPtr)request.Session.Handle;
            NIRfsgPlayback.ReadAndDownloadWaveformFromFile(handle, request.Waveform.FilePath, request.Waveform.Name);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> ConfigureContinuousGeneration(RfsgServiceGenerationConfiguration request, ServerCallContext context)
        {
            var rfsg = sessionMap[(IntPtr)request.Session.Handle];
            Waveform wfm = new Waveform()
            {
                Name = request.WaveformName,
            };
            SG.ConfigureContinuousGeneration(rfsg, wfm, request.MarkerEventExportTerminal);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> Initiate(RfsgServiceSession request, ServerCallContext context)
        {
            var rfsg = sessionMap[(IntPtr)request.Handle];
            rfsg.Initiate();
            return Task.FromResult(new Empty());
        }

        public override Task<RfsgServiceGenerationStatus> CheckGenerationStatus(RfsgServiceSession request, ServerCallContext context)
        {
            var rfsg = sessionMap[(IntPtr)request.Handle];
            return Task.FromResult(new RfsgServiceGenerationStatus()
            {
                Complete = Convert.ToBoolean(rfsg.CheckGenerationStatus())
            });
        }

        public override Task<Empty> AbortGeneration(RfsgServiceTimeout request, ServerCallContext context)
        {
            var rfsg = sessionMap[(IntPtr)request.Session.Handle];
            SG.AbortGeneration(rfsg, request.TimeoutMs);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> CloseInstrument(RfsgServiceSession request, ServerCallContext context)
        {
            var rfsg = sessionMap[(IntPtr)request.Handle];
            rfsg.Close();
            sessionMap.Remove((IntPtr)request.Handle);
            return Task.FromResult(new Empty());
        }
    }
}
