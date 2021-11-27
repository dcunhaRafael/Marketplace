using Domain.Payload;
using System;
using System.Collections.Generic;

namespace Presentation.Web.Models.UpdateIS {
    public class UpdateISViewModel {
        public UpdateISViewModel() {
            Results = new List<ExcelItem>();
        }

        public IList<ExcelItem> Results { get; set; }
    }

    public class ExcelItem {
        public string SheetName { get; set; }
        public string TakerName{ get; set; }
        public string InsuredName { get; set; }
        public long PolicyNumber { get; set; }
        public DateTime StartOfTerm { get; set; }
        public DateTime EndOfTerm { get; set; }
        public decimal InsuredAmount { get; set; }
        public decimal? UpdatedInsuredAmount { get; set; }
    }
}
