using Domain.Exceptions;
using Domain.Payload;
using LazyCache;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Portal.Web.Models.Payment;
using Presentation.Web.Controllers;
using Presentation.Web.Services.Proxy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Portal.Web.Controllers {
    public class PaymentController : BaseController {
        private readonly IWebHostEnvironment webHostingEnvironment;
        private readonly ICommonService commonService;
        private readonly IPaymentService paymentService;

        public PaymentController(
            IAppCache memoryCache, ILogger<PaymentController> logger, IWebHostEnvironment webHostingEnvironment,
            IPaymentService paymentService, ICommonService commonService) : base(memoryCache, logger, commonService) {
            this.webHostingEnvironment = webHostingEnvironment;
            this.commonService = commonService;
            this.paymentService = paymentService;
        }

        public IActionResult LatePaymentSlip() {
            try {

                var model = new LatePaymentSlipViewModel() {
                    Products = base.GetCached<IList<Product>>("ListProducts", () => commonService.ListProducts())
                };
                return View("~/Views/Payment/LatePaymentSlip.cshtml", model);

            } catch (ApplicationException e) {
                return base.ReturnError(e.Message);
            } catch (IntegrationException e) {
                return base.ReturnError(e.Message);
            } catch (Exception e) {
                return base.ReturnException(MethodBase.GetCurrentMethod(), new { }, e);
            }
        }

        [HttpPost]
        public IActionResult LatePaymentSlipGrid(LatePaymentSlipViewModel model) {
            try {

                model.Results = paymentService.ListLatePaymentSlip(model.BrokerLegacyCode, model.TakerLegacyCode, model.InsuredLegacyCode,
                    model.ProductLegacyCode, model.PolicyNumber, model.EndorsementNumber, model.InstallmentNumber, model.PremiumValue,
                    model.OurNumber, model.FromDate, model.ToDate);

                var totalCount = model.Results.Count;
                var totalSum = model.Results.Sum(x => x.TotalPremiumValue) ?? 0M;

                var ranges = base.GetCached<IList<FixedDomain>>("ListLatePaymentSlipAgings", () => commonService.ListLatePaymentSlipAgings());
                foreach (var item in ranges) {
                    var parts = item.StringValue.Split("|");
                    var beginRange = int.Parse(parts[0]);
                    var endRange = int.Parse(parts[1]);

                    var itemCount = model.Results.Where(x => x.LateDays >= beginRange && x.LateDays <= endRange).Count();
                    var itemSum = model.Results.Where(x => x.LateDays >= beginRange && x.LateDays <= endRange).Sum(x => x.TotalPremiumValue) ?? 0M;

                    if (itemCount > 0) {
                        model.SummaryByCount.Add(new LatePaymentSlipSummary() {
                            Name = item.Name,
                            Count = itemCount,
                            Percentage = Math.Round((Convert.ToDecimal(itemCount) / Convert.ToDecimal(totalCount)) * 100M, 0),
                            BackgroundColor = parts[2]
                        });
                    }
                    if (totalSum > 0M) {
                        model.SummaryByValue.Add(new LatePaymentSlipSummary() {
                            Name = item.Name,
                            Value = itemSum,
                            Percentage = Math.Round((itemSum / totalSum) * 100M, 0),
                            BackgroundColor = parts[2]
                        });
                    }
                }

                return PartialView("~/Views/Payment/LatePaymentSlipGrid.cshtml", model);

            } catch (ApplicationException e) {
                return base.ReturnError(e.Message);
            } catch (IntegrationException e) {
                return base.ReturnError(e.Message);
            } catch (Exception e) {
                return base.ReturnException(MethodBase.GetCurrentMethod(), new { model }, e);
            }
        }

        [HttpPost]
        public IActionResult LatePaymentSlipDetail(string ourNumber) {
            try {

                var model = new LatePaymentSlipViewModel() {
                    CurrentItem = paymentService.GetLatePaymentSlip(ourNumber)
                };
                return PartialView("~/Views/Payment/LatePaymentSlipDetail.cshtml", model);

            } catch (ApplicationException e) {
                return base.ReturnError(e.Message);
            } catch (IntegrationException e) {
                return base.ReturnError(e.Message);
            } catch (Exception e) {
                return base.ReturnException(MethodBase.GetCurrentMethod(), new { ourNumber }, e);
            }
        }

    }
}
