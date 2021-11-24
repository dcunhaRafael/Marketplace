using System;

namespace Domain.Payload {
    public class SelicCorrection {
        public DateTime Date { get; set; }
        public decimal ValueFull { get; set; }
        public int WorkDays { get; set; }
        public int CorrectionDays { get; set; }
        public decimal ValueCorrection { get; set; }
    }
}
