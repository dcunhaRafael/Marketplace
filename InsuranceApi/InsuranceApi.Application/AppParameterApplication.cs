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
    public class AppParameterApplication : IAppParameterApplication {
        private readonly IAppParameterDao appParameterDao;

        public AppParameterApplication(IAppParameterDao appParameterDao) {
            this.appParameterDao = appParameterDao;
        }

        public async Task<IList<ParametroEntity>> ListAsync() {
            try {
                return await appParameterDao.ListAsync();
            } catch (DaoException e) {
                throw new ApplicationException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ApplicationException("Erro ao listar parâmetros.", e);
            }
        }

        public async Task<ParametroEntity> GetAsync(AppParameterEnum parameter) {
            try {
                return await appParameterDao.GetAsync(parameter);
            } catch (DaoException e) {
                throw new ApplicationException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ApplicationException("Erro ao obter parâmetro por id.", e);
            }
        }   
    }
}
