using Domain.Entities;
using Domain.Enumerators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services {
    public interface IFixedDomainService {
        Task<IList<FixedDomain>> List(string name, FixedDomainGroupNameEnum? groupName);
        Task<FixedDomain> Get(int id);
        Task Save(FixedDomain item, Domain.Payload.LoggerComplement complement);
    }
}
