using Domain.Enumerators;
using System;

namespace Domain.Entities {
    public class AppService: BaseEntity {
        public int? AppServiceId { get; set; }
        public string Name { get; set; }
        public int Timeout { get; set; }
        public DateTime? KeepAlive { get; set; }
        public ExecutionStatusEnum? ExecutionStatus { get; set; }
        public DateTime? ExecutionDate { get; set; }
        public string ExecutionMessage { get; set; }
        public string ExecutionData { get; set; }
    }
}
