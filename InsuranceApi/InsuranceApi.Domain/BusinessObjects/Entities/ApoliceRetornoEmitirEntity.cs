using InsuranceApi.Domain.BusinessObjects.Enumerators;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class ApoliceRetornoEmitirEntity {
        public long NumeroApolice { get; set; }
        public StatusPropostaEnum StatusProposta { get; set; }
    }
}
