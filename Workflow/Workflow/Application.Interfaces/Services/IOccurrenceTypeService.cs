using Domain.Payload;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services {
    public interface IOccurrenceTypeService {
        Task<IList<OccurrenceType>> ListAsync(OccurrenceTypeFilters filters);
        Task<OccurrenceType> GetAsync(int occurrenceTypeId);
        Task<int> SaveAsync(OccurrenceType item);
        Task DeleteAsync(OccurrenceType item);
    }
}
