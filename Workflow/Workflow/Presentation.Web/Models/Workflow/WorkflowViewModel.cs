using Domain.Payload;
using System.Collections.Generic;

namespace Presentation.Web.Models.Workflow {
    public class WorkflowViewModel {
        public WorkflowViewModel() {
            Filters = new ProposalOccurrenceFilters();
            Occurrences = new List<ProposalOccurrence>();
            ProductList = new List<Product>();
            CoverageList = new List<Coverage>();
            OccurrenceTypeList = new List<OccurrenceType>();
            LiberationUsers = new List<User>();
            Histories = new List<ProposalOccurrenceHistory>();
            Documents = new List<ProposalOccurrenceDocument>();
            RefusalReasons = new List<RefusalReason>();
        }

        public ProposalOccurrenceFilters Filters { get; set; }
        public IList<ProposalOccurrence> Occurrences { get; set; }

        public IList<Product> ProductList { get; set; }
        public IList<Coverage> CoverageList { get; set; }
        public IList<OccurrenceType> OccurrenceTypeList { get; set; }
        public IList<User> LiberationUsers { get; set; }
        public IList<ProposalOccurrenceHistory> Histories { get; set; }
        public IList<ProposalOccurrenceDocument> Documents { get; set; }
        public IList<RefusalReason> RefusalReasons { get; set; }

        public long ProposalOccurrenceId { get; set; }
        public int ProposalNumber { get; set; }
        public int ForwardUserId { get; set; }
        public string UserComments { get; set; }
        public string LoggedUserName { get; set; }
        public long ProposalOccurrenceDocumentId { get; set; }
        public int DocumentTypeId { get; set; }
        public int RefusalReasonId { get; set; }

        public bool IsEditable { get; set; }
    }
}
