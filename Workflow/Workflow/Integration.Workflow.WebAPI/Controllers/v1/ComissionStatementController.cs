using Application.Interfaces.Services;
using Integration.Workflow.WebAPI.Models.ComissionStatement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Integration.Workflow.WebAPI.Controllers.v1 {
    [ApiController]
    [Route("api/comissionStatement")]
    public class ComissionStatementController : BaseController {
        private readonly IComissionStatementService comissionStatementService;

        public ComissionStatementController(ILogger<ComissionStatementController> logger, 
            IComissionStatementService comissionStatementService) : base(logger) {
            this.comissionStatementService = comissionStatementService;
        }

        [HttpPost("ListComissionStatement")]
        public IActionResult ListComissionStatement(ListComissionStatementRequest request) {
            try {

                var items = comissionStatementService.ListComissionStatement(request.StatementNumber, request.FromDate, request.ToDate, 
                    request.Status, request.BrokerLegacyCode, request.BrokerSusepCode, request.BrokerUserId, request.LoggedUserId );

                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("GetComissionStatementCover")]
        public async Task<IActionResult> GetComissionStatementCover(ListComissionStatementDetaisRequest request) {
            try {

                var item = await comissionStatementService.GetComissionStatementCover(request.StatementNumber, request.Competency,
                    request.BrokerLegacyCode, request.BrokerSusepCode, request.BrokerUserId, request.LoggedUserId);

                return base.ReturnSuccess(data: item);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("ListComissionStatementEntries")]
        public IActionResult ListComissionStatementEntries(ListComissionStatementEntriesRequest request) {
            try {

                var items = comissionStatementService.ListComissionStatementEntries(request.StatementNumber, request.Competency,
                    request.BrokerLegacyCode, request.BrokerSusepCode, request.BrokerUserId, request.LoggedUserId);

                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("ExportComissionStatement")]
        public async Task<IActionResult> ExportComissionStatement(ExportComissionStatementRequest request) {
            try {

                var item = await comissionStatementService.ExportComissionStatement(request.TemplateFile, request.StatementNumber, request.Competency,
                                        request.BrokerName, request.BrokerCnpjNumber, request.BrokerLegacyCode, request.BrokerSusepCode, request.BrokerUserId, request.LoggedUserId);

                return base.ReturnSuccess(data: item);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }
    }
}
