using Domain.Payload;
using System;

namespace Integration.BMG.Mappers {
    public static class RenewalApiMapper {

        public static PolicyRenovation Map(ProposalAdded response) {
            try {
                var mappedList = new PolicyRenovation() {
                    ProposalId = response.Id,
                    NewPremiumValue = response.ValorPremioTarifario,
                    NewProposalStatusId = response.CodigoStatus
                };

                return mappedList;
            } catch (Exception e) {
                throw new InvalidCastException(e.Message, e);
            }
        }

    }
}