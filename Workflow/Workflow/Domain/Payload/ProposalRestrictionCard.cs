using Domain.Enumerators;
using System;

namespace Domain.Payload {
    public class ProposalRestrictionCard {
        public int ProposalRestrictionId { get; set; }
        public bool IsChecked { get; set; }
        public string RestrictionName { get; set; }
        public DateTime RestrictionDate { get; set; }
        public RestrictionStatusEnum RestrictionStatus { get; set; }
        public string BrokerName { get; set; }
        public long BrokerCpfCnpjNumber { get; set; }
        public string ProposalLinkName { get; set; }
        public int ProposalId { get; set; }
        public int ProposalNumber { get; set; }
        public DateTime ProposalDate { get; set; }
        public decimal InsuredAmount { get; set; }
        public decimal TariffPremium { get; set; }
        public DateTime StartOfTerm { get; set; }
        public DateTime EndOfTerm { get; set; }
        public string ProductName { get; set; }
        public string CoverageName { get; set; }
        public string TakerName { get; set; }
        public long TakerCpfCnpjNumber { get; set; }
        public string InsuredName { get; set; }
        public long InsuredCpfCnpjNumber { get; set; }

        public int ProductCoverageRestrictionId { get; set; }

        public int InformationTimeout { get; set; }
        public int WarningTimeout { get; set; }
        public int DangerTimeout { get; set; }

        public bool IsAutomaticRefusal { get; set; }
        public int? AutomaticRefusalReasonId { get; set; }

        public int PendingDocumentCount { get; set; }

        public double? RestrictionDays {
            get {
                double? days = null;
                if (RestrictionDate != null) {
                    days = (DateTime.Now.Date - RestrictionDate.Date).TotalDays;
                }
                return days;
            }
        }

        public string RestrictionDaysTimeoutColor {
            get {
                if (RestrictionDays != null) {
                    if (RestrictionDays <= InformationTimeout) {
                        return "transparent-color";
                    } else if (RestrictionDays > InformationTimeout && RestrictionDays <= WarningTimeout) {
                        return "warning-color";
                    } else {
                        return "danger-color";
                    }
                }
                return "transparent-color";
            }
        }
    }
}
