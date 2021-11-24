using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System;
using System.Collections.Generic;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class ProposalEntity {
        public ProposalEntity() {
            Product = new ProductEntity();
            SalesChannel = new SalesChannelEntity();
            Broker = new CorretorConsultaEntity();
            Taker = new TakerModel();
            Insured = new InsuredEntity();
            DadosCobranca = new DadosCobrancaEntity();
            DadosGarantia = new DadosGarantiaEntity();
            UsuarioInclusao = new UsuarioEntity();
            UsuarioAlteracao = new UsuarioEntity();
            Parcelas = new List<ParcelaEntity>();
            InsuredObject = new InsuredObjectEntity();
            ParametrosCalculoCobertura = new CoberturaParametrosCalculoEntity();
            TakerParametersCalculation = new TakerCalculationParameters();
            Comissao = new List<ProposalCommissionDistributionEntity>();
            Clausulas = new List<ProposalClausulaEntity>();

        }

        public int Id { get; set; }
        public int IdApolice { get; set; }
        public int IdEndosso { get; set; }
        public long? CodigoApolice { get; set; }
        public DateTime DataProposta { get; set; }
        public DateTime DataValidade { get; set; }
        public DateTime? DataInicioVigencia { get; set; }
        public DateTime? DataFimVigencia { get; set; }
        public int? DiasPrazoVigencia { get; set; }
        public int CodigoStatus { get; set; }
        public string DescricaoStatus { get; set; }
        public TipoSeguroEnum TipoSeguro { get; set; }
        public TipoSeguradoEnum TipoSegurado { get; set; }
        public StatusPropostaEnum? StatusProposta { get; set; }
        public StatusAssinaturaPropostaEnum? StatusAssinatura { get; set; }
        public int CodigoProposta { get; set; }
        public int IdEndossoImposto { get; set; }
        public long? IdPessoaProdutor { get; set; }
        public bool IsPremioInformado { get; set; }
        public IList<ProposalClausulaEntity> Clausulas { get; set; }
        public ProductEntity Product { get; set; }
        public SalesChannelEntity SalesChannel { get; set; }
        public CorretorConsultaEntity Broker { get; set; }
        public TakerModel Taker { get; set; }
        public InsuredEntity Insured { get; set; }
        public DadosCobrancaEntity DadosCobranca { get; set; }
        public DadosGarantiaEntity DadosGarantia { get; set; }
        public List<ParcelaEntity> Parcelas { get; set; }
        public UsuarioEntity UsuarioInclusao { get; set; }
        public UsuarioEntity UsuarioAlteracao { get; set; }
        public InsuredObjectEntity InsuredObject { get; set; }
        public CoberturaParametrosCalculoEntity ParametrosCalculoCobertura { get; set; }
        public TakerCalculationParameters TakerParametersCalculation { get; set; }
        public List<ProposalCommissionDistributionEntity> Comissao { get; set; }


        //-- Incluso para o Marketplace
        public int CdRetorno { get; set; }
        public string NmRetorno { get; set; }
    }
}
