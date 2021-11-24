using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class SalesChannelEntity {

        public int? SalesChannelId { get; set; }
        public string Name { get; set; }
        public CanalDeVendaEnum? Status { get; set; }
        public DateTime? StartValidaty { get; set; }
        public DateTime? EndValidaty { get; set; }
        public bool IsBiddingDefault { get; set; }
        public bool IsByRegion { get; set; }
        public int UserId { get; set; }
        public DateTime DateUtc { get; set; }
    }
}
