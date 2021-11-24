using System;
using System.Collections.Generic;

namespace RenewalApi.Web.ViewModels {
    public class InstallmentReturnViewModel {

        public InstallmentReturnViewModel() {
            Parcelas = new List<ParcelaRetornoViewModel>();
        }
        public int? CodigoFormaPagamentoPrimeiraParcela { get; set; }
        public string NomeFormaPagamentoPrimeiraParcela { get; set; }
        public int? CodigoFormaPagamentoDemaisParcelas { get; set; }
        public string NomeFormaPagamentoDemaisParcelas { get; set; }
        public int? CodigoParcelamentoPremio { get; set; }
        public string NomeParcelamentoPremio { get; set; }
        public int? CodigoPeriodoPagamento { get; set; }
        public string NomePeriodoPagamento { get; set; }
        public DateTime? DataVencimentoParcela { get; set; }
        public int? DiaVencimentoParcela { get; set; }
        public List<ParcelaRetornoViewModel> Parcelas { get; set; }
    }
}
