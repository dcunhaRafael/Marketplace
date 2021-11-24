namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class TomadorBuscarEntity {

        public int? IdPessoaCorretor { get; set; }
        public int? IdPessoaTomador { get; set; }
        public bool? Ativo { get; set; }
        public bool? EnderecoPadrao { get; set; }
        public bool? Situacao { get; set; }
        public bool? CcgFormalizado { get; set; }
        public bool? DominioTomador { get; set; }
        public bool? DominioCorretor { get; set; }
        public bool? ListaCorretor { get; set; }
        public string NomePessoa { get; set; }
        public string CpfCnpj { get; set; }
        //public int? IdUsuario { get; set; }
        public bool? ListarTodos { get; set; }

    }
}
