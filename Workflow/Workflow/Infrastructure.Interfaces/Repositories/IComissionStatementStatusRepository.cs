using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories {
    public interface IComissionStatementStatusRepository {
        Task<IList<ComissionStatementStatus>> ListAsync();
    }
}
