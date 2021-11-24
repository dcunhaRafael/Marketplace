using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Services.Rest.Messages.Legacy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace InsuranceApi.Services.Rest.Mappers {
    public static class ZipCodeMapper {

        /// <summary>
        /// Converte a resposta da busca de cep
        /// </summary>
        /// <param name="raw">Resposta crua do serviço</param>
        /// <returns></returns>
        public static ZipCodeEntity MapCepBuscarResponseToEntity(CepBuscarResponse response) {
            try {
                var cep = response.Consulta_CEP.FirstOrDefault();
                if (cep == null)
                    return null;
                var mappedList = new ZipCodeEntity() {
                    Logradouro = cep.nm_endereco,
                    Bairro = cep.nm_bairro,
                    Cidade = cep.nm_cidade,
                    IdUf = BaseMapper.StringToInt(cep.cd_uf),
                    UF = cep.nm_uf,
                    IdCidade = int.Parse(cep.id_local)                    
                };
                return mappedList;
            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados do legado (Cep_Buscar).", e);
            }
        }
    }
}
