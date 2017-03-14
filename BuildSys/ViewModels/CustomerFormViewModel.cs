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
            this.parent = parent;
        }

        public CustomerModel customer { get; set; }

        public ICommand saveCustomerCommand
        {
            get
            {
                return new RelayCommand(() => saveCustomer(), () => canSaveCustomer());
            }
        }

        public void saveCustomer ()
        {
            MessageBox.Show("Now Save the Customer", "Information");

            // Reset the form
            parent.ViewModel = new CustomerFormViewModel(parent);
        }

        public Boolean canSaveCustomer ()
        {
            // TODO: Only enable button if all Customer Validates
            return true;
        }

    }
}