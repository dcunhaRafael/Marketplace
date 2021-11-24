using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using InsuranceApi.Domain.Common.Exceptions;
using InsuranceApi.Domain.Interfaces.Application;
using InsuranceApi.Domain.Interfaces.Service;
using System;
using System.Linq;
using System.Threading.Tasks;
using ApplicationException = InsuranceApi.Domain.Common.Exceptions.ApplicationException;

namespace InsuranceApi.Application {
    public class ProposalJudicialApplication : IProposalJudicialApplication {
        private readonly IProposalApplication proposalApplication;
        private readonly IProductApplication productApplication;
        private readonly ICoverageApplication coverageApplication;
        private readonly IInsuredService insuredService;
        private readonly IZipCodeApplication zipCodeApplication;
        private readonly IInsuredApplication insuredApplication;
        private readonly IAppParameterApplication appParameterApplication;

        public ProposalJudicialApplication(IProposalApplication proposalApplication, IInsuredService insuredService, IInsuredApplication insuredApplication,
             IZipCodeApplication zipCodeApplication, IProductApplication productApplication, ICoverageApplication coverageApplication, IAppParameterApplication appParameterApplication) {
            this.appParameterApplication = appParameterApplication;
            this.proposalApplication = proposalApplication;
            this.productApplication = productApplication;
            this.coverageApplication = coverageApplication;
            this.insuredService = insuredService;
            this.zipCodeApplication = zipCodeApplication;
            this.insuredApplication = insuredApplication;
        }

        public async Task<ProposalReturnGravarEntity> AddAsync(ProposalJudicialEntity entity, int brokerUserId) {
            try {

                ProposalEntity proposalEntity = new ProposalEntity() {
                    Product = await productApplication.GetAsync(entity.CodigoProduto)
                };
                if (proposalEntity.Product == null) {
                    throw new BusinessException("Produto não encontrado.");
                }

                proposalEntity.DadosGarantia.Coverage = await coverageApplication.GetAsync(entity.CodigoProduto, entity.CodigoModalidade);
                if (proposalEntity.DadosGarantia.Coverage == null) {
                    throw new BusinessException("Configurações da Modalidade x Produto não localizadas.");
                }

                var parameter = await appParameterApplication.ListAsync();
                var parameterCoverage =
                    parameter.Where(x => x.AppParameterId.Equals((int)AppParameterEnum.ModalidadeTipoSeguroContrato)).ToList();

                var isCoverageContract = parameterCoverage.Where(x => x.Value.Equals(entity.CodigoModalidade)).FirstOrDefault();
                if (isCoverageContract == null)
                    throw new BusinessException("Não foi possível encontrar a modalidade 'Contrato'.");

                proposalEntity.DataProposta = DateTime.Now;
                proposalEntity.DataInicioVigencia = DateTime.Now;
                proposalEntity.DadosGarantia.TermType = null;
                proposalEntity.StatusProposta = StatusPropostaEnum.EmNegociacao;
                proposalEntity.TipoSeguro = TipoSeguroEnum.Contrato;
                proposalEntity.DadosGarantia.ValorImportanciaSegurada = entity.ValorGarantia;
                proposalEntity.DadosGarantia.NumeroLicitacao = entity.NumeroContrato;
                proposalEntity.DiasPrazoVigencia = entity.DiasPrazoVigencia;
                proposalEntity.DataFimVigencia = proposalEntity.DataInicioVigencia?.AddDays(entity.DiasPrazoVigencia);
                proposalEntity.Taker.CpfCnpjCorretor = entity.CpfCnpjTomador;
                proposalEntity.Broker.IdUsuarioCorretor = brokerUserId;

                await LoadInsured(proposalEntity, entity.Segurado);

                return await proposalApplication.AddAsync(proposalEntity);

            } catch (Exception e) {
                if ((e is BusinessException || e is DaoException || e is ServiceException)) {
                    throw e;
                }
                throw new ApplicationException("Erro na gravação da proposta.", e);
            }
        }
        private async Task LoadInsured(ProposalEntity entity, InsuredEntity insured) {
            var address = await zipCodeApplication.GetAsync(insured.Endereco.Cep);
            entity.Insured = new InsuredEntity {
                Nome = insured.Nome,
                TipoPessoa = (insured.CpfCnpj.ToString().Length <= 11 ? TipoPessoaEnum.PessoaFisica : TipoPessoaEnum.PessoaJuridica),
                CpfCnpj = insured.CpfCnpj,
                IdPessoa = null,
                Contato = null,
                Endereco = new EnderecoEntity {
                    Logradouro = address.Logradouro,
                    Numero = string.IsNullOrWhiteSpace(insured.Endereco.Numero) ? "s/n" : insured.Endereco.Numero,
                    Complemento = insured.Endereco.Complemento,
                    Bairro = address.Bairro,
                    Cidade = address.Cidade,
                    UF = address.UF,
                    IdUf = address.IdUf,
                    Cep = insured.Endereco.Cep,
                    IdCidade = address.IdCidade.Value,
                    TipoEndereco = new TipoEnderecoEntity {
                        IdTipoEndereco = 5, //-- Fixo Cobrança
                        NomeTipoEndereco = ""
                    }
                }
            };

            var registeredInsured = await insuredApplication.IsRegisteredAsync(insured.CpfCnpj.Value);
            if (registeredInsured == null) {
                var addedData = await insuredService.AddAsync(entity.Insured);
                entity.Insured.IdPessoa = addedData.IdPessoa;
                entity.Insured.Endereco.IdEndereco = addedData.IdEndereco;
            } else {
                entity.Insured.IdPessoa = registeredInsured.IdPessoa;
                entity.Insured.Endereco.IdEndereco = registeredInsured.Endereco.IdEndereco;
            }
        }
    }
}
