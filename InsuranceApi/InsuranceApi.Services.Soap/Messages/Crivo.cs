namespace InsuranceApi.Services.Soap.Messages {
    public class CrivoRequest {
        public string sUser { get; set; }
        public string sPassword { get; set; }
        public string sPolitica { get; set; }

        public string sParametros { get; set; }
    }

    public class CrivoResponse {
        public decimal qualidade_empresa { get; set; }
        public decimal pontuacao { get; set; }
        public decimal limite_de_credito { get; set; }
        public decimal condicao_rating { get; set; }
        public decimal mv_taxa { get; set; }
    }
}
