using Domain.Payload;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Domain.Util.Log {
    public class BaseLogger {
        private readonly ILogger<dynamic> logger;

        public BaseLogger(ILogger<dynamic> logger) {
            this.logger = logger;
        }

        private Dictionary<string, object> ToDictionary(LoggerComplement complement) {
            return new Dictionary<string, object> { ["actiontype"] = (int?)complement?.ActionType, ["transactionid"] = complement?.TransactionId, ["userid"] = complement?.UserId };
        }

        public void LogTrace(MethodBase method, string message, LoggerComplement complement = null) {
            using (logger.BeginScope(this.ToDictionary(complement))) {
                logger.LogTrace($"{method.Name}:: {message}");
            }
        }

        public void LogTrace(MethodBase method, string message, object methodParams, LoggerComplement complement = null) {
            using (logger.BeginScope(this.ToDictionary(complement))) {
                logger.LogTrace($"{method.Name}:: {message}:: {Newtonsoft.Json.JsonConvert.SerializeObject(methodParams)}");
            }
        }

        public void LogDebug(MethodBase method, string message, LoggerComplement complement = null) {
            using (logger.BeginScope(this.ToDictionary(complement))) {
                logger.LogDebug($"{method.Name}:: {message}");
            }
        }

        public void LogDebug(MethodBase method, string message, object data, LoggerComplement complement = null) {
            using (logger.BeginScope(this.ToDictionary(complement))) {
                logger.LogDebug($"{method.Name}:: {message}:: Data: {Newtonsoft.Json.JsonConvert.SerializeObject(data)}");
            }
        }

        public void LogError(MethodBase method, object data, Exception e, LoggerComplement complement = null) {
            using (logger.BeginScope(this.ToDictionary(complement))) {
                logger.LogError(e, $"{method.Name}:: {Newtonsoft.Json.JsonConvert.SerializeObject(data)}");
            }
        }
    }
}
