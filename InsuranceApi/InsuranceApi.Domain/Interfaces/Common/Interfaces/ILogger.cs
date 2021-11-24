using System;
using System.Threading.Tasks;

namespace InsuranceApi.Domain.Interfaces.Common.Interfaces {
    public interface ILogger {

        Task Information(string message, Exception exception = null, object param = null);
        Task Warning(string message, Exception exception = null, object param = null);
        Task Error(string message, Exception exception = null, object param = null);
    }
}
