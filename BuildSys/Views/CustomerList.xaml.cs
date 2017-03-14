using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using BuildSys.Models;

namespace BuildSys.Views
{
    /// <summary>
    /// Interaction logic for CustomerList.xaml
    /// </summary>
    public partial class CustomerList : UserControl
    {
        public CustomerList()
        {
            InitializeComponent();

            Customers = CustomerModel.getCustomers();

            DataContext = this;
        }

        public DataTable Customers { get; }
    }
}
