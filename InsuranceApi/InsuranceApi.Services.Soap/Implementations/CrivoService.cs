using CreditTalk;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using InsuranceApi.Domain.Common.Exceptions;
using InsuranceApi.Domain.Common.Extension;
using InsuranceApi.Domain.Interfaces.Service;
using InsuranceApi.Services.Soap.Mappers;
using System;
using System.Reflection;
using System.ServiceModel;
using System.Threading.Tasks;

namespace InsuranceApi.Services.Soap.Implementations {
    public class CrivoService : ICrivoService {

        public static BasicHttpBinding getBasicHttpBinding() {
            BasicHttpBinding myBinding = new BasicHttpBinding();
            myBinding.Name = "BasicHttpEndpoint";
            myBinding.OpenTimeout = TimeSpan.FromMinutes(1);
            myBinding.CloseTimeout = TimeSpan.FromMinutes(1);
            myBinding.SendTimeout = TimeSpan.FromMinutes(3);
            myBinding.ReceiveTimeout = TimeSpan.FromMinutes(10);
            myBinding.AllowCookies = true;
            myBinding.BypassProxyOnLocal = false;
            // myBinding.HostNameComparisonMode = HostNameComparisonMode.StrongWildcard;
            myBinding.MaxBufferPoolSize = 2147483647;
            myBinding.MaxBufferSize = 2147483647;
            myBinding.MaxReceivedMessageSize = 2147483647;
            //myBinding.TextEncoding = Encoding.UTF8;
            //myBinding.MessageEncoding = WSMessageEncoding.Mtom;
            myBinding.TransferMode = TransferMode.Buffered;
            //myBinding.MessageEncoding = System.ServiceModel.WSMessageEncoding.Text;
            myBinding.UseDefaultWebProxy = true;
            //myBinding.MessageEncoding = WSMessageEncoding.Text;
            myBinding.Security.Mode = BasicHttpSecurityMode.TransportCredentialOnly;
            //myBinding.Security.Message.ClientCredentialType = BasicHttpMessageCredentialType.UserName;
            //myBinding.Security.Message.AlgorithmSuite = SecurityAlgorithmSuite.Default;
            //myBinding.Security.Transport.ClientCredentialType = HttpClientCredentialType.Windows;
            //myBinding.Security.Transport.ProxyCredentialType = HttpProxyCredentialType.Windows;
            // myBinding.Security.Transport.Realm = "";
            myBinding.ReaderQuotas.MaxDepth = 32;
            myBinding.ReaderQuotas.MaxStringContentLength = 100000;
            myBinding.ReaderQuotas.MaxArrayLength = 16384;
            myBinding.ReaderQuotas.MaxBytesPerRead = 4096;
            myBinding.ReaderQuotas.MaxNameTableCharCount = 16384;
            return myBinding;
        }
        public async Task<CreditPolicyEntity> GetAsync(GetCrivoEntity filtros) {
            var entity = new CreditPolicyEntity();
            var result = new SetPolicyEvalValuesObjectXmlResponse();
            try {

                EndpointAddress end = new EndpointAddress(filtros.Endpoint);
                var factory = new ChannelFactory<CreditTalkStatelessSoap>(getBasicHttpBinding(), end);

                var serviceProxy = factory.CreateChannel();
                ((IClientChannel)((object)serviceProxy)).OperationTimeout = TimeSpan.FromSeconds(600);
                ((IClientChannel)((object)serviceProxy)).Open();

                result = await serviceProxy.SetPolicyEvalValuesObjectXmlAsync(new SetPolicyEvalValuesObjectXmlRequest() { sUser = filtros.Usuario, sPassword = filtros.Senha, sPolitica = filtros.Politica, sParametros = filtros.Parametros });
                ((IClientChannel)((object)serviceProxy)).Close();

                ValidarMensagensCriticas(result.ResultadoXml.Respostas);

                CrivoMapper.MapArrayResultVariavelXmlToEntity(result.ResultadoXml.Drivers, ref entity);

                return entity;

            } catch (ValidationException e) {
                throw new ServiceException(ProviderEnum.Transunion, e.GetMessages(), MethodBase.GetCurrentMethod(), new { filtros }, result);
            } catch (Exception e) {
                throw new ServiceException("Erro na comunicação com a TransUnion (BuscarPoliticaDeCredito)", e);
            }
        }

        private void ValidarMensagensCriticas(ResultRespostasXml[] respostas) {
            foreach (var item in respostas) {
                if (item.Criterio == EnumExtension.GetEnumDescription(TypePoliticsEnum.PoliticaDeCredito)) {
                    if (item.Sistema.ToLower().Equals("nok")) { //-- Respostas não OK
                        switch (item.Resposta.ToLower()) {
                            case "possui restritivos":
                                throw new ValidationException("possui restritivos");
                            case "possui restrições":
                                throw new ValidationException("possui restritivos");
                            case "consta registro":
                                throw new ValidationException("consultra improbidade admintrativa e inegibilidade");
                            case "situação irregular":
                                throw new ValidationException("situação irregular na Receita Federal");
                            case "upper middle":
                            case "recusado":
                                throw new ValidationException("fora da política de aceitação automática do gestor de crédito");
                            case "menor que 2 anos":
                                throw new ValidationException("menor de 2 anos de fundação");
                            case "menor que R$ 300 mil  de faturamento":
                                throw new ValidationException("Faturamento menor que R$ 300 mil/anos");
                            default:    //-- Respostas não mapeadas
                                throw new ValidationException(item.Resposta);
                        }
                    }
                } else if (item.Criterio == EnumExtension.GetEnumDescription(TypePoliticsEnum.ZipOnline30_ColetaRazaoSocial)) {
                    if (item.Sistema.ToLower().Equals("nok")) { //-- Respostas não OK
                        switch (item.Resposta.ToLower()) {
                            default:    //-- Respostas não mapeadas
                                throw new ValidationException(item.Resposta);
                        }
                    }
                } else if (item.Criterio == EnumExtension.GetEnumDescription(TypePoliticsEnum.ZipOnline30_ColetaEndereco)) {
                    if (item.Sistema.ToLower().Equals("nok")) { //-- Respostas não OK
                        switch (item.Resposta.ToLower()) {
                            default:    //-- Respostas não mapeadas
                                throw new ValidationException(item.Resposta);
                        }
                    }
                } else if (item.Criterio == EnumExtension.GetEnumDescription(TypePoliticsEnum.ZipOnline30_VerificaStatusSRF)) {
                    if (item.Sistema.ToLower().Equals("nok")) { //-- Respostas não OK
                        switch (item.Resposta.ToLower()) {
                            default:    //-- Respostas não mapeadas
                                throw new ValidationException(item.Resposta);
                        }
                    }
                }
            }
        }
    }
}
