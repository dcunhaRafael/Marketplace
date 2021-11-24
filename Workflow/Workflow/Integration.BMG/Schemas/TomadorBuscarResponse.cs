using System.Collections.Generic;

namespace Integration.BMG.Schemas {

    public class TomadorBuscarResponse {
        public int cd_retorno { get; set; }
        public string nm_retorno { get; set; }
        public List<TomadorBuscarItem> Tomador_Buscar { get; set; }
    }

}