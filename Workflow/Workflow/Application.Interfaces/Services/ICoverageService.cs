using Domain.Enumerators;
using Domain.Payload;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services {
    public interface ICoverageService {
        Task<IList<Coverage>> ListAsync(int productId, RecordStatusEnum? status);
    }
}
