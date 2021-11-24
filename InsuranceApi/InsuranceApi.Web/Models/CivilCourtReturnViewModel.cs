using AutoMapper;
using AutoMapper.Configuration.Annotations;
using InsuranceApi.Domain.BusinessObjects.Entities;

namespace InsuranceApi.Web.ViewModels {
    [AutoMap(typeof(CivilCourtEntity))]
    public class CivilCourtReturnViewModel {

        [SourceMember(nameof(CivilCourtEntity.CivilCourtId))]
        public int Codigo { get; set; }

        [SourceMember(nameof(CivilCourtEntity.LaborCourtId))]
        public int CodigoTribunal { get; set; }

        [SourceMember(nameof(CivilCourtEntity.Name))]
        public string Descricao { get; set; }
    }
}
