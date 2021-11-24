using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class PropostaFiltrosEntity {

        public PropostaFiltrosEntity() {
            Corretor = new CorretorConsultaEntity();
        }

        public long? IdApolice { get; set; }
        public int? CodigoProposta { get; set; }
        public int? IdTomador { get; set; }
        public int? IdSegurado { get; set; }
        public int? CodigoProduto { get; set; }
        public StatusPropostaEnum? StatusProposta { get; set; }
        public CorretorConsultaEntity Corretor { get; set; }

        // Não tem no serviço...
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
    }
}
