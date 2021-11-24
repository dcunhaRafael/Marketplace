using AutoMapper;
using AutoMapper.Configuration.Annotations;
using InsuranceApi.Domain.BusinessObjects.Entities;
using System;

namespace InsuranceApi.Web.ViewModels {

    [AutoMap(typeof(TakerModel))]
    public class TakerReturnViewModel {


        [SourceMember(nameof(TakerModel.NomePessoa))]
        public string NomeRazaoSocial { get; set; }

        [SourceMember(nameof(TakerModel.CpfCnpj))]
        public string CpfCnpjTomador { get; set; }

        [SourceMember(nameof(TakerModel.ClasseRisco))]
        public string ClasseRisco { get; set; }

        [SourceMember(nameof(TakerModel.Limite))]
        public Decimal LimiteCredito { get; set; }

        [SourceMember(nameof(TakerModel.LimiteUtilizado))]
        public Decimal LimiteUtilizado { get; set; }

        [SourceMember(nameof(TakerModel.LimiteDisponivel))]
        public Decimal SaldoTomador { get; set; }

        [SourceMember(nameof(TakerModel.Taxa))]
        public Decimal Taxa { get; set; }

    }
}
