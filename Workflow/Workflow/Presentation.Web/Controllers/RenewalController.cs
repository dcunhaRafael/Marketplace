using Domain.Enumerators;
using Domain.Exceptions;
using Domain.Payload;
using LazyCache;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.Web.Models.Renewal;
using Presentation.Web.Services.Proxy;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Presentation.Web.Controllers {
    public class RenewalController : BaseController {
        private readonly ILogger<RenewalController> _logger;
        private readonly ICommonService commonService;
        private readonly IProposalService proposalService;
        private readonly IRenewalService renewalService;

        public RenewalController(IAppCache memoryCache, ILogger<RenewalController> logger,
            ICommonService commonService, IProposalService proposalService, IRenewalService renewalService) : base(memoryCache, logger, commonService) {
            _logger = logger;
            this.commonService = commonService;
            this.proposalService = proposalService;
            this.renewalService = renewalService;
        }

        public IActionResult Renewals(bool hideHeader = false) {
            try {

                ViewData.Add("HideHeader", hideHeader);
                var model = new RenewalViewModel() {
                    Filters = new Domain.Payload.PolicyBatch() {

                    }
                };
                return View("~/Views/Renewal/Renewals.cshtml", model);

            } catch (Exception e) {
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { hideHeader }, e);
                }
            }
        }

        [HttpPost]
        public IActionResult RenewalsGrid(RenewalViewModel model) {
            try {

                model.Filters.LoggedUserId = base.LoggedUserId;
                model.Results = renewalService.ListBatches(model.Filters);

                return PartialView("~/Views/Renewal/RenewalsGrid.cshtml", model);

            } catch (Exception e) {
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { model }, e);
                }
            }
        }

        [HttpPost]
        public IActionResult BatchDetails(int batchNumber) {
            try {

                var model = new RenewalViewModel() {
                    CurrentBatch = this.renewalService.GetBatch(batchNumber),
                    CurrentBatchItems = this.renewalService.ListBatchItems(batchNumber)
                };

                return PartialView("~/Views/Renewal/BatchDetails.cshtml", model);

            } catch (Exception e) {
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { batchNumber }, e);
                }
            }
        }

        [HttpPost]
        public IActionResult ProposalData(int id) {
            try {

                var model = new RenewalViewModel() {
                    CurrentDocument = renewalService.GetPolicy(id)
                };

                return PartialView("~/Views/Renewal/ProposalData.cshtml", model);

            } catch (Exception e) {
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { id }, e);
                }
            }
        }

        [HttpPost]
        public IActionResult SaveProposal(RenewalViewModel model) {
            try {

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
        public IActionResult TransmitProposal(RenewalViewModel model) {
            try {

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
        public IActionResult IssueProposal(RenewalViewModel model) {
            try {

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
