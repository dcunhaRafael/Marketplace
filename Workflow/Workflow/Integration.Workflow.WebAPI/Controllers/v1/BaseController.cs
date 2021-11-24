using Domain.Payload;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Reflection;

namespace Integration.Workflow.WebAPI.Controllers.v1 {

    [ApiController]
    public class BaseController : ControllerBase {
        //private readonly IUser _appUser;
        private readonly ILogger _logger;

        //protected Guid UsuarioId { get; set; }
        //protected int UsuarioExternalId { get; set; }
        //protected bool UsuarioAutenticado { get; set; }

        public BaseController(ILogger logger) {
            _logger = logger;
            //if (appUser.IsAuthenticated()) {
            //    UsuarioId = appUser.GetUserId();
            //    UsuarioExternalId = appUser.GetExternalId();
            //    UsuarioAutenticado = true;
            //}
        }

        protected ActionResult ReturnSuccess(string message = "", object data = null) {
            return Ok(new ServiceReturn() {
                Success = true,
                Message = message,
                Data = JsonConvert.SerializeObject(data)
            });
        }

        protected ActionResult ReturnError(MethodBase caller, Exception exception, string customMessage = null, object data = null) {
            string errorMessage = string.IsNullOrWhiteSpace(customMessage) ? exception.Message : customMessage;
            _logger.LogError(exception, errorMessage, data);
            return BadRequest(new ServiceReturn() {
                Success = false,
                Message = errorMessage,
                Data = JsonConvert.SerializeObject(data)
            });
        }

    }
}