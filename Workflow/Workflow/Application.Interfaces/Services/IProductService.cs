using Domain.Enumerators;
using Domain.Payload;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services {
    public interface IProductService {
        Task<IList<Product>> ListAsync(RecordStatusEnum? status);
    }
}
