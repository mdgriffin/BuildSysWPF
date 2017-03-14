using BuildSys.Models;
using System;
using System.Windows;
using System.Windows.Input;

namespace BuildSys.ViewModels
{
    
    public class CustomerFormViewModel: BaseViewModel
    {
       MainViewModel parent;
        
        /// <summary>
        /// Initializes a new instance of the CustomerFormViewModel class.
        /// </summary>
        public CustomerFormViewModel(MainViewModel parent)
        {
            customer = new CustomerModel();
            showBusinessInputs = "Collapsed";
            this.parent = parent;
            
        }

        public CustomerModel customer { get; set; }
        public String showBusinessInputs { get; set; }

        public ICommand saveCustomerCommand
        {
            get
            {
                return new RelayCommand(() => saveCustomer(), () => canSaveCustomer());
            }
        }

        public void saveCustomer ()
        {
            if (!customer.HasErrors)
            {
                customer.insertCustomer();
                // Reset the form
                parent.ViewModel = new CustomerFormViewModel(parent);
            } else
            {

            }
        }

        public Boolean canSaveCustomer ()
        {
            // TODO: Only enable button if all Customer Validates
            return true;
        }

    }
}