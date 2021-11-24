using Application.Interfaces.Services;
using Domain.Payload;
using Integration.Workflow.WebAPI.Models.Proposal;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Integration.Workflow.WebAPI.Controllers.v1 {
    [ApiController]
    [Route("api/proposal")]
    public class ProposalController : BaseController {
        private readonly IProposalOccurrenceService proposalOccurrenceService;

        public ProposalController(ILogger<ProposalController> logger, IProposalOccurrenceService proposalOccurrenceService) : base(logger) {
            this.proposalOccurrenceService = proposalOccurrenceService;
        }

        [HttpPost("AnalyzeProposal")]
        public async Task<IActionResult> AnalyzeProposal(AnalyzeProposalRequest request) {
            try {

                var analysisResult = await proposalOccurrenceService.AnalyzeProposal(request.ProposalNumber, request.LoggedUserId);
                var response = new ValidateProposalResponse() {
                    IsRefused = analysisResult.IsRefused,
                    IsApproved = analysisResult.IsApproved,
                    Occurrences = analysisResult.Occurrences
                };

                return base.ReturnSuccess(data: response);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("ListOccurrences")]
        public async Task<IActionResult> ListOccurrences(ProposalOccurrenceFilters request) {
            try {

                var items = await proposalOccurrenceService.ListAsync(request);
                
                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("GetOccurrence")]
        public async Task<IActionResult> GetOccurrence(GetOccurrenceRequest request) {
            try {

                var items = await proposalOccurrenceService.GetAsync(request.ProposalOccurrenceId);
                
                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("ApproveOccurrence")]
        public async Task<IActionResult> ApproveOccurrence(ProposalOccurrenceApprove item) {
            try {

                await proposalOccurrenceService.ApproveAsync(item);
                
                return base.ReturnSuccess();

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("RefuseOccurrence")]
        public async Task<IActionResult> RefuseOccurrence(ProposalOccurrenceRefuse item) {
            try {

                await proposalOccurrenceService.RefuseAsync(item);
                
                return base.ReturnSuccess();

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("ForwardOccurrence")]
        public async Task<IActionResult> ForwardOccurrence(ProposalOccurrenceForward item) {
            try {

                await proposalOccurrenceService.ForwardAsync(item);
                
                return base.ReturnSuccess();

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("ListOccurrenceDocuments")]
        public async Task<IActionResult> ListOccurrenceDocuments(ListOccurrenceDocumentsRequest request) {
            try {

                var items = await proposalOccurrenceService.ListDocumentsAsync(request.ProposalOccurrenceId);

                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("GetOccurrenceDocument")]
        public async Task<IActionResult> GetOccurrenceDocument(GetOccurrenceDocumentRequest request) {
            try {

                var document = await proposalOccurrenceService.GetDocumentAsync(request.ProposalOccurrenceDocumentId);

                return base.ReturnSuccess(data: new GetOccurrenceDocumentResponse() {
                    FileName = document.FileName,
                    FileContents = Convert.ToBase64String(document.FileContents)
                });

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("AddOccurrenceDocument")]
        public async Task<IActionResult> AddOccurrenceDocument(AddOccurrenceDocumentRequest request) {
            try {

                var id = await proposalOccurrenceService.AddDocumentAsync(new ProposalOccurrenceDocument() {
                    DocumentTypeId = request.DocumentTypeId,
                    FileName = request.FileName,
                    FileContents = Convert.FromBase64String(request.FileContentsBase64),
                    ProposalOccurrenceId = request.ProposalOccurrenceId
                }, request.LoggedUserId);

                return base.ReturnSuccess(data: id);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("DeleteOccurrenceDocument")]
        public async Task<IActionResult> DeleteOccurrenceDocument(DeleteOccurrenceDocumentRequest request) {
            try {

                await proposalOccurrenceService.DeleteDocumentAsync(request.ProposalOccurrenceDocumentId, request.LoggedUserId);

                return base.ReturnSuccess();

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("ListOccurrenceHistories")]
        public async Task<IActionResult> ListOccurrenceHistories(ListOccurrenceHistoriesRequest request) {
            try {

                var items = await proposalOccurrenceService.ListHistoriesAsync(request.ProposalOccurrenceId);

                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("ListOccurrenceLiberationUsers")]
        public async Task<IActionResult> ListOccurrenceLiberationUsers(ListOccurrenceLiberationUsersRequest request) {
            try {

                var items = await proposalOccurrenceService.ListLiberationUsersAsync(request.ProposalOccurrenceId);

                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

    }
}
