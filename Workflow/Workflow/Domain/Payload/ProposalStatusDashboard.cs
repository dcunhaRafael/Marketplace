using Domain.Enumerators;

namespace Domain.Payload {
    public class ProposalStatusDashboard {

        public string Name { get; set; }
        public string ChartLegendName { get; set; }
        public string ChartBackgroundColor { get; set; }
        public string ChartBorderColor { get; set; }
        public int Count{ get; set; }
    }
}
