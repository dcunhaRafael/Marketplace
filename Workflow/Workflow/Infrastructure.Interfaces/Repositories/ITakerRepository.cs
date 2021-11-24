using Domain.Entities;
using Domain.Enumerators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories {
    public interface ITakerRepository {
        Task<IList<Taker>> ListAsync(int brokerId, string name, RecordStatusEnum? status);
    }
}
