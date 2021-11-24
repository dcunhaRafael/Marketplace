using InsuranceApi.Domain.Interfaces.Application;
using InsuranceApi.Domain.Interfaces.InfraIdentity;
using InsuranceApi.Infra.CrossCutting.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace InsuranceApi.Web.Controllers.v1 {

    [Route("api/usuario")]
    public class UserController : BaseController {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(IUser user, ILogger<AuthenticationController> logger,
                              SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
                              IAuditApplication auditApplication) : base(user, logger, auditApplication) {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        //[ApiExplorerSettings(IgnoreApi = true)]
        //[HttpPost("usuario_criar")]
        //public async Task<ActionResult> AddUserAsync(UserViewModel model) {
        //    try {

        //        if (!ModelState.IsValid) return base.ReturnErrorAndLogModelState(ModelState, model);

        //        var user = new ApplicationUser {
        //            UserName = model.Login,
        //            Email = model.Email,
        //            ExternalId = model.CodigoInternoUsuario,
        //            EmailConfirmed = true
        //        };

        //        var result = await userManager.CreateAsync(user, model.Password);
        //        if (result.Succeeded) {
        //            await signInManager.SignInAsync(user, false);
        //        }

        //        foreach (var error in result.Errors) {
        //            throw new Exception(error.Description);
        //        }

        //        return base.ReturnSuccess(model);

        //    } catch (Exception exception) {
        //        return ReturnErrorAndLog(exception, MethodBase.GetCurrentMethod(), null);
        //    }
        //}
    }
}
