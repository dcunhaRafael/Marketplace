using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Service {
    public interface IProductService {
        Task<IList<ProductEntity>> ListAsync(int brokerUserId);
    }
}
