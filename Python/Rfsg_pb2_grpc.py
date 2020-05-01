# Generated by the gRPC Python protocol compiler plugin. DO NOT EDIT!
import grpc

import Rfsg_pb2 as Rfsg__pb2
from google.protobuf import empty_pb2 as google_dot_protobuf_dot_empty__pb2


class NIRfsgServiceStub(object):
    """Missing associated documentation comment in .proto file"""

    def __init__(self, channel):
        """Constructor.

        Args:
            channel: A grpc.Channel.
        """
        self.Initialize = channel.unary_unary(
                '/NationalInstruments.ApplicationsEngineering.Services.NIRfsgGrpc.NIRfsgService/Initialize',
                request_serializer=Rfsg__pb2.RfsgServiceResource.SerializeToString,
                response_deserializer=Rfsg__pb2.RfsgServiceSession.FromString,
                )
        self.GetDefaultInstrumentConfiguration = channel.unary_unary(
                '/NationalInstruments.ApplicationsEngineering.Services.NIRfsgGrpc.NIRfsgService/GetDefaultInstrumentConfiguration',
                request_serializer=Rfsg__pb2.RfsgServiceSession.SerializeToString,
                response_deserializer=Rfsg__pb2.RfsgServiceInstrumentConfiguration.FromString,
                )
        self.ConfigureInstrument = channel.unary_unary(
                '/NationalInstruments.ApplicationsEngineering.Services.NIRfsgGrpc.NIRfsgService/ConfigureInstrument',
                request_serializer=Rfsg__pb2.RfsgServiceInstrumentConfiguration.SerializeToString,
                response_deserializer=google_dot_protobuf_dot_empty__pb2.Empty.FromString,
                )
        self.ReadAndDownloadWaveformFromFile = channel.unary_unary(
                '/NationalInstruments.ApplicationsEngineering.Services.NIRfsgGrpc.NIRfsgService/ReadAndDownloadWaveformFromFile',
                request_serializer=Rfsg__pb2.RfsgServiceWaveformDownloadConfiguration.SerializeToString,
                response_deserializer=google_dot_protobuf_dot_empty__pb2.Empty.FromString,
                )
        self.ConfigureContinuousGeneration = channel.unary_unary(
                '/NationalInstruments.ApplicationsEngineering.Services.NIRfsgGrpc.NIRfsgService/ConfigureContinuousGeneration',
                request_serializer=Rfsg__pb2.RfsgServiceGenerationConfiguration.SerializeToString,
                response_deserializer=google_dot_protobuf_dot_empty__pb2.Empty.FromString,
                )
        self.Initiate = channel.unary_unary(
                '/NationalInstruments.ApplicationsEngineering.Services.NIRfsgGrpc.NIRfsgService/Initiate',
                request_serializer=Rfsg__pb2.RfsgServiceSession.SerializeToString,
                response_deserializer=google_dot_protobuf_dot_empty__pb2.Empty.FromString,
                )
        self.CheckGenerationStatus = channel.unary_unary(
                '/NationalInstruments.ApplicationsEngineering.Services.NIRfsgGrpc.NIRfsgService/CheckGenerationStatus',
                request_serializer=Rfsg__pb2.RfsgServiceSession.SerializeToString,
                response_deserializer=Rfsg__pb2.RfsgServiceGenerationStatus.FromString,
                )
        self.AbortGeneration = channel.unary_unary(
                '/NationalInstruments.ApplicationsEngineering.Services.NIRfsgGrpc.NIRfsgService/AbortGeneration',
                request_serializer=Rfsg__pb2.RfsgServiceTimeout.SerializeToString,
                response_deserializer=google_dot_protobuf_dot_empty__pb2.Empty.FromString,
                )
        self.CloseInstrument = channel.unary_unary(
                '/NationalInstruments.ApplicationsEngineering.Services.NIRfsgGrpc.NIRfsgService/CloseInstrument',
                request_serializer=Rfsg__pb2.RfsgServiceSession.SerializeToString,
                response_deserializer=google_dot_protobuf_dot_empty__pb2.Empty.FromString,
                )


class NIRfsgServiceServicer(object):
    """Missing associated documentation comment in .proto file"""

    def Initialize(self, request, context):
        """Missing associated documentation comment in .proto file"""
        context.set_code(grpc.StatusCode.UNIMPLEMENTED)
        context.set_details('Method not implemented!')
        raise NotImplementedError('Method not implemented!')

    def GetDefaultInstrumentConfiguration(self, request, context):
        """Missing associated documentation comment in .proto file"""
        context.set_code(grpc.StatusCode.UNIMPLEMENTED)
        context.set_details('Method not implemented!')
        raise NotImplementedError('Method not implemented!')

    def ConfigureInstrument(self, request, context):
        """Missing associated documentation comment in .proto file"""
        context.set_code(grpc.StatusCode.UNIMPLEMENTED)
        context.set_details('Method not implemented!')
        raise NotImplementedError('Method not implemented!')

    def ReadAndDownloadWaveformFromFile(self, request, context):
        """Missing associated documentation comment in .proto file"""
        context.set_code(grpc.StatusCode.UNIMPLEMENTED)
        context.set_details('Method not implemented!')
        raise NotImplementedError('Method not implemented!')

    def ConfigureContinuousGeneration(self, request, context):
        """Missing associated documentation comment in .proto file"""
        context.set_code(grpc.StatusCode.UNIMPLEMENTED)
        context.set_details('Method not implemented!')
        raise NotImplementedError('Method not implemented!')

    def Initiate(self, request, context):
        """Missing associated documentation comment in .proto file"""
        context.set_code(grpc.StatusCode.UNIMPLEMENTED)
        context.set_details('Method not implemented!')
        raise NotImplementedError('Method not implemented!')

    def CheckGenerationStatus(self, request, context):
        """Missing associated documentation comment in .proto file"""
        context.set_code(grpc.StatusCode.UNIMPLEMENTED)
        context.set_details('Method not implemented!')
        raise NotImplementedError('Method not implemented!')

    def AbortGeneration(self, request, context):
        """Missing associated documentation comment in .proto file"""
        context.set_code(grpc.StatusCode.UNIMPLEMENTED)
        context.set_details('Method not implemented!')
        raise NotImplementedError('Method not implemented!')

    def CloseInstrument(self, request, context):
        """Missing associated documentation comment in .proto file"""
        context.set_code(grpc.StatusCode.UNIMPLEMENTED)
        context.set_details('Method not implemented!')
        raise NotImplementedError('Method not implemented!')


def add_NIRfsgServiceServicer_to_server(servicer, server):
    rpc_method_handlers = {
            'Initialize': grpc.unary_unary_rpc_method_handler(
                    servicer.Initialize,
                    request_deserializer=Rfsg__pb2.RfsgServiceResource.FromString,
                    response_serializer=Rfsg__pb2.RfsgServiceSession.SerializeToString,
            ),
            'GetDefaultInstrumentConfiguration': grpc.unary_unary_rpc_method_handler(
                    servicer.GetDefaultInstrumentConfiguration,
                    request_deserializer=Rfsg__pb2.RfsgServiceSession.FromString,
                    response_serializer=Rfsg__pb2.RfsgServiceInstrumentConfiguration.SerializeToString,
            ),
            'ConfigureInstrument': grpc.unary_unary_rpc_method_handler(
                    servicer.ConfigureInstrument,
                    request_deserializer=Rfsg__pb2.RfsgServiceInstrumentConfiguration.FromString,
                    response_serializer=google_dot_protobuf_dot_empty__pb2.Empty.SerializeToString,
            ),
            'ReadAndDownloadWaveformFromFile': grpc.unary_unary_rpc_method_handler(
                    servicer.ReadAndDownloadWaveformFromFile,
                    request_deserializer=Rfsg__pb2.RfsgServiceWaveformDownloadConfiguration.FromString,
                    response_serializer=google_dot_protobuf_dot_empty__pb2.Empty.SerializeToString,
            ),
            'ConfigureContinuousGeneration': grpc.unary_unary_rpc_method_handler(
                    servicer.ConfigureContinuousGeneration,
                    request_deserializer=Rfsg__pb2.RfsgServiceGenerationConfiguration.FromString,
                    response_serializer=google_dot_protobuf_dot_empty__pb2.Empty.SerializeToString,
            ),
            'Initiate': grpc.unary_unary_rpc_method_handler(
                    servicer.Initiate,
                    request_deserializer=Rfsg__pb2.RfsgServiceSession.FromString,
                    response_serializer=google_dot_protobuf_dot_empty__pb2.Empty.SerializeToString,
            ),
            'CheckGenerationStatus': grpc.unary_unary_rpc_method_handler(
                    servicer.CheckGenerationStatus,
                    request_deserializer=Rfsg__pb2.RfsgServiceSession.FromString,
                    response_serializer=Rfsg__pb2.RfsgServiceGenerationStatus.SerializeToString,
            ),
            'AbortGeneration': grpc.unary_unary_rpc_method_handler(
                    servicer.AbortGeneration,
                    request_deserializer=Rfsg__pb2.RfsgServiceTimeout.FromString,
                    response_serializer=google_dot_protobuf_dot_empty__pb2.Empty.SerializeToString,
            ),
            'CloseInstrument': grpc.unary_unary_rpc_method_handler(
                    servicer.CloseInstrument,
                    request_deserializer=Rfsg__pb2.RfsgServiceSession.FromString,
                    response_serializer=google_dot_protobuf_dot_empty__pb2.Empty.SerializeToString,
            ),
    }
    generic_handler = grpc.method_handlers_generic_handler(
            'NationalInstruments.ApplicationsEngineering.Services.NIRfsgGrpc.NIRfsgService', rpc_method_handlers)
    server.add_generic_rpc_handlers((generic_handler,))


 # This class is part of an EXPERIMENTAL API.
class NIRfsgService(object):
    """Missing associated documentation comment in .proto file"""

    @staticmethod
    def Initialize(request,
            target,
            options=(),
            channel_credentials=None,
            call_credentials=None,
            compression=None,
            wait_for_ready=None,
            timeout=None,
            metadata=None):
        return grpc.experimental.unary_unary(request, target, '/NationalInstruments.ApplicationsEngineering.Services.NIRfsgGrpc.NIRfsgService/Initialize',
            Rfsg__pb2.RfsgServiceResource.SerializeToString,
            Rfsg__pb2.RfsgServiceSession.FromString,
            options, channel_credentials,
            call_credentials, compression, wait_for_ready, timeout, metadata)

    @staticmethod
    def GetDefaultInstrumentConfiguration(request,
            target,
            options=(),
            channel_credentials=None,
            call_credentials=None,
            compression=None,
            wait_for_ready=None,
            timeout=None,
            metadata=None):
        return grpc.experimental.unary_unary(request, target, '/NationalInstruments.ApplicationsEngineering.Services.NIRfsgGrpc.NIRfsgService/GetDefaultInstrumentConfiguration',
            Rfsg__pb2.RfsgServiceSession.SerializeToString,
            Rfsg__pb2.RfsgServiceInstrumentConfiguration.FromString,
            options, channel_credentials,
            call_credentials, compression, wait_for_ready, timeout, metadata)

    @staticmethod
    def ConfigureInstrument(request,
            target,
            options=(),
            channel_credentials=None,
            call_credentials=None,
            compression=None,
            wait_for_ready=None,
            timeout=None,
            metadata=None):
        return grpc.experimental.unary_unary(request, target, '/NationalInstruments.ApplicationsEngineering.Services.NIRfsgGrpc.NIRfsgService/ConfigureInstrument',
            Rfsg__pb2.RfsgServiceInstrumentConfiguration.SerializeToString,
            google_dot_protobuf_dot_empty__pb2.Empty.FromString,
            options, channel_credentials,
            call_credentials, compression, wait_for_ready, timeout, metadata)

    @staticmethod
    def ReadAndDownloadWaveformFromFile(request,
            target,
            options=(),
            channel_credentials=None,
            call_credentials=None,
            compression=None,
            wait_for_ready=None,
            timeout=None,
            metadata=None):
        return grpc.experimental.unary_unary(request, target, '/NationalInstruments.ApplicationsEngineering.Services.NIRfsgGrpc.NIRfsgService/ReadAndDownloadWaveformFromFile',
            Rfsg__pb2.RfsgServiceWaveformDownloadConfiguration.SerializeToString,
            google_dot_protobuf_dot_empty__pb2.Empty.FromString,
            options, channel_credentials,
            call_credentials, compression, wait_for_ready, timeout, metadata)

    @staticmethod
    def ConfigureContinuousGeneration(request,
            target,
            options=(),
            channel_credentials=None,
            call_credentials=None,
            compression=None,
            wait_for_ready=None,
            timeout=None,
            metadata=None):
        return grpc.experimental.unary_unary(request, target, '/NationalInstruments.ApplicationsEngineering.Services.NIRfsgGrpc.NIRfsgService/ConfigureContinuousGeneration',
            Rfsg__pb2.RfsgServiceGenerationConfiguration.SerializeToString,
            google_dot_protobuf_dot_empty__pb2.Empty.FromString,
            options, channel_credentials,
            call_credentials, compression, wait_for_ready, timeout, metadata)

    @staticmethod
    def Initiate(request,
            target,
            options=(),
            channel_credentials=None,
            call_credentials=None,
            compression=None,
            wait_for_ready=None,
            timeout=None,
            metadata=None):
        return grpc.experimental.unary_unary(request, target, '/NationalInstruments.ApplicationsEngineering.Services.NIRfsgGrpc.NIRfsgService/Initiate',
            Rfsg__pb2.RfsgServiceSession.SerializeToString,
            google_dot_protobuf_dot_empty__pb2.Empty.FromString,
            options, channel_credentials,
            call_credentials, compression, wait_for_ready, timeout, metadata)

    @staticmethod
    def CheckGenerationStatus(request,
            target,
            options=(),
            channel_credentials=None,
            call_credentials=None,
            compression=None,
            wait_for_ready=None,
            timeout=None,
            metadata=None):
        return grpc.experimental.unary_unary(request, target, '/NationalInstruments.ApplicationsEngineering.Services.NIRfsgGrpc.NIRfsgService/CheckGenerationStatus',
            Rfsg__pb2.RfsgServiceSession.SerializeToString,
            Rfsg__pb2.RfsgServiceGenerationStatus.FromString,
            options, channel_credentials,
            call_credentials, compression, wait_for_ready, timeout, metadata)

    @staticmethod
    def AbortGeneration(request,
            target,
            options=(),
            channel_credentials=None,
            call_credentials=None,
            compression=None,
            wait_for_ready=None,
            timeout=None,
            metadata=None):
        return grpc.experimental.unary_unary(request, target, '/NationalInstruments.ApplicationsEngineering.Services.NIRfsgGrpc.NIRfsgService/AbortGeneration',
            Rfsg__pb2.RfsgServiceTimeout.SerializeToString,
            google_dot_protobuf_dot_empty__pb2.Empty.FromString,
            options, channel_credentials,
            call_credentials, compression, wait_for_ready, timeout, metadata)

    @staticmethod
    def CloseInstrument(request,
            target,
            options=(),
            channel_credentials=None,
            call_credentials=None,
            compression=None,
            wait_for_ready=None,
            timeout=None,
            metadata=None):
        return grpc.experimental.unary_unary(request, target, '/NationalInstruments.ApplicationsEngineering.Services.NIRfsgGrpc.NIRfsgService/CloseInstrument',
            Rfsg__pb2.RfsgServiceSession.SerializeToString,
            google_dot_protobuf_dot_empty__pb2.Empty.FromString,
            options, channel_credentials,
            call_credentials, compression, wait_for_ready, timeout, metadata)