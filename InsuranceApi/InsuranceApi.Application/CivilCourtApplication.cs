using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.BusinessObjects.Enumerators;
using InsuranceApi.Domain.Common.Exceptions;
using InsuranceApi.Domain.Interfaces.Application;
using InsuranceApi.Domain.Interfaces.Dao.Ebix;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationException = InsuranceApi.Domain.Common.Exceptions.ApplicationException;

namespace InsuranceApi.Application {
    public class CivilCourtApplication : ICivilCourtApplication {
        private readonly ICivilCourtDao civilCourtDao;

        public CivilCourtApplication(ICivilCourtDao civilCourtDao) {
            this.civilCourtDao = civilCourtDao;
        }

        public async Task<IList<CivilCourtEntity>> ListAsync(int laborCourtId, string name) {
            try {
                return await civilCourtDao.ListAsync(laborCourtId, name, RecordStatusEnum.Ativo);
            } catch (ServiceException e) {
                throw new ApplicationException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ApplicationException("Erro ao listar varas.", e);
            }
        }

        public async Task<CivilCourtEntity> GetAsync(int civilCourtId, int laborCourtId) {
            try {
                return await civilCourtDao.GetAsync(civilCourtId, laborCourtId);
            } catch (ServiceException e) {
                throw new ApplicationException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ApplicationException("Erro ao obter vara.", e);
            }
        }

        public async Task UpdateAsync(CivilCourtEntity entity) {
            try {
                await civilCourtDao.UpdateExternalCodeAsync(entity);
            } catch (ServiceException e) {
                throw new ApplicationException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ApplicationException("Erro ao alterar código externo da vara.", e);
            }
        }
    }
}
