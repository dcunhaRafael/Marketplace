namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class AssinaturaImpressoEntity {
        public int IdImpresso { get; set; }
        public string NomeArquivo { get; set; }
        public string ConteudoBase64 { get; set; }
    }
}
