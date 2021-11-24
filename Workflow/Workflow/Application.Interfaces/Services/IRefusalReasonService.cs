using Domain.Enumerators;
using Domain.Payload;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services {
    public interface IRefusalReasonService {
        Task<IList<RefusalReason>> ListAsync(RecordStatusEnum? status);
    }
}
