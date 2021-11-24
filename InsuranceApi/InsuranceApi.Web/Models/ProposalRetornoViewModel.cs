using InsuranceApi.Domain.BusinessObjects.Enumerators;
using InsuranceApi.Web.ViewModels;
using System;

namespace InsuranceApi.Web.ViewModel {

    public class ProposalRetornoViewModel {

        public int? CodigoProduto { get; set; }
        public int? CodigoModalidade { get; set; }
        public DateTime? DataProposta { get; set; }
        public DateTime? DataInicioVigencia { get; set; }
        public DateTime? DataFimVigencia { get; set; }
        public long? CodigoApolice { get; set; }
        public int CodigoEndosso { get; set; }
        public int CodigoProposta { get; set; }
        public int NumeroProposta { get; set; }           
        public Decimal? ValorPremioTarifario { get; set; }
        public Decimal? ValorPremioNet { get; set; }
        public Decimal? PercentualComissao { get; set; }
        public Decimal? ValorComissao { get; set; }
        public Decimal? ValorImportanciaSegurada { get; set; }
        public Decimal? ValorIof { get; set; }
        public Decimal? PercentualAgravo { get; set; }
        public int CodigoStatus { get; set; }
        public string DescricaoStatus { get; set; }
        public InstallmentReturnViewModel Parcelamento { get; set; }
    }
}
