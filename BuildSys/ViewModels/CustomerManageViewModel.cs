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

            CustomerList = CustomerModel.getCustomerList();
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
                    filterCustomers();
                }
            }
        }

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

        public ICommand createQuoteCmd
        {
            get
            {
                return new RelayCommand((customerId) => navigateTo(new QuoteFormViewModel(parent, (int)customerId)), param => true);
            }
        }

        public ICommand viewCustomerQuoteCmd
        {
            get
            {
                return new RelayCommand((customerId) => viewCustomerQuotes((int)customerId), param => true);
            }
        }

        public ICommand restoreCustomerCmd
        {
            get
            {
                return new RelayCommand((customerId) => restoreCustomer((int)customerId), param => true);
            }
        }

        public void editCustomer (int customerId)
        {
            navigateTo(new CustomerFormViewModel(parent, customerId));
        }

        public void deleteCustomer(int customerId)
        {
            CustomerModel.deleteCustomer(customerId);

            CustomerModel deletedCustomer = CustomerList.Where(cust => cust.customerId == customerId).First();

            CustomerList.Remove(deletedCustomer);
            originalCustomerList.Remove(deletedCustomer);

            DeletedCustomerList.Add(deletedCustomer);
        }

        public void restoreCustomer(int customerId)
        {
            CustomerModel.restoreCustomer(customerId);
            CustomerModel restoredCustomer = DeletedCustomerList.Where(cust => cust.customerId == customerId).First();

            CustomerList.Add(restoredCustomer);
            originalCustomerList.Add(restoredCustomer);

            DeletedCustomerList.Remove(restoredCustomer);

            filterCustomers();
        }

        public void viewCustomerQuotes(int customerId)
        {
            navigateTo(new CustomerQuoteManageViewModel(parent, customerId));
        }

        public void filterCustomers ()
        {
            CustomerList = new ObservableCollection<CustomerModel>(originalCustomerList);
            Regex matchName = new Regex(@"^" + customerFilter + @".+", RegexOptions.IgnoreCase);

            if (customerFilter.Length > 0)
            {
                CustomerList.Where(cust => !matchName.IsMatch(cust.firstname) && !matchName.IsMatch(cust.surname) && !matchName.IsMatch(cust.companyName))
                    .ToList()
                    .All(i => CustomerList.Remove(i));
            }
        }
    }

}
