using System.Collections.Generic;

namespace Integration.BMG.Schemas {

    public class TomadorBuscarLimitesCreditoResponse : CodigoRetornoResponse {
        public List<TomadorBuscarLimitesCreditoItem> Tomador_BuscarLimitesCredito { get; set; }
    }
}