using BuildSys.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;

namespace BuildSys.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        public MainViewModel()
        {
            ViewModel = new CustomerManageViewModel(this);
        }

        private BaseViewModel _ViewModel;
        public BaseViewModel ViewModel
        {
            get
            {
                return _ViewModel;
            }
            set
            {
                _ViewModel = value;
                NotifyPropertyChanged("ViewModel");
            }
        }

        public ICommand goToManageCustomer
        {
            get
            {
                return new RelayCommand(param => ViewModel = new CustomerManageViewModel(this), param => true);
            }
        }

        public ICommand goToRegCustomer
        {
            get
            {
                return new RelayCommand(param => ViewModel = new CustomerFormViewModel(this), param => true);
            }
        }

        public ICommand goToRegMaterial
        {
            get
            {
                return new RelayCommand(param => ViewModel = new MaterialFormViewModel(this), param => true);
            }
        }

        public ICommand goToManageMaterial
        {
            get
            {
                return new RelayCommand(param => ViewModel = new MaterialManageViewModel(this), param => true);
            }
        }

        public ICommand goToManageQuotes
        {
            get
            {
                return new RelayCommand(param => ViewModel = new QuoteManageViewModel(this), param => true);
            }
        }

        public ICommand goToQuoteAnalysis
        {
            get
            {
                return new RelayCommand(param => ViewModel = new QuoteAnalysisViewModel(this), param => true);
            }
        }

        public ICommand goToCustomerAnalysis
        {
            get
            {
                return new RelayCommand(param => ViewModel = new CustomerAnalysisViewModel(this), param => true);
            }
        }

    }
}
