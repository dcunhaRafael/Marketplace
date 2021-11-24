using InsuranceApi.Web.Models;

namespace InsuranceApi.Web.ViewModels {
    public class InsertTakerViewModel {

        public InsertTakerViewModel() {

            Contact = new ContactViewModel();
        }
        public string CpfCnpj { get; set; }
        public ContactViewModel Contact { get; set; }
    }
}
