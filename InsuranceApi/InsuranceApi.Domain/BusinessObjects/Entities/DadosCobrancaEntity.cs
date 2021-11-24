using System;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class DadosCobrancaEntity {

        public DadosCobrancaEntity() {
            FormaPagamentoPrimeiraParcela = new FormaPagamentoEntity();
            FormaPagamentoDemaisParcelas = new FormaPagamentoEntity();
            Parcelamento = new ParcelamentoEntity();
            PeridiocidadePagamento = new PeridiocidadePagamentoEntity();
        }

        public DateTime? DataVencimentoPrimeiraParcela { get; set; }
        public int? DiaCobranca { get; set; }
        public FormaPagamentoEntity FormaPagamentoPrimeiraParcela { get; set; }
        public FormaPagamentoEntity FormaPagamentoDemaisParcelas { get; set; }
        public ParcelamentoEntity Parcelamento { get; set; }
        public PeridiocidadePagamentoEntity PeridiocidadePagamento { get; set; }
    }
}

