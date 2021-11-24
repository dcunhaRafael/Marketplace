using Domain.Entities;
using Domain.Enumerators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories {
    public interface IProductRepository {
        Task<IList<Product>> ListAsync(RecordStatusEnum? status);
    }
}
