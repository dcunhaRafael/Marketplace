using AutoMapper;
using AutoMapper.Configuration.Annotations;
using InsuranceApi.Domain.BusinessObjects.Entities;

namespace InsuranceApi.Web.ViewModels {

    [AutoMap(typeof(CoverageEntity))]
    public class CoverageViewModel {

        [SourceMember(nameof(CoverageEntity.IdCobertura))]
        public int CodigoModalidade { get; set; }

        [SourceMember(nameof(CoverageEntity.NomeCobertura))]
        public string NomeModalidade { get; set; }
    }
}
