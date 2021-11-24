using System;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class ContatoEntity {

        public string Nome { get; set; }
        public long? Cpf { get; set; }
        public string Email { get; set; }
        public DateTime? DataNascimento { get; set; }

        // Marketplace
        public int? Id { get; set; }
    }
}
