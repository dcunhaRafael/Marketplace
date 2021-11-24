using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class CoberturaAgravoEntity {

        public int CoverageId { get; set; }
        public bool HasGrievance { get; set; }
        public bool IsRequired { get; set; }
       // public GrievanceTypeEnum? GrievanceType { get; set; }
        public decimal? DefaultValue { get; set; }
        public decimal? MinValue { get; set; }
        public decimal? MaxValue { get; set; }
        public bool IsLocked { get; set; }
        public RecordStatusEnum Status { get; set; }
        public int UserId { get; set; }
        public DateTime DateUtc { get; set; }
    }
}
