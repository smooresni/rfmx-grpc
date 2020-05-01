using Grpc.Core;
using Grpc.Core.Interceptors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NationalInstruments.ApplicationsEngineering.Services
{
    public class GrpcInterceptor : Interceptor
    {
        private class LogEntryType
        {
            private readonly string logEntryType;

            public static LogEntryType ProcedureCall
            {
                get
                {
                    return new LogEntryType("CALL");
                }
            }

            public static LogEntryType ProcedureReturn
            {
                get
                {
                    return new LogEntryType("RETURN");
                }
            }

            public static LogEntryType Exception
            {
                get
                {
                    return new LogEntryType("ERROR");
                }
            }

            private LogEntryType(string logEntryType)
            {
                this.logEntryType = logEntryType;
            }

            public override string ToString()
            {
                return logEntryType;
            }
        }

        private void LogToConsole(LogEntryType entryType, string message)
        {
            Console.WriteLine($"[{DateTime.Now.ToString("HH:mm:ss.fff")}] <{entryType}> {message}");
        }

        private Dictionary<string, string> GetMutablePropertyValuePairs(object obj)
        {
            var properties = obj.GetType().GetProperties();
            var writeableProperties = properties.Where(property => property.CanWrite);
            return writeableProperties.ToDictionary(
                property => { 
                    return property.Name; 
                }, 
                property => { 
                    string quotes = property.PropertyType.Equals(typeof(string)) ? "\"" : ""; 
                    return $"{quotes}{property.GetValue(obj)}{quotes}"; 
                });
        }

        public override Task<TResponse> UnaryServerHandler<TRequest, TResponse>(TRequest request, ServerCallContext context, UnaryServerMethod<TRequest, TResponse> continuation)
        {
            Task<TResponse> response = null;
            try
            {
                var paramValuePairs = GetMutablePropertyValuePairs(request).Select(propertyValue => { return $"{propertyValue.Key}={propertyValue.Value}"; });
                string paramValueString = string.Join(", ", paramValuePairs);
                LogToConsole(LogEntryType.ProcedureCall, $"RFmxService.{continuation.Method.Name}({paramValueString})");

                response = base.UnaryServerHandler(request, context, continuation);

                var returnValuePairs = GetMutablePropertyValuePairs(response.Result).Select(propertyValue => { return $"{propertyValue.Key}={propertyValue.Value}"; });
                string returnValueString = string.Join(", ", returnValuePairs);
                LogToConsole(LogEntryType.ProcedureReturn, $"{"{"}{returnValueString}{"}"}");
            }
            catch (RpcException)
            {
                throw; // prevent this type of exception from being caught by the more generic exception
            }
            catch (Exception e)
            {
                string message = $"{e.GetType().Name}: {e.Message}";
                LogToConsole(LogEntryType.Exception, message);
                throw new RpcException(new Status(StatusCode.Unknown, message));
            }
            return response;
        }
    }
}
