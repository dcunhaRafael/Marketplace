using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class PropostaRetornoVerificarAprovacaoEntity {
        public bool Success { get; set; }
        public int ReturnCode { get; set; }
        public string Message { get; set; }

        public StatusPropostaEnum StatusProposta { get; set; }
    }
}
