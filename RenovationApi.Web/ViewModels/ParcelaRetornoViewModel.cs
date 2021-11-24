using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System;

namespace RenewalApi.Web.ViewModels {
    public class ParcelaRetornoViewModel {

        public int NumeroParcela { get; set; }
        public decimal ValorAdicionalFracionamento { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal ValorIof { get; set; }
        public decimal ValorPremioTarifario { get; set; }
        public decimal ValorPremioTotal { get; set; }
        public decimal ValorCusto { get; set; }
        public StatusParcelaEnum StatusParcela { get; set; }
    }
}
