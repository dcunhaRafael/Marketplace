using Domain.Enumerators;
using System;

namespace Domain.Entities {
    public class BaseEntity {
        public RecordStatusEnum? Status { get; set; }
        public int? InclusionUserId { get; set; }
        public DateTime? InclusionDate { get; set; }
        public User InclusionUser { get; set; }
        public int? LastChangeUserId { get; set; }
        public DateTime? LastChangeDate { get; set; }
        public User LastChangeUser { get; set; }
    }
}
