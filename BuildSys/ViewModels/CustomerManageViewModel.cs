using BuildSys.Models;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
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
            }  
        }

        public void filterCustomers ()
        {
            // TODO: Need to have a copy of the whole area
            //var myResult = CustomerList.Where(cust => cust.firstname.Equals(customerFilter));
            //CustomerList.Where(cust => !cust.firstname).ToList().All(i => CustomerList.Remove(i));
        }

    }

}
