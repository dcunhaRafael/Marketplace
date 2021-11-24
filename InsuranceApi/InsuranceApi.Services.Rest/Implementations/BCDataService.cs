using Flurl.Http;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.Common.Exceptions;
using InsuranceApi.Domain.Common.Extension;
using InsuranceApi.Domain.Common.HttpClient.Configurations;
using InsuranceApi.Domain.Common.HttpClient.Enumerators;
using InsuranceApi.Domain.Interfaces.Common;
using InsuranceApi.Domain.Interfaces.Service;
using InsuranceApi.Services.Rest.Mappers;
using InsuranceApi.Services.Rest.Messages.Legacy;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Services.Rest.Implementations {
    public class BCDataService : IBCDataService {
        private readonly IHttpServiceClientFactory httpRestClient;

        public BCDataService(IHttpServiceClientFactory httpRestClient) {
            this.httpRestClient = httpRestClient;
        }

        public async Task<IList<BCDataSerieEntity>> ListAsync(int seriesCode, DateTime start, DateTime finish) {
            try {

                var response = await httpRestClient.GetBCService(seriesCode)
                                        .Request(AppConfigHttp.RotaConsultarSeries)
                                        .SetQueryParam("formato", "json")
                                        .SetQueryParam("dataInicial", start.FormatDate())
                                        .SetQueryParam("dataFinal", finish.FormatDate())
                                        .WithHeaders(new {
                                            Accept = "application/json",
                                            User_Agent = "Flurl"
                                        //User -Agent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/94.0.4606.81 Safari/537.36 Edg/94.0.992.50"
                                    })
                                        .GetStringAsync()
                                        .ConfigureAwait(false);

                //?formato=json&dataInicial={1}&dataFinal={2}

                var entity = Newtonsoft.Json.JsonConvert.DeserializeObject<BCDataTemporalSeries[]>(response);

                return BCDataMapper.MapBCDataTemporalSeriesToEntity(entity);

            } catch (InvalidCastException e) {
                throw new ServiceException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ServiceException("Erro na comunicação com o Banco Central (BCDataTemporalSeries).", e);
            }
        }
    }
}
