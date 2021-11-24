using Domain.Enumerators;
using Domain.Payload;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services {
    public interface IBrokerService {
        Task<IList<Broker>> ListAsync(string name, RecordStatusEnum? status);
    }
}
