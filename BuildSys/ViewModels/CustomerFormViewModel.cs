using BuildSys.Models;

namespace BuildSys.ViewModels
{
    
    public class CustomerFormViewModel: BaseViewModel
    {
        /// <summary>
        /// Initializes a new instance of the CustomerFormViewModel class.
        /// </summary>
        public CustomerFormViewModel()
        {
            customer = new CustomerModel();
        }

        public CustomerModel customer { get; set; }
    }
}