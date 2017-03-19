using BuildSys.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            //parent.ViewModel = new CustomerFormViewModel(parent, customerId);
            MessageBox.Show("Delete Customer " + customerId);
        }

    }

}
