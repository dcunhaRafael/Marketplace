using Domain.Exceptions;
using Domain.Payload;
using LazyCache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Presentation.Web.Models;
using Presentation.Web.Models.Configuration;
using Presentation.Web.Services.Proxy;
using System;
using System.Linq;
using System.Reflection;

namespace Presentation.Web.Controllers {
    public class ConfigurationController : BaseController {
        private readonly ILogger<ConfigurationController> logger;
        private readonly AppSettings appSettings;
        private readonly ICommonService commonService;
        private readonly IConfigurationService configurationService;

        public ConfigurationController(IAppCache memoryCache, ILogger<ConfigurationController> logger, IOptions<AppSettings> appSettings,
            ICommonService commonService, IConfigurationService configurationService) : base(memoryCache, logger, commonService) {
            this.logger = logger;
            this.appSettings = appSettings.Value;
            this.commonService = commonService;
            this.configurationService = configurationService;
        }

        #region Cadastro de ocorrências

        public IActionResult Occurrences(bool hideHeader = false) {
            ViewData.Add("HideHeader", hideHeader);
            var model = new OccurrencesViewModel() {
                ProductList = commonService.ListProducts(),
                ProfileList = commonService.ListProfiles(),
                Documents = null
            };
            return View("~/Views/Configuration/Occurrences/Occurrences.cshtml", model);
        }

        [HttpPost]
        public IActionResult OccurrencesGrid(OccurrencesViewModel model) {
            try {

                model = new OccurrencesViewModel() {
                    Occurrences = configurationService.ListOccurrenceTypes(model.Filters)
                };

                return PartialView("~/Views/Configuration/Occurrences/OccurrencesGrid.cshtml", model);

            } catch (Exception e) {
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { model }, e);
                }
            }
        }

        [HttpPost]
        public IActionResult OccurrenceData(int? id, bool editable) {
            try {

                var model = new OccurrencesViewModel() {
                    ProductList = commonService.ListProducts(),
                    ProfileList = commonService.ListProfiles(),
                    Documents = commonService.ListDocumentTypes(),
                    IsEditable = editable
                };

                if (id != null) {
                    model.CurrentItem = configurationService.GetOccurrenceType(id.Value);
                    if (model.CurrentItem.ProductId != null) {
                        model.CoverageList = commonService.ListCoverages(model.CurrentItem.ProductId.Value);
                    }
                    if (model.CurrentItem.ProfileId != null) {
                        model.LiberationUsers = commonService.ListUsers(model.CurrentItem.ProfileId.Value);
                        if (model.CurrentItem.LiberationUsers.Count > 0) {
                            for (int i = 0; i < model.LiberationUsers.Count; i++) {
                                var user = model.CurrentItem.LiberationUsers.FirstOrDefault(x => x.UserId == model.LiberationUsers[i].UserId);
                                if (user != null) {
                                    model.LiberationUsers[i].ParentId = user.OccurrenceTypeLiberationUserId;
                                    model.LiberationUsers[i].IsChecked = true;
                                }
                            }
                        }
                    }

                    if (model.CurrentItem.Documents.Count > 0) {
                        for (int i = 0; i < model.Documents.Count; i++) {
                            var document = model.CurrentItem.Documents.FirstOrDefault(x => x.DocumentTypeId == model.Documents[i].DocumentTypeId);
                            if (document != null) {
                                model.Documents[i].OccurrenceTypeDocumentId = document.OccurrenceTypeDocumentId;
                                model.Documents[i].IsChecked = true;
                                model.Documents[i].IsRequired = document.IsRequired;
                            }
                        }
                    }
                }

                return PartialView("~/Views/Configuration/Occurrences/OccurrenceData.cshtml", model);

            } catch (Exception e) {
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { id, editable }, e);
                }
            }
        }

        [HttpPost]
        public IActionResult OccurrenceLiberationUser(int profileId) {
            try {

                var model = new OccurrencesViewModel() {
                    LiberationUsers = commonService.ListUsers(profileId),
                    IsEditable = true
                };

                return PartialView("~/Views/Configuration/Occurrences/OccurrenceLiberationUser.cshtml", model);

            } catch (Exception e) {
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { profileId }, e);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult OccurrenceSave(OccurrencesViewModel model) {
            try {

                model.CurrentItem.LoggedUserId = base.LoggedUserId;
                model.CurrentItem.Documents = model.Documents.Where(x => x.IsChecked)
                                                             .Select(y => new OccurrenceTypeDocument() {
                                                                 OccurrenceTypeDocumentId = y.OccurrenceTypeDocumentId,
                                                                 DocumentTypeId = y.DocumentTypeId,
                                                                 IsRequired = y.IsRequired
                                                             }).ToList();
                model.CurrentItem.LiberationUsers = model.LiberationUsers.Where(x => x.IsChecked)
                                                                         .Select(y => new OccurrenceTypeLiberationUser() {
                                                                             OccurrenceTypeLiberationUserId = y.ParentId,
                                                                             UserId = y.UserId
                                                                         }).ToList();

                var id = configurationService.SaveOccurrenceType(model.CurrentItem);
                return base.ReturnSuccess(data: new { id });

            } catch (Exception e) {
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { model }, e);
                }
            }
        }

        [HttpPost]
        public IActionResult OccurrenceDelete(int id) {
            try {

                configurationService.DeleteOccurrenceType(id, base.LoggedUserId);
                return base.ReturnSuccess();

            } catch (Exception e) {
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { id }, e);
                }
            }
        }

        [HttpPost]
        public IActionResult OccurrenceCopyModal(int id) {
            try {

                var model = new OccurrencesViewModel() {
                    CurrentItem = new OccurrenceType() { OccurrenceTypeId = id },
                    ProductList = commonService.ListProducts()
                };

                return PartialView("~/Views/Configuration/Occurrences/OccurrenceCopyModal.cshtml", model);

            } catch (Exception e) {
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { id }, e);
                }
            }
        }

        [HttpPost]
        public IActionResult OccurrenceCopy(OccurrencesViewModel model) {
            try {

                var id = configurationService.CopyOccurrenceType(model.CurrentItem.OccurrenceTypeId.Value, model.CopyToProductId, model.CopyToCoverageId, base.LoggedUserId);
                return base.ReturnSuccess(data: new { id });

            } catch (Exception e) {
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { model }, e);
                }
            }
        }

        #endregion

        #region Cadastro de lotes

        public IActionResult PolicyBatchConfiguration(bool hideHeader = false) {
            ViewData.Add("HideHeader", hideHeader);
            var model = new PolicyBatchConfigurationViewModel() { };
            return View("~/Views/Configuration/PolicyBatchConfiguration/PolicyBatchConfiguration.cshtml", model);
        }

        [HttpPost]
        public IActionResult PolicyBatchConfigurationGrid(PolicyBatchConfigurationViewModel model) {
            try {

                model.Configurations = configurationService.ListPolicyBatchConfiguration(model.Filters);

                return PartialView("~/Views/Configuration/PolicyBatchConfiguration/PolicyBatchConfigurationGrid.cshtml", model);

            } catch (Exception e) {
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { model }, e);
                }
            }
        }

        [HttpPost]
        public IActionResult PolicyBatchConfigurationData(int? id, bool editable) {
            try {

                var model = new PolicyBatchConfigurationViewModel() {
                    IsEditable = editable
                };

                if (id != null) {
                    model.CurrentItem = configurationService.GetPolicyBatchConfiguration(id.Value);
                }

                return PartialView("~/Views/Configuration/PolicyBatchConfiguration/PolicyBatchConfigurationData.cshtml", model);

            } catch (Exception e) {
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { id, editable }, e);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PolicyBatchConfigurationSave(PolicyBatchConfigurationViewModel model) {
            try {

                model.CurrentItem.LoggedUserId = base.LoggedUserId;

                var id = configurationService.SavePolicyBatchConfiguration(model.CurrentItem);
                return base.ReturnSuccess(data: new { id });

            } catch (Exception e) {
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { model }, e);
                }
            }
        }

        [HttpPost]
        public IActionResult PolicyBatchConfigurationDelete(int id) {
            try {

                configurationService.DeletePolicyBatchConfiguration(id, base.LoggedUserId);
                return base.ReturnSuccess();

            } catch (Exception e) {
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { id }, e);
                }
            }
        }

        [HttpPost]
        public IActionResult PolicyBatchConfigurationMailGrid(int id) {
            try {

                var model = new PolicyBatchConfigurationViewModel() {
                    CurrentItem = new PolicyBatchConfiguration() { PolicyBatchConfigurationId = id },
                    Mails = configurationService.ListPolicyBatchConfigurationMails(id),
                    IsEditable = true
                };

                return PartialView("~/Views/Configuration/PolicyBatchConfiguration/PolicyBatchConfigurationMailGrid.cshtml", model);

            } catch (Exception e) {
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { id }, e);
                }
            }
        }

        [HttpPost]
        public IActionResult PolicyBatchConfigurationMailData(int policyBatchConfigurationId, int? policyBatchConfigurationMailId, bool editable) {
            try {

                var model = new PolicyBatchConfigurationViewModel() {
                    CurrentMail = new PolicyBatchConfigurationMail() {
                        PolicyBatchConfigurationId = policyBatchConfigurationId
                    },
                    IsEditable = editable
                };

                if (policyBatchConfigurationMailId != null) {
                    model.CurrentMail = configurationService.GetPolicyBatchConfigurationMail(policyBatchConfigurationMailId.Value);
                }

                model.Destinations = commonService.ListUsers(appSettings.SubscriptionProfileId);
                if (model.CurrentMail.Destinations.Count > 0) {
                    for (int i = 0; i < model.Destinations.Count; i++) {
                        var user = model.CurrentMail.Destinations.FirstOrDefault(x => x.UserId == model.Destinations[i].UserId);
                        if (user != null) {
                            model.Destinations[i].ParentId = user.PolicyBatchConfigurationMailDestinationId;
                            model.Destinations[i].IsChecked = true;
                        }
                    }
                }

                return PartialView("~/Views/Configuration/PolicyBatchConfiguration/PolicyBatchConfigurationMailData.cshtml", model);

            } catch (Exception e) {
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { policyBatchConfigurationId, policyBatchConfigurationMailId, editable }, e);
                }
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult PolicyBatchConfigurationMailSave(PolicyBatchConfigurationViewModel model) {
            try {

                model.CurrentMail.LoggedUserId = base.LoggedUserId;
                model.CurrentMail.Destinations = model.Destinations.Where(x => x.IsChecked)
                                                                         .Select(y => new PolicyBatchConfigurationMailDestination() {
                                                                             PolicyBatchConfigurationMailDestinationId = y.ParentId,
                                                                             PolicyBatchConfigurationMailId = model.CurrentMail.PolicyBatchConfigurationMailId,
                                                                             UserId = y.UserId
                                                                         }).ToList();

                var id = configurationService.SavePolicyBatchConfigurationMail(model.CurrentMail);
                return base.ReturnSuccess(data: new { id });

            } catch (Exception e) {
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { model }, e);
                }
            }
        }

        [HttpPost]
        public IActionResult PolicyBatchConfigurationMailDelete(int id) {
            try {

                configurationService.DeletePolicyBatchConfigurationMail(id, base.LoggedUserId);
                return base.ReturnSuccess();

            } catch (Exception e) {
                if ((e is IntegrationException)) {
                    return base.ReturnError(e.Message);
                } else {
                    return base.ReturnException(MethodBase.GetCurrentMethod(), new { id }, e);
                }
            }
        }

        #endregion

    }
}
