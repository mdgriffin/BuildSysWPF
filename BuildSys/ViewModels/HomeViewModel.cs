using BuildSys.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BuildSys.ViewModels
{
    public class HomeViewModel: BaseViewModel
    {
        public HomeViewModel (BaseViewModel parent)
        {
            this.parent = parent;

            numCustomers = CustomerModel.getNumCustomers();
            numMaterials = MaterialModel.getNumMaterials();
            numQuotes = QuoteModel.getNumQuotes();
        }

        public int numCustomers { get; set; }
        public int numMaterials { get; set; }
        public int numQuotes { get; set; }

        public ICommand regCustomerCmd
        {
            get
            {
                return new RelayCommand(param => navigateTo(new CustomerFormViewModel(this)), param => true);
            }
        }

        public ICommand createMaterialCmd
        {
            get
            {
                return new RelayCommand(param => navigateTo(new MaterialFormViewModel(this)), param => true);
            }
        }

        public ICommand createQuoteCmd
        {
            get
            {
                return new RelayCommand(param => navigateTo(new QuoteCustomerSelectViewModel(this)), param => true);
            }
        }

    }
}
