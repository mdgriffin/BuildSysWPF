using BuildSys.Models;
using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace BuildSys.ViewModels
{
    
    public class CustomerFormViewModel: BaseViewModel
    {
        
        // Default constructor for registering a new customer
        public CustomerFormViewModel(BaseViewModel parent)
        {
            customer = new CustomerModel();
            customer.PropertyChanged += onCustomerPropertyChanged;

            // The business inputs are not visible by default
            showBusinessInputs = "Collapsed";

            btnText = "Register Customer";
            this.parent = parent;
        }

        // Default constructor for updating the customer
        public CustomerFormViewModel(BaseViewModel parent, int customerId)
        {
            this.parent = parent;

            // Get the customer being updated
            customer = CustomerModel.getCustomer(customerId);

            // Register another event handler for checking when the customer property is changed
            customer.PropertyChanged += onCustomerPropertyChanged;

            // Set the business inputs to visible or collapsed based on account type
            if (customer.accountType == 'B')
            {
                showBusinessInputs = "Visible";
            } else
            {
                showBusinessInputs = "Collapsed";
            }

            // Since we are updating, set the correct button text
            btnText = "Update Customer";
        }

        // Event handler called when a propert is changed
        // Used to show and hide the business inputs
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

        // Initializing the public variables accessed in the view
        public CustomerModel customer { get; set; }
        public String showBusinessInputs { get; set; }
        public String btnText { get; set; }

        // Binding a command to handle clicks on the save customer button
        public ICommand saveCustomerCommand
        {
            get
            {
                return new RelayCommand(param => saveCustomer(), param => canSaveCustomer());
            }
        }

        // Saves or updates the customer
        public void saveCustomer ()
        {
            // Forces all properties to be validated
            customer.validateAllProps();

            // Check if the form has errors
            if (!customer.HasErrors)
            {
                // Check are we updating or registering
                if (btnText.Equals("Update Customer"))
                {
                    // Update the Customer's record
                    customer.update();

                    MessageBox.Show("Customer Updated Successfully");

                    // Navigate to the same view model, in effect resetting the form
                    navigateTo(new CustomerManageViewModel(parent));
                }
                else
                {
                    // Insert this customer model
                    customer.insertCustomer();

                    MessageBox.Show("Sucessfully Register a new customer");

                    // Reset the form
                    parent.ViewModel = new CustomerFormViewModel(parent);
                }
            }
        }

        // Method used with the saveCustomerCommand to enable and diabled the button
        public Boolean canSaveCustomer ()
        {
            return !customer.HasErrors;
        }

    }
}