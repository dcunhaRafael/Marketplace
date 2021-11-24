using AutoMapper;
using AutoMapper.Configuration.Annotations;
using InsuranceApi.Domain.BusinessObjects.Entities;

namespace InsuranceApi.Web.ViewModels {

    [AutoMap(typeof(LegalRecourseTypeEntity))]
    public class LegalRecourseTypeReturnViewMdoel {

        [SourceMember(nameof(LegalRecourseTypeEntity.LegalRecourseTypeId))]
        public int Codigo { get; set; }

        [SourceMember(nameof(LegalRecourseTypeEntity.Name))]
        public string Nome { get; set; }          
    }
}
