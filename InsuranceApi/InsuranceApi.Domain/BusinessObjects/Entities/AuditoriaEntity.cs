using System;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class AuditoriaEntity {
        public int Id { get; set; }
        public DateTime DataHora { get; set; }
        public int TipoUsuarioId { get; set; }
        public int UsuarioId { get; set; }
        public string UrlChamada { get; set; }
        public string Funcionalidade { get; set; }
        public string TipoAcao { get; set; }
        public string IP { get; set; }
        public string Navegador { get; set; }
        public string SistemaOperacional { get; set; }
        public string Nivel { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }

        // Auxiliar para fazer o de-para entre Ebix e Avivatec
        public string UsuarioNome { get; set; }
    }
}
