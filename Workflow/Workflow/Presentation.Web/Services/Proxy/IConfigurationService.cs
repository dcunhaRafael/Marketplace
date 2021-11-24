using Domain.Payload;
using System.Collections.Generic;

namespace Presentation.Web.Services.Proxy {
    public interface IConfigurationService {
        IList<OccurrenceType> ListOccurrenceTypes(OccurrenceTypeFilters filters);
        OccurrenceType GetOccurrenceType(int occurrenceTypeId);
        int SaveOccurrenceType(OccurrenceType item);
        void DeleteOccurrenceType(int occurrenceTypeId, int loggedUserId);
        int CopyOccurrenceType(int occurrenceTypeId, int productId, int coverageId, int loggedUserId);


        IList<PolicyBatchConfiguration> ListPolicyBatchConfiguration(PolicyBatchConfigurationFilters filters);
        PolicyBatchConfiguration GetPolicyBatchConfiguration(int policyBatchConfigurationId);
        int SavePolicyBatchConfiguration(PolicyBatchConfiguration item);
        void DeletePolicyBatchConfiguration(int policyBatchConfigurationId, int loggedUserId);
        IList<PolicyBatchConfigurationMail> ListPolicyBatchConfigurationMails(int policyBatchConfigurationId);
        PolicyBatchConfigurationMail GetPolicyBatchConfigurationMail(int policyBatchConfigurationMailId);
        int SavePolicyBatchConfigurationMail(PolicyBatchConfigurationMail item);
        void DeletePolicyBatchConfigurationMail(int policyBatchConfigurationMailId, int loggedUserId);
    }
}
