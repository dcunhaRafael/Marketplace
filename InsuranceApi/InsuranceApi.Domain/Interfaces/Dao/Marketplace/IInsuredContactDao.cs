using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Dao.Marketplace {
    public interface IInsuredContactDao {
        Task<int> AddAsync(InsuredEntity item);
    }
}
