using Domain.Payload;
using System.Collections.Generic;

namespace Presentation.Web.Services.Proxy {
    public interface ICommonService {
        IList<Product> ListProducts();
        IList<Coverage> ListCoverages(int productId);
        IList<Profile> ListProfiles();
        IList<DocumentType> ListDocumentTypes();
        IList<User> ListUsers(int profileId);
        IList<Broker> ListBrokers(string name);
        IList<Taker> ListTakers(int brokerId, string name);
        IList<Insured> ListInsureds(string name);
        IList<RefusalReason> ListRefusalReasons();
        IList<OccurrenceType> ListOccurrenceTypes();
        void AddAudit(Audit audit);
        IList<UpdateIndex> ListUpdateIndexes();
        IList<ComissionStatementStatus> ListComissionStatementStatus();
    }
}
