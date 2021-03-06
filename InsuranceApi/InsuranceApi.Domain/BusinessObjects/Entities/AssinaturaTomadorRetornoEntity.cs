using System;
using System.Collections.Generic;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class AssinaturaTomadorRetornoEntity {

        public int IdTransacao { get; set; }
        public string Location { get; set; }
        public DateTime Timestamp { get; set; }
        public int Status { get; set; }
        public List<string> Erros { get; set; }
    }
}
