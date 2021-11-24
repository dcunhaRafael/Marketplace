using Domain.Exceptions;
using Domain.Payload;
using LazyCache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.Web.Services.Proxy;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Presentation.Web.Controllers {
    public class CommonController : BaseController {
        private readonly ICommonService commonService;

        public CommonController(IAppCache memoryCache, ILogger<CommonController> logger, ICommonService commonService) : base(memoryCache, logger, commonService) {
            this.commonService = commonService;
        }

        public ActionResult KeepAlive() {
            return Json(new {
                Status = "OK"
            });
        }

        public IActionResult ListCoverages(int productId) {
            try {
                base.AddAudit(LogLevel.Trace, "Common", "Listar coberturas", new { productId });

                var items = commonService.ListCoverages(productId);
                base.AddAudit(LogLevel.Debug, "Common", "Listar coberturas", new { productId }, items);
                return Json(new {
                    Coverages = items
                });

            } catch (Exception e) {
                base.AddAudit(LogLevel.Error, "Common", "Listar coberturas", new { productId }, e);
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { productId }, e);
                }
            }
        }

        public IActionResult ListBrokers(string name) {
            try {
                base.AddAudit(LogLevel.Trace, "Common", "Listar corretores", new { name });

                IList<Broker> items = new List<Broker>();
                if (!string.IsNullOrWhiteSpace(name)) {
                    items = commonService.ListBrokers(name);
                }
                base.AddAudit(LogLevel.Debug, "Common", "Listar corretores", new { name }, items);
                return Json(new {
                    Brokers = items
                });

            } catch (Exception e) {
                base.AddAudit(LogLevel.Error, "Common", "Listar corretores", new { name }, e);
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { name }, e);
                }
            }
        }

        public IActionResult ListTakers(int brokerId, string name) {
            try {
                base.AddAudit(LogLevel.Trace, "Common", "Listar empresas", new { brokerId, name });

                IList<Taker> items = new List<Taker>();
                if (!string.IsNullOrWhiteSpace(name)) {
                    items = commonService.ListTakers(brokerId, name);
                }
                base.AddAudit(LogLevel.Debug, "Common", "Listar empresas", new { brokerId, name }, items);
                return Json(new {
                    Takers = items
                });

            } catch (Exception e) {
                base.AddAudit(LogLevel.Error, "Common", "Listar empresas", new { brokerId, name }, e);
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { brokerId, name }, e);
                }
            }
        }

        public IActionResult ListInsureds(string name) {
            try {
                base.AddAudit(LogLevel.Trace, "Common", "Listar segurados", new { name });

                IList<Insured> items = new List<Insured>();

                if (!string.IsNullOrWhiteSpace(name)) {
                    items = commonService.ListInsureds(name);
                }
                base.AddAudit(LogLevel.Debug, "Common", "Listar segurados", new { name }, items);
                return Json(new {
                    Insureds = items
                });

            } catch (Exception e) {
                base.AddAudit(LogLevel.Error, "Common", "Listar segurados", new { name }, e);
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { name }, e);
                }
            }
        }

        public IActionResult ListUpdateIndexes() {
            try {
                base.AddAudit(LogLevel.Trace, "Common", "Listar índices de atualização", new { });

                var items = commonService.ListUpdateIndexes();
                base.AddAudit(LogLevel.Debug, "Common", "Listar índices de atualização", new { }, items);
                return Json(new {
                    Insureds = items
                });

            } catch (Exception e) {
                base.AddAudit(LogLevel.Error, "Common", "Listar name ", new { }, e);
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { }, e);
                }
            }
        }
    }
}
