using Newtonsoft.Json;

namespace Integration.BMG.Schemas {

    public class TomadorInserirCcgResponse : CodigoRetornoResponse {
        public TomadorInserirCcgDocumentoResponse InserirDocumento { get; set; }
    }
}