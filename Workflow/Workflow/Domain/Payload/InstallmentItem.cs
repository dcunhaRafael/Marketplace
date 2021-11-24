using Domain.Enumerators;
using System;

namespace Domain.Payload {
    public class InstallmentItem {

        public int NumeroParcela { get; set; }
        public decimal ValorAdicionalFracionamento { get; set; }
        public DateTime DataVencimento { get; set; }
        public decimal ValorIof { get; set; }
        public decimal ValorPremioTarifario { get; set; }
        public decimal ValorPremioTotal { get; set; }
        public decimal ValorCusto { get; set; }
        public InstallmentStatusEnum StatusParcela { get; set; }
    }
}
