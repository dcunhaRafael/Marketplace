using Domain.Enumerators;
using Domain.Payload;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services {
    public interface IInsuredService {
        Task<IList<Insured>> ListAsync(string name, RecordStatusEnum? status);
    }
}
