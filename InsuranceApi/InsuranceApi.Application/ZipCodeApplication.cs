using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.Interfaces.Application;
using InsuranceApi.Domain.Interfaces.Service;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceApi.Application {
    public class ZipCodeApplication : IZipCodeApplication {
        private readonly IZipCodeService zipCodeService;
        private readonly ICityService cityService;

        public ZipCodeApplication(IZipCodeService zipCodeService, ICityService cityService) {
            this.zipCodeService = zipCodeService;
            this.cityService = cityService;
        }

        public async Task<ZipCodeEntity> GetAsync(string zipCode) {
            var address = await zipCodeService.GetAsync(zipCode);
            if (address.IdCidade == null) {
                var city = await cityService.ListAsync(address.IdUf.Value, address.Cidade);
                address.IdCidade = city.FirstOrDefault().IdCidade;
                address.IdUf = city.FirstOrDefault().CodigoUf;
            }
            return address;
        }
    }
}
