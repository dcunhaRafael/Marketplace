using System;
using System.Threading.Tasks;

namespace Application.Interfaces.Services {
    public interface ISelicService {
        Task<DateTime> SyncDailyAsync(DateTime fromDate, DateTime toDate);
        Task<DateTime> SyncMonthlyAsync(DateTime fromDate, DateTime toDate);
        Task<decimal> ApplyCorrectionAsync(decimal initialValue, DateTime fromDate, DateTime toDate);
    }
}
