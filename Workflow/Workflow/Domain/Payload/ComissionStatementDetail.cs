using Domain.Entities;
using System;

namespace Domain.Payload {
    public class ComissionStatementDetail {
        public ComissionStatementDetail() {
            this.Broker = new Broker();
        }

        public Broker Broker { get; set; }
        public int StatementNumber { get; set; }
        public string Competency { get; set; }
        public int EntryCount { get; set; }
        public decimal? ComissionValue { get; set; }
        public DateTime? PayDay { get; set; }
        public DateTime? PaymentRequestDate { get; set; }
        public string StatusName { get; set; }
        public decimal? NotTaxableComissionValue { get; set; }


        // Novos
        //public DateTime? dataAbertura { get; set; }
        //public DateTime? dataFechamento { get; set; }
        //public decimal? valorImposto { get; set; }
        //public decimal? valorComissaoTributavel { get; set; }
        //public string NumeroReciboPagamento { get; set; }
        //public DateTime? DataRetornoPagamento { get; set; }
        //public string codigoSucursal { get; set; }
        //public string nomeSucursaal { get; set; }
    }
}
