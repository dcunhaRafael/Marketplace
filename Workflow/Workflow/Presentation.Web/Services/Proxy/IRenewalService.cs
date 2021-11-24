using Domain.Payload;
using System.Collections.Generic;

namespace Presentation.Web.Services.Proxy {
    public interface IRenewalService {
        IList<PolicyBatch> ListBatches(PolicyBatch filters);
        PolicyBatch GetBatch(int policyBatchId);
        IList<PolicyRenovation> ListBatchItems(int policyBatchId);
        PolicyRenovation GetPolicy(int policyRenovationId);
    }
}
