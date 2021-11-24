using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class DocumentoInserirEntity {
        public TipoDominioDocumentoEnum IdDominio { get; set; }
        public TipoDocumentoEnum IdTipoDocumento { get; set; }
        public int IdDomTpDocumento { get; set; }
        public string ChaveValores { get; set; }
        public int IdUsuario { get; set; }
        public string Observacoes { get; set; }
        public string NomeArquivo { get; set; }
        public string ConteudoBase64 { get; set; }
    }
}
