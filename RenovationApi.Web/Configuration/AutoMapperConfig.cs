using AutoMapper;
using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Domain.Common.Extension;
using RenewalApi.Web.ViewModel;
using RenewalApi.Web.ViewModels;

namespace RenewalApi.Web.Configuration {
    public class AutoMapperConfig : Profile {
        public AutoMapperConfig() {

            CreateMap<ParcelamentoRetornoEntity, InstallmentReturnViewModel>().ReverseMap();
            CreateMap<ParcelaEntity, ParcelaRetornoViewModel>().ReverseMap();
            CreateMap<RenewalJudicialViewModel, ProposalRenewalEntity>().ReverseMap();
           // CreateMap<ProposalContractViewModel, ProposalContractEntity>().ReverseMap();
           // CreateMap<InsuredViewModel, InsuredEntity>().ReverseMap();
           // CreateMap<AddressViewModel, EnderecoEntity>().ReverseMap();
           // CreateMap<ContactViewModel, ContatoEntity>().ReverseMap();

           // CreateMap<InsertTakerViewModel, TakerModel>()
           // .ForMember(destination => destination.Contato, options => options.MapFrom(source => source.Contact))
           // .ForPath(destination => destination.Contato.Cpf, options => options.MapFrom(source => long.Parse(Extends.ApenasNumericos(source.Contact.Cpf))))
           // .ForMember(destination => destination.CpfCnpj, options => options.MapFrom(source => Extends.ApenasNumericos(source.CpfCnpj)));

           // CreateMap<ProposalAppealViewModel, ProposalAppealEntity>()
           //.ForMember(destination => destination.CpfCnpjReclamente, options => options.MapFrom(source => Extends.ApenasNumericos(source.CpfCnpjReclamente)))
           //.ForMember(destination => destination.CpfCnpjTomador, options => options.MapFrom(source => Extends.ApenasNumericos(source.CpfCnpjTomador)));

            CreateMap<ProposalEntity, ProposalRetornoViewModel>()
           .ForMember(destination => destination.CodigoApolice, options => options.MapFrom(source => source.IdApolice))
           .ForMember(destination => destination.CodigoEndosso, options => options.MapFrom(source => source.IdEndosso))
           .ForMember(destination => destination.CodigoProposta, options => options.MapFrom(source => source.CodigoProposta))
           .ForMember(destination => destination.NumeroProposta, options => options.MapFrom(source => source.CodigoProposta))
           .ForMember(destination => destination.DataInicioVigencia, options => options.MapFrom(source => source.DataInicioVigencia))
           .ForMember(destination => destination.DataFimVigencia, options => options.MapFrom(source => source.DataFimVigencia))
           .ForMember(destination => destination.DataProposta, options => options.MapFrom(source => source.DataProposta))
           .ForMember(destination => destination.CodigoProduto, options => options.MapFrom(source => source.Product.CodigoExterno))
           .ForMember(destination => destination.CodigoModalidade, options => options.MapFrom(source => source.DadosGarantia.Coverage.ExternalCode))
           .ForMember(destination => destination.ValorIof, options => options.MapFrom(source => source.DadosGarantia.ValorIOF))
           .ForMember(destination => destination.ValorPremioTarifario, options => options.MapFrom(source => source.DadosGarantia.ValorPremioTarifario))
           .ForMember(destination => destination.PercentualComissao, options => options.MapFrom(source => source.DadosGarantia.PercentualComissao))
           .ForMember(destination => destination.ValorComissao, options => options.MapFrom(source => source.DadosGarantia.ValorComissao))
           .ForMember(destination => destination.PercentualAgravo, options => options.MapFrom(source => source.DadosGarantia.PercentualAgravo))
           .ForMember(destination => destination.ValorImportanciaSegurada, options => options.MapFrom(source => source.DadosGarantia.ValorImportanciaSeguradaRecursal))
           .ForMember(destination => destination.CodigoStatus, options => options.MapFrom(source => source.CodigoStatus))
           .ForMember(destination => destination.DescricaoStatus, options => options.MapFrom(source => source.DescricaoStatus))
           .ForPath(destination => destination.Parcelamento.DataVencimentoParcela, options => options.MapFrom(source => source.DadosCobranca.DataVencimentoPrimeiraParcela))
           .ForPath(destination => destination.Parcelamento.DiaVencimentoParcela, options => options.MapFrom(source => source.DadosCobranca.DiaCobranca))
           .ForPath(destination => destination.Parcelamento.CodigoPeriodoPagamento, options => options.MapFrom(source => source.DadosCobranca.PeridiocidadePagamento.IdPeridiocidadePagamento))
           .ForPath(destination => destination.Parcelamento.NomePeriodoPagamento, options => options.MapFrom(source => source.DadosCobranca.PeridiocidadePagamento.NomePeridiocidadePagamento))
           .ForPath(destination => destination.Parcelamento.CodigoParcelamentoPremio, options => options.MapFrom(source => source.DadosCobranca.Parcelamento.IdParcelamento))
           .ForPath(destination => destination.Parcelamento.NomeParcelamentoPremio, options => options.MapFrom(source => source.DadosCobranca.Parcelamento.DescricaoParcelamento))
           .ForPath(destination => destination.Parcelamento.CodigoFormaPagamentoPrimeiraParcela, options => options.MapFrom(source => source.DadosCobranca.FormaPagamentoPrimeiraParcela.CodigoFormaPagamento))
           .ForPath(destination => destination.Parcelamento.NomeFormaPagamentoPrimeiraParcela, options => options.MapFrom(source => source.DadosCobranca.FormaPagamentoPrimeiraParcela.NomeFormaPagamento))
           .ForPath(destination => destination.Parcelamento.CodigoFormaPagamentoDemaisParcelas, options => options.MapFrom(source => source.DadosCobranca.FormaPagamentoPrimeiraParcela.CodigoFormaPagamento))
           .ForPath(destination => destination.Parcelamento.NomeFormaPagamentoDemaisParcelas, options => options.MapFrom(source => source.DadosCobranca.FormaPagamentoDemaisParcelas.NomeFormaPagamento))
           .ForPath(destination => destination.Parcelamento.Parcelas, options => options.MapFrom(source => source.Parcelas));
        }
    }
}
