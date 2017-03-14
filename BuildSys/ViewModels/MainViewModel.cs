using BuildSys.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BuildSys.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        public MainViewModel()
        {
            //CurrentView = new CustomerForm();
            ViewModel = new CustomerFormViewModel(this);

            menuTitle = "Menu Item 1";
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

        public String menuTitle;
    }
}
