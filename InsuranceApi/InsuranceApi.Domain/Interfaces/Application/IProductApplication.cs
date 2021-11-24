using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Application {
    public interface IProductApplication {

        Task<List<ProdutoModalidadeEntity>> ListAsync(int brokerUserId);
        Task<ProductEntity> GetAsync(string externalCode);
    }
}
