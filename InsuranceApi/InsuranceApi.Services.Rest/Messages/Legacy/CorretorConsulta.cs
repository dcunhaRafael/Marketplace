using System.Collections.Generic;

namespace InsuranceApi.Services.Rest.Messages.Legacy {
    public class CorretorConsultaRequest {
        public string nm_pessoa { get; set; }
        public decimal? nr_cnpj_cpf { get; set; }
        public string cd_susep_corretor { get; set; }
        public string dv_ativo { get; set; }
        public int id_usuario { get; set; }
        public string cd_susep { get; set; }
    }

    public class CorretorConsultaResponse {
        public int cd_retorno { get; set; }
        public string nm_retorno { get; set; }
        public List<CorretorConsultaItem> Consulta_Corretor { get; set; }
    }

    public class CorretorConsultaItem {
        public string cd_tp_pessoa { get; set; }
        public string id_pessoa_corretor { get; set; }
        public string nm_pessoa { get; set; }
        public string nr_cnpj_cpf { get; set; }
        public string cd_susep_corretor { get; set; }
        public string id_usuario_corretor { get; set; }
        public string dv_ativo { get; set; }
        public string id_usuario { get; set; }
        public string cd_susep { get; set; }
    }
}


