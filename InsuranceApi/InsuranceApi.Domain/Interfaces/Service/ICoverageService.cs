using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Service {
    public interface ICoverageService {
      Task<IList<CoverageEntity>> ListAsync(int externalCode, int brokerUserId);
    }
}
