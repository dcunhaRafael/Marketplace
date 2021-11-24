using System;
using System.Collections.Generic;

namespace Domain.Payload {
    public class Person {

        public Person() {
            PersonType = new FixedDomain();
            Gender = new FixedDomain();
            MaritalStatus = new FixedDomain();
            Addresses = new List<PersonAddress>();
            Contacts = new List<PersonContact>();
        }

        public int Id { get; set; }
        public FixedDomain PersonType { get; set; }
        public long? CpfCnpjNumber { get; set; }
        public string Name { get; set; }
        public string RgNumber { get; set; }
        public string IssuingBody { get; set; }
        public FixedDomain Gender { get; set; }
        public DateTime? BornDate { get; set; }
        public FixedDomain MaritalStatus { get; set; }
        public long? SpouseCpfNumber { get; set; }
        public string SpouseName { get; set; }
        public string IeNumber { get; set; }
        public string ImNumber { get; set; }
        public DateTime? OpeningDate { get; set; }

        // Childs
        public IList<PersonAddress> Addresses { get; set; }
        public IList<PersonContact> Contacts { get; set; }
    }
}
