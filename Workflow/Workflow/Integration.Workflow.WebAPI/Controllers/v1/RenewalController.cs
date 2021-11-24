using Application.Interfaces.Services;
using Domain.Payload;
using Integration.Interfaces.Legacy;
using Integration.Workflow.WebAPI.Models.Proposal;
using Integration.Workflow.WebAPI.Models.Renewal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Integration.Workflow.WebAPI.Controllers.v1 {
    [ApiController]
    [Route("api/renewal")]
    public class RenewalController : BaseController {
        private readonly IPolicyBatchService policyBatchService;
        private readonly IPolicyRenovationService policyRenovationService;
        private readonly IRenewalApiService renewalApiService;

        public RenewalController(ILogger<RenewalController> logger,
            IPolicyBatchService policyBatchService, IPolicyRenovationService policyRenovationService, IRenewalApiService renewalApiService) : base(logger) {
            this.policyBatchService = policyBatchService;
            this.policyRenovationService = policyRenovationService;
            this.renewalApiService = renewalApiService;
        }

        [HttpPost("ListBatches")]
        public async Task<IActionResult> ListOccurrences(PolicyBatch request) {
            try {

                var items = await policyBatchService.ListAsync(request);

                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("GetBatch")]
        public async Task<IActionResult> GetBatch(GetBatchRequest request) {
            try {

                var items = await policyBatchService.GetAsync(request.PolicyBatchId);

                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("ListBatchItems")]
        public async Task<IActionResult> ListBatchItems(ListBatchItemsRequest request) {
            try {

                var items = await policyRenovationService.ListAsync(request.PolicyBatchId);

                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("GetPolicy")]
        public async Task<IActionResult> GetPolicy(GetPolicyRequest request) {
            try {

                var items = await policyRenovationService.GetAsync(request.PolicyRenovationId);

                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("SaveRenovation")]
        public IActionResult SaveRenovation(PolicyRenovation request) {
            try {

                var items = renewalApiService.SaveRenewalJudicial(request);

                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("TrasmitRenovation")]
        public IActionResult TrasmitRenovation(TrasmitRenovationRequest request) {
            try {

                var items = renewalApiService.TransmitProposal(new PolicyRenovation() {
                    NewProposalNumber = request.ProposalNumber
                });

                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("IssueRenovation")]
        public IActionResult IssueRenovation(IssueRenovationRequest request) {
            try {

                var items = renewalApiService.IssuePolicy(new PolicyRenovation() {
                    NewProposalNumber = request.ProposalNumber
                });

                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("PrintRenovation")]
        public IActionResult PrintRenovation(GetPolicyRequest request) {
            try {

                var items = renewalApiService.PrintProposal(new PolicyRenovation() {
                    EndorsementId = request.PolicyRenovationId
                });

                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }
    }
}
