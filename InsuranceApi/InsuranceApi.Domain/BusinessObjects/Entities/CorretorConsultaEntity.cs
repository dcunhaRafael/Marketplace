namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class CorretorConsultaEntity {
        public int TipoPessoa { get; set; }
        public int IdPessoa { get; set; }
        public string NomePessoa { get; set; }
        public string CpfCnpj { get; set; }
        public string CodigoSusep { get; set; }
        public string IsAtivo { get; set; }
        public int? IdUsuarioCorretor { get; set; }
        public long? IdPessoaCanalVenda { get; set; }
    }
}
