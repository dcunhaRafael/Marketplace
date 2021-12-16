using Application.Interfaces.Services;
using Domain.Enumerators;
using Domain.Payload;
using Integration.Workflow.WebAPI.Models.Common;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Integration.Workflow.WebAPI.Controllers.v1 {
    [ApiController]
    [Route("api/common")]
    public class CommonController : BaseController {
        private readonly IAuditService auditService;
        private readonly IBrokerService brokerService;
        private readonly IComissionStatementService comissionStatementService;
        private readonly ICoverageService coverageService;
        private readonly IDocumentTypeService documentTypeService;
        private readonly IFixedDomainService fixedDomainService;
        private readonly IInsuredService insuredService;
        private readonly IOccurrenceTypeService occurrenceTypeService;
        private readonly IProductService productService;
        private readonly IProfileService profileService;
        private readonly IRefusalReasonService refusalReasonService;
        private readonly ITakerService takerService;
        private readonly IUpdateIndexService updateIndexService;
        private readonly IUserService userService;

        public CommonController(ILogger<CommonController> logger, IAuditService auditService,
            IBrokerService brokerService, IComissionStatementService comissionStatementService, ICoverageService coverageService, IDocumentTypeService documentTypeService,
            IFixedDomainService fixedDomainService, IInsuredService insuredService, IOccurrenceTypeService occurrenceTypeService, IProductService productService, 
            IProfileService profileService, IRefusalReasonService refusalReasonService, ITakerService takerService, IUpdateIndexService updateIndexService, 
            IUserService userService) : base(logger) {
            this.auditService = auditService;
            this.brokerService = brokerService;
            this.comissionStatementService = comissionStatementService;
            this.coverageService = coverageService;
            this.documentTypeService = documentTypeService;
            this.fixedDomainService = fixedDomainService;
            this.insuredService = insuredService;
            this.occurrenceTypeService = occurrenceTypeService;
            this.productService = productService;
            this.profileService = profileService;
            this.refusalReasonService = refusalReasonService;
            this.takerService = takerService;
            this.updateIndexService = updateIndexService;
            this.userService = userService;
        }

        [HttpPost("ListProducts")]
        public async Task<IActionResult> ListProducts() {
            try {

                var items = await productService.ListAsync(RecordStatusEnum.Active);

                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("ListCoverages")]
        public async Task<IActionResult> ListCoverages([FromBody] int productId) {
            try {

                var items = await coverageService.ListAsync(productId, RecordStatusEnum.Active);

                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("ListProfiles")]
        public async Task<IActionResult> ListProfiles() {
            try {

                var items = await profileService.ListAsync(RecordStatusEnum.Active);

                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("ListUsers")]
        public async Task<IActionResult> ListUsers(ListUsersRequest request) {
            try {

                var items = await userService.ListAsync(request.ProfileId, RecordStatusEnum.Active);

                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("ListDocumentTypes")]
        public async Task<IActionResult> ListDocumentTypes() {
            try {

                var items = await documentTypeService.ListAsync(RecordStatusEnum.Active);

                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("ListBrokers")]
        public async Task<IActionResult> ListBrokers(ListBrokersRequest request) {
            try {

                var items = await brokerService.ListAsync(request.Name, RecordStatusEnum.Active);

                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("ListTakers")]
        public async Task<IActionResult> ListTakers(ListTakersRequest request) {
            try {

                var items = await takerService.ListAsync(request.BrokerId, request.Name, RecordStatusEnum.Active);

                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("ListInsureds")]
        public async Task<IActionResult> ListInsureds(ListInsuredsRequest request) {
            try {

                var items = await insuredService.ListAsync(request.Name, RecordStatusEnum.Active);

                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("ListRefusalReasons")]
        public async Task<IActionResult> ListRefusalReasons() {
            try {

                var items = await refusalReasonService.ListAsync(RecordStatusEnum.Active);

                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("ListOccurrenceTypes")]
        public async Task<IActionResult> ListOccurrenceTypes() {
            try {

                var items = await occurrenceTypeService.ListAsync(new Domain.Payload.OccurrenceTypeFilters() {
                    Status = RecordStatusEnum.Active
                });

                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("AddAudit")]
        public async Task<IActionResult> AddAudit(Audit audit) {
            try {

                await auditService.AddAsync(audit);

                return base.ReturnSuccess();

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("ListUpdateIndexes")]
        public async Task<IActionResult> ListUpdateIndexes() {
            try {

                var items = await updateIndexService.ListAsync();

                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("ListComissionStatementStatus")]
        public async Task<IActionResult> ListComissionStatementStatus() {
            try {

                var items = await comissionStatementService.ListAsync();

                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("ListLatePaymentSlipAgings")]
        public async Task<IActionResult> ListLatePaymentSlipAgings() {
            try {

                var items = await fixedDomainService.List(null, FixedDomainGroupNameEnum.LatePaymentSlipAgings);

                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }
    }
}
