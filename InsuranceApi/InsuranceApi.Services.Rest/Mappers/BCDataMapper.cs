using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.Common.Extension;
using InsuranceApi.Services.Rest.Messages.Legacy;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace InsuranceApi.Services.Rest.Mappers {
    public static class BCDataMapper {

        /// <summary>
        /// Converte a resposta da busca de cep
        /// </summary>
        /// <param name="raw">Resposta crua do serviço</param>
        /// <returns></returns>
        public static IList<BCDataSerieEntity> MapBCDataTemporalSeriesToEntity(BCDataTemporalSeries[] response) {
            try {

                var mappedList = new List<BCDataSerieEntity>();
                foreach (var item in response) {
                    mappedList.Add(new BCDataSerieEntity() {
                        Date = DateTime.ParseExact(item.data, "dd/MM/yyyy", null),
                        Value = decimal.Parse(item.valor, new CultureInfo("en-US"))
                    });
                }
                return mappedList;

            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados do legado (BCDataTemporalSeries).", e);
            }
        }
    }
}
