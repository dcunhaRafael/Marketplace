using Application.Interfaces.Services;
using Domain.Enumerators;
using Domain.Exceptions;
using Domain.Payload;
using Domain.Util.Log;
using Infrastructure.Interfaces.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Application.Services {
    public class ProductService : BaseLogger, IProductService {
        private readonly IProductRepository productRepository;

        public ProductService(ILogger<ProductService> logger, IProductRepository productRepository) : base(logger) {
            this.productRepository = productRepository;
        }

        public async Task<IList<Product>> ListAsync(RecordStatusEnum? status) {
            var methodParameters = new { status };
            LogTrace(MethodBase.GetCurrentMethod(), "Init", methodParameters);
            try {

                var items = await productRepository.ListAsync(status);
                var payloads = from a in items select new Product() { ProductId = a.ProductId, Name = a.Name };
                return payloads.ToList();

            } catch (Exception e) {
                if (!(e is ServiceException || e is IntegrationException)) {
                    LogError(MethodBase.GetCurrentMethod(), methodParameters, e, new LoggerComplement());
                }
                throw new ServiceException($"Erro na listagem dos produtos: {e.Message}", e);
            } finally {
                LogTrace(MethodBase.GetCurrentMethod(), "End");
            }
        }

    }
}
