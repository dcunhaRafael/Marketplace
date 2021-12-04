using Domain.Payload;
using System;
using System.Collections.Generic;

namespace Integration.Interfaces.Legacy {
    public interface ILegacyBrokerService {
        List<ComissionStatement> ListComissionStatement(int? statementNumber, DateTime? fromDate, DateTime? toDate, int? status, string brokerLegacyCode, string brokerSusepCode, int brokerUserId, LoggerComplement loggerComplement);
        List<ComissionStatementDetail> ListComissionStatementDetails(int statementNumber, string competency, string brokerLegacyCode, string brokerSusepCode, int brokerUserId, LoggerComplement loggerComplement);
        List<ComissionStatementType> ListComissionStatementTypes(int statementNumber, string competency, string brokerLegacyCode, string brokerSusepCode, int brokerUserId, LoggerComplement loggerComplement);
        List<ComissionStatementBusiness> ListComissionStatementBusiness(int statementNumber, string competency, string brokerLegacyCode, string brokerSusepCode, int brokerUserId, LoggerComplement loggerComplement);
        List<ComissionStatementEntry> ListComissionStatementEntries(int statementNumber, string competency, string brokerLegacyCode, string brokerSusepCode, int brokerUserId, LoggerComplement loggerComplement);
    }
}
