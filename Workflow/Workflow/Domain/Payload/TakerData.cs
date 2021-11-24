using System;
using System.Collections.Generic;

namespace Domain.Payload {
    public class TakerData {

        public TakerData() {
            Person = new Person();
            Broker = new Broker();
            CreditAssessments = new List<TakerCreditAssessment>();
            CreditSubLimits = new List<TakerCreditSubLimit>();
        }

        public int Id { get; set; }
        public Person Person { get; set; }
        public Broker Broker { get; set; }
        public string LegacyCode { get; set; }
        public string AddressLegacyCode { get; set; }
        public bool IsActive { get; set; }
        public bool IsCcgSigned { get; set; }

        // Childs
        public IList<TakerCreditAssessment> CreditAssessments { get; set; }
        public IList<TakerCreditSubLimit> CreditSubLimits { get; set; }
        

        // Extras
        public decimal? TotalInsuredAmountValue { get; set; }
        public decimal? AvailableBalanceValue { get; set; }
        public DateTime? RegistrationExpirationDate { get; set; }

    }
}
