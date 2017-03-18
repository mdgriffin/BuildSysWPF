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

            CustomerList = CustomerModel.getCustomers();

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

        public ICommand editCustomer
        {
            get
            {
                return new RelayCommand(() => MessageBox.Show("Find the Row ID!"), () => true);
            }
        }

    }

}
