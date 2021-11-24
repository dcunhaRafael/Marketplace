using Domain.Enumerators;
using Domain.Payload;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services {
    public interface ITakerService {
        Task<IList<Taker>> ListAsync(int brokerId, string name, RecordStatusEnum? status);
    }
}
