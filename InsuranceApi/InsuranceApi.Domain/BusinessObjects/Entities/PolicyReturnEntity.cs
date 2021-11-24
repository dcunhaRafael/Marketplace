using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class PolicyReturnEntity {
        public string NumeroApolice { get; set; }
        public int? CodigoApolice { get; set; }
        public string NumeroProposta { get; set; }
        public string NomeSegurado { get; set; }
        public string NomeTomador { get; set; }
        public string NomeCorretor { get; set; }
        public string DescricaoProduto { get; set; }
        public DateTime Data { get; set; }
        public StatusApoliceEnum StatusApolice { get; set; }
        public int id_endosso { get; set; }
        public int? IdUsuarioInclusao { get; set; }
        public DateTime DataInicioVigencia { get; set; }
        public DateTime DataFinalVigencia { get; set; }
        public string DescricaoModalidade { get; set; }
        public long CnpjTomador { get; set; }
        public decimal? ValorIS { get; set; }
        public decimal? ValorPremio { get; set; }

    }
}
