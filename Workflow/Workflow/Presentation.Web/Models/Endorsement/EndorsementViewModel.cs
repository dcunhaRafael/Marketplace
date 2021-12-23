using Domain.Payload;
using System.Collections.Generic;

namespace Presentation.Web.Models.Endorsement {
    public class EndorsementViewModel {
        public EndorsementViewModel() {
            PolicyEndorsements = new List<PolicyRenovation>();
        }

        public long? PolicyNumber { get; set; }
        public IList<PolicyRenovation> PolicyEndorsements { get; set; }


        public bool IsEditable { get; set; }
    }
}
