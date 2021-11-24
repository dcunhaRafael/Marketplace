using Domain.Exceptions;
using LazyCache;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.Web.Models.Workflow;
using Presentation.Web.Services.Proxy;
using System;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Presentation.Web.Controllers {
    public class WorkflowController : BaseController {
        private readonly ICommonService commonService;
        private readonly IProposalService proposalService;

        public WorkflowController(IAppCache memoryCache, ILogger<WorkflowController> logger,
            ICommonService commonService, IProposalService proposalService) : base(memoryCache, logger, commonService) {
            this.commonService = commonService;
            this.proposalService = proposalService;
        }

        public IActionResult Occurrences(bool hideHeader = false) {
            try {

                ViewData.Add("HideHeader", hideHeader);
                var model = new WorkflowViewModel() {
                    ProductList = commonService.ListProducts(),
                    OccurrenceTypeList = commonService.ListOccurrenceTypes(),
                    Filters = new Domain.Payload.ProposalOccurrenceFilters() {
                        DateTo = DateTime.Now,
                        DateFrom = DateTime.Now.AddDays(-7)
                    }
                };
                return View("~/Views/Workflow/Occurrences.cshtml", model);

            } catch (Exception e) {
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { hideHeader }, e);
                }
            }
        }

        [HttpPost]
        public IActionResult OccurrencesGrid(WorkflowViewModel model) {
            try {

                model.Filters.LoggedUserId = base.LoggedUserId;
                model.Filters.LoggedUserProfileId = base.LoggedUserProfileId;
                model.Occurrences = proposalService.ListOccurrences(model.Filters);

                return PartialView("~/Views/Workflow/OccurrencesGrid.cshtml", model);

            } catch (Exception e) {
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { model }, e);
                }
            }
        }

        [HttpPost]
        public IActionResult OccurrenceLiberationUsers(long proposalOccurrenceId) {
            try {

                var model = new WorkflowViewModel() {
                    LiberationUsers = proposalService.ListOccurrenceLiberationUsers(proposalOccurrenceId)
                };

                return PartialView("~/Views/Workflow/OccurrenceLiberationUsersModal.cshtml", model);

            } catch (Exception e) {
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { proposalOccurrenceId }, e);
                }
            }
        }

        [HttpPost]
        public IActionResult OccurrenceHistories(long proposalOccurrenceId) {
            try {

                var model = new WorkflowViewModel() {
                    Histories = proposalService.ListOccurrenceHistories(proposalOccurrenceId)
                };

                return PartialView("~/Views/Workflow/OccurrenceHistoriesModal.cshtml", model);

            } catch (Exception e) {
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { proposalOccurrenceId }, e);
                }
            }
        }

        [HttpPost]
        public IActionResult OccurrenceForward(int proposalNumber, long proposalOccurrenceId) {
            try {

                var model = new WorkflowViewModel() {
                    LiberationUsers = proposalService.ListOccurrenceLiberationUsers(proposalOccurrenceId),
                    ProposalNumber = proposalNumber,
                    ProposalOccurrenceId = proposalOccurrenceId
                };

                return PartialView("~/Views/Workflow/OccurrenceForwardModal.cshtml", model);

            } catch (Exception e) {
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { proposalNumber, proposalOccurrenceId }, e);
                }
            }
        }

        [HttpPost]
        public IActionResult OccurrenceForwardConfirm(WorkflowViewModel model) {
            try {

                proposalService.ForwardOccurrence(new Domain.Payload.ProposalOccurrenceForward() {
                    ProposalNumber = model.ProposalNumber,
                    ProposalOccurrenceId = model.ProposalOccurrenceId,
                    ForwardUserId = model.ForwardUserId,
                    UserComments = model.UserComments,
                    LoggedUserId = base.LoggedUserId
                });

                return base.ReturnSuccess();

            } catch (Exception e) {
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { model }, e);
                }
            }
        }

        [HttpPost]
        public IActionResult OccurrenceDocuments(long proposalOccurrenceId) {
            try {

                var occurrence = proposalService.GetOccurrence(proposalOccurrenceId);

                var model = new WorkflowViewModel() {
                    Documents = proposalService.ListOccurrenceDocuments(proposalOccurrenceId),
                    ProposalOccurrenceId = proposalOccurrenceId,
                    IsEditable = (occurrence.OccurrenceStatus == Domain.Enumerators.OccurrenceStatusEnum.Pending)
                };

                return PartialView("~/Views/Workflow/OccurrenceDocumentsModal.cshtml", model);

            } catch (Exception e) {
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { proposalOccurrenceId }, e);
                }
            }
        }

        [HttpPost]
        public IActionResult OccurrenceDocumentUpload(IFormFile file, long proposalOccurrenceId, int documentTypeId) {
            try {

                if (file == null || file.Length == 0) {
                    throw new Exception("Arquivo não chegou ao servidor.");
                }
                var fileSize = file.Length;
                if (fileSize > 5242880) {    //TODO Configurar, hj fixado em 5MB
                    throw new Exception("Arquivo excede o tamanho máximo permitido (5MB).");
                }

                byte[] bytes;
                using (var memoryStream = new MemoryStream()) {
                    file.CopyTo(memoryStream);
                    bytes = memoryStream.ToArray();
                }

                var proposalOccurrenceDocumentId = proposalService.AddOccurrenceDocument(new Domain.Payload.ProposalOccurrenceDocument() {
                    ProposalOccurrenceId = proposalOccurrenceId,
                    DocumentTypeId = documentTypeId,
                    FileName = file.FileName,
                    FileContents = bytes
                }, base.LoggedUserId);

                return base.ReturnSuccess(data: new { ProposalOccurrenceDocumentId = proposalOccurrenceDocumentId, file.FileName });

            } catch (Exception e) {
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { file, proposalOccurrenceId, documentTypeId }, e);
                }
            }
        }

        [HttpPost]
        public IActionResult OccurrenceDocumentDownload(long proposalOccurrenceDocumentId) {
            try {

                var document = proposalService.GetOccurrenceDocument(proposalOccurrenceDocumentId);

                return File(document.FileContents, "application/octet-stream", document.FileName);

            } catch (Exception e) {
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { proposalOccurrenceDocumentId }, e);
                }
            }
        }

        [HttpPost]
        public IActionResult OccurrenceDocumentDelete(long proposalOccurrenceDocumentId) {
            try {

                proposalService.DeleteOccurrenceDocument(proposalOccurrenceDocumentId, base.LoggedUserId);
                return base.ReturnSuccess();

            } catch (Exception e) {
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { proposalOccurrenceDocumentId }, e);
                }
            }
        }

        [HttpPost]
        public IActionResult OccurrenceApproval() {
            try {

                var model = new WorkflowViewModel() {
                    LoggedUserName = base.LoggedUserName
                };

                return PartialView("~/Views/Workflow/OccurrenceApprovalModal.cshtml", model);

            } catch (Exception e) {
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { }, e);
                }
            }
        }

        [HttpPost]
        public IActionResult OccurrenceApprovalConfirm(WorkflowViewModel model) {
            try {

                foreach (var item in model.Occurrences.Where(x => x.IsChecked).ToList()) {
                    proposalService.ApproveOccurrence(new Domain.Payload.ProposalOccurrenceApprove() {
                        ProposalOccurrenceId = item.ProposalOccurrenceId,
                        UserComments = model.UserComments,
                        LoggedUserId = base.LoggedUserId
                    });
                }

                return base.ReturnSuccess();

            } catch (Exception e) {
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { model }, e);
                }
            }
        }

        [HttpPost]
        public IActionResult OccurrenceRefusal() {
            try {

                var model = new WorkflowViewModel() {
                    LoggedUserName = base.LoggedUserName,
                    RefusalReasons = commonService.ListRefusalReasons()
                };

                return PartialView("~/Views/Workflow/OccurrenceRefusalModal.cshtml", model);

            } catch (Exception e) {
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { }, e);
                }
            }
        }

        [HttpPost]
        public IActionResult OccurrenceRefusalConfirm(WorkflowViewModel model) {
            try {

                foreach (var item in model.Occurrences.Where(x => x.IsChecked).ToList()) {
                    proposalService.RefuseOccurrence(new Domain.Payload.ProposalOccurrenceRefuse() {
                        ProposalOccurrenceId = item.ProposalOccurrenceId,
                        RefusalReasonId = model.RefusalReasonId,
                        UserComments = model.UserComments,
                        LoggedUserId = base.LoggedUserId
                    });
                }

                return base.ReturnSuccess();

            } catch (Exception e) {
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { model }, e);
                }
            }
        }

    }
}
