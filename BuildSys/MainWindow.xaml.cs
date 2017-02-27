using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BuildSys
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            _mainFrame.Navigate(new CustomerForm());

            //_mainFrame.NavigationService.Navigate(new Uri("http://www.google.com/"));

            /* == Example of navigating between 
            _mainFrame.NavigationService.GoBack();
            _mainFrame.NavigationService.GoForward();
            _mainFrame.NavigationService.Refresh();
            */
        }

        private void CustomerReg_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new CustomerForm());
        }

        private void CustomerList_Click(object sender, RoutedEventArgs e)
        {
            _mainFrame.Navigate(new CustomerList());
        }
    }
}
