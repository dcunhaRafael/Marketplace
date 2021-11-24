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
    public class LaborCourtApplication : ILaborCourtApplication {
        private readonly ILaborCourtDao laborCourtDao;

        public LaborCourtApplication(ILaborCourtDao laborCourtDao) {
            this.laborCourtDao = laborCourtDao;
        }

        public async Task<IList<LaborCourtEntity>> ListAsync(string name) {
            try {
                return await laborCourtDao.ListAsync(name, RecordStatusEnum.Ativo);
            } catch (ServiceException e) {
                throw new ApplicationException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ApplicationException("Erro ao listar tribunais.", e);
            }
        }

        public async Task<LaborCourtEntity> GetAsync(int laborCourtId) {
            try {
                return await laborCourtDao.GetAsync(laborCourtId);
            } catch (ServiceException e) {
                throw new ApplicationException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ApplicationException("Erro ao obter tribunal.", e);
            }
        }

        public async Task UpdateAsync(LaborCourtEntity item) {
            try {
                await laborCourtDao.UpdateExternalCodeAsync(item);
            } catch (ServiceException e) {
                throw new ApplicationException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ApplicationException("Erro ao alterar código externo do tribunal.", e);
            }
        }
    }
}
