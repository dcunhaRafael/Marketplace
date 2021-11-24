using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.Common.Exceptions;
using InsuranceApi.Domain.Interfaces.Application;
using InsuranceApi.Domain.Interfaces.Dao.Ebix;
using System;
using System.Threading.Tasks;
using ApplicationException = InsuranceApi.Domain.Common.Exceptions.ApplicationException;

namespace InsuranceApi.Application {
    public class SalesChannelApplication : ISalesChannelApplication {
        private readonly ISalesChannelDao salesChannelDao;

        public SalesChannelApplication(ISalesChannelDao salesChannelDao) {
            this.salesChannelDao = salesChannelDao;
        }

        public async Task<SalesChannelEntity> GetAsync(int productId, long personId) {
            try {
                return await salesChannelDao.GetAsync(productId, personId);
            } catch (ServiceException e) {
                throw new ApplicationException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ApplicationException("Erro na busca do canal de venda", e);
            }
        }
    }
}
