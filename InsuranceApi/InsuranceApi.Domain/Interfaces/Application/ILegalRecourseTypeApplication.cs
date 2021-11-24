using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Application {
    public interface ILegalRecourseTypeApplication {
        Task<IList<LegalRecourseTypeEntity>> ListAsync();
        Task<LegalRecourseTypeEntity> GetAsync(int legalRecourseTypeId);
        Task<RecursoParametroEntity> GetParameterAsync(int legalRecourseTypeId);
        Task<TakerAppealFeeEntity> GetAppealFeeAsync(int takerExternalId, int productId, int coverageId, decimal insuredAmount, int termTypeId);
        Task<RateEntity> GetRateAsync(decimal insuredAmount, int termTypeId);
    }
}
