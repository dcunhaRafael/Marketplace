using System.Collections.Generic;

namespace Integration.BMG.Schemas {
    public class CorretorExtratoComissaoPorRamoResponse : CodigoRetornoResponse {
        public List<CorretorExtratoComissaoPorRamoItem> ConsultaExtratoComissaoPorRamo { get; set; }
    }
}

