using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Services.Rest.Messages.Legacy;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceApi.Services.Rest.Mappers {
    public static class CityMapper {
        public static CidadeConsultarRequest MapCidadeConsultarEntityToRequest(int codigoUf, string nomeCidade) {
            try {
                CidadeConsultarRequest request = new CidadeConsultarRequest
                {
                    cd_uf = codigoUf,
                    nm_cidade = nomeCidade
                };
                return request;
            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados da requisição (Consulta_UF).", e);
            }
        }

        public static List<CidadeConsultarEntity> MapCidadeConsultarResponseToEntity(CidadeConsultarResponse response) {
            try {
                var mappedList = new List<CidadeConsultarEntity>();
                foreach (var item in response.Consulta_Cidade) {
                    mappedList.Add(new CidadeConsultarEntity()
                    {
                        CodigoUf = item.cd_uf,
                        IdCidade = int.Parse(item.chave_local),
                        NomeCidade = item.nm_cidade
                    });
                }
                return mappedList;
            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados da requisição (Consulta_UF).", e);
            }
        }
    }
}

