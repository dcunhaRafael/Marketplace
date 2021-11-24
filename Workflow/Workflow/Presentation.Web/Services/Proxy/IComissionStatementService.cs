﻿using Domain.Payload;
using System;
using System.Collections.Generic;

namespace Presentation.Web.Services.Proxy {
    public interface IComissionStatementService {
        IList<ComissionStatement> ListComissionStatement(int? statementNumber, DateTime? fromDate, DateTime? toDate, int? status, Broker broker, int loggedUserId);
        IList<ComissionStatementDetail> ListComissionStatementDetais(int statementNumber, string competency, Broker broker, int loggedUserId);
        IList<ComissionStatementType> ListComissionStatementTypes(int statementNumber, string competency, Broker broker, int loggedUserId);
        IList<ComissionStatementBusiness> ListComissionStatementBusiness(int statementNumber, string competency, Broker broker, int loggedUserId);
        IList<ComissionStatementEntry> ListComissionStatementEntries(int statementNumber, string competency, Broker broker, int loggedUserId);
    }
}
