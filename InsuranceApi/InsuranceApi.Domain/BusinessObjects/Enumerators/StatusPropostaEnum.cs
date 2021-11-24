using InsuranceApi.Domain.Common.Extension;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace InsuranceApi.Domain.BusinessObjects.Enumerators {
    public enum StatusPropostaEnum {
        //[Description("Iniciada")]
        //Iniciada = 0,

        [Description("Em Análise")]
        EmAnalise = 613,                //BTG:1,
        [Description("Em Negociação")]
        EmNegociacao = 601,             //BTG:2,            
        [Description("Em Subscrição")]
        EmSubscricao = 611,             //BTG:3,            
        [Description("Cancelada")]
        Cancelada = 603,                //BTG:4,
        [Description("Emitida")]
        Emitida = 602,                  //BTG:5,                 
        [Description("Recusada")]
        Recusada = 604,                //BTG:6,   
        [Description("Recusada com Devolução")]
        RecusadaDevolucao = 605,
        [Description("Pré-Aprovada")]
        PreAprovada = 606,
        [Description("Aprovada Pelo Subscritor")]
        AprovadaPeloSubscritor = 612,   //BTG:7,  
    }

    public static class StatusPropostaEnumUtil {

        /// <summary>
        /// Retorna lista com todos os itens do enum
        /// </summary>
        public static IEnumerable<KeyValuePair<string, string>> GetList() {
            return from status in System.Enum.GetValues(typeof(StatusPropostaEnum)).Cast<StatusPropostaEnum>()
                   select new KeyValuePair<string, string>(((int)status).ToString(),
                                                            status.GetEnumDescription());
        }

    }
}
