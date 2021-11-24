using CreditTalk;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using InsuranceApi.Domain.Common.Extension;
using System;

namespace InsuranceApi.Services.Soap.Mappers {
    public static class CrivoMapper {


        public static CreditPolicyEntity MapArrayResultVariavelXmlToEntity(ResultDriverXml[] response, ref CreditPolicyEntity entity) {
            try {

                foreach (var resp in response) {
                    foreach (var produto in resp.Produtos) {
                        foreach (var variavel in produto.Variaveis) {
                            MapResultVariavelXmlToEntity(produto.Nome, variavel, ref entity);
                        }
                    }
                }
                return entity;

            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados da requisição (BuscarPoliticaDeCredito).", e);
            }
        }

        public static CreditPolicyEntity MapResultVariavelXmlToEntity(string nomeProduto, ResultVariavelXml response, ref CreditPolicyEntity entity) {
            try {

                if (nomeProduto.Equals(EnumExtension.GetEnumDescription(TypePoliticsEnum.PoliticaDeCredito)) ||
                    nomeProduto.Equals(EnumExtension.GetEnumDescription(TypePoliticsEnum.DefiniçãoLimiteRating))) {

                    if (response.Nome.ToUpper() == EnumExtension.GetEnumDescription(PoliticsCreditEnum.MV_RAZAO_SOCIAL)) {
                        entity.RazaoSocial = response.Valor;
                    } else if (response.Nome.ToUpper() == EnumExtension.GetEnumDescription(PoliticsCreditEnum.MV_LIMITE)) {
                        entity.LimiteCredito = response.Valor.ToNullableDecimalCleanDiv100();
                    } else if (response.Nome.ToUpper() == EnumExtension.GetEnumDescription(PoliticsCreditEnum.MV_PONTUACAO_BMG)) {
                        entity.Pontuacao = response.Valor.ToNullableInt();
                    } else if (response.Nome.ToUpper() == EnumExtension.GetEnumDescription(PoliticsCreditEnum.MV_RATING)) {
                        entity.Rating = response.Valor;
                    } else if (response.Nome.ToUpper() == EnumExtension.GetEnumDescription(PoliticsCreditEnum.MV_TAXA)) {
                        entity.Taxa = response.Valor.ToNullableDecimalPercentage();
                    } else if (response.Nome.ToUpper() == EnumExtension.GetEnumDescription(DefinitionLimiteRatingEnum.QUALIDADE_EMPRESA)) {
                        entity.QualidadeEmpresa = response.Valor.ToNullableDecimalCleanDiv100();
                    } else if (response.Nome.ToUpper() == EnumExtension.GetEnumDescription(PoliticsCreditEnum.MV_CEP)) {
                        entity.Endereco.Cep = response.Valor;
                    } else if (response.Nome.ToUpper() == EnumExtension.GetEnumDescription(PoliticsCreditEnum.MV_LOGRADOURO)) {
                        entity.Endereco.Logradouro = response.Valor;
                    } else if (response.Nome.ToUpper() == EnumExtension.GetEnumDescription(PoliticsCreditEnum.MV_NR_LOGRADOURO)) {
                        entity.Endereco.Numero = response.Valor;
                    } else if (response.Nome.ToUpper() == EnumExtension.GetEnumDescription(PoliticsCreditEnum.MV_COMPLEMENTO)) {
                        entity.Endereco.Complemento = response.Valor;
                    } else if (response.Nome.ToUpper() == EnumExtension.GetEnumDescription(PoliticsCreditEnum.MV_BAIRRO)) {
                        entity.Endereco.Bairro = response.Valor;
                    } else if (response.Nome.ToUpper() == EnumExtension.GetEnumDescription(PoliticsCreditEnum.MV_CIDADE)) {
                    } else if (response.Nome.ToUpper() == EnumExtension.GetEnumDescription(PoliticsCreditEnum.MV_CIDADE)) {
                        entity.Endereco.Cidade = response.Valor;
                    } else if (response.Nome.ToUpper() == EnumExtension.GetEnumDescription(PoliticsCreditEnum.MV_UF)) {
                        entity.Endereco.UF = response.Valor;
                    }

                } else if (nomeProduto.Equals(EnumExtension.GetEnumDescription(TypePoliticsEnum.ZipOnline30_ColetaRazaoSocial)) ||
                           nomeProduto.Equals(EnumExtension.GetEnumDescription(TypePoliticsEnum.ZipOnline30_ColetaEndereco)) ||
                           nomeProduto.Equals(EnumExtension.GetEnumDescription(TypePoliticsEnum.ZipOnline30_VerificaStatusSRF)) ||
                           nomeProduto.Equals(EnumExtension.GetEnumDescription(TypePoliticsEnum.ZipOnline30_ConsultaPorCnpjPj))) {

                    if (response.Nome.ToUpper().Equals(EnumExtension.GetEnumDescription(PoliticsCreditEnum.DiscretaRazaoSocial).ToUpper())) {
                        entity.RazaoSocial = response.Valor;
                    } else if (response.Nome.ToUpper().Equals(EnumExtension.GetEnumDescription(PoliticsCreditEnum.DiscretaCepEnderecoSituacaoSRF).ToUpper())) {
                        entity.Endereco.Cep = string.Format("{0:D8}", response.Valor.ToNullableInt());
                    } else if (response.Nome.ToUpper().Equals(EnumExtension.GetEnumDescription(PoliticsCreditEnum.DiscretaLogradouroEnderecoSituacaoSRF).ToUpper())) {
                        entity.Endereco.Logradouro = response.Valor;
                    } else if (response.Nome.ToUpper().Equals(EnumExtension.GetEnumDescription(PoliticsCreditEnum.DiscretaNumeroLogradouroEnderecoSituacaoSRF).ToUpper())) {
                        entity.Endereco.Numero = response.Valor;
                    } else if (response.Nome.ToUpper().Equals(EnumExtension.GetEnumDescription(PoliticsCreditEnum.DiscretaComplementoEnderecoSituacaoSRF).ToUpper())) {
                        entity.Endereco.Complemento = response.Valor;
                    } else if (response.Nome.ToUpper().Equals(EnumExtension.GetEnumDescription(PoliticsCreditEnum.DiscretaBairroEnderecoSituacaoSRF).ToUpper())) {
                        entity.Endereco.Bairro = response.Valor;
                    } else if (response.Nome.ToUpper().Equals(EnumExtension.GetEnumDescription(PoliticsCreditEnum.DiscretaCidadeEnderecoSituacaoSRF).ToUpper())) {
                        entity.Endereco.Cidade = response.Valor;
                    } else if (response.Nome.ToUpper().Equals(EnumExtension.GetEnumDescription(PoliticsCreditEnum.DiscretaUfEnderecoSituacaoSRF).ToUpper())) {
                        entity.Endereco.UF = response.Valor;
                    }

                }
                return entity;

            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados da requisição (BuscarPoliticaDeCredito).", e);
            }
        }
    }
}
