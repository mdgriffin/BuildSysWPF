using BuildSys.Models;
using System;
using System.ComponentModel;
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
            customer.PropertyChanged += onCustomerPropertyChanged;

            showBusinessInputs = "Collapsed";

            btnText = "Register Customer";
            this.parent = parent;
        }

        public CustomerFormViewModel(MainViewModel parent, int customerId)
        {
            customer = CustomerModel.getCustomer(customerId);
            customer.PropertyChanged += onCustomerPropertyChanged;

            if (customer.accountType == 'B')
            {
                showBusinessInputs = "Visible";
            } else
            {
                showBusinessInputs = "Collapsed";
            }

            btnText = "Update Customer";
            this.parent = parent;
        }

        public void onCustomerPropertyChanged(object sender, PropertyChangedEventArgs e)
        {

            if (e.PropertyName.Equals("accountType"))
            {
                if (customer.accountType == 'B')
                {
                    showBusinessInputs = "Visible";
                } else
                {
                    showBusinessInputs = "Collapsed";
                }
                NotifyPropertyChanged("showBusinessInputs");
            }
        }

        public CustomerModel customer { get; set; }
        public String showBusinessInputs { get; set; }
        public String btnText { get; set; }

        public ICommand saveCustomerCommand
        {
            get
            {
                return new RelayCommand(param => saveCustomer(), param => canSaveCustomer());
            }
        }

        public void saveCustomer ()
        {
            customer.validateAllProps();

            if (!customer.HasErrors)
            {
                if (btnText.Equals("Update Customer"))
                {
                    // Update the Customer's record
                    customer.update();

                    MessageBox.Show("Customer Updated Successfully");

                    parent.ViewModel = new CustomerManageViewModel(parent);
                }
                else
                {
                    customer.insertCustomer();

                    MessageBox.Show("Sucessfully Register a new customer");

                    // Reset the form
                    parent.ViewModel = new CustomerFormViewModel(parent);
                }
            }
        }

        public Boolean canSaveCustomer ()
        {
            return !customer.HasErrors;
        }

    }
}