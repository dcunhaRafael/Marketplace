using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Dao.Marketplace {
    public interface IProposalParcDao {
        Task AddAsync(int proposalId, IList<ParcelaEntity> parcelas);
    }
}
