namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class ZipCodeEntity {

        public string Logradouro { get; set; }
        public string Bairro { get; set; }
        public string Cidade { get; set; }
        public int? IdUf { get; set; }
        public string UF { get; set; }
        public int? IdCidade  { get; set; }
    }
}
