using Domain.Enumerators;
using Domain.Payload;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services {
    public interface IUserService {
        Task<IList<User>> ListAsync(int profileId, RecordStatusEnum? status);
    }
}
