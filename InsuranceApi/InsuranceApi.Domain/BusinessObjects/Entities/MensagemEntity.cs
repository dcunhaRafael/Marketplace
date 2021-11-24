namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class MensagemEntity {
        public MensagemEntity(string mensagem) {
            Mensagem = mensagem;
        }

        public string Mensagem { get; }
    }
}
