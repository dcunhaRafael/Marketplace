using Application.Interfaces.Services;
using Integration.Workflow.WebAPI.Models.Payment;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

namespace Integration.Workflow.WebAPI.Controllers.v1 {
    [ApiController]
    [Route("api/payment")]
    public class PaymentController : BaseController {
        private readonly IPaymentService paymentService;

        public PaymentController(ILogger<PaymentController> logger,
            IPaymentService paymentService) : base(logger) {
            this.paymentService = paymentService;
        }

        [HttpPost("ListLatePaymentSlip")]
        public IActionResult ListLatePaymentSlip(ListLatePaymentSlipRequest request) {
            try {

                var items = paymentService.ListLatePaymentSlip(request.BrokerLegacyCode, request.TakerLegacyCode, request.InsuredLegacyCode, 
                                request.ProductLegacyCode, request.PolicyNumber, request.EndorsementNumber, request.InstallmentNumber, 
                                request.PremiumValue, request.OurNumber, request.FromDate, request.ToDate);

                return base.ReturnSuccess(data: items);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

        [HttpPost("GetLatePaymentSlip")]
        public IActionResult GetLatePaymentSlip(GetLatePaymentSlipRequest request) {
            try {

                var item = paymentService.GetLatePaymentSlip(request.OurNumber);

                return base.ReturnSuccess(data: item);

            } catch (Exception e) {
                return base.ReturnError(MethodBase.GetCurrentMethod(), e);
            }
        }

    }
}
