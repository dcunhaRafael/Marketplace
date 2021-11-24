using System;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public  class ParametroEntity {
        public int? AppParameterId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public int UserId { get; set; }
        public DateTime DateUtc { get; set; }
    }
}
