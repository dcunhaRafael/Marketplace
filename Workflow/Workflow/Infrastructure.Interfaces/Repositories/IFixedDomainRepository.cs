using Domain.Entities;
using Domain.Enumerators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories {
    public interface IFixedDomainRepository {
        Task<IList<FixedDomain>> ListAsync(FixedDomainGroupNameEnum group);
        Task<IList<FixedDomain>> ListAsync(string name, FixedDomainGroupNameEnum? groupName);

        Task<FixedDomain> GetAsync(int id);
        Task<FixedDomain> GetAsync(FixedDomainGroupNameEnum group, string legacyCode);
        Task<int?> GetIdAsync(FixedDomainGroupNameEnum group, string legacyCode);
        Task UpdateAsync(FixedDomain item);
    }
}
