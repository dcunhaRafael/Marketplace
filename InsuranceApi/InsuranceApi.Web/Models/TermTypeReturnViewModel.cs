using AutoMapper;
using AutoMapper.Configuration.Annotations;
using InsuranceApi.Domain.BusinessObjects.Entities;

namespace InsuranceApi.Web.ViewModels {
    [AutoMap(typeof(TermTypeEntity))]
    public class TermTypeReturnViewModel {

        [SourceMember(nameof(TermTypeEntity.Id))]
        public int Codigo { get; set; }
        [SourceMember(nameof(TermTypeEntity.Name))]
        public string Descricao { get; set; }
    }
}
