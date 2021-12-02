using Domain.Enumerators;
using Domain.Exceptions;
using Domain.Payload;
using Domain.Util.Extensions;
using LazyCache;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portal.Web.Models.ComissionStatement;
using Presentation.Web.Controllers;
using Presentation.Web.Services.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Portal.Web.Controllers {
    public class ComissionStatementController : BaseController {
        private readonly IComissionStatementService comissionStatementService;
        private readonly ICommonService commonService;

        public ComissionStatementController(
            IAppCache memoryCache, ILogger<ComissionStatementController> logger,
            IComissionStatementService comissionStatementService, ICommonService commonService) : base(memoryCache, logger, commonService) {
            this.comissionStatementService = comissionStatementService;
            this.commonService = commonService;
        }

        public IActionResult ComissionStatements() {
            try {

                var model = new ComissionStatementViewModel() {
                    Status = base.GetCached<IList<ComissionStatementStatus>>("ComissionStatementStatus", () => commonService.ListComissionStatementStatus())
                };
                return View("~/Views/ComissionStatement/ComissionStatements.cshtml", model);

            } catch (ApplicationException e) {
                return base.ReturnError(e.Message);
            } catch (IntegrationException e) {
                return base.ReturnError(e.Message);
            } catch (Exception e) {
                return base.ReturnException(MethodBase.GetCurrentMethod(), new { }, e);
            }
        }

        [HttpPost]
        public IActionResult ComissionStatementGrid(ComissionStatementViewModel model) {
            try {

                switch (model.SearchPeriodId) {
                    case SearchRangeEnum.Last30Days:
                        model.FromDate = DateTime.Now.Date.AddDays(-30);
                        model.ToDate = DateTime.Now.Date;
                        break;
                    case SearchRangeEnum.Last60Days:
                        model.FromDate = DateTime.Now.Date.AddDays(-60);
                        model.ToDate = DateTime.Now.Date;
                        break;
                    case SearchRangeEnum.Last90Days:
                        model.FromDate = DateTime.Now.Date.AddDays(-90);
                        model.ToDate = DateTime.Now.Date;
                        break;
                    case SearchRangeEnum.SpecificYearMonth:
                        if (model.MonthNumber == null) {
                            model.FromDate = new DateTime(model.YearNumber.Value, 1, 1);
                            model.ToDate = new DateTime(model.YearNumber.Value, 12, 31);
                        } else {
                            model.FromDate = new DateTime(model.YearNumber.Value, model.MonthNumber.Value, 1);
                            model.ToDate = (new DateTime(model.MonthNumber.Value, model.MonthNumber.Value, 1)).ToLastDayOfMonth();
                        }
                        break;
                    case SearchRangeEnum.SpecificPeriod:
                    default:
                        break;
                }

                model.Statements = comissionStatementService.ListComissionStatement(model.StatementNumber, model.FromDate, model.ToDate, model.StatusId, model.Broker, base.LoggedUserId);
                model.Status = base.GetCached<IList<ComissionStatementStatus>>("ComissionStatementStatus", () => commonService.ListComissionStatementStatus());

                var totalValue = model.Statements.Where(x => model.Status.Any(y => y.Name.Equals(x.StatusName))).Sum(x => x.ComissionValue) ?? 1.0M;

                model.Summary = new List<ComissionStatementSummary>();
                if (totalValue != 0M) {
                    var summary = model.Statements.GroupBy(y => y.StatusName, y => y, (key, g) => new { StatusName = key, ComissionValue = g.Sum(x => x.ComissionValue) });

                    foreach (var item in model.Status) {
                        var value = summary.FirstOrDefault(x => x.StatusName.Equals(item.Name))?.ComissionValue ?? 0M;
                        model.Summary.Add(new ComissionStatementSummary() {
                            Status = model.Status.FirstOrDefault(x => x.Name.Equals(item.Name)),
                            Value = value,
                            Percentage = Math.Round((value / totalValue) * 100M, 0)
                        });
                    }
                }

                for (int i = 0; i < model.Statements.Count; i++) {
                    var status = model.Status.FirstOrDefault(x => x.Name.Equals(model.Statements[i].StatusName));
                    model.Statements[i].StatusBackgroundColor = status.BackgroundColor;
                    model.Statements[i].StatusTextColor = status.TextColor;
                }

                return PartialView("~/Views/ComissionStatement/ComissionStatementGrid.cshtml", model);

            } catch (ApplicationException e) {
                return base.ReturnError(e.Message);
            } catch (IntegrationException e) {
                return base.ReturnError(e.Message);
            } catch (Exception e) {
                return base.ReturnException(MethodBase.GetCurrentMethod(), new { model }, e);
            }
        }

        [HttpPost]
        public IActionResult ComissionStatementCover(ComissionStatementViewModel model) {
            try {

                model.CurrentStatement = comissionStatementService.ListComissionStatement(model.StatementNumber, null, null, null, model.Broker, 
                                                                                          base.LoggedUserId).FirstOrDefault(x => x.Competency.Equals(model.Competency));
                model.StatementDetails = comissionStatementService.ListComissionStatementDetais(model.StatementNumber.Value, model.Competency, model.Broker, base.LoggedUserId);
                model.StatementTypes = comissionStatementService.ListComissionStatementTypes(model.StatementNumber.Value, model.Competency, model.Broker, base.LoggedUserId);
                model.StatementBusiness = comissionStatementService.ListComissionStatementBusiness(model.StatementNumber.Value, model.Competency, model.Broker, base.LoggedUserId);

                return PartialView("~/Views/ComissionStatement/ComissionStatementCover.cshtml", model);

            } catch (ApplicationException e) {
                return base.ReturnError(e.Message);
            } catch (IntegrationException e) {
                return base.ReturnError(e.Message);
            } catch (Exception e) {
                return base.ReturnException(MethodBase.GetCurrentMethod(), new { model }, e);
            }
        }

        [HttpPost]
        public IActionResult ComissionStatementEntries(ComissionStatementViewModel model) {
            try {

                model.CurrentStatement = comissionStatementService.ListComissionStatement(model.StatementNumber, null, null, null, model.Broker, 
                                                                                          base.LoggedUserId).FirstOrDefault(x => x.Competency.Equals(model.Competency));
                model.StatementEntries = comissionStatementService.ListComissionStatementEntries(model.StatementNumber.Value, model.Competency, model.Broker, base.LoggedUserId);

                return PartialView("~/Views/ComissionStatement/ComissionStatementEntries.cshtml", model);

            } catch (ApplicationException e) {
                return base.ReturnError(e.Message);
            } catch (IntegrationException e) {
                return base.ReturnError(e.Message);
            } catch (Exception e) {
                return base.ReturnException(MethodBase.GetCurrentMethod(), new { model }, e);
            }
        }
    }
}
