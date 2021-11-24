namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class DadosReclamanteEntity {

        public DadosReclamanteEntity() {
            Segurado = new InsuredEntity();
        }

        public int? IdReclamante { get; set; }
        public string NomeReclamante { get; set; }
        public long? CpfCnpjReclamante { get; set; }
        public bool IsPrincipal { get; set; }
        public InsuredEntity Segurado { get; set; }
    }
}
