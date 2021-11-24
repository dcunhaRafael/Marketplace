using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories {
    public interface IUpdateIndexRepository {
        Task<IList<UpdateIndex>> ListAsync();
    }
}
