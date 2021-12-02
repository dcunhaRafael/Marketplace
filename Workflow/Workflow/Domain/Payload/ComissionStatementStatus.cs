using System;

namespace Domain.Payload {
    public class ComissionStatementStatus  {

        public ComissionStatementStatus() {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LegacyCode { get; set; }
        public string BackgroundColor { get; set; }
        public string TextColor { get; set; }
    }
}
