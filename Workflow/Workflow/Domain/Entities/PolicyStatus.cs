namespace Domain.Entities {
    public class PolicyStatus : BaseEntity {

        public PolicyStatus() {

        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string LegacyCode { get; set; }
        public string ChartLegendName { get; set; }
        public string ChartBackgroundColor { get; set; }
        public string ChartBorderColor { get; set; }
        public string ChartTextColor { get; set; }
    }
}
