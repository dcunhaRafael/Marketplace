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

        [HttpPost("GetComissionStatementDetail")]
        public async Task<IActionResult> GetComissionStatementDetail(ListComissionStatementDetaisRequest request) {
            try {

                var item = await comissionStatementService.GetComissionStatementDetail(request.StatementNumber, request.Competency,
                    request.BrokerLegacyCode, request.BrokerSusepCode, request.BrokerUserId, request.LoggedUserId);

                return base.ReturnSuccess(data: item);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("ListComissionStatementTypes")]
        public IActionResult ListComissionStatementTypes(ListComissionStatementTypesRequest request) {
            try {

                var items = comissionStatementService.ListComissionStatementTypes(request.StatementNumber, request.Competency,
                    request.BrokerLegacyCode, request.BrokerSusepCode, request.BrokerUserId, request.LoggedUserId);

                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("ListComissionStatementBusiness")]
        public IActionResult ListComissionStatementBusiness(ListComissionStatementBusinessRequest request) {
            try {

                var items = comissionStatementService.ListComissionStatementBusiness(request.StatementNumber, request.Competency,
                    request.BrokerLegacyCode, request.BrokerSusepCode, request.BrokerUserId, request.LoggedUserId);

                return base.ReturnSuccess(data: items);

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

    }
}
