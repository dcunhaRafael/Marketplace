using System;

namespace InsuranceApi.Web.ViewModels {
    public class ContactViewModel {

        public string Nome { get; set; }
        public string Cpf { get; set; }
        public string Email { get; set; }
        public DateTime? DataNascimento { get; set; }
    }
}
