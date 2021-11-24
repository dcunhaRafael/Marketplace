using Domain.Entities;
using Domain.Enumerators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories {
    public interface IPendingInstallmentRepository {
        Task<int> GetPendingCount(long cpfCnpjNumber);
    }
}
