using System;

namespace Domain.Payload {
    public class AppSettings {
        public int SubscriptionProfileId { get; set; }
        public int TechnologyProfileId { get; set; }
        public string PolicyBatchMailSubject { get; set; }
        public string PolicyBatchMailBody { get; set; }
        public string PolicyBatchTableHeader { get; set; }
        public string PolicyBatchTableRow { get; set; }
        public string PolicyBatchErrorMailSubject { get; set; }
        public string PolicyBatchErrorMailBody { get; set; }
        public string PolicyBatchErrorTableHeader { get; set; }
        public string PolicyBatchErrorTableRow { get; set; }
        public string SendAlertMailFinishedSubject { get; set; }
        public string SendAlertMailFinishedBody { get; set; }
        public int BacenDailyCode { get; set; }
        public int BacenMonthlyCode { get; set; }
    }
}
