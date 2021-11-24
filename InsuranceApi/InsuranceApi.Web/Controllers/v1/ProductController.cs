using AutoMapper;
using InsuranceApi.Domain.Interfaces.Application;
using InsuranceApi.Domain.Interfaces.InfraIdentity;
using InsuranceApi.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace InsuranceApi.Web.Controllers {

    [Authorize]
    [Route("api/produto")]
    public class ProductController : BaseController {
        private readonly IMapper mapper;
        private readonly IProductApplication productApplication;

        public ProductController(IUser user, ILogger<ProductController> logger, IMapper mapper,
                                 IAuditApplication auditApplication, IProductApplication productApplication) : base(user, logger, auditApplication) {
            this.mapper = mapper;
            this.productApplication = productApplication;
        }

        [HttpPost("produto_buscar")]
        public async Task<ActionResult> ListDemandAsync() {
            try {

                base.WriteAuditData(LogLevel.Trace, "Listar Produtos", null, null);

                var produtoEntity = await productApplication.ListAsync(base.UsuarioExternalId);
                if (produtoEntity == null || produtoEntity.Count == 0) return NotFound();

                var products = mapper.Map<List<ProductModalityViewModel>>(produtoEntity);
                base.WriteAuditData(LogLevel.Debug, "Listar Produtos", null, products);

                return base.ReturnSuccess(products);

            } catch (Exception e) {
                base.WriteAuditData(LogLevel.Error, "Listar Produtos", null, e);
                return ReturnErrorAndLog(e, MethodBase.GetCurrentMethod(), null);
            }
        }
    }
}