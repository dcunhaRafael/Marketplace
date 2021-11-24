using InsuranceApi.Domain.BusinessObjects.Enumerators;

namespace InsuranceApi.Domain.BusinessObjects.Entities {

    public class TakerModel : BaseEntityAuditorship {
        public TakerModel() {
            MeioPagamento = new MeioPagamentoEntity();
            Endereco = new EnderecoEntity();
            Contato = new ContatoEntity();
        }

        public int? IdTomador { get; set; } //-- PK da tabelaTaker

        public int? IdPessoa { get; set; }
        public string NomePessoa { get; set; }
        public long? CpfCnpj { get; set; }
        public TipoPessoaEnum TipoPessoa { get; set; }
        public string ClasseRisco { get; set; }
        public string DescricaoTipoPessoa { get; set; }
        public decimal? QualidadeEmpresa { get; set; }
        public decimal? Pontuacao { get; set; }
        public decimal? Limite { get; set; }
        public decimal? LimiteDisponivel { get; set; }
        public decimal? Taxa { get; set; }
        public string Rating { get; set; }        
        public decimal? LimiteUtilizado {
            get {
                return (Limite ?? decimal.Zero) - (LimiteDisponivel ?? decimal.Zero);
            }
        }
        public MeioPagamentoEntity MeioPagamento { get; set; }
        public EnderecoEntity Endereco { get; set; }
        public ContatoEntity Contato { get; set; }

        public int? IdUsuarioCorretor { get; set; }
        public string NomeCorretor { get; set; }
        public string CpfCnpjCorretor { get; set; }
        public string CodigoSusepCorretor { get; set; }
        public bool NaoPossuiDadosScore { get; set; }
    }
}
