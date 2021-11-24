using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
  public   class ProposalReturnGravarEntity {

        public ProposalEntity Proposta { get; set; }      
        public bool Success { get; set; }
        public int ReturnCode { get; set; }
        public string Message { get; set; }
    }
}
