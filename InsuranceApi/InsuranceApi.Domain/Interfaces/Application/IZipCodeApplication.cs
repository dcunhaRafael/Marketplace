using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Application {
    public interface IZipCodeApplication {
        Task<ZipCodeEntity> GetAsync(string zipCode);
    }
}
