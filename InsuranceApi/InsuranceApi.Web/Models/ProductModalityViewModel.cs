using AutoMapper;
using AutoMapper.Configuration.Annotations;
using InsuranceApi.Domain.BusinessObjects.Entities;
using System.Collections.Generic;

namespace InsuranceApi.Web.ViewModels {

    [AutoMap(typeof(ProdutoModalidadeEntity))]
    public class ProductModalityViewModel {

        [SourceMember(nameof(ProdutoModalidadeEntity.CodigoProduto))]
        public int Codigo { get; set; }

        [SourceMember(nameof(ProdutoModalidadeEntity.NomeProduto))]
        public string Nome { get; set; }

        [SourceMember(nameof(ProdutoModalidadeEntity.Modalidade))]
        public List<CoverageViewModel> Modalidade { get; set; }
    }
}
