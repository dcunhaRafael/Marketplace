using Domain.Enumerators;
using Domain.Payload;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services {
    public interface IDocumentTypeService {
        Task<IList<DocumentType>> ListAsync(RecordStatusEnum? status);
    }
}
