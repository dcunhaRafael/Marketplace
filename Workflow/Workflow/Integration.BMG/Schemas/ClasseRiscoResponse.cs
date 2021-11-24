using System.Collections.Generic;

namespace Integration.BMG.Schemas {
    public class ClasseRiscoResponse : CodigoRetornoResponse {
        public List<ClasseRiscoItem> Classe_Risco { get; set; }
    }

}