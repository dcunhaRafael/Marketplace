using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using InsuranceApi.Domain.Common.Exceptions;
using InsuranceApi.Domain.Common.Extension;
using InsuranceApi.Domain.Interfaces.Application;
using InsuranceApi.Domain.Interfaces.Dao.Ebix;
using InsuranceApi.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ApplicationException = InsuranceApi.Domain.Common.Exceptions.ApplicationException;

namespace InsuranceApi.Application {
    public class TakerApplication : ITakerApplication {
        private readonly ICityService cityService;
        private readonly ITakerService takerService;
        private readonly ICrivoApplication crivoApplication;
        private readonly ILoadedTakerApplication loadedTakerApplication;
        private readonly IPersonApplication personApplication;
        private readonly IZipCodeApplication zipCodeApplication;
        private readonly ITakerDao takerDao;

        public TakerApplication(ICityService cityService, ITakerService takerService,
                                ICrivoApplication crivoApplication, ILoadedTakerApplication loadedTakerApplication, IPersonApplication personApplication,
                                IZipCodeApplication zipCodeApplication, ITakerDao takerDao) {
            this.cityService = cityService;
            this.takerService = takerService;
            this.crivoApplication = crivoApplication;
            this.loadedTakerApplication = loadedTakerApplication;
            this.personApplication = personApplication;
            this.zipCodeApplication = zipCodeApplication;
            this.takerDao = takerDao;
        }

        public async Task<IList<TakerModel>> ListAsync(string name, string cpfCnpj, int? brokerUserId, bool listAll = false) {
            try {
                return await takerService.ListAsync(name, cpfCnpj, brokerUserId, listAll);
            } catch (ServiceException e) {
                throw new ApplicationException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ApplicationException("Erro na busca do tomador.", e);
            }
        }

        public async Task<TakerModel> GetAsync(string cpfCnpj, int? brokerUserId) {
            TakerModel taker = null;
            try {
                var takers = await takerService.ListAsync(null, cpfCnpj, brokerUserId);

                if (takers != null && takers.Count > 0) {
                    taker = takers.First();
                }
            } catch (Exception ex) {
                if (!ex.Message.Equals("Não existe dados para os parâmetros informados.")) {
                    throw new BusinessException("Não foi possível localizar o tomador .");
                }
            }

            return taker;
        }

        public async Task<TakerCalculationParameters> GetParameterAsync(int takerExternalCode) {
            try {
                return await takerService.GetParametersAsync(takerExternalCode);
            } catch (ServiceException e) {
                throw new ApplicationException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ApplicationException("Erro na buscar parâmetros do cálculo do taker.", e);
            }
        }

        public async Task<TomadorRetornoIncluirEntity> AddAsync(TakerModel taker, int brokerUserId, string ipAddress, string userAgent, int loggedUserId) {
            try {

                var isRegistered = await GetAsync(taker.CpfCnpj.FormatCpfCnpjLongToString(), brokerUserId);
                if (isRegistered != null) {
                    throw new BusinessException("Tomador já cadastrado");
                }
                var crivoData = await crivoApplication.GetCreditPolicyAsync(taker.CpfCnpj.FormatCpfCnpjLongToString());
                if (crivoData == null || string.IsNullOrWhiteSpace(crivoData.RazaoSocial)) {
                    throw new BusinessException("Falha na consulta do CNPJ no Crivo");
                }

                try {

                    taker.TipoPessoa = (taker.CpfCnpj.ToString().Length <= 11 ? TipoPessoaEnum.PessoaFisica : TipoPessoaEnum.PessoaJuridica);
                    taker.NomePessoa = crivoData.RazaoSocial;
                    taker.IdUsuarioCorretor = brokerUserId;
                    taker.Taxa = crivoData.Taxa;
                    taker.Rating = crivoData.Rating;
                    taker.Limite = crivoData.LimiteCredito;
                    taker.LoggedUserId = loggedUserId;

                    if (!string.IsNullOrEmpty(crivoData.Endereco.Cep)) {
                        var buscarCep = await zipCodeApplication.GetAsync(crivoData.Endereco.Cep);
                        if (buscarCep != null && !string.IsNullOrWhiteSpace(buscarCep.Logradouro)) {
                            taker.Endereco.Cep = crivoData.Endereco.Cep;
                            taker.Endereco.Logradouro = buscarCep.Logradouro;
                            taker.Endereco.Bairro = buscarCep.Bairro;
                            taker.Endereco.IdCidade = buscarCep.IdCidade.Value;
                            taker.Endereco.Cidade = buscarCep.Cidade;
                            taker.Endereco.UF = buscarCep.UF;
                            taker.Endereco.IdUf = buscarCep.IdUf;
                        } else {
                            taker.Endereco.IdUf = taker.Endereco.IdUf;
                            var cidade = await cityService.ListAsync(taker.Endereco.IdUf.Value, taker.Endereco.Cidade);
                            taker.Endereco.IdCidade = cidade.FirstOrDefault().IdCidade;
                        }
                    }

                } catch (Exception ex) {
                    taker.Endereco.CodigoRetorno = 1;
                    taker.Endereco.MesagemRetorno = ex.Message;
                }

                if (string.IsNullOrWhiteSpace(taker.Contato.Nome) || taker.Contato.Cpf == null || string.IsNullOrWhiteSpace(taker.Contato.Email) || taker.Contato.DataNascimento == null) {
                    taker.Contato = null;
                }

                taker.Endereco.TipoEndereco.IdTipoEndereco = 5;  //-- Fixo Cobrança
                var takerReturnInsert = await takerService.AddAsync(taker, brokerUserId);

                long personId = 0;
                var pessoaExistente = await personApplication.ListAsync(null, taker.CpfCnpj, null, null);
                if (!pessoaExistente.Any()) {
                    personId = await takerDao.AddAsync(taker);
                } else {
                    personId = pessoaExistente.FirstOrDefault().PersonId.Value;
                }

                await takerDao.UpdateAsync(takerReturnInsert.IdPessoa, personId);
                await loadedTakerApplication.SaveAsync(taker, takerReturnInsert.IdPessoa, brokerUserId, ipAddress, userAgent);

                return takerReturnInsert;

            } catch (Exception e) {
                if ((e is BusinessException || e is DaoException || e is ServiceException)) {
                    throw e;
                }
                throw new ApplicationException("Erro ao cadastrar tomador.", e);
            }
        }

    }
}