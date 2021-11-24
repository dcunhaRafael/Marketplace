using Domain.Payload;

namespace Integration.Interfaces.Legacy {
    public interface IRenewalApiService {
        PolicyRenovation SaveRenewalJudicial(PolicyRenovation model);
        ProposalTransmited TransmitProposal(PolicyRenovation model);
        PolicyIssued IssuePolicy(PolicyRenovation model);
        ProposalPrinted PrintProposal(PolicyRenovation model);
        ProposalPrinted PrintPolicy(PolicyRenovation model);
    }
}
