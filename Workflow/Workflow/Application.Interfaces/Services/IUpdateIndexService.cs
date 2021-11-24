using Domain.Payload;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services {
    public interface IUpdateIndexService {
        Task<IList<UpdateIndex>> ListAsync();
    }
}
