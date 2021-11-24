namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class ParcelamentoEntity {

        public int? IdParcelamento { get; set; }
        public string DescricaoParcelamento { get; set; }
        //public decimal PercentualJuros { get; set; }
        public int QuantidadeParcelas { get; set; }
    }
}
