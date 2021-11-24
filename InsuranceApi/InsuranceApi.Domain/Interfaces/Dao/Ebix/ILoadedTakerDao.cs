using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Dao.Ebix {
    public interface ILoadedTakerDao {

        Task<int> AddAsync(LoadedTakerEntity tomador);
        Task<int> AddSignatureAsync(LoadedTakerSignatureEntity transmissao);
        Task UpdateLegacyIdAsync(int loadedTakerRecordId, int legacyId, int legacyUserId);
        Task UpdateSignatureIdAsync(int loadedTakerSignatureId, int signatureId);
        Task UpateStatusAsync(int loadedTakerRecordId, StatusCargaTomadorEnum status);
    }
}
