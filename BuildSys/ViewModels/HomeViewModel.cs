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

            // Set the public properties for the view
            numCustomers = CustomerModel.getNumCustomers();
            numMaterials = MaterialModel.getNumMaterials();
            numQuotes = QuoteModel.getNumQuotes();
        }

        // The public properties accessible from the view
        public int numCustomers { get; set; }
        public int numMaterials { get; set; }
        public int numQuotes { get; set; }

        // Command for reg customer button
        public ICommand regCustomerCmd
        {
            get
            {
                return new RelayCommand(param => navigateTo(new CustomerFormViewModel(this)), param => true);
            }
        }

        // Command for create material button
        public ICommand createMaterialCmd
        {
            get
            {
                return new RelayCommand(param => navigateTo(new MaterialFormViewModel(this)), param => true);
            }
        }

        // Command for create quote button
        public ICommand createQuoteCmd
        {
            get
            {
                return new RelayCommand(param => navigateTo(new QuoteCustomerSelectViewModel(this)), param => true);
            }
        }

    }
}
