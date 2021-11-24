using Domain.Payload;
using System.Collections.Generic;

namespace Presentation.Web.Models.Configuration {
    public class OccurrencesViewModel {
        public OccurrencesViewModel() {
            Filters = new OccurrenceTypeFilters();
            Occurrences = new List<OccurrenceType>();
            ProductList = new List<Product>();
            CoverageList = new List<Coverage>();
            ProfileList = new List<Profile>();
            CurrentItem = new OccurrenceType();
            LiberationUsers = new List<User>();
            Documents = new List<DocumentType>();
        }

        public OccurrenceTypeFilters Filters { get; set; }
        public IList<OccurrenceType> Occurrences { get; set; }

        public IList<Product> ProductList { get; set; }
        public IList<Coverage> CoverageList { get; set; }
        public IList<Profile> ProfileList { get; set; }
        public IList<DocumentType> Documents { get; set; }
        public IList<User> LiberationUsers { get; set; }

        public OccurrenceType CurrentItem { get; set; }

        public bool IsEditable { get; set; }

        public int CopyToProductId { get; set; }
        public int CopyToCoverageId { get; set; }
       
    }
}
