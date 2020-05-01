using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using NationalInstruments.ReferenceDesignLibraries;
using NationalInstruments.ReferenceDesignLibraries.SA;
using NationalInstruments.RFmx.InstrMX;
using NationalInstruments.RFmx.NRMX;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace NationalInstruments.ApplicationsEngineering.Services.RFmxGrpc
{
    public class RFmxServiceImpl : RFmxService.RFmxServiceBase
    {
        private readonly Dictionary<IntPtr, RFmxInstrMX> sessionMap = new Dictionary<IntPtr, RFmxInstrMX>();

        #region Instr
        public override Task<RFmxSession> Initialize(RFmxResource request, ServerCallContext context)
        {
            var instr = RFmxInstrMX.GetSession(request.Name, request.OptionString, out bool isNewSession);
            var handle = instr.GetInstrumentHandle().DangerousGetHandle();
            if (isNewSession)
                sessionMap.Add(handle, instr);
            return Task.FromResult(new RFmxSession()
            {
                Handle = handle.ToInt64()
            });
        }

        public override Task<RFmxInstrumentConfiguration> GetDefaultInstrumentConfiguration(RFmxSession request, ServerCallContext context)
        {
            var defaultInstrumentConfig = RFmxInstr.InstrumentConfiguration.GetDefault();
            return Task.FromResult(new RFmxInstrumentConfiguration()
            {
                Session = request,
                FrequencyReferenceSource = defaultInstrumentConfig.FrequencyReferenceSource,
                LOSharingMode = (RFmxInstrumentConfiguration.Types.LocalOscillatorSharingMode)defaultInstrumentConfig.LOSharingMode
            });
        }

        public override Task<Empty> ConfigureInstrument(RFmxInstrumentConfiguration request, ServerCallContext context)
        {
            var instr = sessionMap[(IntPtr)request.Session.Handle];
            RFmxInstr.ConfigureInstrument(instr, new RFmxInstr.InstrumentConfiguration()
            {
                FrequencyReferenceSource = request.FrequencyReferenceSource,
                LOSharingMode = (LocalOscillatorSharingMode)request.LOSharingMode
            });
            return Task.FromResult(new Empty());
        }

        public override Task<RFmxCommonConfiguration> GetDefaultCommonConfiguration(RFmxSession request, ServerCallContext context)
        {
            var defaultCommonConfig = CommonConfiguration.GetDefault();
            return Task.FromResult(new RFmxCommonConfiguration()
            {
                Session = request,
                SelectedPorts = defaultCommonConfig.SelectedPorts,
                CenterFrequencyHz = defaultCommonConfig.CenterFrequency_Hz,
                ReferenceLevelDBm = defaultCommonConfig.ReferenceLevel_dBm,
                ExternalAttenuationDB = defaultCommonConfig.ExternalAttenuation_dB,
                TriggerEnabled = defaultCommonConfig.TriggerEnabled,
                DigitalTriggerSource = defaultCommonConfig.DigitalTriggerSource,
                TriggerDelayS = defaultCommonConfig.TriggerDelay_s
            });
        }

        public override Task<RFmxAutoLevelConfiguration> GetDefaultAutoLevelConfiguration(Empty request, ServerCallContext context)
        {
            var defaultAutoLevelConfig = AutoLevelConfiguration.GetDefault();
            return Task.FromResult(new RFmxAutoLevelConfiguration()
            {
                Enabled = defaultAutoLevelConfig.Enabled,
                MeasurementInterval = defaultAutoLevelConfig.MeasurementInterval_s
            });
        }

        public override Task<Empty> Close(RFmxSession request, ServerCallContext context)
        {
            var handle = (IntPtr)request.Handle;
            var instr = sessionMap[handle];
            instr.Close();
            if (instr.IsDisposed)
                sessionMap.Remove(handle);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> ForceClose(RFmxSession request, ServerCallContext context)
        {
            var handle = (IntPtr)request.Handle;
            var instr = sessionMap[handle];
            instr.ForceClose();
            sessionMap.Remove(handle);
            return Task.FromResult(new Empty());
        }
        #endregion

        #region NR
        private RFmxNRMX GetNRSignalConfiguration(RFmxInstrMX instr, string signalName)
        {
            if (string.IsNullOrEmpty(signalName))
                return instr.GetNRSignalConfiguration();
            return instr.GetNRSignalConfiguration(signalName);
        }

        public override Task<RFmxNRComponentCarrierConfiguration> NRGetDefaultComponentCarrierConfiguration(Empty request, ServerCallContext context)
        {
            var componentCarrierConfig = RFmxNR.ComponentCarrierConfiguration.GetDefault();
            return Task.FromResult(new RFmxNRComponentCarrierConfiguration()
            {
                BandwidthHz = componentCarrierConfig.Bandwidth_Hz,
                CellId = componentCarrierConfig.CellId,
                PuschDmrsAdditionalPositions = componentCarrierConfig.PuschDmrsAdditionalPositions,
                PuschDmrsConfigurationType = (RFmxNRComponentCarrierConfiguration.Types.RFmxNRMXPuschDmrsConfigurationType)componentCarrierConfig.PuschDmrsConfigurationType,
                PuschDmrsDuration = (RFmxNRComponentCarrierConfiguration.Types.RFmxNRMXPuschDmrsDuration)componentCarrierConfig.PuschDmrsDuration,
                PuschDmrsTypeAPosition = componentCarrierConfig.PuschDmrsTypeAPosition,
                PuschMappingType = (RFmxNRComponentCarrierConfiguration.Types.RFmxNRMXPuschMappingType)componentCarrierConfig.PuschMappingType,
                PuschModulationType = (RFmxNRComponentCarrierConfiguration.Types.RFmxNRMXPuschModulationType)componentCarrierConfig.PuschModulationType,
                PuschNumberOfResourceBlocks = componentCarrierConfig.PuschNumberOfResourceBlocks,
                PuschResourceBlockOffset = componentCarrierConfig.PuschResourceBlockOffset,
                PuschSlotAllocation = componentCarrierConfig.PuschSlotAllocation,
                PuschSymbolAllocation = componentCarrierConfig.PuschSymbolAllocation,
                PuschTransformPrecodingEnabled = Convert.ToBoolean(componentCarrierConfig.PuschTransformPrecodingEnabled),
                SubcarrierSpacingHz = componentCarrierConfig.SubcarrierSpacing_Hz
            });
        }

        public override Task<RFmxNRStandardConfiguration> NRGetDefaultStandardConfiguration(RFmxSession request, ServerCallContext context)
        {
            var standardConfig = RFmxNR.StandardConfiguration.GetDefault();
            var response = new RFmxNRStandardConfiguration()
            {
                Session = request,
                AutoResourceBlockDetectionEnabled = Convert.ToBoolean(standardConfig.AutoResourceBlockDetectionEnabled),
                Band = standardConfig.Band,
                DownlinkTestModel = (RFmxNRStandardConfiguration.Types.RFmxNRMXDownlinkTestModel)standardConfig.DownlinkTestModel,
                DownlinkTestModelDuplexScheme = (RFmxNRStandardConfiguration.Types.RFmxNRMXDownlinkTestModelDuplexScheme)standardConfig.DownlinkTestModelDuplexScheme,
                FrequencyRange = (RFmxNRStandardConfiguration.Types.RFmxNRMXFrequencyRange)standardConfig.FrequencyRange,
                LinkDirection = (RFmxNRStandardConfiguration.Types.RFmxNRMXLinkDirection)standardConfig.LinkDirection,
                SelectorString = "",
                SignalName = ""
            };
            foreach (var componentCarrierConfig in standardConfig.ComponentCarrierConfigurations)
                response.ComponentCarrierConfigurations.Add(new RFmxNRComponentCarrierConfiguration()
                {
                    BandwidthHz = componentCarrierConfig.Bandwidth_Hz,
                    CellId = componentCarrierConfig.CellId,
                    PuschDmrsAdditionalPositions = componentCarrierConfig.PuschDmrsAdditionalPositions,
                    PuschDmrsConfigurationType = (RFmxNRComponentCarrierConfiguration.Types.RFmxNRMXPuschDmrsConfigurationType)componentCarrierConfig.PuschDmrsConfigurationType,
                    PuschDmrsDuration = (RFmxNRComponentCarrierConfiguration.Types.RFmxNRMXPuschDmrsDuration)componentCarrierConfig.PuschDmrsDuration,
                    PuschDmrsTypeAPosition = componentCarrierConfig.PuschDmrsTypeAPosition,
                    PuschMappingType = (RFmxNRComponentCarrierConfiguration.Types.RFmxNRMXPuschMappingType)componentCarrierConfig.PuschMappingType,
                    PuschModulationType = (RFmxNRComponentCarrierConfiguration.Types.RFmxNRMXPuschModulationType)componentCarrierConfig.PuschModulationType,
                    PuschNumberOfResourceBlocks = componentCarrierConfig.PuschNumberOfResourceBlocks,
                    PuschResourceBlockOffset = componentCarrierConfig.PuschResourceBlockOffset,
                    PuschSlotAllocation = componentCarrierConfig.PuschSlotAllocation,
                    PuschSymbolAllocation = componentCarrierConfig.PuschSymbolAllocation,
                    PuschTransformPrecodingEnabled = Convert.ToBoolean(componentCarrierConfig.PuschTransformPrecodingEnabled),
                    SubcarrierSpacingHz = componentCarrierConfig.SubcarrierSpacing_Hz
                });
            return Task.FromResult(response);
        }

        public override Task<RFmxNRModAccConfiguration> NRGetDefaultModAccConfiguration(RFmxSession request, ServerCallContext context)
        {
            var modAccConfig = RFmxNR.ModAccConfiguration.GetDefault();
            return Task.FromResult(new RFmxNRModAccConfiguration()
            {
                Session = request,
                AveragingCount = modAccConfig.AveragingCount,
                AveragingEnabled = Convert.ToBoolean(modAccConfig.AveragingEnabled),
                EvmUnit = (RFmxNRModAccConfiguration.Types.RFmxNRMXModAccEvmUnit)modAccConfig.EvmUnit,
                MeasurementLength = modAccConfig.MeasurementLength,
                MeasurementLengthUnit = (RFmxNRModAccConfiguration.Types.RFmxNRMXModAccMeasurementLengthUnit)modAccConfig.MeasurementLengthUnit,
                MeasurementOffset = modAccConfig.MeasurementOffset,
                SelectorString = "",
                SignalName= ""
            });
        }

        public override Task<RFmxNRAcpConfiguration> NRGetDefaultAcpConfiguration(RFmxSession request, ServerCallContext context)
        {
            var acpConfig = RFmxNR.AcpConfiguration.GetDefault();
            return Task.FromResult(new RFmxNRAcpConfiguration()
            {
                Session = request,
                AveragingCount = acpConfig.AveragingCount,
                AveragingEnabled = Convert.ToBoolean(acpConfig.AveragingEnabled),
                AveragingType = (RFmxNRAcpConfiguration.Types.RFmxNRMXAcpAveragingType)acpConfig.AveragingType,
                MeasurementMethod = (RFmxNRAcpConfiguration.Types.RFmxNRMXAcpMeasurementMethod)acpConfig.MeasurementMethod,
                NoiseCompensationEnabled = Convert.ToBoolean(acpConfig.NoiseCompensationEnabled),
                NumberOfEndcOffsets = acpConfig.NumberOfEndcOffsets,
                NumberOfEutraOffsets = acpConfig.NumberOfEutraOffsets,
                NumberOfNrOffsets = acpConfig.NumberOfNrOffsets,
                NumberOfUtraOffsets = acpConfig.NumberOfUtraOffsets,
                SelectorString = "",
                SignalName = "",
                SweepTimeAuto = Convert.ToBoolean(acpConfig.SweepTimeAuto),
                SweepTimeIntervalS = acpConfig.SweepTimeInterval_s
            });
        }

        public override Task<Empty> NRConfigureCommon(RFmxCommonConfiguration request, ServerCallContext context)
        {
            var instr = sessionMap[(IntPtr)request.Session.Handle];
            var nr = GetNRSignalConfiguration(instr, request.SignalName);
            var commonConfig = new CommonConfiguration()
            {
                SelectedPorts = request.SelectedPorts,
                CenterFrequency_Hz = request.CenterFrequencyHz,
                ReferenceLevel_dBm = request.ReferenceLevelDBm,
                ExternalAttenuation_dB = request.ExternalAttenuationDB,
                TriggerEnabled = request.TriggerEnabled,
                DigitalTriggerSource = request.DigitalTriggerSource,
                TriggerDelay_s = request.TriggerDelayS
            };
            RFmxNR.ConfigureCommon(nr, commonConfig, request.SelectorString);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> NRConfigureStandard(RFmxNRStandardConfiguration request, ServerCallContext context)
        {
            var instr = sessionMap[(IntPtr)request.Session.Handle];
            var nr = GetNRSignalConfiguration(instr, request.SignalName);
            var standardConfig = new RFmxNR.StandardConfiguration()
            {
                AutoResourceBlockDetectionEnabled = (RFmxNRMXAutoResourceBlockDetectionEnabled)(request.AutoResourceBlockDetectionEnabled ? 1 : 0),
                Band = request.Band,
                ComponentCarrierConfigurations = new RFmxNR.ComponentCarrierConfiguration[request.ComponentCarrierConfigurations.Count],
                DownlinkTestModel = (RFmxNRMXDownlinkTestModel)request.DownlinkTestModel,
                DownlinkTestModelDuplexScheme = (RFmxNRMXDownlinkTestModelDuplexScheme)request.DownlinkTestModelDuplexScheme,
                FrequencyRange = (RFmxNRMXFrequencyRange)request.FrequencyRange,
                LinkDirection = (RFmxNRMXLinkDirection)request.LinkDirection
            };
            for (int i = 0; i < request.ComponentCarrierConfigurations.Count; i++)
            {
                var requestComponentCarrierConfiguration = request.ComponentCarrierConfigurations[i];
                standardConfig.ComponentCarrierConfigurations[i] = new RFmxNR.ComponentCarrierConfiguration()
                {
                    Bandwidth_Hz = requestComponentCarrierConfiguration.BandwidthHz,
                    CellId = requestComponentCarrierConfiguration.CellId,
                    PuschDmrsAdditionalPositions = requestComponentCarrierConfiguration.PuschDmrsAdditionalPositions,
                    PuschDmrsConfigurationType = (RFmxNRMXPuschDmrsConfigurationType)requestComponentCarrierConfiguration.PuschDmrsConfigurationType,
                    PuschDmrsDuration = (RFmxNRMXPuschDmrsDuration)requestComponentCarrierConfiguration.PuschDmrsDuration,
                    PuschDmrsTypeAPosition = requestComponentCarrierConfiguration.PuschDmrsTypeAPosition,
                    PuschMappingType = (RFmxNRMXPuschMappingType)requestComponentCarrierConfiguration.PuschMappingType,
                    PuschModulationType = (RFmxNRMXPuschModulationType)requestComponentCarrierConfiguration.PuschModulationType,
                    PuschNumberOfResourceBlocks = requestComponentCarrierConfiguration.PuschNumberOfResourceBlocks,
                    PuschResourceBlockOffset = requestComponentCarrierConfiguration.PuschResourceBlockOffset,
                    PuschSlotAllocation = requestComponentCarrierConfiguration.PuschSlotAllocation,
                    PuschSymbolAllocation = requestComponentCarrierConfiguration.PuschSymbolAllocation,
                    PuschTransformPrecodingEnabled = (RFmxNRMXPuschTransformPrecodingEnabled)(requestComponentCarrierConfiguration.PuschTransformPrecodingEnabled ? 1 : 0),
                    SubcarrierSpacing_Hz = requestComponentCarrierConfiguration.SubcarrierSpacingHz
                };
            }
            RFmxNR.ConfigureStandard(nr, standardConfig, request.SelectorString);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> NRConfigureModAcc(RFmxNRModAccConfiguration request, ServerCallContext context)
        {
            var instr = sessionMap[(IntPtr)request.Session.Handle];
            var nr = GetNRSignalConfiguration(instr, request.SignalName);
            var modAccConfiguration = new RFmxNR.ModAccConfiguration()
            {
                AveragingCount = request.AveragingCount,
                AveragingEnabled = (RFmxNRMXModAccAveragingEnabled)(request.AveragingEnabled ? 1 : 0),
                EvmUnit = (RFmxNRMXModAccEvmUnit)request.EvmUnit,
                MeasurementLength = request.MeasurementLength,
                MeasurementLengthUnit = (RFmxNRMXModAccMeasurementLengthUnit)request.MeasurementLengthUnit,
                MeasurementOffset = request.MeasurementOffset
            };
            RFmxNR.ConfigureModacc(nr, modAccConfiguration, request.SelectorString);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> NRConfigureAcp(RFmxNRAcpConfiguration request, ServerCallContext context)
        {
            var instr = sessionMap[(IntPtr)request.Session.Handle];
            var nr = GetNRSignalConfiguration(instr, request.SignalName);
            var acpConfig = new RFmxNR.AcpConfiguration()
            {
                AveragingCount = request.AveragingCount,
                AveragingEnabled = (RFmxNRMXAcpAveragingEnabled)(request.AveragingEnabled ? 1 : 0),
                AveragingType = (RFmxNRMXAcpAveragingType)request.AveragingType,
                MeasurementMethod = (RFmxNRMXAcpMeasurementMethod)request.MeasurementMethod,
                NoiseCompensationEnabled = (RFmxNRMXAcpNoiseCompensationEnabled)(request.NoiseCompensationEnabled ? 1 : 0),
                NumberOfEndcOffsets = request.NumberOfEndcOffsets,
                NumberOfEutraOffsets = request.NumberOfEutraOffsets,
                NumberOfNrOffsets = request.NumberOfNrOffsets,
                NumberOfUtraOffsets = request.NumberOfUtraOffsets,
                SweepTimeAuto = (RFmxNRMXAcpSweepTimeAuto)(request.SweepTimeAuto ? 1 : 0),
                SweepTimeInterval_s = request.SweepTimeIntervalS
            };
            RFmxNR.ConfigureAcp(nr, acpConfig, request.SelectorString);
            return Task.FromResult(new Empty());
        }

        public override Task<Empty> NRSelectAndInitiateMeasurements(RFmxNRMeasurementConfiguration request, ServerCallContext context)
        {
            var instr = sessionMap[(IntPtr)request.Session.Handle];
            var nr = GetNRSignalConfiguration(instr, request.SignalName);
            RFmxNRMXMeasurementTypes[] measurements = new RFmxNRMXMeasurementTypes[request.Measurements.Count];
            for (int i = 0; i < request.Measurements.Count; i++)
                measurements[i] = (RFmxNRMXMeasurementTypes)request.Measurements[i];
            AutoLevelConfiguration autoLevelConfig = new AutoLevelConfiguration()
            {
                Enabled = request.AutoLevelConfiguration.Enabled,
                MeasurementInterval_s = request.AutoLevelConfiguration.MeasurementInterval
            };
            RFmxNR.SelectAndInitiateMeasurements(nr, measurements, autoLevelConfig, request.EnableTraces, request.SelectorString, request.ResultName);
            return Task.FromResult(new Empty());
        }

        public override Task<RFmxNRModAccResults> NRFetchModAccResults(RFmxResultQuery request, ServerCallContext context)
        {
            var instr = sessionMap[(IntPtr)request.Session.Handle];
            var nr = GetNRSignalConfiguration(instr, request.SignalName);
            var modAccResults = RFmxNR.FetchModAcc(nr, request.SelectorString);
            var response = new RFmxNRModAccResults();
            foreach (var result in modAccResults.ComponentCarrierResults)
                response.ComponentCarrierResults.Add(new RFmxNRModAccComponentCarrierResults()
                {
                    MaxPeakCompositeEvm = result.MaxPeakCompositeEvm,
                    MeanFrequencyErrorHz = result.MeanFrequencyError_Hz,
                    MeanRmsCompositeEvm = result.MeanRmsCompositeEvm,
                    PeakCompositeEvmSlotIndex = result.PeakCompositeEvmSlotIndex,
                    PeakCompositeEvmSubcarrierIndex = result.PeakCompositeEvmSubcarrierIndex,
                    PeakCompositeEvmSymbolIndex = result.PeakCompositeEvmSymbolIndex,
                });
            return Task.FromResult(response);
        }

        public override Task<RFmxNRAcpResults> NRFetchAcpResults(RFmxResultQuery request, ServerCallContext context)
        {
            var instr = sessionMap[(IntPtr)request.Session.Handle];
            var nr = GetNRSignalConfiguration(instr, request.SignalName);
            var acpResults = RFmxNR.FetchAcp(nr, request.SelectorString);
            var response = new RFmxNRAcpResults();
            foreach (var componentCarrierResult in acpResults.ComponentCarrierResults)
                response.ComponentCarrierResults.Add(new RFmxNRAcpComponentCarrierResults()
                {
                    AbsolutePowerDBm = componentCarrierResult.AbsolutePower_dBm,
                    RelativePowerDB = componentCarrierResult.RelativePower_dB
                });
            foreach (var offsetResult in acpResults.OffsetResults)
                response.OffsetResults.Add(new RFmxNRAcpOffsetResults()
                {
                    FrequencyHz = offsetResult.Frequency_Hz,
                    IntegrationBandwidthHz = offsetResult.IntegrationBandwidth_Hz,
                    LowerAbsolutePowerDBm = offsetResult.LowerAbsolutePower_dBm,
                    LowerRelativePowerDB = offsetResult.LowerRelativePower_dB,
                    UpperAbsolutePowerDBm = offsetResult.UpperAbsolutePower_dBm,
                    UpperRelativePowerDB = offsetResult.UpperRelativePower_dB
                });
            return Task.FromResult(response);
        }
        #endregion
    }
}
