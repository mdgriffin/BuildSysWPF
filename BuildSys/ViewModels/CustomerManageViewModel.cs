using BuildSys.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
