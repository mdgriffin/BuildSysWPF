using BuildSys.Models;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace BuildSys.ViewModels
{
    public class CustomerManageViewModel : BaseViewModel
    {

        public CustomerManageViewModel(BaseViewModel parent)
        {
            this.parent = parent;

            // Gets a list of all active customers
            CustomerList = CustomerModel.getCustomerList();
            // Gets a list of all deleted customers
            DeletedCustomerList = CustomerModel.getDeletedCustomerList();

            // Keep a copy of the CustomerList so that we can restore the list after filtering
            originalCustomerList = new ObservableCollection<CustomerModel>(CustomerList);
        }

        private String _customerFilter = "";
        public String customerFilter {
            get
            {
                return _customerFilter;
            }
            set
            {
                if (value != _customerFilter)
                {
                    _customerFilter = value;
                    // When changes filter customers
                    filterCustomers();
                }
            }
        }

        // Public variables accessible in the view
        public ObservableCollection<CustomerModel> DeletedCustomerList { get; set; }

        private ObservableCollection<CustomerModel> originalCustomerList;

        private ObservableCollection<CustomerModel> _customerList;
        public ObservableCollection<CustomerModel> CustomerList
        {
            get { return _customerList ?? (_customerList = new ObservableCollection<CustomerModel>()); }
            set
            {
                if (value == null) return;
                _customerList = value;
                NotifyPropertyChanged("CustomerList");
            }
        }

        // Command called when the edit customer button is clicked
        public ICommand editCustomerCmd
        {
            get
            {
                return new RelayCommand((customerId) => editCustomer((int) customerId), param => true);
            }
        }

        // Command called when delete customer button is clicked
        public ICommand deleteCustomerCmd
        {
            get
            {
                return new RelayCommand((customerId) => deleteCustomer((int)customerId), param => true);
            }
        }

        // Handles the create quote button
        public ICommand createQuoteCmd
        {
            get
            {
                return new RelayCommand((customerId) => navigateTo(new QuoteFormViewModel(parent, (int)customerId)), param => true);
            }
        }

        // handles the view customers button click
        public ICommand viewCustomerQuoteCmd
        {
            get
            {
                return new RelayCommand((customerId) => viewCustomerQuotes((int)customerId), param => true);
            }
        }

        // Called when the restore customer button is clicked
        public ICommand restoreCustomerCmd
        {
            get
            {
                return new RelayCommand((customerId) => restoreCustomer((int)customerId), param => true);
            }
        }

        // Nagiviate to the edit customer view
        public void editCustomer (int customerId)
        {
            navigateTo(new CustomerFormViewModel(parent, customerId));
        }

        // Deletes the specified customer
        public void deleteCustomer(int customerId)
        {
            CustomerModel.deleteCustomer(customerId);

            CustomerModel deletedCustomer = CustomerList.Where(cust => cust.customerId == customerId).First();

            // Remove from the specified customer from the customer list
            CustomerList.Remove(deletedCustomer);
            // Also remove from the original customer list
            originalCustomerList.Remove(deletedCustomer);

            // Add to the deleted customer list
            DeletedCustomerList.Add(deletedCustomer);
        }

        // Restores a customer back 
        public void restoreCustomer(int customerId)
        {
            // Changes the customer status back to active
            CustomerModel.restoreCustomer(customerId);
            // Get the model of the restored customer
            CustomerModel restoredCustomer = DeletedCustomerList.Where(cust => cust.customerId == customerId).First();

            // Add the model to the active customer list
            CustomerList.Add(restoredCustomer);
            originalCustomerList.Add(restoredCustomer);

            // Remove from the deleted customer list
            DeletedCustomerList.Remove(restoredCustomer);

            // Re-filter the customers
            filterCustomers();
        }

        // navigate to the specifies customer
        public void viewCustomerQuotes(int customerId)
        {
            navigateTo(new CustomerQuoteManageViewModel(parent, customerId));
        }

        // Filters the customer list based on the filter text input
        public void filterCustomers ()
        {
            // Start the filtering from the full customer list
            CustomerList = new ObservableCollection<CustomerModel>(originalCustomerList);
            // Create regex to match the customer
            Regex matchName = new Regex(@"^" + customerFilter + @".+", RegexOptions.IgnoreCase);

            // Only filter is text has been entered
            if (customerFilter.Length > 0)
            {
                // Uses a lambda to filter down customers, check if firstname, surname or company name matches
                CustomerList.Where(cust => !matchName.IsMatch(cust.firstname) && !matchName.IsMatch(cust.surname) && !matchName.IsMatch(cust.companyName))
                    .ToList()
                    .All(i => CustomerList.Remove(i));
            }
        }
    }

}
