using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.Interfaces.Application;
using InsuranceApi.Domain.Interfaces.Service;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace InsuranceApi.Application {
    public class InsuredApplication : IInsuredApplication {
        private readonly IInsuredService insuredService;

        public InsuredApplication(IInsuredService insuredService) {
            this.insuredService = insuredService;
        }

        public async Task<InsuredEntity> GetAsync(string cpfCnpj) {
            InsuredEntity insured = null;
            SeguradoBuscarEntity filters = new SeguradoBuscarEntity {
                NomePessoa = null,
                CpfCnpj = cpfCnpj
            };

            var insureds = await insuredService.ListAsync(filters);
            if (insureds != null && insureds.Count > 0) {
                insured = insureds.First();
            }
            return insured;
        }

        public async Task<InsuredEntity> IsRegisteredAsync(long cpfCnpj) {
            InsuredEntity insured = null;
            try {

                SeguradoBuscarEntity filters = new SeguradoBuscarEntity {
                    NomePessoa = null,
                    CpfCnpj = cpfCnpj.ToString()
                };

                var insureds = await insuredService.ListAsync(filters);
                if (insureds != null && insureds.Count > 0) {
                    insured = insureds.First();
                }

            } catch (Exception e) {
                //TODO i4pro gera erro quando não encontra
                if (!e.Message.Equals("Não existe dados para os parâmetros informados.")
                && !e.Message.Equals("Não existe dados para os parâmetros informado.")) {
                    throw e;
                }
            }
            return insured;
        }
    }
}
