using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Dao.Ebix {
    public  interface IProposalSignatureDao {
        Task AddAsync(ProposalAssinaturaEntity item);
        Task UpdateAsync(ProposalAssinaturaEntity item);
        Task<ProposalAssinaturaEntity> GetAsync(int proposalCode);
        Task<StatusAssinaturaPropostaEnum> GetStatusAsync(int proposalCode);
    }
}
