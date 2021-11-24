namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class SeguradoBuscarEntity {

        public int? IdPessoa { get; set; }
        public string NomePessoa { get; set; }
        public string CpfCnpj { get; set; }
        public bool? Ativo { get; set; }
        public bool? EnderecoPadrao { get; set; }
    }
}
