using Domain.Exceptions;
using Domain.Payload;
using LazyCache;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Presentation.Web.Controllers;
using Presentation.Web.Models.Endorsement;
using Presentation.Web.Services.Proxy;
using System;
using System.Reflection;

namespace Portal.Web.Controllers {
    public class EndorsementController : BaseController {
        private readonly IWebHostEnvironment webHostingEnvironment;
        //private readonly IEndorsementService EndorsementService;
        private readonly ICommonService commonService;

        public EndorsementController(
            IAppCache memoryCache, ILogger<EndorsementController> logger, IWebHostEnvironment webHostingEnvironment,
            //IEndorsementService EndorsementService, 
            ICommonService commonService) : base(memoryCache, logger, commonService) {
            this.webHostingEnvironment = webHostingEnvironment;
            //this.EndorsementService = EndorsementService;
            this.commonService = commonService;
        }

        public IActionResult Endorsements() {
            try {

                var model = new EndorsementViewModel() {
                    //Status = base.GetCached<IList<EndorsementStatus>>("EndorsementStatus", () => commonService.ListEndorsementStatus())
                };
                return View("~/Views/Endorsement/Endorsements.cshtml", model);

            } catch (ApplicationException e) {
                return base.ReturnError(e.Message);
            } catch (IntegrationException e) {
                return base.ReturnError(e.Message);
            } catch (Exception e) {
                return base.ReturnException(MethodBase.GetCurrentMethod(), new { }, e);
            }
        }

        [HttpPost]
        public IActionResult EndorsementGrid(EndorsementViewModel model) {
            try {

                return PartialView("~/Views/Endorsement/EndorsementsGrid.cshtml", model);

            } catch (ApplicationException e) {
                return base.ReturnError(e.Message);
            } catch (IntegrationException e) {
                return base.ReturnError(e.Message);
            } catch (Exception e) {
                return base.ReturnException(MethodBase.GetCurrentMethod(), new { model }, e);
            }
        }

        [HttpGet]
        public IActionResult Endorsement() {
            try {

                var model = new EndorsementViewModel();
                return View("~/Views/Endorsement/Endorsement.cshtml", model);

            } catch (ApplicationException e) {
                return base.ReturnError(e.Message);
            } catch (IntegrationException e) {
                return base.ReturnError(e.Message);
            } catch (Exception e) {
                return base.ReturnException(MethodBase.GetCurrentMethod(), null, e);
            }
        }

        [HttpPost]
        public IActionResult PolicyEndorsementsGrid(long policyNumber) {
            try {

                var model = new EndorsementViewModel();
                model.PolicyEndorsements.Add(
                    new PolicyRenovation() {
                        PolicyRenovationId = 1,
                        EndorsementTypeName = "Sem movimento",
                        ProposalNumber = 12345678,
                        EndorsementNumber = 23232,
                        InclusionUserId = 1,
                        InclusionDate = DateTime.Now,
                        StartOfTerm = DateTime.Now.AddDays(-365),
                        EndOfTerm = DateTime.Now.AddDays(365),
                        InsuredAmount = 10000M,
                        PremiumValue = 100M,
                        NewProposalStatusName = "Em análise"
                    }
                );

                return PartialView("~/Views/Endorsement/PolicyEndorsementsGrid.cshtml", model);

            } catch (ApplicationException e) {
                return base.ReturnError(e.Message);
            } catch (IntegrationException e) {
                return base.ReturnError(e.Message);
            } catch (Exception e) {
                return base.ReturnException(MethodBase.GetCurrentMethod(), new { policyNumber }, e);
            }
        }


    }
}
