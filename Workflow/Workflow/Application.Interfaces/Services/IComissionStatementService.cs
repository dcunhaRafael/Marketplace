using Domain.Payload;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Interfaces.Services {
    public interface IComissionStatementService {
        Task<IList<ComissionStatementStatus>> ListAsync();
        IList<ComissionStatement> ListComissionStatement(int? statementNumber, DateTime? fromDate, DateTime? toDate, int? status, string brokerLegacyCode, string brokerSusepCode, int brokerUserId, int LoggedUserId);
        Task<ComissionStatementCover> GetComissionStatementCover(int statementNumber, string competency, string brokerLegacyCode, string brokerSusepCode, int brokerUserId, int LoggedUserId);
        IList<ComissionStatementEntry> ListComissionStatementEntries(int statementNumber, string competency, string brokerLegacyCode, string brokerSusepCode, int brokerUserId, int LoggedUserId);
        Task<ExportedFile> ExportComissionStatement(string templateFile, int statementNumber, string competency, string brokerName, long brokerCnpjNumber, string brokerLegacyCode, string brokerSusepCode, int brokerUserId, int loggedUserId);
    }
}
