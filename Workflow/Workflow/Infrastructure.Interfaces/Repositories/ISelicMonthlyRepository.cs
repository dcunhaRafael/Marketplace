using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories {
    public interface ISelicMonthlyRepository {
        Task<IList<SelicMonthly>> ListAsync(DateTime fromDate, DateTime toData);
        Task SaveAsync(SelicMonthly item);
        Task<IList<Domain.Payload.SelicCorrection>> ListCorrectionAsync(DateTime fromDate, DateTime toDate);
    }
}
