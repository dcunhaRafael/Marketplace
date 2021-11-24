using Domain.Payload;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Integration.Interfaces.Legacy {
    public interface IBacenService {
        IList<SelicTax> ListDaily(DateTime start, DateTime finish);
        IList<SelicTax> ListMonthly(DateTime start, DateTime finish);
    }
}
