using EbixUsers.Web.ViewModels;
using InsuranceApi.Domain.Interfaces.Application;
using InsuranceApi.Domain.Interfaces.InfraIdentity;
using InsuranceApi.Infra.CrossCutting.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace EbixUsers.Web.Controllers.v1 {

    [Route("api/usuario")]
    public class UserController : BaseController {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        public UserController(IUser user, ILogger<UserController> logger,
                              SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager,
                              IAuditApplication auditApplication) : base(user, logger, auditApplication) {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        [HttpPost("usuario_buscar")]
        public async Task<ActionResult> GetUserAsync(UserGetViewModel model) {
            var request = new { model?.Id, model?.Login, model?.Email };
            try {
                base.WriteAuditData(LogLevel.Trace, "Busca de Usuário", request, null);

                if (string.IsNullOrWhiteSpace(model.Id) && string.IsNullOrWhiteSpace(model.Login) && string.IsNullOrWhiteSpace(model.Email)) {
                    throw new Exception($"Informe algum parâmetro para busca do usuário.");
                }

                ApplicationUser existentUser = null;
                if (!string.IsNullOrWhiteSpace(model.Id)) {
                    existentUser = await userManager.FindByIdAsync(model.Id);
                }
                if (existentUser == null && !string.IsNullOrWhiteSpace(model.Login)) {
                    existentUser = await userManager.FindByNameAsync(model.Login);
                }
                if (existentUser == null && !string.IsNullOrWhiteSpace(model.Email)) {
                    existentUser = await userManager.FindByEmailAsync(model.Email);
                }
                if (existentUser == null) {
                    throw new Exception($"Usuário não localizado.");
                }
                if (existentUser.LockoutEnabled) {
                    if (DateTime.MaxValue.Date.Equals(existentUser.LockoutEnd?.Date)) {
                        throw new Exception($"Usuário inativado.");
                    } else {
                        throw new Exception($"Usuário bloqueado até {existentUser.LockoutEnd.ToString()}.");
                    }
                }

                return base.ReturnSuccess(new { existentUser.Id, existentUser.UserName, existentUser.Email, BrokerUserId = existentUser.ExternalId });

            } catch (Exception exception) {
                base.WriteAuditData(LogLevel.Error, "Busca de Usuário", request, exception);
                return ReturnErrorAndLog(exception, MethodBase.GetCurrentMethod(), null);
            }
        }

        [HttpPost("usuario_criar")]
        public async Task<ActionResult> AddUserAsync(UserAddViewModel model) {
            if (!ModelState.IsValid) return base.ReturnErrorAndLogModelState(ModelState, model);

            var request = new { model.Login, Password = string.IsNullOrWhiteSpace(model.Password) ? "não informada" : new string('*', model.Password.Length), model.Email, model.BrokerUserId };
            try {
                base.WriteAuditData(LogLevel.Trace, "Criação de Usuário", request, null);

                var existentUser = await userManager.FindByNameAsync(model.Login);
                if (existentUser != null) {
                    throw new Exception($"Já existe outro usuário cadastrado com o login '{model.Login}'.");
                }

                var user = new ApplicationUser {
                    UserName = model.Login,
                    Email = model.Email,
                    ExternalId = model.BrokerUserId,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, model.Password);
                base.WriteAuditData(LogLevel.Debug, "Criação de Usuário", request, result);
                if (!result.Succeeded) {
                    foreach (var error in result.Errors) {
                        throw new Exception(error.Description);
                    }
                }

                var createdUser = await userManager.FindByNameAsync(model.Login);
                if (createdUser == null) {
                    throw new Exception("Falha na criação do usuário.");
                }

                return base.ReturnSuccess(createdUser.Id);

            } catch (Exception exception) {
                base.WriteAuditData(LogLevel.Error, "Criação de Usuário", request, exception);
                return ReturnErrorAndLog(exception, MethodBase.GetCurrentMethod(), null);
            }
        }

        [HttpPost("usuario_alterar")]
        public async Task<ActionResult> UpdateUserAsync(UserUpdateViewModel model) {
            if (!ModelState.IsValid) return base.ReturnErrorAndLogModelState(ModelState, model);

            var request = new { model.Id, model.Login, model.Email, model.BrokerUserId };
            try {
                base.WriteAuditData(LogLevel.Trace, "Alteração de Usuário", request, null);

                var existentUser = await userManager.FindByIdAsync(model.Id);
                if (existentUser == null) {
                    throw new Exception($"Usuário com identificador '{model.Id}' não localizado.");
                }

                existentUser.UserName = model.Login;
                existentUser.Email = model.Email;
                existentUser.ExternalId = model.BrokerUserId;

                var user = new ApplicationUser {
                    UserName = model.Login,
                    Email = model.Email,
                    ExternalId = model.BrokerUserId,
                    EmailConfirmed = true
                };

                var result = await userManager.UpdateAsync(existentUser);
                base.WriteAuditData(LogLevel.Debug, "Alteração de Usuário", request, result);
                if (!result.Succeeded) {
                    foreach (var error in result.Errors) {
                        throw new Exception(error.Description);
                    }
                }

                return base.ReturnSuccess();

            } catch (Exception exception) {
                base.WriteAuditData(LogLevel.Error, "Alteração de Usuário", request, exception);
                return ReturnErrorAndLog(exception, MethodBase.GetCurrentMethod(), null);
            }
        }

        [HttpPost("usuario_alterar_senha")]
        public async Task<ActionResult> UpdatePasswordAsync(UserUpdatePasswordViewModel model) {
            if (!ModelState.IsValid) return base.ReturnErrorAndLogModelState(ModelState, model);

            var request = new {
                model.Id,
                OldPassword = string.IsNullOrWhiteSpace(model.OldPassword) ? "não informada" : new string('*', model.OldPassword.Length),
                NewPassword = string.IsNullOrWhiteSpace(model.NewPassword) ? "não informada" : new string('*', model.NewPassword.Length)
            };
            try {
                base.WriteAuditData(LogLevel.Trace, "Alteração de Senha do Usuário", request, null);

                var existentUser = await userManager.FindByIdAsync(model.Id);
                if (existentUser == null) {
                    throw new Exception($"Usuário com identificador '{model.Id}' não localizado.");
                }

                var result = await userManager.ChangePasswordAsync(existentUser, model.OldPassword, model.NewPassword);
                base.WriteAuditData(LogLevel.Debug, "Alteração de Senha do Usuário", request, result);
                if (!result.Succeeded) {
                    foreach (var error in result.Errors) {
                        throw new Exception(error.Description);
                    }
                }

                return base.ReturnSuccess();

            } catch (Exception exception) {
                base.WriteAuditData(LogLevel.Error, "Alteração de Senha do Usuário", request, exception);
                return ReturnErrorAndLog(exception, MethodBase.GetCurrentMethod(), null);
            }
        }

        [HttpPost("usuario_inativar")]
        public async Task<ActionResult> InactivateUserAsync(UserInactivateViewModel model) {
            if (!ModelState.IsValid) return base.ReturnErrorAndLogModelState(ModelState, model);

            var request = new { model.Id };
            try {
                base.WriteAuditData(LogLevel.Trace, "Inativação de Usuário", request, null);

                var existentUser = await userManager.FindByIdAsync(model.Id);
                if (existentUser == null) {
                    throw new Exception($"Usuário com identificador '{model.Id}' não localizado.");
                }

                var result = await userManager.SetLockoutEnabledAsync(existentUser, true);
                base.WriteAuditData(LogLevel.Debug, "Inativação de Usuário", request, result);
                if (result.Succeeded) {
                    result = await userManager.SetLockoutEndDateAsync(existentUser, DateTime.MaxValue.Date);
                    base.WriteAuditData(LogLevel.Debug, "Inativação de Usuário", request, result);
                }
                if (!result.Succeeded) {
                    foreach (var error in result.Errors) {
                        throw new Exception(error.Description);
                    }
                }

                return base.ReturnSuccess();

            } catch (Exception exception) {
                base.WriteAuditData(LogLevel.Error, "Inativação de Usuário", request, exception);
                return ReturnErrorAndLog(exception, MethodBase.GetCurrentMethod(), null);
            }
        }

    }
}
