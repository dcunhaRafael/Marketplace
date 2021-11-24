using System.Runtime.Serialization;

/// <summary>
/// Contratos para os casos onde os serviços não são devolvidos na estrutura padrão
/// ai é devolvida string de dados e a aplicação se vira com o parse
/// </summary>
namespace Domain.Util.HttpClients {
    public class RawRequest : BaseRequest {
        public string RequestUri { get; set; }
    }
}
