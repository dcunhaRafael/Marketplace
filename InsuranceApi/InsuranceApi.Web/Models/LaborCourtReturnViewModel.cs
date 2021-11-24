using AutoMapper;
using AutoMapper.Configuration.Annotations;
using InsuranceApi.Domain.BusinessObjects.Entities;

namespace InsuranceApi.Web.ViewModels {

    [AutoMap(typeof(LaborCourtEntity))]
    public class LaborCourtReturnViewModel {

        [SourceMember(nameof(LaborCourtEntity.LaborCourtId))]
        public int Codigo { get; set; }
        [SourceMember(nameof(LaborCourtEntity.Name))]
        public string Nome { get; set; }
    }
}
