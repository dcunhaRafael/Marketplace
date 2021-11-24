using Domain.Entities;
using Domain.Enumerators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories {
    public interface IDocumentTypeRepository {
        Task<IList<DocumentType>> ListAsync(RecordStatusEnum? status);
        Task<DocumentType> GetAsync(int documentTypeId);
    }
}
