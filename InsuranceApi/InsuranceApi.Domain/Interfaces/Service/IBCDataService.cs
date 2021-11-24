using InsuranceApi.Domain.BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Service {
    public interface IBCDataService {
        Task<IList<BCDataSerieEntity>> ListAsync(int seriesCode, DateTime start, DateTime finish);
    }
}
