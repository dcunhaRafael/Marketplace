using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System;

namespace InsuranceApi.Web.ViewModels {
    public class PolicySearchViewModel {                
        public string NumeroApolice { get; set; }
        public int? NumeroProposta { get; set; }                              
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }       
        
    }
}
