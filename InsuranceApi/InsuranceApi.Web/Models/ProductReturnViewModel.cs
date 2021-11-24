using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Collections.Generic;

namespace InsuranceApi.Web.ViewModels {

    public class ProductReturnViewModel {   
        public int? Codigo { get; set; }   
        public string Nome { get; set; }
        public List<CoverageEntity> Modalidade { get; set; }
    }
}
