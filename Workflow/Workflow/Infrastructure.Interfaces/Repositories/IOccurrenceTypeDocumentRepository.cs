using Domain.Entities;
using Domain.Enumerators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories {
    public interface IOccurrenceTypeDocumentRepository {
        Task<IList<OccurrenceTypeDocument>> ListAsync(int occurrenceTypeId);
        Task AddAsync(OccurrenceTypeDocument item);
        Task UpdateAsync(OccurrenceTypeDocument item);
        Task UpdateStatusNotInAsync(OccurrenceType master, IList<int> ids);
    }
}
