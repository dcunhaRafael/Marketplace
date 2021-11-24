using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Dao.Marketplace {
    public interface IPaymentInstallmentsDao {
        Task AddAsync(int policyId, IList<ParcelaEntity> parcelas);
    }
}
