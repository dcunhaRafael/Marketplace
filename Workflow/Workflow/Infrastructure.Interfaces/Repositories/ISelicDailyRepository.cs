using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories {
    public interface ISelicDailyRepository {
        Task<IList<SelicDaily>> ListAsync(DateTime fromDate, DateTime toData);
        Task SaveAsync(SelicDaily item);
    }
}
