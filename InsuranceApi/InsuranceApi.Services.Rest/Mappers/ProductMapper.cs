using InsuranceApi.Domain.BusinessObjects.Entities;
using InsuranceApi.Services.Rest.Messages.Legacy;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceApi.Services.Rest.Mappers {
    public static class ProductMapper {

        /// <summary>
        /// Converte a resposta da busca de produtos
        /// </summary>
        /// <param name="response">Dados retornados pelo serviço</param>
        /// <returns></returns>
        public static List<ProductEntity> MapProdutoBuscarResponseToEntity(ProdutoBuscarResponse response) {
            try {
                var mappedList = new List<ProductEntity>();
                foreach (var item in response.Produto_Buscar) {
                    mappedList.Add(new ProductEntity() {
                        CodigoExterno = item.cd_produto,
                        NomeProduto = item.nm_produto
                    });
                }
                return mappedList;
            } catch (Exception e) {
                throw new InvalidCastException("Erro no mapeamento dos dados do legado (Produto_Buscar).", e);
            }
        }

    }
}