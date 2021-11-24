using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class MeioPagamentoEntity {

        public string Nome { get; set; }
        public string NumeroBanco { get; set; }
        public string TipoConta { get; set; }
        public string Agencia { get; set; }
        public string AgenciaDv { get; set; }
        public string Conta { get; set; }
        public string ContaDv { get; set; }
    }
}
