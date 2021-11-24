using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class PolicySearchEntity {        

        public int? IdApolice { get; set; }
        public string NumeroApolice { get; set; }
        public int? NumeroProposta { get; set; }
        public int? IdTomador { get; set; }
        public int? IdSegurado { get; set; }
        public int? CodigoProduto { get; set; }
        public StatusApoliceEnum? StatusApolice { get; set; }
        public DateTime? DataInicial { get; set; }
        public DateTime? DataFinal { get; set; }
        public int? IdUsuario { get; set; }
        public string CodigoSusepUsuario { get; set; }

    }
}
