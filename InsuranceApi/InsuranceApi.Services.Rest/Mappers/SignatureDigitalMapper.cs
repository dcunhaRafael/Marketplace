using Flurl.Http;
using Newtonsoft.Json;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.MessagesService;
using InsuranceApi.Domain.Common.Extension;
using InsuranceApi.Services.Rest.Messages.Legacy;
using System;
using System.Net.Http;

namespace InsuranceApi.Services.Rest.Mappers {
    public static class SignatureDigitalMapper {
        public static AssinaturaGravarRequest MapAssinaturaGravarEntityToRequest(AssinaturaGravarEntity entity) {
            try {
                AssinaturaGravarRequest request = new AssinaturaGravarRequest
                {
                    cpf_cnpj = entity.CpfCnpj.FormatCpfCnpjToString()?.ApenasNumericos(),
                    nome_cliente = entity.Nome,
                    numero_documento = entity.CodigoProposta,
                    ip = entity.IpAddress,
                    user_agent = entity.UserAgent,
                    impresso = new Documento()
                    {
                        nome = entity.Documento.NomeArquivo,
                        bytes = entity.Documento.ConteudoBase64
                    }
                };
                return request;
            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados da requisição (Assinatura_Gravar).", e);
            }
        }

        public static AssinaturaRetornoGravarEntity MapAssinaturaGravarResponseToEntity(HttpResponseMessage raw) {
            try {

                var mappedItem = new AssinaturaRetornoGravarEntity()
                {
                    Location = raw.GetHeaderValue("Location")
                };
                if (mappedItem.Location.IsNullOrEmpty()) {

                    var response = JsonConvert.DeserializeObject<AssinaturaGravarResponse>(raw.Content.ReadAsStringAsync().Result);                   
                    mappedItem.Status = response.status;
                    mappedItem.Timestamp = response.timestamp;
                    mappedItem.Erros = response.erros;
                } else {
                    var charIndex = mappedItem.Location.LastIndexOf("/");
                    mappedItem.IdTransacao = mappedItem.Location.Substring(charIndex + 1).ToInt();
                }

                return mappedItem;
            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados do legado (Assinatura_Gravar).", e);
            }
        }

        public static AssinaturaConsultarEntity MapAssinaturaConsultarResponseToEntity(AssinaturaConsultarResponse response) {
            try {          
                var mappedList = new AssinaturaConsultarEntity()
                {
                    IdTransacao = response.id_transacao,
                    CpfCnpj = BaseMapper.StringToLong(response.cpf_cnpj),
                    Nome = response.nome_cliente,
                    CodigoProposta = response.numero_documento,
                    IpAddress = response.ip,
                    UserAgent = response.user_agent,
                    ImpressoOriginal = new ImpressoEntity() { Href = response.impresso_original.href },
                    ImpressoAssinado = new ImpressoEntity() { Href = response.impresso_assinado.href },
                    TimestampInclusao = response.timestamp_inclusao
                };
                return mappedList;
            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados do legado (Assinatura_Consultar).", e);
            }
        }

        public static AssinaturaImpressoEntity MapAssinaturaImpressaoResponseToEntity(AssinaturaImpressaoResponse response) {
            try {                
              
                var mappedList = new AssinaturaImpressoEntity()
                {
                    IdImpresso = response.id,
                    NomeArquivo = response.nome,
                    ConteudoBase64 = response.bytes
                };
                return mappedList;
            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados do legado (Assinatura_Imprimir).", e);
            }
        }

        public static AssinaturaGravarTomadorRequest MapAssinaturaGravarTomadorEntityToRequest(AssinaturaTomadorEntity entity) {
            try {
                AssinaturaGravarTomadorRequest request = new AssinaturaGravarTomadorRequest
                {
                    cnpj = string.Format("{0:D14}", entity.CnpjTomador),
                    razao_social = entity.RazaoSocialTomador,
                    cpf = string.Format("{0:D11}", entity.CpfRepresentanteLegal),
                    nome_cliente = entity.NomeRepresentanteLegal,
                    email = entity.EmailRepresentanteLegal,
                    data_nascimento = BaseMapper.DateToString(entity.NascimentoRepresentanteLegal),
                    numero_documento = entity.IdTomador,
                    ip = entity.IpAddress,
                    user_agent = entity.UserAgent,
                    impresso = new Documento()
                    {
                        nome = entity.Impresso.NomeArquivo,
                        bytes = entity.Impresso.ConteudoBase64
                    }
                };
                return request;
            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados da requisição (Assinatura_GravarTomador).", e);
            }
        }

        public static AssinaturaTomadorRetornoEntity MapAssinaturaGravarTomadorResponseToEntity(HttpResponseMessage raw) {
            try {

                var mappedItem = new AssinaturaTomadorRetornoEntity()
                {
                    Location = raw.GetHeaderValue("Location")
                };
                if (mappedItem.Location.IsNullOrEmpty()) {
                    var response = JsonConvert.DeserializeObject<AssinaturaGravarResponse>(raw.Content.ReadAsStringAsync().Result);
                    mappedItem.Status = response.status;
                    mappedItem.Timestamp = response.timestamp;
                    mappedItem.Erros = response.erros;
                } else {
                    var charIndex = mappedItem.Location.LastIndexOf("/");
                    mappedItem.IdTransacao = mappedItem.Location.Substring(charIndex + 1).ToInt();
                }

                return mappedItem;
            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados do legado (Assinatura_GravarTomador).", e);
            }
        }

    }
}
