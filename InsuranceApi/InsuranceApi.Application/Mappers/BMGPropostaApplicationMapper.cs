using SeguroGarantia.Domain.BusinessObjects.Entities;
using SeguroGarantia.Domain.BusinessObjects.Enumerators;
using SeguroGarantia.Domain.Common.Extension;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Transactions;

namespace SeguroGarantia.Application.Mappers
{
    public static class BMGPropostaApplicationMapper
    {
        public static BMGPropostaEntity MapperProposta(PropostaEntity entity)
        {
            try
            {
                var mappedObj = new BMGPropostaEntity()
                {

                    TipoSeguro = TipoSeguroEnum.Recursal,
                    Corretor = new CorretorConsultaEntity { IdUsuarioCorretor = 521 },
                    DadosGarantia = new DadosGarantiaEntity()
                    {
                        NumeroProcesso = entity.NumeroProcesso,                      
                        TipoRecurso = new TipoRecursoEntity() { LegalRecourseTypeId = entity.CodigoTipoRecurso },
                        Prazo = new TipoPrazoEntity() { Id = entity.CodigoPrazoAno },
                        Tribunal = new TribunalEntity() { ExternalCode = entity.CodigoTribunal },
                        Vara = new VaraEntity() { ExternalCode = entity.CodigoVara },                       
                        Reclamantes = MapDadosReclamente(entity)                        
                    },
                    Tomador = new TomadorEntity()
                    {
                        CpfCnpjCorretor = entity.CpfCnpjTomador,
                    },
                    Produto = new ProdutoEntity()
                    {
                        CodigoProduto = entity.CodigoProduto.ToInt()
                    }
                };

                return mappedObj;
            }
            catch (Exception e)
            {
                throw new InvalidCastException("Erro no mapeamento dos dados da requisição (Proposta_Gravar).", e);
            }
        }

        private static List<DadosReclamanteEntity> MapDadosReclamente(PropostaEntity entity)
        {
            List<DadosReclamanteEntity> mapperList = new List<DadosReclamanteEntity>();

            var mapperObj = new DadosReclamanteEntity()
            {
                IsPrincipal = true,
                CpfCnpjReclamante = entity.CpfCnpjReclamente.ToLong(),
                NomeReclamante = "Teste teste",
                Segurado = new SeguradoEntity()
                {

                    Endereco = new EnderecoEntity()
                    {
                        Cep = entity.CepReclamente,
                        Bairro = entity.BairroReclamente,
                        Cidade = entity.CidadeReclamente,
                        UF = entity.UfReclamente,
                        Complemento = entity.ComplementoReclamente
                    }
                }
            };

            mapperList.Add(mapperObj);

            return mapperList;
        }
    }
}
