using InsuranceApi.Domain.BusinessObjects.Enumerators;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class ProposalCommissionDistributionEntity {

        public ProposalCommissionDistributionEntity() {
            Producer = new CorretorConsultaEntity();
        }

        public int? ProposalCommissionDistributionId { get; set; }
        public int ProposalId { get; set; }
        public byte TypeOfComission { get; set; }
        public int ProducerId { get; set; }
        public byte IsPrincipalProducer { get; set; }
        public decimal ParticipationPercentage { get; set; }
        public decimal ProposalPercentage { get; set; }
        public byte Print { get; set; }
        public decimal ComissionValue { get; set; }
        public int policyId { get; set; }
        public int ProposalCode { get; set; }
        public string ProducerCpfCnpj { get; set; }
        public StatusPropostaEnum StatusCode { get; set; }

        public CorretorConsultaEntity Producer { get; set; }
    }
}
