using InsuranceApi.Domain.Interfaces.Application;
using InsuranceApi.Domain.Interfaces.InfraIdentity;
using InsuranceApi.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace InsuranceApi.Web.Controllers.v1 {

    [Authorize]
    [Route("api/boleto")]
    public class BilletController : BaseController {
        private readonly IPolicyApplication policyApplication;

        public BilletController(IUser user, ILogger<BilletController> logger,
                                IAuditApplication auditApplication, IPolicyApplication policyApplication) : base(user, logger, auditApplication) {
            this.policyApplication = policyApplication;
        }

        [HttpPost("obter_boleto")]
        public async Task<ActionResult> PrintBilletAsync(BilletViewModel model) {
            try {

                base.WriteAuditData(LogLevel.Trace, "Obter Boleto", model, null);

                if (!ModelState.IsValid) { return base.ReturnErrorAndLogModelState(ModelState); }

                var billet = await policyApplication.PrintBilletAsync(model.CodigoEndosso);
                base.WriteAuditData(LogLevel.Debug, "Obter Boleto", model, billet);

                return base.ReturnSuccess(billet);

            } catch (Exception e) {
                base.WriteAuditData(LogLevel.Error, "Obter Boleto", model, e);
                return base.ReturnErrorAndLog(e, MethodBase.GetCurrentMethod(), null);
            }
        }
    }
}
