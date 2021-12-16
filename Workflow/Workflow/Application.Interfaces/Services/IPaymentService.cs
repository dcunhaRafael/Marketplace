using Domain.Payload;
using System;
using System.Collections.Generic;

namespace Application.Interfaces.Services {
    public interface IPaymentService {
        IList<LatePaymentSlip> ListLatePaymentSlip(string brokerLegacyCode, string takerLegacyCode, string insuredLegacyCode, string productLegacyCode,
            long? policyNumber, int? endorsementNumber, int? installmentNumber, decimal? premiumValue, string ourNumber,
            DateTime? fromDate, DateTime? toDate);
        LatePaymentSlip GetLatePaymentSlip(string ourNumber);
    }
}
