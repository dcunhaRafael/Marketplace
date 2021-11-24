using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class TakerScoreEntity {


        public int? TakerScoreId { get; set; }
        public int? TakerId { get; set; }
        public decimal? CompanyQuality { get; set; }
        public decimal? Score { get; set; }
        public decimal? CreditLimit { get; set; }
        public string Rating { get; set; }
        public decimal? Tax { get; set; }
        public DateTime DateUtc { get; set; }
    }
}
