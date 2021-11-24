using Newtonsoft.Json;
using System.Collections.Generic;

namespace Integration.BMG.Schemas {

    public class TomadorBuscarParametrosCalculoResponse : CodigoRetornoResponse {
        public List<TomadorBuscarParametrosCalculoItem> Tomador_BuscarParametrosCalculo { get; set; }
    }
}