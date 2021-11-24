using Domain.Entities;
using Domain.Enumerators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories {
    public interface IBrokerRepository {
        Task<IList<Broker>> ListAsync(string name, RecordStatusEnum? status);
    }
}
