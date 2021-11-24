using Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories {
    public interface IOccurrenceTypeRepository {
        Task<IList<OccurrenceType>> ListAsync(OccurrenceType filters);
        Task<OccurrenceType> GetAsync(int occurrenceTypeId);
        Task<int> AddAsync(OccurrenceType item);
        Task UpdateAsync(OccurrenceType item);
        Task UpdateStatusAsync(OccurrenceType item);
    }
}
