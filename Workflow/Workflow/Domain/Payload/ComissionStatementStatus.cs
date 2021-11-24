using System;

namespace Domain.Payload {
    public class ComissionStatementStatus  {

        public ComissionStatementStatus() {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LegacyCode { get; set; }

        public string BackgroundColor {
            get {
                var random = new Random(this.Id);
                var color = string.Format("#{0:X6}", random.Next(0x1000000)); // = "#A197B9"
                return color;
            }
        }
    }
}
