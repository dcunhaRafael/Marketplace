using InsuranceApi.Domain.BusinessObjects.Enumerators;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class InsuredEntity {

        public int Id { get; set; }
        public int? IdPessoa { get; set; }
        public string Nome { get; set; }
        public long? CpfCnpj { get; set; }
        public TipoPessoaEnum TipoPessoa { get; set; }
        public object DescricaoTipoPessoa { get; set; }
        public object FlagAtivo { get; set; }
        public object DescricaoFlagAtivo { get; set; }
        public EnderecoEntity Endereco { get; set; }
        public ContatoEntity Contato { get; set; }
        public int InsuredTypeId { get; set; }
    }
}
