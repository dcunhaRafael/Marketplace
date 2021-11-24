using System.Collections.Generic;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class ProdutoModalidadeEntity {

        public ProdutoModalidadeEntity() {
            Modalidade = new List<CoverageEntity>();
        }
        public int? CodigoProduto { get; set; }
        public string NomeProduto { get; set; }
        public List<CoverageEntity> Modalidade { get; set; }
    }
}
