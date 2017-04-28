using BuildSys.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BuildSys.ViewModels
{
    public class QuoteCustomerSelectViewModel : BaseViewModel
    {

        public QuoteCustomerSelectViewModel (BaseViewModel parent)
        {
            this.parent = parent;

            // Get the list of active customers
            CustomerList = CustomerModel.getCustomerList();

            // Keep a copy of the CustomerList so that we can restore the list after filtering
            originalCustomerList = new ObservableCollection<CustomerModel>(CustomerList);
        }

        // The filter input string
        private String _customerFilter;
        public String customerFilter
        {
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

        // Kept as a copy of the customer list
        private ObservableCollection<CustomerModel> originalCustomerList;

        private ObservableCollection<CustomerModel> _customerList = new ObservableCollection<CustomerModel>();
        public ObservableCollection<CustomerModel> CustomerList
        {
            get { return _customerList ?? (_customerList = new ObservableCollection<CustomerModel>()); }
            set
            {
                if (value == null) return;
                _customerList = value;
                // Notify so that view is updated
                NotifyPropertyChanged("CustomerList");
            }
        }

        // Filter the customers based on user input
        public void filterCustomers()
        {
            // Get the original list so the whole list is filtered
            CustomerList = new ObservableCollection<CustomerModel>(originalCustomerList);
            Regex matchName = new Regex(@"^" + customerFilter + @".+", RegexOptions.IgnoreCase);

            if (customerFilter.Length > 0)
            {
                // Filter the customer list, removing any customers that do not match
                CustomerList.Where(cust => !matchName.IsMatch(cust.firstname) && !matchName.IsMatch(cust.surname) && !matchName.IsMatch(cust.companyName))
                    .ToList()
                    .All(i => CustomerList.Remove(i));
            }
        }

        // Called when create quote button is clicked
        public ICommand createQuoteCmd
        {
            get
            {
                return new RelayCommand((customerId) => navigateTo(new QuoteFormViewModel(parent, (int)customerId)), param => true);
            }
        }
    }
}
