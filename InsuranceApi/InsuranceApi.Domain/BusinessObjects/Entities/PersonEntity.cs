using InsuranceApi.Domain.BusinessObjects.Enumerators;
using System;
using System.Collections.Generic;
using System.Text;

namespace InsuranceApi.Domain.BusinessObjects.Entities {
    public class PersonEntity {
        public long? PersonId { get; set; }
        public TipoPessoaEnum? PersonTypeId { get; set; }
        public string Name { get; set; }
        public string Document { get; set; }
        public string FormattedDocument { get; set; }
        public string DocumentCity { get; set; }
        public string FantasyName { get; set; }
        public string Activity { get; set; }
        public DateTime? BornDate { get; set; }
        public string Observation { get; set; }
        public int? GenderId { get; set; }

        public StatusTypeEnum? PersonStatusTypeId { get; set; }
        public int UserId { get; set; }
        public DateTime DateUtc { get; set; }
    }
}
