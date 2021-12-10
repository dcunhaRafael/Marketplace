using System.Collections.Generic;

namespace Integration.BMG.Schemas {
    public class CorretorDetalheExtratoComissaoResponse : CodigoRetornoResponse {
        public List<CorretorDetalheExtratoComissaoItem> Corretor_Detalhe_Extrato_Comissao { get; set; }
    }
}

