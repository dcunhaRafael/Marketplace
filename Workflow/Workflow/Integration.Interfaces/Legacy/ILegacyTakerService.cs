using Domain.Entities;
using Domain.Payload;
using System.Collections.Generic;

namespace Integration.Interfaces.Legacy {
    public interface ILegacyTakerService {

        TakerData Get(string takerLegacyCode, int brokerUserId, LoggerComplement loggerComplement);
        IList<TakerData> List(string takerLegacyCode, string name, long? cpfCnpjNumber, int brokerUserId, bool isActive, bool defaultAddress, bool listBroker, bool listTaker, LoggerComplement loggerComplement);
        TakerCreditLimit GetCreditLimit(string takerId, LoggerComplement loggerComplement);
    }
}
