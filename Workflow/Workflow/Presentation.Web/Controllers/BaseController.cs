using DeviceDetectorNET;
using LazyCache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Presentation.Web.Models;
using Presentation.Web.Services.Proxy;
using System;
using System.Diagnostics;
using System.Net;
using System.Reflection;

namespace Presentation.Web.Controllers {
    public class BaseController : Controller {
        private readonly IAppCache memoryCache;
        private readonly ILogger<BaseController> logger;
        private readonly ICommonService commonService;

        public BaseController(IAppCache memoryCache, ILogger<BaseController> logger, ICommonService commonService) {
            this.memoryCache = memoryCache;
            this.logger = logger;
            this.commonService = commonService;
        }

        public int LoggedUserId {
            get {
                return 1346; // 4705;   //TODO Depende da integração com o Marketplace
            }
        }

        public string LoggedUserName {
            get {
                return "devagci teste dev";   //TODO Depende da integração com o Marketplace
            }
        }

        public int LoggedUserProfileId {
            get {
                return 1;   //TODO Depende da integração com o Marketplace
            }
        }

        public int LoggedUserTypeId {
            get {
                return 6;   //TODO Depende da integração com o Marketplace
            }
        }

        protected T GetCached<T>(string cacheKey, Func<T> getItemCallback) where T : class {
            var item = memoryCache.GetOrAdd(cacheKey, cacheEntry => {
                cacheEntry.SlidingExpiration = TimeSpan.FromHours(1);
                return getItemCallback();
            });
            return item;
        }

        public JsonResult ReturnSuccess(string message = null, object data = null) {
            Response.StatusCode = (int)HttpStatusCode.OK;
            return Json(
                new {
                    Success = true,
                    Message = message,
                    Response = data
                });
        }

        public IActionResult ReturnError(string message, object data = null) {
            //    if ((ex is DaoException || ex is ServiceException || ex is ApplicationException)) {
            //        //-- Exception das demais camadas não precisa logar na camada de apresentação
            //    } else if (ex is BusinessException || ex is IdentityException || ex is ArgumentException) {
            //        //-- Exceptions geradas por regras de validação gera alertas no log
            //        Log.Warn(LogMessage.BuildDefaultControllerLogMessage(method, methodParameters), ex);
            //    } else {
            //        //-- Qualquer outra exception grava como erro no log
            //        Log.Error(LogMessage.BuildDefaultControllerLogMessage(method, methodParameters), ex);
            //    }
            //return new StatusCodeResult(200);   HttpStatusCodeResult(410, "Unable to find customer.")

            bool isAjaxRequest = Request.Headers["x-requested-with"] == "XMLHttpRequest";
            if (isAjaxRequest) {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(
                    new {
                        Success = false,
                        Message = message,
                        Response = data
                    });
            } else {
                return View("~/Views/Home/Error.cshtml", new ErrorViewModel {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Referrer = HttpContext.Request.Headers["referer"],
                    ExceptionMessage = message
                });
            }
        }

        public IActionResult ReturnException(MethodBase method, object methodParams, Exception e, string customMessage = null, object data = null) {
            //    if ((ex is DaoException || ex is ServiceException || ex is ApplicationException)) {
            //        //-- Exception das demais camadas não precisa logar na camada de apresentação
            //    } else if (ex is BusinessException || ex is IdentityException || ex is ArgumentException) {
            //        //-- Exceptions geradas por regras de validação gera alertas no log
            //        Log.Warn(LogMessage.BuildDefaultControllerLogMessage(method, methodParameters), ex);
            //    } else {
            //        //-- Qualquer outra exception grava como erro no log
            //        Log.Error(LogMessage.BuildDefaultControllerLogMessage(method, methodParameters), ex);
            //    }

            bool isAjaxRequest = Request.Headers["x-requested-with"] == "XMLHttpRequest";
            if (isAjaxRequest) {
                Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                return Json(
                    new {
                        Success = false,
                        Message = (string.IsNullOrWhiteSpace(customMessage) ? e.Message : customMessage),
                        Response = data
                    });
            } else {
                return View("~/Views/Home/Error.cshtml", new ErrorViewModel {
                    RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier,
                    Referrer = HttpContext.Request.Headers["referer"],
                    ExceptionMessage = (string.IsNullOrWhiteSpace(customMessage) ? e.Message : customMessage)
                });
            }
        }

        public void AddAudit(LogLevel level, string featureName, string actionName, object request = null, object response = null) {
            try {

                // Somente grava se o tipo de log estiver habilitado
                if (!logger.IsEnabled(level)) {
                    return;
                }

                var dd = new DeviceDetector(HttpContext.Request.Headers["User-Agent"]);
                dd.Parse();
                var clientInfo = dd.GetClient();
                var osInfo = dd.GetOs();

                commonService.AddAudit(new Domain.Payload.Audit() {
                    Level = level.ToString(),
                    FeatureName = featureName,
                    ActionName = actionName,
                    Url = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Headers["Host"]}{HttpContext.Request.Path}",
                    IpAddress = HttpContext.Connection.RemoteIpAddress.ToString(),
                    BrowserName = $"{clientInfo.Match.Name} ({clientInfo.Match.Version})",
                    OsName = $"{osInfo.Match.Name} {osInfo.Match.Version}",
                    Request = JsonConvert.SerializeObject(request),
                    Response = JsonConvert.SerializeObject(response),
                    UserTypeId = this.LoggedUserTypeId,
                    UserId = this.LoggedUserId,
                });

            } catch (Exception e) {
                logger.LogError(e, "Erro gravando registro de auditoria", new { level, featureName, actionName, request, response });
            }
        }

    }
}
