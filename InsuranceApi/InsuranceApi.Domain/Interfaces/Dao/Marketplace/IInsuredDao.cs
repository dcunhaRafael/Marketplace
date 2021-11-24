using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Dao.Marketplace {
    public interface IInsuredDao {
        Task<InsuredEntity> GetAsync(long cpfCnpj);
        Task<int> AddAsync(InsuredEntity item);
    }
}
