using Domain.Entities;
using Domain.Enumerators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories {
    public interface IOccurrenceTypeLiberationUserRepository {
        Task<IList<OccurrenceTypeLiberationUser>> ListAsync(int occurrenceTypeId);
        Task AddAsync(OccurrenceTypeLiberationUser item);
        Task UpdateAsync(OccurrenceTypeLiberationUser item);
        Task UpdateStatusNotInAsync(OccurrenceType master, IList<int> ids);
    }
}
