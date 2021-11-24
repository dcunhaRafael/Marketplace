using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.Common.Exceptions;
using InsuranceApi.Domain.Interfaces.Application;
using InsuranceApi.Domain.Interfaces.Dao.Ebix;
using InsuranceApi.Domain.Interfaces.Service;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ApplicationException = InsuranceApi.Domain.Common.Exceptions.ApplicationException;

namespace InsuranceApi.Application {
    public class ProductApplication : IProductApplication {
        private readonly IProductDao produtoDao;
        private readonly IProductService produtoService;
        private readonly ICoverageService coberturaService;
        private readonly ICoverageApplication coverageApplication;

        public ProductApplication(IProductService produtoService, ICoverageService coberturaService, ICoverageApplication coverageApplication, IProductDao produtoDao) {
            this.produtoDao = produtoDao;
            this.produtoService = produtoService;
            this.coberturaService = coberturaService;
            this.coverageApplication = coverageApplication;
        }

        public async Task<ProductEntity> GetAsync(string externalCode) {
            try {
                return await produtoDao.GetAsync(externalCode);
            } catch (ServiceException e) {
                throw new ApplicationException(e.Message, e.InnerException);
            } catch (Exception e) {
                throw new ApplicationException("Erro na buscar o produto externo.", e);
            }
        }

        public async Task<List<ProdutoModalidadeEntity>> ListAsync(int brokerUserId) {
            List<ProdutoModalidadeEntity> produtoModalidadeEntities = new List<ProdutoModalidadeEntity>();

            var produtoEntity = await produtoService.ListAsync(brokerUserId);
            if (produtoEntity != null) {

                foreach (var produto in produtoEntity) {

                    ProdutoModalidadeEntity produtoModalidade = new ProdutoModalidadeEntity();

                    produtoModalidade.CodigoProduto = produto.CodigoExterno;
                    produtoModalidade.NomeProduto = produto.NomeProduto;

                    var coberturaEntity = await coberturaService.ListAsync(produto.CodigoExterno.Value, brokerUserId); 
                    if (coberturaEntity != null) {
                        foreach (var cobertura in coberturaEntity) {
                            var coberturaCadastrada = await coverageApplication.GetAsync(produto.CodigoExterno.ToString(), cobertura.IdCobertura.ToString());
                            if (coberturaCadastrada != null) {
                                produtoModalidade.Modalidade.Add(cobertura);
                            }
                        }
                    }
                    produtoModalidadeEntities.Add(produtoModalidade);
                }
            }
            return produtoModalidadeEntities;
        }
    }
}
