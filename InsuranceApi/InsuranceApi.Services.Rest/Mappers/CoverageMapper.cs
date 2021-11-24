using InsuranceApi.Domain.BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using InsuranceApi.Services.Rest.Messages.Legacy;

namespace InsuranceApi.Services.Rest.Mappers {
    public static class CoverageMapper {


        public static List<CoverageEntity> MapCoberturaBuscarResponseToEntity(CoberturaBuscarResponse response) {
            try {
                var mappedList = new List<CoverageEntity>();
                foreach (var item in response.Cobertura_Buscar) {
                    mappedList.Add(new CoverageEntity()
                    {
                        IdCobertura = int.Parse(item.id_produto_cobertura),
                        NomeCobertura = item.nm_cobertura
                    });
                }
                return mappedList;
            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados do legado (Cobertura_Buscar).", e);
            }
        }
    }
}
