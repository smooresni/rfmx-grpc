import grpc
from google.protobuf.empty_pb2 import Empty
import RFmx_pb2
import RFmx_pb2_grpc
import Rfsg_pb2
import Rfsg_pb2_grpc


def run():
    # NOTE(gRPC Python Team): .close() is possible on a channel and should be
    # used in circumstances in which the with statement does not fit the needs
    # of the code.
    with grpc.insecure_channel('semoore-pxi:50051') as channel:
        rfmx = RFmx_pb2_grpc.RFmxServiceStub(channel)
        rfsg = Rfsg_pb2_grpc.NIRfsgServiceStub(channel)

        # rfsg_session = rfsg.Initialize(Rfsg_pb2.RfsgServiceResource(Name="VST1_01", Reset=False, IdQuery=True))
        # rfsg_instr_config = rfsg.GetDefaultInstrumentConfiguration(rfsg_session)
        # rfsg.ConfigureInstrument(rfsg_instr_config)
        # rfsg.Initiate(rfsg_session)
        # input("Press any key to abort generation..")
        # rfsg.CloseInstrument(rfsg_session)
        rfmx_session = rfmx.Initialize(RFmx_pb2.RFmxResource(Name='VST1_02', OptionString=''))

        nr_default_instr_config = rfmx.GetDefaultInstrumentConfiguration(rfmx_session)
        nr_default_common_config = rfmx.GetDefaultCommonConfiguration(rfmx_session)
        nr_default_standard_config = rfmx.NRGetDefaultStandardConfiguration(rfmx_session)
        nr_default_modacc_config = rfmx.NRGetDefaultModAccConfiguration(rfmx_session)
        nr_default_acp_config = rfmx.NRGetDefaultAcpConfiguration(rfmx_session)

        rfmx.ConfigureInstrument(nr_default_instr_config)
        rfmx.NRConfigureCommon(nr_default_common_config)
        rfmx.NRConfigureStandard(nr_default_standard_config)
        rfmx.NRConfigureModAcc(nr_default_modacc_config)
        rfmx.NRConfigureAcp(nr_default_acp_config)

        default_auto_level_config = rfmx.GetDefaultAutoLevelConfiguration(Empty())
        nr_measurement_config = RFmx_pb2.RFmxNRMeasurementConfiguration(Session=rfmx_session, AutoLevelConfiguration=default_auto_level_config)
        nr_measurement_config.Measurements.append(RFmx_pb2.RFmxNRMeasurementConfiguration.MODACC)

        rfmx.NRSelectAndInitiateMeasurements(nr_measurement_config)
        nr_modacc_results = rfmx.NRFetchModAccResults(RFmx_pb2.RFmxResultQuery(Session=rfmx_session))
        print(nr_modacc_results)
        rfmx.ForceClose(rfmx_session)


if __name__ == '__main__':
    run()
