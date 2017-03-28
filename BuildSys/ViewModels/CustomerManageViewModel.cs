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
    public class CustomerManageViewModel: BaseViewModel
    {
        MainViewModel parent;

        public CustomerManageViewModel (MainViewModel parent)
        {
            this.parent = parent;

            CustomerList = CustomerModel.getCustomerList();

            // Keep a copy of the CustomerList so that we can restore the list after filtering
            originalCustomerList = new ObservableCollection<CustomerModel>(CustomerList);
        }

        private String _customerFilter;
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
                    filterCustomers();
                }
            }
        }

        private ObservableCollection<CustomerModel> originalCustomerList;

        private ObservableCollection<CustomerModel> _customerList = new ObservableCollection<CustomerModel>();
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

        public ICommand editCustomerCmd
        {
            get
            {
                return new RelayCommand((customerId) => editCustomer((int) customerId), param => true);
            }
        }

        public ICommand deleteCustomerCmd
        {
            get
            {
                return new RelayCommand((customerId) => deleteCustomer((int)customerId), param => true);
            }
        }

        public void editCustomer (int customerId)
        {
            parent.ViewModel = new CustomerFormViewModel(parent, customerId);
        }

        public void deleteCustomer(int customerId)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("Are you sure?", "Delete Confirmation", System.Windows.MessageBoxButton.YesNo);
            
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                CustomerModel.deleteCustomer(customerId);
                MessageBox.Show("Customer Deleted");
                CustomerList.Where(cust => cust.customerId == customerId).ToList().All(i => CustomerList.Remove(i));
                originalCustomerList.Where(cust => cust.customerId == customerId).ToList().All(i => CustomerList.Remove(i));
            }  
        }

        public void filterCustomers ()
        {
            CustomerList = new ObservableCollection<CustomerModel>(originalCustomerList);
            Regex matchName = new Regex(@"^" + customerFilter + @".+", RegexOptions.IgnoreCase);

            if (customerFilter.Length > 0)
            {
                CustomerList.Where(cust => !matchName.IsMatch(cust.firstname) && !matchName.IsMatch(cust.surname))
                    .ToList()
                    .All(i => CustomerList.Remove(i));
            }
        }

        public ICommand createQuoteCmd
        {
            get
            {
                return new RelayCommand((customerId) => parent.ViewModel = new QuoteFormViewModel(parent, (int) customerId), param => true);
            }
        }

    }

}
