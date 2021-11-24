using Application.Interfaces.Services;
using Domain.Payload;
using Integration.Workflow.WebAPI.Models.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace Integration.Workflow.WebAPI.Controllers.v1 {
    [ApiController]
    [Route("api/configuration")]
    public class ConfigurationController : BaseController {
        private readonly IOccurrenceTypeService occurrenceTypeService;
        private readonly IPolicyBatchConfigurationService policyBatchConfigurationService;

        public ConfigurationController(ILogger<CommonController> logger, IOccurrenceTypeService occurrenceTypeService,
            IPolicyBatchConfigurationService policyBatchConfigurationService) : base(logger) {
            this.occurrenceTypeService = occurrenceTypeService;
            this.policyBatchConfigurationService = policyBatchConfigurationService;
        }

        [HttpPost("ListOccurrenceTypes")]
        public async Task<IActionResult> ListOccurrenceTypes(OccurrenceTypeFilters filters) {
            try {

                var items = await occurrenceTypeService.ListAsync(filters);

                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("GetOccurrenceType")]
        public async Task<IActionResult> GetOccurrenceType(GetOccurrenceTypeRequest occurrence) {
            try {

                var item = await occurrenceTypeService.GetAsync(occurrence.OccurrenceTypeId);

                return base.ReturnSuccess(data: item);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("SaveOccurrenceType")]
        public async Task<IActionResult> SaveOccurrenceType(OccurrenceType occurrence) {
            try {

                var id = await occurrenceTypeService.SaveAsync(occurrence);

                return base.ReturnSuccess(data: id);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("DeleteOccurrenceType")]
        public async Task<IActionResult> DeleteOccurrenceType(DeleteOccurrenceTypeRequest occurrence) {
            try {

                await occurrenceTypeService.DeleteAsync(new OccurrenceType() {
                    OccurrenceTypeId = occurrence.OccurrenceTypeId,
                    LoggedUserId = occurrence.LoggedUserId
                });

                return base.ReturnSuccess();

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("ListPolicyBatchConfiguration")]
        public async Task<IActionResult> ListPolicyBatchConfiguration(PolicyBatchConfigurationFilters filters) {
            try {

                var items = await policyBatchConfigurationService.ListAsync(filters);

                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("GetPolicyBatchConfiguration")]
        public async Task<IActionResult> GetPolicyBatchConfiguration(PolicyBatchConfigurationRequest item) {
            try {

                var items = await policyBatchConfigurationService.GetAsync(item.PolicyBatchConfigurationId);

                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("SavePolicyBatchConfiguration")]
        public async Task<IActionResult> SavePolicyBatchConfiguration(PolicyBatchConfiguration item) {
            try {

                var id = await policyBatchConfigurationService.SaveAsync(item);

                return base.ReturnSuccess(data: id);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("DeletePolicyBatchConfiguration")]
        public async Task<IActionResult> DeletePolicyBatchConfiguration(PolicyBatchConfigurationRequest item) {
            try {

                await policyBatchConfigurationService.DeleteAsync(new PolicyBatchConfiguration() {
                    PolicyBatchConfigurationId = item.PolicyBatchConfigurationId,
                    LoggedUserId = item.LoggedUserId
                });

                return base.ReturnSuccess();

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("ListPolicyBatchConfigurationMails")]
        public async Task<IActionResult> ListPolicyBatchConfigurationMails(PolicyBatchConfigurationRequest item) {
            try {

                var items = await policyBatchConfigurationService.ListMailsAsync(item.PolicyBatchConfigurationId);

                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("GetPolicyBatchConfigurationMail")]
        public async Task<IActionResult> GetPolicyBatchConfigurationMail(PolicyBatchConfigurationMailRequest item) {
            try {

                var items = await policyBatchConfigurationService.GetMailAsync(item.PolicyBatchConfigurationMailId);

                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("SavePolicyBatchConfigurationMail")]
        public async Task<IActionResult> SavePolicyBatchConfigurationMail(PolicyBatchConfigurationMail item) {
            try {

                var id = await policyBatchConfigurationService.SaveMailAsync(item);

                return base.ReturnSuccess(data: id);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("DeletePolicyBatchConfigurationMail")]
        public async Task<IActionResult> DeletePolicyBatchConfigurationMail(PolicyBatchConfigurationMailRequest item) {
            try {

                await policyBatchConfigurationService.DeleteMailAsync(new PolicyBatchConfigurationMail() {
                    PolicyBatchConfigurationMailId = item.PolicyBatchConfigurationMailId,
                    LoggedUserId = item.LoggedUserId
                });

                return base.ReturnSuccess();

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

    }
}
