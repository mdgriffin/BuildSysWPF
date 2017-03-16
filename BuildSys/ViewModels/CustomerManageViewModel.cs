using BuildSys.Models;
using System;
using System.Collections.Generic;
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

            customers = CustomerModel.getCustomers();
        }

        public DataTable customers { get; set; }

        public ICommand editCustomer
        {
            get
            {
                return new RelayCommand(() => MessageBox.Show("Find the Row ID!"), () => true);
            }
        }
    }
}
