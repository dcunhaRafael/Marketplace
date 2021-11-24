using InsuranceApi.Domain.BusinessObjects.AppSettings;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using InsuranceApi.Domain.Common.Exceptions;
using InsuranceApi.Domain.Common.Extension;
using InsuranceApi.Domain.Common.HttpClient.Configurations;
using InsuranceApi.Domain.Interfaces.Application;
using InsuranceApi.Domain.Interfaces.Dao.Ebix;
using InsuranceApi.Domain.Interfaces.Service;
using Microsoft.Extensions.Options;
using System;
using System.IO;
using System.Threading.Tasks;

namespace InsuranceApi.Application {
    public class LoadedTakerApplication : AppConfigHttp, ILoadedTakerApplication {
        private readonly IAppParameterDao appParameterDao;
        private readonly ILoadedTakerDao loadedTakerDao;
        private readonly ISignatureDigitalService signatureDigitalService;

        public LoadedTakerApplication(IOptions<ServiceSettings> serviceSettings, IAppParameterDao appParameterDao,
            ILoadedTakerDao loadedTakerDao, ISignatureDigitalService signatureDigitalService) : base(serviceSettings) {
            this.appParameterDao = appParameterDao;
            this.signatureDigitalService = signatureDigitalService; ;
            this.loadedTakerDao = loadedTakerDao;
        }

        public async Task SaveAsync(TakerModel taker, int legacyId, int legacyUserId, string ipAddress, string userAgent) {
            try {

                LoadedTakerEntity loadedTaker = new LoadedTakerEntity {

                    // Dados fixos do registro de carga
                    UploadedFileId = null,
                    FileLineIndex = null,
                    RecordStatus = StatusRegistroTomadorEnum.OK,
                    JsonRecord = null,
                    JsonValidation = null,
                    LoadedDate = DateTime.Now,
                    Status = StatusCargaTomadorEnum.Pendente,
                    TakerType = TipoPessoaEnum.PessoaJuridica,
                    ResponseStatus = null,
                    SystemStatus = null,

                    // Dados informados na tela pelo usuário
                    TakerName = taker.NomePessoa,
                    TakerDocumentNumber = taker.CpfCnpj,
                    Address = taker.Endereco.Logradouro,
                    AddressNumber = taker.Endereco.Numero,
                    AddressComplement = taker.Endereco.Complemento,
                    District = taker.Endereco.Bairro,
                    City = taker.Endereco.Cidade,
                    State = taker.Endereco.UF,
                    ZipCode = int.Parse(taker.Endereco.Cep.ApenasNumericos()),
                    LoggedUserId = taker.LoggedUserId,

                    // Dados do Crivo
                    Rating = taker?.Rating,
                    Tax = taker?.Taxa,
                    Limit = taker?.Limite,
                    Score = taker?.Pontuacao.ToNullableLong(),

                };

                var loadedTakerRecordId = await loadedTakerDao.AddAsync(loadedTaker);

                // Atualiza o identificador do tomador na i4pro
                await loadedTakerDao.UpdateLegacyIdAsync(loadedTakerRecordId, legacyId, legacyUserId);

                // Se já tiver os dados de contato faz o envio da assinatura
                if (taker.Contato != null) {

                    // Monta o registro da transmissão
                    LoadedTakerSignatureEntity transmissao = new LoadedTakerSignatureEntity {
                        LegalRepresentativeBornIn = taker.Contato.DataNascimento,
                        LegalRepresentativeDocumentNumber = taker.Contato.Cpf,
                        LegalRepresentativeMail = taker.Contato.Email,
                        LegalRepresentativeName = taker.Contato.Nome,
                        LoadedTakerRecordId = loadedTakerRecordId
                    };

                    // Registra a trasmissão da assinatura (registra antes pois precisa da pk pra uso no serviço de assinatura)
                    transmissao.DateUtc = DateTime.UtcNow;
                    transmissao.UserId = taker.LoggedUserId;
                    var loadedTakerSignatureId = await loadedTakerDao.AddSignatureAsync(transmissao);

                    // Obtém o arquivo de template a ser assinado
                    string templateFile = AppConfigHttp.CargaTomadorAssinaturaDigitalTemplate;

                    if (!File.Exists(templateFile)) {
                        throw new ServiceException("Arquivo de template não configurado corretamente.");
                    }

                    byte[] templateBuffer = File.ReadAllBytes(templateFile);

                    // Busca a origem da empresa
                    var origemEmpresa = await appParameterDao.GetAsync(AppParameterEnum.OrigemEmpresaAssinatura);

                    // Grava a assinatura digital
                    var assinatura = new AssinaturaTomadorEntity {
                        CnpjTomador = loadedTaker.TakerDocumentNumber,
                        RazaoSocialTomador = loadedTaker.TakerName,
                        CpfRepresentanteLegal = transmissao.LegalRepresentativeDocumentNumber,
                        NomeRepresentanteLegal = transmissao.LegalRepresentativeName,
                        EmailRepresentanteLegal = transmissao.LegalRepresentativeMail,
                        NascimentoRepresentanteLegal = transmissao.LegalRepresentativeBornIn,
                        IdTomador = loadedTakerSignatureId,
                        Impresso = new DocumentEntity {
                            ConteudoBase64 = Convert.ToBase64String(templateBuffer),
                            NomeArquivo = string.Format("CCG-{0}.pdf", loadedTaker.TakerDocumentNumber)
                        },
                        IpAddress = ipAddress,
                        UserAgent = userAgent,
                        OrigemEmpresa = origemEmpresa.Value
                    };
                    var retornoAssinatura = await signatureDigitalService.AddAsync(assinatura);

                    // Atualiza o identificador da assinatura
                    await loadedTakerDao.UpdateSignatureIdAsync(loadedTakerSignatureId, retornoAssinatura.IdTransacao);
                    await loadedTakerDao.UpateStatusAsync(transmissao.LoadedTakerRecordId, StatusCargaTomadorEnum.EmProcessoAssinatura);

                }

            } catch (Exception e) {
                if ((e is DaoException || e is ServiceException)) {
                    throw e;
                }
                throw new Domain.Common.Exceptions.ApplicationException("Erro gravando tomador na estrutura de carga.", e);
            }
        }
    }
}
