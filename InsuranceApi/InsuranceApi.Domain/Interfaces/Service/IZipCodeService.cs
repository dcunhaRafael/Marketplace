using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Service {
    public interface IZipCodeService {
        Task<ZipCodeEntity> GetAsync(string zipCode);
    }
}
