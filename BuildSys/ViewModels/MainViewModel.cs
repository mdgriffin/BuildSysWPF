using BuildSys.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace BuildSys.ViewModels
{
    class MainViewModel : BaseViewModel
    {

        public MainViewModel()
        {
            //CurrentView = new CustomerForm();
            ViewModel = new CustomerFormViewModel();
        }

        //UserControl CurrentView { get; set; }
        public BaseViewModel ViewModel { get; set; }
    }


    
}
