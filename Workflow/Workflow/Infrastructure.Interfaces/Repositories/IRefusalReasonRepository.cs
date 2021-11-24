using Domain.Entities;
using Domain.Enumerators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories {
    public interface IRefusalReasonRepository {
        Task<IList<RefusalReason>> ListAsync(RecordStatusEnum? status);
        Task<RefusalReason> GetAsync(int refusalReasonId);
    }
}
