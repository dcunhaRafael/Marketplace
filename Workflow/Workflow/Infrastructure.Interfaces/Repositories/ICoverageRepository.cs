using Domain.Entities;
using Domain.Enumerators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories {
    public interface ICoverageRepository {
        Task<IList<Coverage>> ListAsync(int productId, RecordStatusEnum? status);
    }
}
