using System;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class ApoliceAssinadaEmitirEntity {
        public int IdApolice { get; set; }
        public int IdEndosso { get; set; }
        public bool IndicadorPropostaAssinada { get; set; }
        public DateTime DataAssinaturaProposta { get; set; }
        public string ObservacoesAssinatura { get; set; }
    }
}
