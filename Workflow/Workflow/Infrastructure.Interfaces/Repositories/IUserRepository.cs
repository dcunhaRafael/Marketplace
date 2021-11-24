using Domain.Entities;
using Domain.Enumerators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories {
    public interface IUserRepository {
        Task<IList<User>> ListAsync(int profileId, RecordStatusEnum? status);
        Task<User> GetAsync(int UserId);
    }
}
