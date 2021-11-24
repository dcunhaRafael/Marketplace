using Microsoft.Extensions.Logging;
using System;

namespace Domain.Entities {
    public class AppServiceLog {
        public long? AppServiceLogId { get; set; }
        public int? AppServiceId { get; set; }
        public DateTime InclusionDate { get; set; }
        public LogLevel LogLevel { get; set; }
        public string Message { get; set; }
    }
}
