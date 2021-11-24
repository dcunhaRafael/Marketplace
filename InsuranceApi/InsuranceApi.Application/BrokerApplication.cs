using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.Common.Exceptions;
using InsuranceApi.Domain.Common.Extension;
using InsuranceApi.Domain.Interfaces.Application;
using InsuranceApi.Domain.Interfaces.Dao.Ebix;
using InsuranceApi.Domain.Interfaces.Service;
using System;
using System.Linq;
using System.Threading.Tasks;
using ApplicationException = InsuranceApi.Domain.Common.Exceptions.ApplicationException;

namespace InsuranceApi.Application {
    public class BrokerApplication : IBrokerApplication {
        private readonly IBrokerDao brokerDao;
        private readonly IBrokerService brokerService;

        public BrokerApplication(IBrokerDao brokerDao, IBrokerService brokerService) {
            this.brokerDao = brokerDao;
            this.brokerService = brokerService;
        }

        public async Task<BrokerEntity> GetAsync(int externalCode) {
            try {
                return await brokerDao.GetAsync(externalCode);
            } catch (ApplicationException e) {
                throw new ApplicationException("Erro ao buscar corretor por código externo.", e);
            }
        }

        public async Task<CorretorConsultaEntity> GetAsync(CorretorConsultaEntity loggedBroker, int? proposalUserId) {
            CorretorConsultaEntity corretorAcessoProposta = loggedBroker;
            try {

                if (corretorAcessoProposta == null && proposalUserId != null) {

                    var corretorProposta = await brokerService.GetAsync(proposalUserId.Value);
                    var corretores = await brokerService.ListAsync(new CorretorConsultaEntity {
                        CpfCnpj = corretorProposta.CpfCnpj.ToString()
                    });
                    if (corretores != null && corretores.Count > 0) {
                        corretorAcessoProposta = corretores.First();
                    }
                }

            } catch (Exception ex) {
                if ((ex is ServiceException)) {
                    throw ex;
                }
                if (!ex.Message.Equals("Não existem dados para os parâmetros informados.")) {
                    throw new BusinessException("Corretor associado ao usuário não pode ser localizado no legado");
                }
            }
            return corretorAcessoProposta;
        }

        public async Task<CorretorConsultaEntity> GetAsync(long documentNumber) {
            try {

                var corretores = await brokerService.ListAsync(new CorretorConsultaEntity() { CpfCnpj = documentNumber.FormatLongToCpfCnpj().ApenasNumericos() });
                return corretores.FirstOrDefault();

            } catch (Exception ex) {
                throw ex;
            }
        }
    }
}