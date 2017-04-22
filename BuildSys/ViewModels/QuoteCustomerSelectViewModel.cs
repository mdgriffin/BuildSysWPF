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

            CustomerList = CustomerModel.getCustomerList();

            // Keep a copy of the CustomerList so that we can restore the list after filtering
            originalCustomerList = new ObservableCollection<CustomerModel>(CustomerList);
        }

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

        private ObservableCollection<CustomerModel> originalCustomerList;

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

        public void filterCustomers()
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

        public ICommand createQuoteCmd
        {
            get
            {
                return new RelayCommand((customerId) => navigateTo(new QuoteFormViewModel(parent, (int)customerId)), param => true);
            }
        }
    }
}
