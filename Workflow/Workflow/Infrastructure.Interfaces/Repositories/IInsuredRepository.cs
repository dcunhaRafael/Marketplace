using Domain.Entities;
using Domain.Enumerators;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Interfaces.Repositories {
    public interface IInsuredRepository {
        Task<IList<Insured>> ListAsync(string name, RecordStatusEnum? status);
    }
}
