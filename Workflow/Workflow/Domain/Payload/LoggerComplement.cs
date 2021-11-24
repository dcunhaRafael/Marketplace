using Domain.Enumerators;

namespace Domain.Payload {
    public class LoggerComplement {
        public int? UserId { get; set; }
        public long? TransactionId { get; set; }
        public ActionTypeEnum? ActionType { get; set; }
    }
}
