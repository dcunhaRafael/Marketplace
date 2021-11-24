using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System.Collections.Generic;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class DadosGarantiaEntity {

        public DadosGarantiaEntity() {
            Coverage = new CoverageEntity();
            Reclamantes = new List<DadosReclamanteEntity>();
            LegalRecourseType = new LegalRecourseTypeEntity();
            TermType = new TermTypeEntity();
            LaborCourt = new LaborCourtEntity();
            CivilCourt = new CivilCourtEntity();
        }

        public decimal? ValorPremioTarifario { get; set; }
        public decimal? PercentualComissao { get; set; }
        public decimal? ValorTaxaMoeda { get; set; }
        public decimal? ValorAdicionalFracionamento { get; set; }
        public decimal? ValorComissao { get; set; }
        public decimal? ValorImportanciaSegurada { get; set; }
        public decimal? ValorTaxaRisco { get; set; }
        public int? CodigoMoeda { get; set; }
        public decimal? ValorIOF { get; set; }
        public decimal? PercentualIOF { get; set; }
        public decimal? ValorPremioTotal { get; set; }
        public string NumeroLicitacao { get; set; }
        public decimal? ValorMaximoDepositoRecursal { get; set; }
        public decimal? ValorDepositoRecursal { get; set; }
        public bool PossuiAgravo { get; set; }
        public decimal? PercentualAgravo { get; set; }
        public decimal? ValorImportanciaSeguradaRecursal { get; set; }
        public string NumeroProcesso { get; set; }
        public TipoSeguradoRecursalEnum? TipoDeSegurado { get; set; }
        public CoverageEntity Coverage { get; set; }
        public TermTypeEntity TermType { get; set; }
        public LaborCourtEntity LaborCourt { get; set; }
        public CivilCourtEntity CivilCourt { get; set; }
        public LegalRecourseTypeEntity LegalRecourseType { get; set; }
        public List<DadosReclamanteEntity> Reclamantes { get; set; }


        //-- Judicial INI
        public TipoCredorEntity TipoDeCredor { get; set; }
        public StateEntity EstadoDoCredor { get; set; }
        public MunicipioEntity MunicipioDoCredor { get; set; }

        public decimal? ValorDiscussaoJudicial { get; set; }
        public decimal? ValorImportanciaSeguradaJudicial { get; set; }

        public TipoProcessoEntity TipoAcao { get; set; }
        public string DescricaoOutroTipoAcao { get; set; }
        public decimal? NumeroAcao { get; set; }

        public long? CpfCnpjJuizo { get; set; }
        public string NomeJuizo { get; set; }
        public decimal? NumeroCDA { get; set; }
        public string NumeroProcessoAdministrativo { get; set; }
        public TipoTributoEntity TipoTributo { get; set; }
        public string ComplementoTipoTributo { get; set; }

        public string EmailSolicitante { get; set; }
        public string NomeSolicitante { get; set; }
        public string TelefoneSolicitante { get; set; }

        //-- Judicial FIM

        //-- Compatibilidade com o Marketplace
        public RecursoParametroEntity LegalRecourseTypeParameter { get; set; }
    }
}
