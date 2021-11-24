using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Application {
    public interface ILoadedTakerApplication {
        Task SaveAsync(TakerModel taker, int legacyId, int legacyUserId, string ipAddress, string userAgent);
    }
}
