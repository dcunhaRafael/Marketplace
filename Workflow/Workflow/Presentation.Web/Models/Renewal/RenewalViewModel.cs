using Domain.Payload;
using System.Collections.Generic;

namespace Presentation.Web.Models.Renewal {
    public class RenewalViewModel {
        public RenewalViewModel() {
            Filters = new PolicyBatch();
            Results = new List<PolicyBatch>();
            CurrentBatch = new PolicyBatch();
            CurrentBatchItems = new List<PolicyRenovation>();
            CurrentDocument = new PolicyRenovation();
            RenovationIndexes = new List<UpdateIndex>();
        }

        public PolicyBatch Filters { get; set; }
        public IList<PolicyBatch> Results { get; set; }

        public PolicyBatch CurrentBatch { get; set; }
        public IList<PolicyRenovation> CurrentBatchItems { get; set; }
        public PolicyRenovation CurrentDocument { get; set; }

        public IList<UpdateIndex> RenovationIndexes { get; set; }

        public bool IsEditable { get; set; }
    }
}
