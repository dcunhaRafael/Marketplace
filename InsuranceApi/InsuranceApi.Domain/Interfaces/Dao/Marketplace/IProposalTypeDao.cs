using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Dao.Marketplace {
    public interface IProposalTypeDao {
        Task<ProposalTypeEntity> GetAsync(int productExternalCode, int coverageExternalCode);
    }
}
