using Domain.Enumerators;
using System;
using System.Collections.Generic;

namespace Domain.Payload {
    public class ProposalOccurrence {
        public ProposalOccurrence() {
            this.Proposal = new ProposalHub();
            this.OccurrenceType = new OccurrenceType();
            this.RefusalReason = new RefusalReason();
            this.InclusionUser = new User();
            this.LastChangeUser = new User();
            this.Documents = new List<ProposalOccurrenceDocument>();
            this.Histories = new List<ProposalOccurrenceHistory>();
        }

        public long? ProposalOccurrenceId { get; set; }
        public int? ProposalId { get; set; }
        public int? OccurrenceTypeId { get; set; }
        public OccurrenceStatusEnum? OccurrenceStatus { get; set; }
        public DateTime? ApprovalRefusalDate { get; set; }
        public int? RefusalReasonId { get; set; }
        public string UserComments { get; set; }
        public int DocumentTypeCount { get; set; }
        public int DocumentTypePendingCount { get; set; }
        public ProposalHub Proposal { get; set; }
        public OccurrenceType OccurrenceType { get; set; }
        public RefusalReason RefusalReason { get; set; }
        public DateTime? InclusionDate { get; set; }
        public User InclusionUser { get; set; }
        public DateTime? LastChangeDate { get; set; }
        public User LastChangeUser { get; set; }
        public IList<ProposalOccurrenceDocument> Documents { get; set; }
        public IList<ProposalOccurrenceHistory> Histories { get; set; }
        public bool IsChecked { get; set; }

        public double? OccurrenceDays {
            get {
                double? days = null;
                if (InclusionDate != null) {
                    days = (DateTime.Now.Date - InclusionDate.Value.Date).TotalDays;
                }
                return days;
            }
        }
        public string SignalingTimeout {
            get {

                if (OccurrenceDays != null) {
                    if (OccurrenceDays <= (OccurrenceType.NormalSignalingTimeout ?? 0)) {
                        return "NormalSignaling";
                        //< span class="fa-fw fas fa-traffic-light verde botaoTabelaColor" title="Normal: @(Model.Ocorrencias[i].TotalDays) dia(s)"></span>
                    } else if (OccurrenceDays > (OccurrenceType.NormalSignalingTimeout ?? 0) && OccurrenceDays <= (OccurrenceType.WarningSignalingTimeout ?? 0)) {
                        return "WarningSignaling";
                        //<span class="fa-fw fas fa-traffic-light amarelo botaoTabelaColor" title="Alerta: @(Model.Ocorrencias[i].TotalDays) dia(s)"></span>
                    } else {
                        return "CriticalSignaling";
                        //< span class= "fa-fw fas fa-traffic-light vermelho botaoTabelaColor" title = "Crítico: @(Model.Ocorrencias[i].TotalDays) dia(s)" ></ span >
                    }
                }
                return "HiddenSignaling";
            }
        }


    }
}
