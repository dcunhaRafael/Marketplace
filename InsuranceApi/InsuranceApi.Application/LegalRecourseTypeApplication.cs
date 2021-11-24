using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.Common.Exceptions;
using InsuranceApi.Domain.Interfaces.Application;
using InsuranceApi.Domain.Interfaces.Dao.Ebix;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationException = InsuranceApi.Domain.Common.Exceptions.ApplicationException;

namespace InsuranceApi.Application {
    public class LegalRecourseTypeApplication : ILegalRecourseTypeApplication {
        private readonly ILegalRecourseTypeDao legalRecourseTypeDao;
        private readonly ITakerAppealFeeDao takerAppealFeeDao;
        private readonly IRateDao rateDao;

        public LegalRecourseTypeApplication(ILegalRecourseTypeDao legalRecourseTypeDao, ITakerAppealFeeDao takerAppealFeeDao, IRateDao rateDao) {
            this.legalRecourseTypeDao = legalRecourseTypeDao;
            this.takerAppealFeeDao = takerAppealFeeDao;
            this.rateDao = rateDao;
        }

        public async Task<IList<LegalRecourseTypeEntity>> ListAsync() {
            try {
                return await legalRecourseTypeDao.ListAsync();
            } catch (ServiceException e) {
                throw new ApplicationException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ApplicationException("Erro ao listar tipos de recurso.", e);
            }
        }

        public async Task<LegalRecourseTypeEntity> GetAsync(int legalRecourseTypeId) {
            try {
                var item = await this.legalRecourseTypeDao.GetAsync(legalRecourseTypeId);
                return item;
            } catch (ServiceException e) {
                throw new ApplicationException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ApplicationException("Erro ao buscar tipo de recurso.", e);
            }
        }

        public async Task<RecursoParametroEntity> GetParameterAsync(int legalRecourseTypeId) {
            try {
                var item = await this.legalRecourseTypeDao.GetParameterAsync(legalRecourseTypeId);
                return item;
            } catch (ServiceException e) {
                throw new ApplicationException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ApplicationException("Erro ao buscar parâmetros do tipo de recurso.", e);
            }
        }

        public async Task<TakerAppealFeeEntity> GetAppealFeeAsync(int takerExternalId, int productId, int coverageId, decimal insuredAmount, int termTypeId) {
            try {
                return await takerAppealFeeDao.GetAsync(takerExternalId, productId, coverageId, insuredAmount, termTypeId);
            } catch (ServiceException e) {
                throw new ApplicationException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ApplicationException("Erro ao buscar taxa do tomador.", e);
            }
        }

        public async Task<RateEntity> GetRateAsync(decimal insuredAmount, int termTypeId) {
            try {
                return await rateDao.GetAsync(insuredAmount, termTypeId);
            } catch (ServiceException e) {
                throw new ApplicationException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ApplicationException("Erro ao buscar taxa.", e);
            }
        }
    }
}
