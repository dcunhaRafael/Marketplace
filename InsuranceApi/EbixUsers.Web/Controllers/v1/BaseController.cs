using DeviceDetectorNET;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.Common.Exceptions;
using InsuranceApi.Domain.Interfaces.Application;
using InsuranceApi.Domain.Interfaces.InfraIdentity;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EbixUsers.Web.Controllers {

    [ApiController]
    public class BaseController : ControllerBase {
        private readonly ILogger logger;
        private readonly IAuditApplication auditApplication;

        protected Guid UsuarioId { get; set; }
        protected string UsuarioNome { get; set; }
        protected int UsuarioExternalId { get; set; }
        protected bool UsuarioAutenticado { get; set; }

        public BaseController(IUser appUser, ILogger logger, IAuditApplication auditApplication) {
            this.logger = logger;
            this.auditApplication = auditApplication;
            if (appUser.IsAuthenticated()) {
                UsuarioId = appUser.GetUserId();
                UsuarioNome = appUser.GetUserName();
                UsuarioExternalId = appUser.GetExternalId();
                UsuarioAutenticado = true;
            }
        }

        protected ActionResult ReturnSuccess(object param = null, string message = "") {
            return Ok(new {
                success = true,
                Erro = message,
                data = param
            });
        }

        protected ActionResult ReturnErrorAndLogModelState(ModelStateDictionary modelState = null, object param = null) {
            var erros = NotificarErroModelInvalida(modelState);
            logger.LogWarning(null, erros, param);
            return BadRequest(new {
                success = false,
                Erro = erros,
                data = param
            });
        }

        protected ActionResult ReturnErrorAndLog(Exception ex, System.Reflection.MethodBase method = null, object param = null) {
            if (ex is BusinessException || ex is ArgumentException) {
                logger.LogWarning(ex, ex.Message, param);
            } else {
                logger.LogError(ex, ex.Message, param);
            }
            return BadRequest(new {
                success = false,
                Erro = new MensagemEntity(ex.Message),
                data = param
            });
        }

        private IEnumerable<MensagemEntity> NotificarErroModelInvalida(ModelStateDictionary modelState) {
            List<MensagemEntity> messageList = new List<MensagemEntity>();
            var erros = modelState.Values.SelectMany(e => e.Errors);
            foreach (var erro in erros) {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                messageList.Add(new MensagemEntity(errorMsg));
            }
            return messageList;
        }

        protected void WriteAuditData(LogLevel level, string actionName, object request, object response) {
            try {

                // Somente grava se o tipo de log estiver habilitado
                if (!logger.IsEnabled(level)) {
                    return;
                }

                var dd = new DeviceDetector(HttpContext.Request.Headers["User-Agent"]);
                dd.Parse();
                var clientInfo = dd.GetClient();
                var osInfo = dd.GetOs();

                auditApplication.AddAsync(new AuditoriaEntity() {
                    TipoUsuarioId = 7,  //Fixo: API
                    UsuarioNome = this.UsuarioNome,
                    UrlChamada = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Headers["Host"]}{HttpContext.Request.Path}",
                    Funcionalidade = "API",
                    TipoAcao = actionName,
                    IP = HttpContext.Connection.RemoteIpAddress.ToString(),
                    Navegador = $"{clientInfo.Match.Name} ({clientInfo.Match.Version})",
                    SistemaOperacional = $"{osInfo.Match.Name} {osInfo.Match.Version}",
                    Nivel = level.ToString(),
                    Request = JsonConvert.SerializeObject(request),
                    Response = JsonConvert.SerializeObject(response),
                });

            } catch (Exception e) {
                logger.LogError(e, "Erro na gravação do log de auditoria.", new { level, actionName, request, response });
            }
        }
    }
}