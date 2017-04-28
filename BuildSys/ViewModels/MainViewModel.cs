using System.Windows.Input;

namespace BuildSys.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        public MainViewModel()
        {
            this.parent = this;
            // The first view model is the home view model
            navigateTo(new HomeViewModel(this));
        }

        // Navigation  command, go to the manage customer view
        public ICommand goToManageCustomer
        {
            get
            {
                return new RelayCommand(param => navigateTo(new CustomerManageViewModel(this)), param => true);
            }
        }

        public ICommand goToRegCustomer
        {
            get
            {
                return new RelayCommand(param => navigateTo(new CustomerFormViewModel(this)), param => true);
            }
        }

        public ICommand goToRegMaterial
        {
            get
            {
                return new RelayCommand(param => navigateTo(new MaterialFormViewModel(this)), param => true);
            }
        }

        public ICommand goToManageMaterial
        {
            get
            {
                return new RelayCommand(param => navigateTo(new MaterialManageViewModel(this)), param => true);
            }
        }

        public ICommand goToCreateQuote
        {
            get
            {
                return new RelayCommand(param => navigateTo(new QuoteCustomerSelectViewModel(this)), param => true);
            }
        }

        public ICommand goToManageQuotes
        {
            get
            {
                return new RelayCommand(param => navigateTo(new QuoteManageViewModel(this)), param => true);
            }
        }

        public ICommand goToQuoteAnalysis
        {
            get
            {
                return new RelayCommand(param => navigateTo(new QuoteAnalysisViewModel(this)), param => true);
            }
        }

        public ICommand goToCustomerAnalysis
        {
            get
            {
                return new RelayCommand(param => navigateTo(new CustomerAnalysisViewModel(this)), param => true);
            }
        }

        public ICommand goToMaterialAnalysis
        {
            get
            {
                return new RelayCommand(param => navigateTo(new MaterialAnalysisViewModel(this)), param => true);
            }
        }

        public ICommand goToSettings
        {
            get
            {
                return new RelayCommand(param => navigateTo(new SettingsViewModel(this)), param => true);
            }
        }

    }
}
