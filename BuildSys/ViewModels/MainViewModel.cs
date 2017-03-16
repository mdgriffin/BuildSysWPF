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
            ViewModel = new CustomerFormViewModel(this);
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

        public ICommand goToListCustomer
        {
            get
            {
                return new RelayCommand(() => ViewModel = new CustomerManageViewModel(this), () => true);
            }
        }

        public ICommand goToRegCustomer
        {
            get
            {
                return new RelayCommand(() => ViewModel = new CustomerFormViewModel(this), () => true);
            }
        }
    }
}
