

using Microsoft.Extensions.Logging;
using Serilog.Events;
using System;
using System.Threading;
using System.Threading.Tasks;


namespace InsuranceApi.Domain.Common.Log {
    public class Log : ILogger {


        private readonly Serilog.ILogger _logger;

        public Log(Serilog.ILogger logger) {
            _logger = logger;
        }


        private async Task InsertLog(LogEventLevel logLevel, string message, Exception exception = null, object param = null) {

            await Task.Factory.StartNew(() =>
            {
                //var log = new LogApplication
                //{
                //    Message = message ?? exception?.Message,
                //    Exception = exception.ToBetterString(),
                //    Date = DateTime.Now,
                //    Level = logLevel == LogEventLevel.Information ? "INFO" : logLevel.ToString().ToUpper(),
                //    Logger = "LoggerHub",
                //    UserApplicaton = "Hub",
                //    Thread = Thread.CurrentThread.ManagedThreadId.ToString()
                //};

                //_logApplicationRepository.InsertSync(log);

                message = string.IsNullOrEmpty(message) ? $"{exception?.Message} " : $"{message} ";


                _logger.Write(logLevel, exception, message, param);
            }, TaskCreationOptions.RunContinuationsAsynchronously);
        }
        public async Task Information(string message, Exception exception = null, object param = null) {
            //don't log thread abort exception
            if (exception is ThreadAbortException)
                return;

            await InsertLog(LogEventLevel.Information, message, exception);
        }
        public async Task Warning(string message, Exception exception = null, object param = null) {
            
            if (exception is ThreadAbortException)
                return;

            await InsertLog(LogEventLevel.Warning, message, exception);
        }
        public async Task Error(string message, Exception exception = null, object param = null) {
            //don't log thread abort exception
            if (exception is ThreadAbortException)
                return;

            await InsertLog(LogEventLevel.Error, message, exception);
        }

        void ILogger.Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter) {
            throw new NotImplementedException();
        }

        public bool IsEnabled(LogLevel logLevel) {
            throw new NotImplementedException();
        }

        public IDisposable BeginScope<TState>(TState state) {
            throw new NotImplementedException();
        }
    }
}
