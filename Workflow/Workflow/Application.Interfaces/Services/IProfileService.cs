using Domain.Enumerators;
using Domain.Payload;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services {
    public interface IProfileService {
        Task<IList<Profile>> ListAsync(RecordStatusEnum? status);
    }
}
